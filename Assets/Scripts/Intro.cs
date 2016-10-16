using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Intro : MonoBehaviour {

	public Sprite intro2;
	public Sprite intro3;

	int x;

	void Update() {
		if (Input.GetButtonDown ("PBlueJump")) {
			ChangeIntro ();
		} else if (Input.GetButtonDown ("Back")) {
			SceneManager.LoadScene("scene_lava");
		}
	}

	public void ChangeIntro() {
		x++;

		if (Input.GetButtonDown ("PBlueJump")) {
			if (x == 0) {
				GameObject.Find("Intro").GetComponent<Image>().sprite = intro2;
			}
			else if (x == 1) {
				GameObject.Find("Intro").GetComponent<Image>().sprite = intro3;
			}
		}
	}
}
