using UnityEngine;
using System.Collections;

public class EmployeeMove : MonoBehaviour {
    public float speed;
    public float jumpVel;

    public Vector3 vel;

    public Rigidbody rigid;

    void Awake() {
        Cursor.visible = false;
        rigid = GetComponent<Rigidbody>();
    }

    void Update() {
        vel = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);

		if (GetArrowInput() && (vel != Vector3.zero)) {
            transform.rotation = Quaternion.LookRotation(vel);
        }
        else {
            rigid.angularVelocity = Vector3.zero;
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
