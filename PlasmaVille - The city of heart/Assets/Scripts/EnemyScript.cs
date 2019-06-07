using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public enum AIStates { Search, Attack }
    public AIStates aIStates;

    public GameObject player;

    //Health
    public float health = 100f;

    public Animator enemyAnimator;
    public NavMeshAgent meshAgent;

    public Vector3 PlayerLastKnowPosition;

    public List<Vector3> searchPoints = new List<Vector3>();

    public bool setDestination = false;
    public bool walkingtoDest = false;
    public bool reachDestination = false;
    public bool search = true;

    public float waitTime = 3.0f;

    public int index = 0;

    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        player = Singletons.Instance.SequenceScript.WBCActual.gameObject;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0 && !dead)
        {
            dead = true;
            meshAgent.enabled = false;
            EnemySpwaner.populationCount--;
            enemyAnimator.SetBool("Dead", dead);
            Destroy(gameObject,3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
            return;
        float DistanceBetTarget = Vector3.Distance(player.transform.position, transform.position);
        if (DistanceBetTarget < 15)
            aIStates = AIStates.Attack;
        else
            aIStates = AIStates.Search;


        switch (aIStates)
        {
            case AIStates.Search:
                Searching();
                break;
            case AIStates.Attack:
                Attack();
                break;
        }

    }


    private void Attack()
    {
        if (player == null)
            return;
        transform.LookAt(player.transform);
        if (!setDestination)
        {
            meshAgent.SetDestination(player.transform.position);
            setDestination = true;
        }
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= meshAgent.stoppingDistance || meshAgent.pathPending)
        {
            setDestination = false;
            walkingtoDest = false;
            enemyAnimator.SetBool("Attack", true);
            enemyAnimator.SetBool("Move", false);
        }
        else
        {
            walkingtoDest = true;
            meshAgent.SetDestination(player.transform.position);
            setDestination = true;
            enemyAnimator.SetBool("Move", true);
            enemyAnimator.SetBool("Attack", false);
        }
    }


    void Searching()
    {
        if (search)
        {
            searchPoints.Clear();
            for (int i = 0; i < 3; i++)
            {
                float offsetX = Random.Range(-2f, 2f);
                float offsetZ = Random.Range(-2f, 2f);

                Vector3 originPos = transform.position;
                originPos += new Vector3(offsetX, 0f, offsetZ);

                NavMeshHit hit;
                if (NavMesh.SamplePosition(originPos, out hit, 5, NavMesh.AllAreas))
                {
                    searchPoints.Add(hit.position);
                }
            }
            search = false;
        }
        if (searchPoints.Count > 0)
        {
            if (!meshAgent.isOnNavMesh)
                return;
            if (!setDestination)
            {
                meshAgent.SetDestination(searchPoints[index]);
                setDestination = true;
            }
            float distance = Vector3.Distance(transform.position, searchPoints[index]);
            if (distance <= 1.3f || (reachDestination && meshAgent.pathPending))
            {
                setDestination = false;
                walkingtoDest = false;
                enemyAnimator.SetBool("Move", false);
                waitTime -= Time.deltaTime;
                if (waitTime <= 0)
                {
                    waitTime = 3f;
                    reachDestination = false;
                    index = (index + 1) % searchPoints.Count;
                }
                else
                {
                    reachDestination = true;
                }
            }
            else
            {
                setDestination = true;
                walkingtoDest = true;
                enemyAnimator.SetBool("Move", true);
            }
        }
    }

    public void AttackDamage()
    {
        Debug.Log("Called");
        WBCHealthMiniGame1.HealthWBC--;
    }
}