using UnityEngine;
using System.Collections;

public class Respwan : MonoBehaviour {
    Employee prEmp;
    Employee pbEmp;

    public GameObject pred;
    public GameObject pblue;

    // Use this for initialization
    void Start() {
        prEmp = pred.GetComponent<Employee>();
        pbEmp = pblue.GetComponent<Employee>();
    }

    // Update is called once per frame
    void Update() {
        if (prEmp.isDead) {
            Respawn(prEmp);
        }
        if (pbEmp.isDead) {
            Respawn(pbEmp);
        }
    }

    void Respawn(Employee emp) {
        emp.Live();
        emp.tPosition = new Vector3(
            Camera.main.transform.position.x, emp.tPosition.y, emp.tPosition.z);
    }
}
