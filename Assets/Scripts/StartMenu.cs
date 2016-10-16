using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public void StartGame() {
		SceneManager.LoadScene("scene_lava");
	}

	public void Instructions() {
		SceneManager.LoadScene("Tutorial");
	}

	public void Quit() {
		Application.Quit();
	}
}
