using UnityEngine;
using System.Collections;

public class Respwan : MonoBehaviour {
    Employee redEmployee;
    Employee blueEmployee;

    public GameObject PRed;
    public GameObject PBlue;

    void Start() {
        redEmployee = PRed.GetComponent<Employee>();
        blueEmployee = PBlue.GetComponent<Employee>();
    }

    void Update() {
        if (redEmployee.isDead) 
            Respawn(redEmployee);

        if (blueEmployee.isDead) 
            Respawn(blueEmployee);
    }

    void Respawn(Employee employee) {
        employee.Live();
        employee.tPosition = new Vector3(
            Camera.main.transform.position.x, employee.tPosition.y, employee.tPosition.z);
    }
}
