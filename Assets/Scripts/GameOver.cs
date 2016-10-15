using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {

	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "PBlue") {
			SceneManager.LoadScene ("BlueWins");
		}
		else if (col.gameObject.name == "PRed") {
			SceneManager.LoadScene ("RedWins");
		}
	}
}
