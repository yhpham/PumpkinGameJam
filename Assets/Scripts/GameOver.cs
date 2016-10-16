using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {

	void Update() {
		Physics.IgnoreLayerCollision(9, 10, true);
	}

	void OnCollisionEnter(Collision col){
        Text bluePoints = GameObject.Find("BluePoints").GetComponent<Text>();
        Text redPoints = GameObject.Find("RedPoints").GetComponent<Text>();

        if (int.Parse(bluePoints.text) > int.Parse(redPoints.text)) {
            SceneManager.LoadScene ("BlueWins");
        }
        else {
            SceneManager.LoadScene ("RedWins");
        }
	}
}
