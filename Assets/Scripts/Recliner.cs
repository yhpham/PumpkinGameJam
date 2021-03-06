using UnityEngine;
using System.Collections;

public class Recliner : Sofa {
	public GameObject back;

	public float throwSpeed = 1f;
	private bool active = true;

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Floor") {
			return;
		}

		if (active) {
			coll.gameObject.GetComponent<Employee>().GetFlung(transform.position+Vector3.up*1.5f);
			Vector3 force = Quaternion.AngleAxis (75, transform.forward) * transform.right;
			print (force);
			coll.gameObject.GetComponent<Rigidbody> ().AddForce(force*throwSpeed, ForceMode.Impulse);
			print (coll.gameObject.GetComponent<Rigidbody> ().velocity);
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
