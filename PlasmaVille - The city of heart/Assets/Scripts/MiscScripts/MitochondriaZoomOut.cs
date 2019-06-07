using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MitochondriaZoomOut : MonoBehaviour
{
    private Camera Isocam;

    private bool Zoomout = false;

    public float zoomSpeed= 5f;

    // Start is called before the first frame update
    void Start()
    {
        Isocam = Singletons.Instance.Isocam;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Zoom out camera and set max zoom out to an apt value
            Isocam.GetComponent<CameraPivot>().maxZoom = 7f;
            Zoomout = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Isocam.GetComponent<CameraPivot>().maxZoom = 3f;
            Zoomout = false;
        }
    }

    private void Update()
    {
        if(Zoomout)
        {
            Mathf.Lerp(Isocam.GetComponent<CameraPivot>().currentZoom, 7f, Time.deltaTime * zoomSpeed);
        }
        else
        {
            Mathf.Lerp(Isocam.GetComponent<CameraPivot>().currentZoom, 2f, Time.deltaTime * zoomSpeed);
        }
    }
}
