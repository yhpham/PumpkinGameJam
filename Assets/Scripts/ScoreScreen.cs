using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreScreen : MonoBehaviour {
	public Text bluePoints;
	public Text redPoints;
	// Use this for initialization
	void Start () {
		bluePoints.text = PlayerPrefs.GetInt ("Blue").ToString();
		redPoints.text = PlayerPrefs.GetInt ("Red").ToString();
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
}
