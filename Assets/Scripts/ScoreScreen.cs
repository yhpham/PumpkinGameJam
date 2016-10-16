using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreScreen : MonoBehaviour {
	public Text bluePoints;
	public Text redPoints;
	// Use this for initialization
	void Start () {
		bluePoints.text = PlayerPrefs.GetInt ("Blue").ToString();
		redPoints.text = PlayerPrefs.GetInt ("Red").ToString();
		Invoke ("InputEnabled", 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Replay(){
		SceneManager.LoadScene ("scene_lava");
	}

	public void MainMenu(){
		SceneManager.LoadScene ("Start");
	}

	private void InputEnabled(){
		GameObject.Find ("EventSystem").GetComponent<EventSystem>().enabled = true;
	}
}
