using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Points : MonoBehaviour {
	public Text pointsDisplay;
	public GameObject popUp;

	private int points;

	void Start () {
	}

	void Update () {
	}

	public void Notify(int value, Vector3 pos = default(Vector3)){
		if (value < 0) {
			print (pos);
			GameObject pointsPop = GameObject.Instantiate (popUp);
			pos.y = -3.25f;
			pointsPop.transform.position = pos;
			print (value);
			pointsPop.GetComponentInChildren<TextMesh>().text = value.ToString ();
		} 
		
		points += value; 
		pointsDisplay.text = points.ToString();
	}
}
