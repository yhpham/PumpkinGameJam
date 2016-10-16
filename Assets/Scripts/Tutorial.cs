using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public Sprite player;
	public Sprite god;
	public Sprite powerups;

	int x = 0;

	public void Menu() {
		SceneManager.LoadScene("Start");
	}

	public void ChangeImage() {
		x++;

		if (x % 3 == 0) {
			GameObject.Find("Tutorial").GetComponent<Image>().sprite = player;
		}
		else if (x % 3 == 1) {
			GameObject.Find("Tutorial").GetComponent<Image>().sprite = god;
		}
		else if (x % 3 == 2) {
			GameObject.Find("Tutorial").GetComponent<Image>().sprite = powerups;
		}
	}
}
