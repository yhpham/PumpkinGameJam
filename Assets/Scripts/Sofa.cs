using UnityEngine;
using System.Collections;

public class Sofa : MonoBehaviour {

    void FixedUpdate () {
        if (transform.position.y < -5) {
            Destroy (gameObject);
        }
    }
}
