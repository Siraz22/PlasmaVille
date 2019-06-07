using UnityEngine;
using UnityEngine.AI;

public class ZombieTDDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.gameObject.GetComponent<NavMeshAgent>().ResetPath();

        other.transform.GetComponent<DNABott>().Die();

        if(TD_Stats.Experience>0)
        {
            TD_Stats.Experience--;

        }
        else
        {
            TD_Stats.Lives--;
        }
    }
}
