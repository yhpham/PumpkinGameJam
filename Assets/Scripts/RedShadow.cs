using UnityEngine;
using System.Collections;

public class RedShadow : MonoBehaviour {

	void Update () {
		Vector3 pos = GameObject.Find("PRed").transform.position;
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
	}
}
