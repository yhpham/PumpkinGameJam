using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public Sprite player;
	public Sprite god;

	int x = 0;

	void Update(){
		if (Input.GetButtonDown ("PBlueJump")) {
			ChangeImage ();
		} else if (Input.GetButtonDown ("Back")) {
			Menu ();
		}
	}

	public void Menu() {
		SceneManager.LoadScene("Start");
	}

	public void ChangeImage() {
		x++;

		if (x % 2 == 0) {
			GameObject.Find("Tutorial").GetComponent<Image>().sprite = player;
		}
		else if (x % 2 == 1) {
			GameObject.Find("Tutorial").GetComponent<Image>().sprite = god;
		}
	}
}
