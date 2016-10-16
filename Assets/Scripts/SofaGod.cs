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

	static Color invalid = new Color (1f, 1f, 1f, 0.3f);
	private Color myColor;

	private GameObject currentSofa;
	private GameObject aimingSofa;
	private int nextSofaID;

	private float cooldown = 0f;
	private float moveCoolDown = 0f;
	public Vector3 offset = new Vector3 (0f, 10f, 0f);
	private Vector3 currentInput;
	bool fast = false;
	public int canDropCount = 0;
	
	bool canDrop {
		get { return canDropCount == 0; }
	}

	void Start () {
		currentInput = calculateInput ();
		nextSofaID = 2; //long couch is first couch
		SetNextSofa ();
		myColor = aimingSofa.GetComponentsInChildren<Renderer> ()[0].material.color;
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
			aimingSofa.transform.rotation = Quaternion.Euler (new Vector3 (0f, aimingSofa.transform.rotation.eulerAngles.y - 90, 0f));
		}
		else if (Input.GetButtonDown(rightBumper)) {
			aimingSofa.transform.rotation = Quaternion.Euler (new Vector3 (0f, aimingSofa.transform.rotation.eulerAngles.y + 90, 0f));

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
		currentSofa = GameObject.Instantiate( sofas[nextSofaID] );
		currentSofa.transform.position = aimingSofa.transform.position + Vector3.up*4;
		currentSofa.transform.rotation = aimingSofa.transform.rotation;
		currentSofa.tag = sofaTag;
		currentSofa.GetComponent<Sofa> ().shouldFall = true;

		foreach (Renderer rend  in currentSofa.GetComponentsInChildren<Renderer>()) {
			rend.material = matSolid;
		}

		Destroy (aimingSofa);

		//transform.position += Vector3.right * 3;
		currentSofa.GetComponent<Rigidbody> ().useGravity = true;
		currentSofa.GetComponent<Rigidbody> ().isKinematic = true;

		SetNextSofa ();
	}

	void SetNextSofa() {
		nextSofaID = Random.Range (0, sofas.Length);

		aimingSofa = GameObject.Instantiate (sofas [nextSofaID]);
		aimingSofa.transform.position = transform.position - offset;
		aimingSofa.transform.parent = this.transform;
		foreach (BoxCollider c in aimingSofa.GetComponents<BoxCollider> ()) {
			c.isTrigger = true;
			c.size = c.size + Vector3.up * 10;
		}
		Destroy (aimingSofa.GetComponent<Rigidbody> ());

		foreach (Renderer rend  in aimingSofa.GetComponentsInChildren<Renderer>()) {
			rend.material = matTrans;
		}

	}

	void setColor(Color c) {
		foreach (Renderer rend  in aimingSofa.GetComponentsInChildren<Renderer>())
			rend.material.color = c;
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.CompareTag ("Floor")) return;
		if (coll.CompareTag ("PRed")) return;
		if (coll.CompareTag ("PBlue")) return;
		if (coll.CompareTag("Untagged")) return;
        if (coll.CompareTag("Coin")) return;

        ++canDropCount;
		setColor (invalid);
		Vector3 pos = aimingSofa.transform.position;
		pos.y = -1.9f;
		aimingSofa.transform.position = pos;
	}
	
	void OnTriggerExit(Collider coll) {
		if (coll.CompareTag ("Floor")) return;
		if (coll.CompareTag ("PRed")) return;
		if (coll.CompareTag ("PBlue")) return;
		if (coll.CompareTag("Untagged")) return;
        if (coll.CompareTag("Coin")) return;
        --canDropCount;

		if (canDropCount <= 0) {
			setColor (myColor);
			Vector3 pos = aimingSofa.transform.position;
			pos.y = -2.9f;
			aimingSofa.transform.position = pos;
		}
	}
		
}
