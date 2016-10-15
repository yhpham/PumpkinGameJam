using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Employee : MonoBehaviour {
    Collider collide;
    public bool isDead {
        get { return _isDead; }
    }
    bool _isDead = false;

    public float speed;
    public float jumpVel;

    public bool isGrounded = true;
    public bool isJumping = false;
    bool jumpClicked;

	public Image lives;
    public Vector3 vel;
    public Rigidbody rigid;

	public string horizontal = "PBlueHorizontal";
	public string vertical = "PBlueVertical";
	public string jump = "PBlueJump";

    void Awake() {
        Cursor.visible = false;
        rigid = GetComponent<Rigidbody>();
        collide = GetComponent<Collider>();
    }

    void FixedUpdate() {
        if (!CameraFrustum.S.InCameraView(collide))
            Die();

        vel = new Vector3(Input.GetAxis(horizontal), 0, Input.GetAxis(vertical)) * speed;

        if (GetArrowInput() && (vel != Vector3.zero)) {
            transform.rotation = Quaternion.LookRotation(vel);
        }
        else {
            rigid.angularVelocity = Vector3.zero;
        }

		jumpClicked = Input.GetAxis(jump) != 0;

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
       /* return Input.GetKey(KeyCode.LeftArrow)
            || Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.UpArrow)
            || Input.GetKey(KeyCode.DownArrow);*/
		return Input.GetAxis (horizontal) != 0 || Input.GetAxis (vertical) != 0;                                                                                                                                                                                                                                                                                                                                                                                                                                                 
    }

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Floor") {
			Die ();
		} else if (col.gameObject.tag != this.gameObject.tag) {
			//col.gameObject.GetComponent<Employee>().speed = .5f;
			speed *=.5f;
		} else {
			speed *= 2f;
		}
	}

    public void Die() {
        _isDead = true;
		lives.rectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, lives.rectTransform.rect.width - 33f);
    }

    public void Live() {
        _isDead = false;
    }

    public Vector3 tPosition {
        get { return transform.position; }
        set { transform.position = value; }
    }
}
