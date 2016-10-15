using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EmployeeMove : MonoBehaviour {
    public float speed;
    public float jumpVel;
    public float jumpDur;

    public bool isGrounded = true;
    public bool isJumping = false;

    bool jumpClicked;
    float jumpHeldForSeconds;

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
}
