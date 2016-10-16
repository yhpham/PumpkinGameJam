using UnityEngine;
using System.Collections;

public class PointsPopUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("End", GetComponent<Animator> ().GetCurrentAnimatorClipInfo (0).Length);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void End(){
		Destroy (transform.parent.gameObject);
	}
}
