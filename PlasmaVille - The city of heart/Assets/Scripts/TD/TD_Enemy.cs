using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TD_Enemy : MonoBehaviour
{
    public Transform target;

    private void Start()
    {
        target = Singletons.Instance.towerDefence_endpoint;
        gameObject.GetComponent<NavMeshAgent>().SetDestination(target.position);
    }
}
