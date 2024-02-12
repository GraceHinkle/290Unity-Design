using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RayShooter : MonoBehaviour {
    private Camera cam;
    private RaycastHit hit;
    private float centerX; 
    private float centerY; 
    void Start() {
        cam = GetComponent<Camera>();

        //
        if (cam == null)
        {
            Debug.LogError("Camera component not found on the object. Please attach a camera or fix the script.");
            enabled = false; // Disable the script to prevent further errors
            return;
        }
        //
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void OnGUI() {

        //
        if (cam == null)
            return; // Skip GUI rendering if the camera is not available
        //
        int size = 50;
        centerX = cam.pixelWidth / 2 - size / 4; 
        centerY = cam.pixelHeight / 2 - size / 2; 
        GUI.Label(new Rect(centerX, centerY, size, size), "+");

        
        if (Event.current.type == EventType.Repaint)
        {
            float labelWidth = 2500;
            float labelHeight = 500;
            float labelX = 10; 
            float labelY = 10; 

            GUI.Label(new Rect(labelX, labelY, labelWidth, labelHeight), "Hit Point: " + hit.point.ToString());
        }
    }
    void Update() {

        //
        if (cam == null)
            return; // Skip raycasting if the camera is not available
        //
        if (Input.GetMouseButtonDown(0)) {
            Vector3 point = new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0);
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                    if (target != null) {
                        target.ReactToHit();
                    } else {
                        StartCoroutine(SphereIndicator(hit.point));
                    }
            }
        }
    }
    private IEnumerator SphereIndicator(Vector3 pos) {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(1);
        Destroy(sphere);
        }
}