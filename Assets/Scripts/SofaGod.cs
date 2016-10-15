using UnityEngine;
using System.Collections;

public class SofaGod : MonoBehaviour {

	public GameObject[] sofas;
	public float moveSpeed = 1f;

	private GameObject currentSofa;
	private int nextSofaID;

	private float cooldown = 2f;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		currentSofa = GameObject.Instantiate(sofas[0]);
		currentSofa.transform.parent = this.transform;
		currentSofa.transform.position = transform.position;
		nextSofaID = Random.Range (0, 2);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.JoystickButton0) && cooldown < 0f) {
			DropSofa ();
			cooldown = 2f;
		}
		cooldown -= Time.deltaTime;


		rb.velocity = new Vector3 (Input.GetAxis ("Horizontal2"), 0f, Input.GetAxis ("Vertical2")) * moveSpeed;

	}

	void DropSofa(){
		Renderer[] rends = currentSofa.GetComponentsInChildren<Renderer> ();

		Color color = rends [0].material.color;
		print (rends [0].material.color);
		color.a = 1f;
		print (color);

		foreach (Renderer rend  in rends) {
			rend.material.color = color;
		}
		currentSofa.transform.parent = null;
		currentSofa.GetComponent<Rigidbody> ().useGravity = true;

		Invoke("SetNextSofa", .5f);

	}

	void SetNextSofa(){
		currentSofa = GameObject.Instantiate( sofas[nextSofaID] );
		currentSofa.transform.position = transform.position;
		currentSofa.transform.parent = this.transform;

		nextSofaID = Random.Range (0, 2);
	}
}
