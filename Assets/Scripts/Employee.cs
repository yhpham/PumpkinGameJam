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

	public string horizontal = "PBlueHorizontal";
	public string vertical = "PBlueVertical";
	public string jump = "PBlueJump";

    float startEarning;
    const float earningInterval = 0.5f;

    const int pointsForCouch = -20;
    const int pointsForDying = -50;
    const int pointsForLiving = 10;
    const int pointsForEndFirst = 150;

    const float powerUpDuration = 5.0f; 

    bool invincible = false;
    float invincibilityTimer = 0;

    bool extraPoints = false;
    float extraPointsTimer = 0;
    const int extraPointMultiplier = 2;

	private Points points;

    void Awake() {
        Cursor.visible = false;
        rigid = GetComponent<Rigidbody>();
		points = GetComponentInChildren<Points> ();
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
		return (Input.GetAxis(horizontal) != 0) || (Input.GetAxis(vertical) != 0);                                                                                                                                                                                                                                                                                                                                                                                                                                                 
    }

	void OnCollisionEnter(Collision col) {
        if (invincible) {
            return;
        }

		if (col.gameObject.CompareTag("Floor")) {
			Die();
		}
		else if ((col.gameObject.tag != gameObject.tag) && (col.gameObject.tag != "Safe")) {
            disableMovement = false;
			points.Notify(pointsForCouch);
        }
        else if (col.gameObject.CompareTag(gameObject.tag)) {
            disableMovement = false;
        }
        else if (col.gameObject.CompareTag("LevelEnd")) {
			points.Notify(pointsForEndFirst);
        }

        isJumping = false;
    }

    void OnTriggerEnter(Collider col) {
        if (invincible || extraPoints) {
            return;
        }
    }

    void PowerUpTimers() {
        if (invincible) {
            invincibilityTimer += Time.deltaTime;
            
            if (invincibilityTimer > powerUpDuration) {
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
        if (_isDead) {
            return;
        }

        startEarning += Time.deltaTime;

        if (startEarning > earningInterval) {
			if (extraPoints) {
				points.Notify(pointsForLiving * extraPointMultiplier);
            }
			else {
				points.Notify(pointsForLiving);
            	startEarning = 0;
            }
        }
    }

    public void GetFlung() {
		GetComponent<Rigidbody>().velocity = Vector3.zero;
        disableMovement = true;
    }

    public bool isDead {
        get { return _isDead; }
    }

    public void Die() {
		points.Notify(pointsForDying);
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
