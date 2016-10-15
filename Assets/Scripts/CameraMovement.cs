using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public static CameraMovement cam;
	public float moveSpeed = 1f;

	void Awake() {
		cam = this;
	}
	
	void FixedUpdate() {
		transform.position += new Vector3 (moveSpeed, 0f, 0f) * Time.deltaTime;
	}
}
