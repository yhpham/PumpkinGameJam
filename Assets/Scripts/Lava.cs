using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {
    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "PRed" || col.gameObject.tag == "PBlue") {
            Employee.S.Die();
        }
    }
}
