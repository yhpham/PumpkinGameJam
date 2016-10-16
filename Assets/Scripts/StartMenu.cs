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
		SceneManager.LoadScene("Intro");
	}

	public void Instructions() {
		SceneManager.LoadScene("Tutorial");
	}

	public void Quit() {
		Application.Quit();
	}
}
