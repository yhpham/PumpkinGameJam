using UnityEngine;
using System.Collections;

public class Sofa : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.y < -5) {
			Destroy (gameObject);
		}
	}
}
