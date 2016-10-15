using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.name == "PBlue") {
			SceneManager.LoadScene ("BlueWins");
		} else if (coll.gameObject.name == "PRed") {
			SceneManager.LoadScene ("RedWins");
		}
	}
}
