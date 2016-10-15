using UnityEngine;
using System.Collections;

public class Sofa : MonoBehaviour {

    void Start () { }

    void FixedUpdate () {
        if (transform.position.y < -5) {
            Destroy (gameObject);
        }
    }

    void OnCollisionEnter(Collision col) {
       /* if (col.gameObject.tag != gameObject.tag) {
			col.gameObject.GetComponent<Employee>().speed = .5f;
        }*/
    }
}
