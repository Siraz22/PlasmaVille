using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class NavMeshPlayerMvt : MonoBehaviour {

    public Camera IsoCam;
    public NavMeshAgent playerAgent;
    public Animator AnimController;

    float AnimationSmoothtime = 0.1f;

    //To filter raycast hits
    public LayerMask MovementMask;

    public GameObject WalkHereUI;
    private GameObject WalkUIInstantiated;

    public AudioManager audioscript;
    private bool WalkSoundPlayed = false;
    public float speedPercent;

    // Use this for initialization
    void Start ()
    {
        audioscript = Singletons.Instance.AudioScript;
	}

    public float extraRotationSpeed;

    void extraRotation()
    {
        Vector3 lookrotation = playerAgent.steeringTarget - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), extraRotationSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update ()
    {

		if(Input.GetMouseButtonDown(1))
        {
            Ray ray = IsoCam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            //See if ray hits something stored in hit variable
            if(Physics.Raycast(ray,out hit,500f,MovementMask))
            {
                Debug.Log("Mask hit");

                playerAgent.SetDestination(hit.point);

                //Destroy any previous WalkUI
                if(WalkUIInstantiated!=null)
                {
                    Destroy(WalkUIInstantiated);
                }

                WalkUIInstantiated = Instantiate(WalkHereUI, hit.point, Quaternion.identity);
            }
            
        }

        /*
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = IsoCam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            //See if ray hits something stored in hit variable
            if (Physics.Raycast(ray, out hit))
            {
                //Interact
                //For Notes
                GenericNoteScript currentNote = hit.collider.GetComponent<GenericNoteScript>();

                if(currentNote!=null)
                {
                    //We hit an interactiable note
                }
            }

        }
        */

        speedPercent = playerAgent.velocity.magnitude / playerAgent.speed;
        AnimController.SetFloat("speedPercent", speedPercent, AnimationSmoothtime, Time.deltaTime);

        if (speedPercent>0f)
        {
            extraRotation();
            
            //Play sound once
            if(!WalkSoundPlayed)
            {
                audioscript.CharacterAS.clip = audioscript.RunningAC;
                audioscript.CharacterAS.Play();
                WalkSoundPlayed = true;
            }
            
        }
        else
        {
            //Player has stoped
            audioscript.CharacterAS.Stop();
            WalkSoundPlayed = false;
        }

    }
}
