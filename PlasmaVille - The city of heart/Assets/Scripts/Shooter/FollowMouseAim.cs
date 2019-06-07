using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowMouseAim : MonoBehaviour
{

    public Camera Isocam;

    public Vector3 LookAtWBC;
    public GameObject WBC;

    public LayerMask MovementMask;

    //Along Z Plane
    Plane plane;
    Vector3 TargetPos;

    void Start()
    {
        plane = new Plane(Vector3.up, 0f);
    }

    void Update()
    {
        Ray ray = Isocam.ScreenPointToRay(Input.mousePosition);

        float distanceToPlane;
              
        //RaycastHit hit;

        /*
        //See if ray hits something stored in hit variable
        if (Physics.Raycast(ray, out hit, MovementMask))
        {
            Debug.Log("We hit" + hit.collider.name + " " + hit.point);
            transform.position = new Vector3(hit.point.x, 0, hit.point.z);
            
            //WalkUIInstantiated = Instantiate(WalkHereUI, hit.point, Quaternion.identity);
        }
        */

        if (plane.Raycast(ray, out distanceToPlane))
        {
            TargetPos = ray.GetPoint(distanceToPlane);

            LookAtWBC = new Vector3(TargetPos.x - WBC.transform.position.x,
                                        WBC.transform.position.y, TargetPos.z - WBC.transform.position.z);

            transform.rotation = Quaternion.LookRotation(LookAtWBC);

            transform.position = new Vector3(TargetPos.x, TargetPos.y, TargetPos.z);
        }
    }
}
