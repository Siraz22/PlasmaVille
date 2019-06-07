using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayerMiniRBC : MonoBehaviour
{
    //Player Ref for Nav Mesh
    public Transform target;
    NavMeshAgent agent;
    public float LookRadius = 0.5f;

    [Header("Health UI and ELements")]
    public float Health = 100f;
    
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(target.position);

        //Only move the AI enemy when it is within the range of it's look radius
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= LookRadius)
        {
            //Can Move to target        

            if (distance <= agent.stoppingDistance)
            {
                //Face The target
                FaceTheTarget();

                //Attack the Player
            }
        }        
    }

    void FaceTheTarget()
    {
        Vector3 directionToFace = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToFace.x, 0, directionToFace.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
