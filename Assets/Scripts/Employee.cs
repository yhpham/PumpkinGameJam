using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Employee : MonoBehaviour {
    public Vector3 vel;
    public Rigidbody rigid;

    public float speed;
    public float jumpVel;
    public bool isJumping = false;

    bool _isDead = false;
    bool disableMovement = false;

	public string jump = "PBlueJump";
	public string vertical = "PBlueVertical";
	public string horizontal = "PBlueHorizontal";
	public string myCouchTag;

	public AudioClip[] sounds;
	private AudioSource soundSource;

    float startEarning;
    const float earningInterval = 0.5f;

    const int pointsForCouch = -20;
    const int pointsForDying = -50;
    const int pointsForLiving = 10;
    const int pointsForEndFirst = 150;
    const int pointsForCoin = 50;

    private Points points;

    void Awake() {
        Cursor.visible = false;
        rigid = GetComponent<Rigidbody>();
        points = GetComponentInChildren<Points>();
		soundSource = GetComponent<AudioSource> ();
        Live();
    }

    void FixedUpdate() {
        Landing();

        if (!disableMovement) {
            Move();
        }

        Score();
    }

    void Move() {
        vel = new Vector3(Input.GetAxis(horizontal), 0, Input.GetAxis(vertical)) * speed;

        if (GetArrowInput() && (vel != Vector3.zero)) {
            transform.rotation = Quaternion.LookRotation(vel);
        }
        else {
            rigid.angularVelocity = Vector3.zero;
        }

        if (Input.GetButton(jump) && !isJumping) {
            isJumping = true;
			soundSource.PlayOneShot (sounds [3], .7f);

            rigid.AddForce(Vector3.up * jumpVel, ForceMode.VelocityChange);
        }
        else {
            vel.y = rigid.velocity.y;
        }

        rigid.velocity = vel;
    }

    void Landing() {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * 1.35f, Color.green);

        if (Physics.Raycast(transform.position + Vector3.down, Vector3.down * 1.35f, out hit)) {
            if (hit.collider.CompareTag("Floor")) {
                isJumping = true;
            }

            if (GetComponent<Rigidbody>().velocity.y != 0) {
                return;
            }
			//soundSource.PlayOneShot (sounds [2]);
            isJumping = false;
        }
        else {
            isJumping = true;
        }
    }

    bool GetArrowInput() {
        return (Input.GetAxis(horizontal) != 0) || (Input.GetAxis(vertical) != 0);
    }

    void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag("Floor")) {
			int idx = Mathf.Clamp(Random.Range (0, 10), 0, 1);
			soundSource.PlayOneShot (sounds [idx]);
			print (idx);
			Die (col.contacts[0].point);
		}
		else if (col.gameObject.CompareTag(myCouchTag) || col.gameObject.CompareTag("Safe")) {
			disableMovement = false;
		}
        else if (col.gameObject.CompareTag("LevelEnd")) {
            points.Notify(pointsForEndFirst);
        }
		else {
			disableMovement = false;
			points.Notify(pointsForCouch, col.contacts[0].point);
		}
    }

    void OnTriggerEnter(Collider col) {        
        if (col.tag == "Coin") {
			points.Notify(pointsForCoin, transform.position);
			soundSource.PlayOneShot (sounds [4]);
            Destroy(col.gameObject);
        }
    }

    void Score() {
        if (_isDead) {
            return;
        }

        startEarning += Time.deltaTime;

        if (startEarning > earningInterval) {
            points.Notify(pointsForLiving);
            startEarning = 0;
        }
    }

	public void GetFlung(Vector3 newPos) {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
		transform.position = newPos;
        disableMovement = true;
    }

    public bool isDead {
        get { return _isDead; }
    }

    public void Die(Vector3 pos) {
        points.Notify(pointsForDying, pos);
        _isDead = true;
        disableMovement = true;
        startEarning = 0;
    }

    public void Live() {
        _isDead = false;
        startEarning = 0;
    }

    public Vector3 tPosition {
        get { return transform.position; }
        set { transform.position = value; }
    }
}
