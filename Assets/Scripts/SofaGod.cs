using UnityEngine;
using System.Collections;

public class SofaGod : MonoBehaviour {

	public string horizontal = "BlueHorizontal";
	public string vertical = "BlueVertical";
	public string leftBumper = "BlueLeftBumper";
	public string rightBumper = "BlueRightBumper";
	public string dropButton = "BlueDrop";
	public string sofaTag = "PBlue";

	public Material matSolid;
	public Material matTrans;
	public GameObject[] sofas;
	public float moveSpeed = 1f;

	private GameObject currentSofa;
	private GameObject aimingSofa;
	private int nextSofaID;

	private float cooldown = 0f;
	private float moveCoolDown = 0f;
	public Vector3 offset = new Vector3 (0f, 10f, 0f);
	private bool canDrop = true;
	private Vector3 currentInput;
	bool fast = false;

	void Start () {
		currentInput = calculateInput ();

		nextSofaID = Random.Range (0, sofas.Length);
		SetNextSofa ();
	}
	
	void Update () {
		Vector3 newInput = calculateInput ();
		if (newInput != currentInput || moveCoolDown < 0f) {
			Vector3 temp = transform.position + newInput;
			if (temp.z < 5f && temp.z > -2f) {
				transform.position = temp;
				if (newInput != currentInput)
					fast = false;
				if (fast) {
					moveCoolDown = 0.1f;

				} else {
					moveCoolDown = 0.3f;
					fast = true;
				}
			}
		}
		moveCoolDown -= Time.deltaTime;
		currentInput = newInput;

		if (Input.GetButtonDown(dropButton) && cooldown < 0f && canDrop) {
			DropSofa ();
			cooldown = 2f;
		}
			
		if (Input.GetButtonDown(leftBumper)) {
			currentSofa.transform.rotation = Quaternion.Euler (new Vector3 (0f, currentSofa.transform.rotation.eulerAngles.y + 90, 0f));
			aimingSofa.transform.rotation = currentSofa.transform.rotation;
		}
		else if (Input.GetButtonDown(rightBumper)) {
			currentSofa.transform.rotation = Quaternion.Euler (new Vector3 (0f, currentSofa.transform.rotation.eulerAngles.y - 90, 0f));
			aimingSofa.transform.rotation = currentSofa.transform.rotation;

		}
		cooldown -= Time.deltaTime;

		//rb.velocity = new Vector3 (Input.GetAxis (horizontal), 0f, Input.GetAxis (vertical)) * moveSpeed;
	}

	Vector3 calculateInput() {
		// Returns a vector3 of the input with priority for horizontal movement
		float h = Mathf.Round(Input.GetAxisRaw(horizontal));
		float v = (h == 0) ? Mathf.Round(Input.GetAxisRaw (vertical)) : 0;
		//Vector3 newPos = new Vector3 (h, 0, v) *moveSpeed + transform.position;
		//newPos.x = Mathf.Clamp (newPos.x, CameraMovement.cam.transform.position.x - 10, CameraMovement.cam.transform.position.x + 10);
		//newPos.z = Mathf.Clamp (newPos.z, -6, 6);
		return new Vector3(h, 0, v);
	}

	void DropSofa() {
		Destroy (aimingSofa);
		currentSofa.transform.parent = null;
		currentSofa.GetComponent<Rigidbody> ().useGravity = true;

		Invoke("SetNextSofa", .5f);
	}

	void SetNextSofa() {
		currentSofa = GameObject.Instantiate( sofas[nextSofaID] );
		currentSofa.transform.position = transform.position;
		currentSofa.transform.parent = this.transform;
		currentSofa.tag = sofaTag;

		foreach (Renderer rend  in currentSofa.GetComponentsInChildren<Renderer>()) {
			rend.material = matSolid;
		}

		aimingSofa = GameObject.Instantiate (sofas [nextSofaID]);
		aimingSofa.transform.position = transform.position - offset;
		aimingSofa.transform.parent = this.transform;
		foreach(Collider c in aimingSofa.GetComponents<Collider> ()) c.isTrigger = true;
		Destroy (aimingSofa.GetComponent<Rigidbody> ());

		foreach (Renderer rend  in aimingSofa.GetComponentsInChildren<Renderer>()) {
			rend.material = matTrans;
		}

		nextSofaID = Random.Range (0, sofas.Length);
	}

	void OnTriggerEnter(Collider coll) {
		//canDrop = false;
	}

	void OnTriggerExit() {
		//canDrop = true;
	}
		
}
