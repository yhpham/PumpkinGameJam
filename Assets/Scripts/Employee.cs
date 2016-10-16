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

    float startEarning;
    const float earningInterval = 0.5f;

    const int pointsForCouch = -20;
    const int pointsForDying = -50;
    const int pointsForLiving = 10;
    const int pointsForEndFirst = 150;

    const float powerUpDuration = 5.0f;

    bool invincible = false;
    float invincibilityTimer = 0;
    public Material redMaterial;
    public Material blueMaterial;
    public Material invincibilityMat;

    bool extraPoints = false;
    float extraPointsTimer = 0;
    const int extraPointMultiplier = 2;

    private Points points;

    void Awake() {
        Cursor.visible = false;
        rigid = GetComponent<Rigidbody>();
        points = GetComponentInChildren<Points>();
        Live();
    }

    void FixedUpdate() {
        Landing();
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

        if (Input.GetButton(jump) && !isJumping) {
            isJumping = true;
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
            if (GetComponent<Rigidbody>().velocity.y != 0)
                return;
            isJumping = false;
        }
        else
            isJumping = true;
    }

    bool GetArrowInput() {
        return (Input.GetAxis(horizontal) != 0) || (Input.GetAxis(vertical) != 0);
    }

    void OnCollisionEnter(Collision col) {
        if (invincible) {
            return;
        }

		if (col.gameObject.CompareTag("Floor")) {
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
			points.Notify( pointsForCouch, col.contacts[0].point);
		}
    }

    void OnTriggerEnter(Collider col) {
        if (invincible || extraPoints) {
            return;
        }
        switch(col.tag) {
            case "Coin":
                extraPoints = true;
                Destroy(col.gameObject);
                break;

            case "Warranty":
                invincible = true;
                Destroy(col.gameObject);
                break;

            default:
                break;
        }
    }

    float nextFlash = 0.0f;
    float flashPeriod = 0.1f;
    void PowerUpTimers() {
        if (invincible) {
            if (Time.time > nextFlash) {
                InvincibilityFlash();
                nextFlash = Time.time + flashPeriod;
            }
            invincibilityTimer += Time.deltaTime;

            if (invincibilityTimer > powerUpDuration) {
                invincible = false;
                invincibilityTimer = 0;
                switch (gameObject.tag) {
                    case "PRed":
                        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                            rend.material = redMaterial;
                        break;
                    case "PBlue":
                        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                            rend.material = blueMaterial;
                        break;
                }
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

    bool drawInvincible = true;
    void InvincibilityFlash() {
        Renderer[] parts = GetComponentsInChildren<Renderer>();
        if (drawInvincible) {
            drawInvincible = false;
            foreach (Renderer rend in parts)
                rend.material = invincibilityMat;
        }
        else {
            drawInvincible = true;
            switch (gameObject.tag) {
                case "PRed":
                    foreach (Renderer rend in parts)
                        rend.material = redMaterial;
                    break;
                case "PBlue":
                    foreach (Renderer rend in parts)
                        rend.material = blueMaterial;
                    break;
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
