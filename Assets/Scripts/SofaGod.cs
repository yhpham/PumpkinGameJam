using UnityEngine;
using System.Collections;

public class SofaGod : MonoBehaviour {

	public GameObject[] sofas;

	private GameObject currentSofa;
	private int nextSofaID;

	private float cooldown = 3f;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		currentSofa = GameObject.Instantiate(sofas[1]);
		currentSofa.transform.position = transform.position;
		nextSofaID = Random.Range (0, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.JoystickButton0) && cooldown < 0f) {
			DropSofa ();
			cooldown = 3f;
		}
		cooldown -= Time.deltaTime;



	}

	void DropSofa(){
		Color color = currentSofa.GetComponent<Renderer> ().material.color;
		color.a = 255f;
		currentSofa.GetComponent<Renderer> ().material.color = color;

		currentSofa.GetComponent<Rigidbody> ().useGravity = true;

		Invoke("SetNextSofa", .5f);

	}

	void SetNextSofa(){
		currentSofa = GameObject.Instantiate( sofas[nextSofaID] );
		currentSofa.transform.position = transform.position;

		nextSofaID = Random.Range (0, 1);
	}
}
