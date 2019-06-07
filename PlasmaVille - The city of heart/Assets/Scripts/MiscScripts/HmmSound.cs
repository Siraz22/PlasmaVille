using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HmmSound : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}