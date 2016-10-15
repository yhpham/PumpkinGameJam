using UnityEngine;
using System.Collections;

public class CameraFrustum : MonoBehaviour {
    Camera cam;
    Plane[] planes;

    public static CameraFrustum S;

    void Awake() {
        cam = Camera.main;
        planes = GeometryUtility.CalculateFrustumPlanes(cam);

        S = this;
    }

    void FixedUpdate() {
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
    }

    public bool InCameraView(Collider obj) {
        return GeometryUtility.TestPlanesAABB(planes, obj.bounds);
    }
}
