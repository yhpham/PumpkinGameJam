using UnityEngine;
using System.Collections;

public class BlueShadow : MonoBehaviour {

	RaycastHit hit;

	void Update () {
		Vector3 pos = GameObject.Find("PBlue").transform.position;

		if (Physics.Raycast(pos, -Vector3.up, out hit)) {
			transform.position = new Vector3(pos.x, hit.point.y, pos.z);
		}
		else {
			transform.position = new Vector3(pos.x, transform.position.y, pos.z);
		}
	}
}
