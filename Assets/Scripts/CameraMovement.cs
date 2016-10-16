using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public static CameraMovement cam;
	public float moveSpeed = 1f;
	public bool started = false;

	public GameObject player1;
	public GameObject player2;

	void Awake() {
		cam = this;
	}

	void Start () {
		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		float targetaspect = 16.0f / 9.0f;

		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;

		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();

		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f) {  
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;

			camera.rect = rect;
		}
		else { // add pillarbox
			float scalewidth = 1.0f / scaleheight;

			Rect rect = camera.rect;

			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}
	}

	void Update() {
		if (started) {
			return;
		}

		if (player1.transform.position.x > transform.position.x) {
			started = true;
		}

		if (player2.transform.position.x > transform.position.x) {
			started = true;
		}
	}

	void FixedUpdate() {
		if (started) {
			transform.position += new Vector3 (moveSpeed, 0f, 0f) * Time.deltaTime;
		}
	}
}
