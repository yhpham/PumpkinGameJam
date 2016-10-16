using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {

	void Update() {
		Physics.IgnoreLayerCollision(9, 10, true);
	}

	void OnCollisionEnter(Collision col){

		int blue = int.Parse(GameObject.Find("BluePoints").GetComponent<Text>().text);
		int red = int.Parse(GameObject.Find("RedPoints").GetComponent<Text>().text);
		PlayerPrefs.SetInt ("Blue", blue);
		PlayerPrefs.SetInt ("Red", red);

        if (blue > red) {
            SceneManager.LoadScene ("BlueWins");
        }
        else {
            SceneManager.LoadScene ("RedWins");
        }
	}
}
