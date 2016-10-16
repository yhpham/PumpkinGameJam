using UnityEngine;
using System.Collections;

public class Sofa : MonoBehaviour {

	void Update() {
		Physics.IgnoreLayerCollision(9, 10, true);
	}

    void FixedUpdate() {
        if (transform.position.y < -5) {
            Destroy (gameObject);
        }
    }
}
