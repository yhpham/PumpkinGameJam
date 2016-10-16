using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public Sprite intro1;
	public Sprite intro2;
	public Sprite intro3;

	int x = 0;

	public void StartGame() {
		Intro();
	}

	public void Instructions() {
		SceneManager.LoadScene("Tutorial");
	}

	public void Quit() {
		Application.Quit();
	}

	public void Intro() {

		while (x < 3) {
			if (Input.GetButtonDown ("PBlueJump")) {
				if (x == 0) {
					GameObject.Find("Image").GetComponent<Image>().sprite = intro1;
				}
				else if (x == 1) {
					GameObject.Find("Image").GetComponent<Image>().sprite = intro2;
				}
				else if (x == 2) {
					GameObject.Find("Image").GetComponent<Image>().sprite = intro3;
				}

				x++;
			}
		}
			
		SceneManager.LoadScene("scene_lava");
	}
}
