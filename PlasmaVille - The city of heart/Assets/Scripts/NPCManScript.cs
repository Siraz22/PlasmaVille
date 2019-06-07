using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCManScript : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();

    public bool setDestination;
    public bool walkToDestination;
    public bool reachDestination;

    public float waitTime = 0.5f;

    public NavMeshAgent meshAgent;
    public Animator NPCAnimator;

    public int index = 0;
    public bool walkarounders = false;
    // Start is called before the first frame update
    void Start()
    {
        if (walkarounders == true)
        {
            waitTime = Random.Range(0, 5);
        }
        else
        {
            waitTime = Random.Range(0.5f,1.2f );
        }
        meshAgent = GetComponent<NavMeshAgent>();
        NPCAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Wandering();
    }

    void Wandering()
    {
        if (!meshAgent.isOnNavMesh)
            return;
        if (!setDestination)
        {
            meshAgent.SetDestination(waypoints[index].position);
            setDestination = true;
        }
        float distance = Vector3.Distance(transform.position, waypoints[index].position);
        if (distance <= meshAgent.stoppingDistance || reachDestination && meshAgent.pathPending)
        {
            setDestination = false;
            walkToDestination = false;
            NPCAnimator.SetBool("Walk", false);
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                reachDestination = false;
                waitTime = 5f;
                index = (index + 1) % waypoints.Count;
            }
            else
            {
                reachDestination = true;
            }

        }
        else
        {
            setDestination = true;
            walkToDestination = true;
            NPCAnimator.SetBool("Walk", true);
        }

    }
}
