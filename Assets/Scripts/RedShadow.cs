using UnityEngine;
using System.Collections;

public class RedShadow : MonoBehaviour {

	RaycastHit hit;

	void Update () {
		Vector3 pos = GameObject.Find("PRed").transform.position;

		if (Physics.Raycast(pos, -Vector3.up * 20, out hit)) {
		 	transform.position = new Vector3(pos.x, hit.point.y, pos.z);
		}
		else {
		 	transform.position = new Vector3(pos.x, transform.position.y, pos.z);
		}
	}
}
