using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherMiniGame2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Completed Mini Game 2

           StartCoroutine( Singletons.Instance.SequenceScript.FinishMiniGame2(true));
        }
    }
}
