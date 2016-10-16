using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Points : MonoBehaviour {
	public Text pointsDisplay;
	public GameObject popUp;

	private Animator anim; 
	private int points;
	// Use this for initialization
	void Start () {
		//pointsPop = GetComponent<TextMesh> ().text;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, -transform.parent.rotation.z));
	}

	public void Notify(int value, Vector3 pos = default(Vector3)){
		if (value < 0) {
			print (pos);
			GameObject pointsPop = GameObject.Instantiate (popUp);
			pointsPop.transform.position = pos;
			print (pointsPop.transform.position);
			pointsPop.GetComponent<TextMesh>().text = value.ToString ();
			//anim.SetTrigger ("Animate");
		} 
		points += value; 
		pointsDisplay.text = points.ToString();
	}
}
