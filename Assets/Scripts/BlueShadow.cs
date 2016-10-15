using UnityEngine;
using System.Collections;

public class BlueShadow : MonoBehaviour {

	void Update () {
		Vector3 pos = GameObject.Find("PBlue").transform.position;
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
	}
}
