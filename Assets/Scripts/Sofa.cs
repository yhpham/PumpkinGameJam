using UnityEngine;
using System.Collections;

public class Sofa : MonoBehaviour {

	float speed;
	const float gravity = 9.81f;
	const float buoyancy = 1.8f;
	public bool shouldFall = false;
	public bool rebounding = false;

	private AudioSource sound;
	void Start() {
		speed = 0f;
		sound = GetComponent<AudioSource> ();
	}

	void Update() {
		transform.position += new Vector3 (0, speed, 0) * Time.deltaTime;
		
		if (rebounding) {
			if (transform.position.y < -2.9f) {
				speed += buoyancy * Time.deltaTime;
			} 
			else {
				rebounding = false;
				speed = 0f;
			}
		}

		if (!shouldFall) {
			return;
		}

		if (transform.position.y < -2.9f) {
			rebounding = true;
			shouldFall = false;
			speed = -0.9f;
		}
		else {
			speed -= gravity * Time.deltaTime;
		}
	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Floor") {
			sound.Play ();
		}
	}

    void FixedUpdate() {
        if (transform.position.y < -10) {
            Destroy (gameObject);
        }
    }
}
