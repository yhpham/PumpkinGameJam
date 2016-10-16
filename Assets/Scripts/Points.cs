using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Points : MonoBehaviour {
	public Text pointsDisplay;

	private string pointsPop;
	private Animator anim; 
	private int points;

	void Start () {
		pointsPop = GetComponent<TextMesh> ().text;
		anim = GetComponent<Animator> ();
	}

	void Update () {
		transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, -transform.parent.rotation.z));
	}

	public void Notify(int value){
		if (value < 0) {
			pointsPop = value.ToString ();
			anim.SetTrigger ("Animate");
		} 
		
		points += value; 
		pointsDisplay.text = points.ToString();
	}
}
