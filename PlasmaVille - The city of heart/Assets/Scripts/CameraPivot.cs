using UnityEngine;

public class CameraPivot : MonoBehaviour {

    public Transform Pivot;

    //To make it feel like a profesionally developed game
    public float minZoom = 1f;
    public float maxZoom = 3f;

    public float currentZoom = 1.5f;
    public Vector3 PivotRot;

    public float rotateSensitivity = 4f; //How much the camera will orbit every frame when A or D is pressed
    public float zoomSensitivity = 2f; //How much the camera will zoom when scrolled
    public float Zoomspeed = 4f; //How long it takes for camera to zoom to req val
    public float OrbitSpeed = 10; //How long it takes for camera to rotate to req val

    //Called After Update
    private void LateUpdate()
    {

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
            currentZoom -= ScrollAmount;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            PivotRot.y -= Input.GetAxis("Horizontal") * rotateSensitivity * Time.deltaTime; //For isometric the x axis stays fixed on 30
        }

        //Actual Camera Rig
        Quaternion QT = Quaternion.Euler(0, PivotRot.y, 0);
        Pivot.rotation = Quaternion.Lerp(Pivot.rotation, QT, Time.deltaTime * OrbitSpeed);

        gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, currentZoom, Time.deltaTime * Zoomspeed);
    }

}
