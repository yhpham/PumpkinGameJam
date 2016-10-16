using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Employee : MonoBehaviour {
    bool _isDead = false;
    public bool isDead {
        get { return _isDead; }
    }

    int _points;
    public int points {
        get { return _points; }
    }

    float startEarning;

    public float speed;
    public float jumpVel;

    public bool isGrounded = true;
    public bool isJumping = false;
    bool jumpClicked;
    bool _isDead = false;

    public Vector3 vel;
    public Rigidbody rigid;

	public string horizontal = "PBlueHorizontal";
	public string vertical = "PBlueVertical";
	public string jump = "PBlueJump";

    void Awake() {
        Cursor.visible = false;
        rigid = GetComponent<Rigidbody>();
        Live();
    }

    void FixedUpdate() {
        Move();
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

        jumpClicked = Input.GetButtonDown(jump);

        if (jumpClicked && isGrounded) {
            isGrounded = false;
            isJumping = true;

            rigid.AddForce(Vector3.up * jumpVel, ForceMode.VelocityChange);
        }
        else {
            isGrounded = true;
            isJumping = false;
            jumpClicked = false;

            vel.y = rigid.velocity.y;
        }

        rigid.velocity = vel;
    }

    bool GetArrowInput() {
		return Input.GetAxis (horizontal) != 0 || Input.GetAxis (vertical) != 0;                                                                                                                                                                                                                                                                                                                                                                                                                                                 
    }

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Floor") {
			Die ();
		}
        else if(col.gameObject.tag != gameObject.tag) {
            _points -= 20;
        }
	}

    void Score() {
        if (_isDead)
            return;

        startEarning += Time.deltaTime;
        if(startEarning > 0.5f) {
            _points += 10;
            startEarning = 0;
        }
    }

    public void Die() {
        _isDead = true;
        _points -= 50;
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
