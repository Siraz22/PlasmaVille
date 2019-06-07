using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WBC_Camera : MonoBehaviour
{
    public Transform WBC_Target;

    public Vector3 offset;

    public float RotateSpeed=5f;
    public float SmoothRot=0.5f;

    public Transform pivotWBC;

    // Start is called before the first frame update
    void Start()
    {
        pivotWBC.transform.position = WBC_Target.transform.position;
        pivotWBC.transform.parent = WBC_Target.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X")*RotateSpeed;
        WBC_Target.Rotate(0, horizontal, 0);

        float DesiredYAngle = WBC_Target.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, DesiredYAngle, 0);
        transform.position = WBC_Target.position - (rotation * offset);
        
        /*
        Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotateSpeed, Vector3.up);

        offset = camTurnAngle * offset;

        Vector3 newPos = WBC_Target.position + offset;
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothRot);
        */

        //transform.position = WBC_Target.position - offset;

        transform.LookAt(WBC_Target);

    }
}
