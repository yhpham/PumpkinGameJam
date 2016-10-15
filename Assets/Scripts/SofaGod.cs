using UnityEngine;
using System.Collections;

public class SofaGod : MonoBehaviour {

	public string horizontal = "BlueHorizontal";
	public string vertical = "BlueVertical";
	public string leftBumper = "BlueLeftBumper";
	public string rightBumper = "BlueRightBumper";
	public string dropButton = "BlueDrop";

	public Material matSolid;
	public Material matTrans;
	public GameObject[] sofas;
	public float moveSpeed = 1f;

	private GameObject currentSofa;
	private GameObject aimingSofa;
	private int nextSofaID;

	private float cooldown = 2f;
	public Vector3 offset = new Vector3 (0f, 10f, 0f);
	private Rigidbody rb;
	private BoxCollider coll;
	private bool canDrop = true;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		coll = GetComponent<BoxCollider> ();

		nextSofaID = Random.Range (0, sofas.Length);
		SetNextSofa ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown(dropButton) && cooldown < 0f && canDrop) {
			DropSofa ();
			cooldown = 2f;
		}
			
		if (Input.GetButtonDown(leftBumper)) {
			currentSofa.transform.rotation = Quaternion.Euler (new Vector3 (0f, currentSofa.transform.rotation.eulerAngles.y + 90, 0f));
			aimingSofa.transform.rotation = currentSofa.transform.rotation;
		} else if (Input.GetButtonDown(rightBumper)) {
			currentSofa.transform.rotation = Quaternion.Euler (new Vector3 (0f, currentSofa.transform.rotation.eulerAngles.y - 90, 0f));
			aimingSofa.transform.rotation = currentSofa.transform.rotation;

		}
		cooldown -= Time.deltaTime;


		rb.velocity = new Vector3 (Input.GetAxis (horizontal), 0f, Input.GetAxis (vertical)) * moveSpeed;

	}

	void DropSofa(){
		Destroy (aimingSofa);
		currentSofa.transform.parent = null;
		currentSofa.GetComponent<Rigidbody> ().useGravity = true;

		Invoke("SetNextSofa", .5f);
	}

	void SetNextSofa(){
		currentSofa = GameObject.Instantiate( sofas[nextSofaID] );
		currentSofa.transform.position = transform.position;
		currentSofa.transform.parent = this.transform;
		foreach (Renderer rend  in currentSofa.GetComponentsInChildren<Renderer>()) {
			rend.material = matSolid;
		}

		aimingSofa = GameObject.Instantiate (sofas [nextSofaID]);
		aimingSofa.transform.position = transform.position - offset;
		aimingSofa.transform.parent = this.transform;
		aimingSofa.GetComponent<Collider> ().isTrigger = true;
		Destroy (aimingSofa.GetComponent<Rigidbody> ());

		foreach (Renderer rend  in aimingSofa.GetComponentsInChildren<Renderer>()) {
			rend.material = matTrans;
		}

		nextSofaID = Random.Range (0, sofas.Length);
	}

	void OnTriggerEnter(Collider coll){
		print (coll.name);
		canDrop = false;
	}

	void OnTriggerExit(){
		canDrop = true;
		print ("exit");
	}
		
}
