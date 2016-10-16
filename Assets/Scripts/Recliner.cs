using UnityEngine;
using System.Collections;

public class Recliner : Sofa {
	public GameObject disarmed;
	public GameObject back;

	public float throwSpeed = 1f;
	private bool active = true;

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Floor") {
			return;
		}

		if (active) {
			coll.gameObject.GetComponent<Employee>().GetFlung();
			Vector3 force = Quaternion.AngleAxis (75, transform.forward) * transform.right;
			coll.gameObject.GetComponent<Rigidbody> ().AddForce(force*throwSpeed, ForceMode.Impulse);
			active = false;

			StartCoroutine ("Fling");
		}
	}

	IEnumerator Fling() {
		for (float i = 45; i > 0; --i) {
			back.transform.localRotation = Quaternion.Euler ( new Vector3 (0f, 0f, i));
			yield return null;
		}
	}
}
