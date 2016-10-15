using UnityEngine;
using System.Collections;

public class GlowScript : MonoBehaviour {

	public float maxAlpha = 0.5f;
	public float duration = 5f;
	float aniTime;
	bool fadeIn = true;
	// Use this for initialization
	void Start () {
		aniTime = 0f;
	}

	// Update is called once per frame
	void Update () {
		if (fadeIn)
			aniTime += Time.deltaTime;
		else
			aniTime -= Time.deltaTime;

		if (aniTime >= duration && fadeIn)
			fadeIn = false;
		else if (aniTime <= 0f && !fadeIn)
			fadeIn = true;
		Color c = GetComponent<Renderer> ().material.color;
		c.a = (aniTime/duration) * maxAlpha;
		print (c.a);
		GetComponent<Renderer> ().material.color = c;
	}
}
