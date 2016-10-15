using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Employee : MonoBehaviour {
    public float speed;
    public float jumpVel;

    public bool isGrounded = true;
    public bool isJumping = false;
    bool jumpClicked;

	public Image lives;
    public Vector3 vel;
    public Rigidbody rigid;

    void Awake() {
        Cursor.visible = false;
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        vel = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);

        if (GetArrowInput() && (vel != Vector3.zero)) {
            transform.rotation = Quaternion.LookRotation(vel);
        }
        else {
            rigid.angularVelocity = Vector3.zero;
        }

        jumpClicked = Input.GetKeyDown(KeyCode.Space);

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
        return Input.GetKey(KeyCode.LeftArrow)
            || Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.UpArrow)
            || Input.GetKey(KeyCode.DownArrow);
    }

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Floor") {
			Die();
		} else if (col.gameObject.tag != this.gameObject.tag) {
			//col.gameObject.GetComponent<Employee>().speed = .5f;
		}
	}

    public void Die() {
		lives.rectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, lives.rectTransform.rect.width - 33f);
    }
}
