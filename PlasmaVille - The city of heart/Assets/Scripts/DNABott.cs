using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DNABott : MonoBehaviour {

    public Destination destination;

    public float startHealth = 80f;
    private float health;
    public Image healthbar;

    private bool ZombieDamageDone = false;

    public Transform spwanPoint;
    public NavMeshAgent meshAgent;
    public Animator animator;

    bool dead = false;
    public float timeToDie = 0;
    bool populationUpdated = false;

    public bool setDestination = false;
    public bool reachDetination = false;

    public void TakeDamage(float amount)
    {
        Debug.Log("Take damage");

        health -= amount;

        healthbar.fillAmount = health/startHealth;

        if (health <=0)
        {
            gameObject.tag = "Untagged";
            Die();
        }
    }

    public void Die()
    {    
        timeToDie = PopulationManager.elasped;
        animator.SetBool("dead", dead);
        dead = true;
    }

    // Use this for initialization
    void Start () {
        meshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        destination = FindObjectOfType<Destination>();
	}
	
	// Update is called once per frame
	void Update () {
        SetDestination();
        if (!populationUpdated)
        {
            if (dead)
            {
                populationUpdated = true;
                meshAgent.isStopped = true;
                PopulationManager.populationCount--;
            }
        }
	}

    void SetDestination()
    {
        if (!meshAgent.isOnNavMesh)
            return;
        if (!setDestination && !dead)
        {
            meshAgent.SetDestination(destination.gameObject.transform.position + new Vector3(Random.Range(-0.2f, 0.2f),0f,Random.Range(-0.2f,0.2f)));
            setDestination = true;
        }
        float distance = Vector3.Distance(transform.position, destination.gameObject.transform.position);
        if(distance<=meshAgent.stoppingDistance || (reachDetination && meshAgent.pathPending))
        {
            setDestination = false;
            reachDetination = true;

            if(!ZombieDamageDone)
            {
                //Reduce one experience
                if (TD_Stats.Experience > 0)
                {
                    TD_Stats.Experience--;
                }
                else if (TD_Stats.Lives > 0)
                {
                    //else experience has to be 0
                    TD_Stats.Lives--;
                }

                ZombieDamageDone = true;
            }

            animator.SetBool("Attack", true);
            timeToDie = PopulationManager.elasped;
            StartCoroutine(Dead());
        }
        else
        {
            setDestination = true;
            animator.SetBool("Attack", false);
        }
    }

    IEnumerator Dead()
    {
       yield return new WaitForSeconds(3f);
        dead = true;
    }
}
