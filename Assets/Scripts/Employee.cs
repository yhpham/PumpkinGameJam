using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Employee : MonoBehaviour {
    public float speed;
    public float jumpVel;

    public bool isGrounded = true;
    public bool isJumping = false;

    bool jumpClicked;

    public Vector3 vel;

    public Rigidbody rigid;
    public static Employee S;

    void Awake() {
        Cursor.visible = false;
        rigid = GetComponent<Rigidbody>();

        S = this;
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

    public void Die() {
        
    }
}
