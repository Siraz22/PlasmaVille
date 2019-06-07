using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Elements")]
    public Camera IsoCam;
    Vector3 TargetPosition;
    Vector3 lookAtTarget;
    Quaternion playerRot;
    public float rotspeed = 8f;
    public float movementSpeed = 1.2f;
    bool Moving = false;
    float previousDistanceToPos, currentDistanceToPos;

    [Header("Animation Controllers")]
    public Animator PlayerAnimController;

    // Use this for initialization
    void Start()
    {     
        PlayerAnimController = gameObject.GetComponent<Animator>();
        PlayerAnimController.SetBool("isMoving", false); //Player won't be moving at start by default
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update called");
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles = new Vector3(0, eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(eulerAngles);

        //Editor
        if (Input.GetMouseButton(0))
        {
            PlayerAnimController.SetBool("isMoving", true);
            SetMousePosition();
        }
        else
        {
            Moving = false;
            PlayerAnimController.SetBool("isMoving", false);
        }

        if (Moving)
        {
            //Debug.Log("Moving func called");
            Move();
        }
        
    }

    #region Movement
    void SetMousePosition()
    {
        Ray ray = new Ray();
        float distanceToPlane;
        Plane plane = new Plane();

        //ray = IsoCam.ScreenPointToRay(Input.GetTouch(0).position);
        if (Input.GetMouseButton(0))
        {
            //ThrewGrenadeOnce = false;

            //Boards to bits tutorial to ignore the tall buildings when moving on isometric view uisng touch position on screen
            plane = new Plane(Vector3.up, 0f);
            ray = IsoCam.ScreenPointToRay(Input.mousePosition);
        }

        if (plane.Raycast(ray, out distanceToPlane))
        {
            TargetPosition = ray.GetPoint(distanceToPlane);

            //FOR INSTANTANEOUS
            //this.transform.LookAt(TargetPosition);

            lookAtTarget = new Vector3(TargetPosition.x - transform.position.x,
                                        transform.position.y, TargetPosition.z - transform.position.z);

            playerRot = Quaternion.LookRotation(lookAtTarget);

            //Debug.Log("Works");
            Moving = true;
        }
    }
    
    void Move()
    {
        
        if (Vector3.Distance(transform.position, TargetPosition) > 0.5f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotspeed * Time.deltaTime);

            //Actual Movement
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, movementSpeed * Time.deltaTime);
        }
    }
    #endregion Movement

}