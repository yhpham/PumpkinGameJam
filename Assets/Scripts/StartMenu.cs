using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartMenu : MonoBehaviour {
	//public GameObject instructions;
	// Use this for initialization

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void StartGame(){
		SceneManager.LoadScene("scene_lava");
	}

	public void Quit(){
		Application.Quit();
	}

	public void Instructions(){
		//instructions.enabled = true;
		return;
	}

}
