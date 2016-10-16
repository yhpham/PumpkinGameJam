using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Employee : MonoBehaviour {
    public float speed;
    public float jumpVel;

    bool disableMovement = false;
    public bool isJumping = false;

    public Vector3 vel;
    public Rigidbody rigid;

	public string horizontal = "PBlueHorizontal";
	public string vertical = "PBlueVertical";
	public string jump = "PBlueJump";

    bool _isDead = false;
    public bool isDead {
        get { return _isDead; }
    }

    int _points = 0;
    public int points {
        get { return _points; }
    }

    float startEarning;
    const float earningInterval = 0.5f;

    const int pointsForCouch = -20;
    const int pointsForDying = -50;
    const int pointsForLiving = 10;

    const float powerUpDuration = 5.0f; 

    bool invincible = false;
    float invincibilityTimer = 0;

    bool extraPoints = false;
    float extraPointsTimer = 0;
    const  int extraPointMultiplier = 2;

    void Awake() {
        Cursor.visible = false;
        rigid = GetComponent<Rigidbody>();
        Live();
    }

    void FixedUpdate() {
        if (!disableMovement) {
            Move();
        }
        Score();
        PowerUpTimers();
    }

    void Move() {
        vel = new Vector3(Input.GetAxis(horizontal), 0, Input.GetAxis(vertical)) * speed;

        if (GetArrowInput() && (vel != Vector3.zero)) {
            transform.rotation = Quaternion.LookRotation(vel);
        }
        else {
            rigid.angularVelocity = Vector3.zero;
        }

        if (Input.GetButtonDown(jump) && !isJumping) {
            isJumping = true;
            rigid.AddForce(Vector3.up * jumpVel, ForceMode.VelocityChange);
        }
        else {
            vel.y = rigid.velocity.y;
        }
        rigid.velocity = vel;
    }

    bool GetArrowInput() {
		return Input.GetAxis (horizontal) != 0 || Input.GetAxis (vertical) != 0;                                                                                                                                                                                                                                                                                                                                                                                                                                                 
    }

	void OnCollisionEnter(Collision col) {
        if (invincible)
            return;

		if (col.gameObject.CompareTag("Floor")) {
			Die ();
		}
        else if(col.gameObject.tag != gameObject.tag) {
            disableMovement = false;
            _points += pointsForCouch;
        }
        else if(col.gameObject.CompareTag(gameObject.tag)) {
            disableMovement = false;
        }
        else if (col.gameObject.CompareTag("LevelEnd")) {
            _points += 1000;
        }
        isJumping = false;
    }

    void OnTriggerEnter(Collider col) {
        if (invincible || extraPoints)
            return;

        if (col.gameObject.CompareTag("Invincible")) {
            invincible = true;
        }
        else if (col.gameObject.CompareTag("ExtraPoints")) {
            extraPoints = true;
        }
    }

    void PowerUpTimers() {
        if(invincible) {
            invincibilityTimer += Time.deltaTime;
            if(invincibilityTimer > powerUpDuration) {
                invincible = false;
                invincibilityTimer = 0;
            }
        }
        if (extraPoints) {
            extraPointsTimer += Time.deltaTime;
            if (extraPointsTimer > powerUpDuration) {
                extraPoints = false;
                extraPointsTimer = 0;
            }
        }
    }

    void Score() {
        if (_isDead)
            return;

        startEarning += Time.deltaTime;
        if(startEarning > earningInterval) {
            if (extraPoints)
                _points += pointsForLiving * extraPointMultiplier;
            else
                _points += pointsForLiving;
            startEarning = 0;
        }
    }

    public void GetFlung() {
        disableMovement = true;
    }

    public void Die() {
        _isDead = true;
        disableMovement = true;
        _points += pointsForDying;
        //FlyingText.getInstance(pointsForDying.ToString(), tPosition, 20, Color.blue, 10f, 5f, GameObject.Find("Canvas").transform);
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
