using UnityEngine;
using System.Collections;

public class WBCController : MonoBehaviour
{

    //private Vector3 moveInput;
    //private Vector3 moveVelocity;

    private Vector3 MoveDirection;
    public float movementSpeed;
    public float GravityScale;

    public Transform pivotWBC;

    private Animator WBCAnimator;
    public CharacterController controller;

    [Header("Gun Settings")]
    public GunScript gunScript;

    private void Start()
    {
        //For trial as of now
        gameObject.GetComponent<Animator>().SetBool("combatReady", false);
        WBCAnimator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        //For Blend Trees
        float xvel = Input.GetAxis("Horizontal");
        float yvel = Input.GetAxis("Vertical");

        MoveAnimate(xvel, yvel);

        #region Old Code
        /*
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
            gameObject.GetComponent<Animator>().SetBool("isMoving", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * movementSpeed;
            gameObject.GetComponent<Animator>().SetBool("isMoving", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * movementSpeed;
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isMoving", false);
        }
        
        //moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        //moveVelocity = moveInput * moveSpeed;


        //To rotate WBC
        Ray cameraRay = Isocam.ScreenPointToRay(Input.mousePosition);

        /*
        //To ignore collisions with buildings
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(groundPlane.Raycast(cameraRay,out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
        */
        #endregion

        //MoveDirection = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, 0f, Input.GetAxis("Vertical")*movementSpeed);

        //Slow fall counter
        float yStore = MoveDirection.y;

        MoveDirection = (transform.forward * Input.GetAxis("Vertical") )+ (transform.right*Input.GetAxis("Horizontal"));
        MoveDirection = MoveDirection.normalized * movementSpeed;

        MoveDirection.y = yStore;

        MoveDirection.y = MoveDirection.y + (Physics.gravity.y * GravityScale);
        controller.Move(MoveDirection * Time.deltaTime);

        //Shoot bullet for fire
        if (Input.GetMouseButtonDown(0))
        {
            gunScript.isFiring = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            gunScript.isFiring = false;
        }
    }

    public void MoveAnimate(float Velx, float Vely)
    {
        WBCAnimator.SetFloat("VelX", Velx);
        WBCAnimator.SetFloat("VelY", Vely);
    }

}