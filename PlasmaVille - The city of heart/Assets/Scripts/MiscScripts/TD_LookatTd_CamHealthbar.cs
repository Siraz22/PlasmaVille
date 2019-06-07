using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TD_LookatTd_CamHealthbar : MonoBehaviour
{
    public Camera TD_Cam;

    private void Start()
    {
        TD_Cam = Singletons.Instance.TD_Cam;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + TD_Cam.transform.rotation * Vector3.forward, TD_Cam.transform.rotation * Vector3.up);
    }
}
