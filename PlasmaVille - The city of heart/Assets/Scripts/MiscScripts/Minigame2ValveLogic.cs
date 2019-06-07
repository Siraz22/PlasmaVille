using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame2ValveLogic : MonoBehaviour
{
    //private Animator ValveAnimController;
    private Animation ValveAnimationAttatched;
    //public AnimationClip animation;

    private void Start()
    {
        //ValveAnimController = gameObject.GetComponentInParent<Animator>();
        ValveAnimationAttatched = GetComponentInParent<Animation>();
        //animation = GetComponent<AnimationClip>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("CalledTrigger");
            ValveAnimationAttatched.Play("minigame2valve");
            ValveAnimationAttatched.Play("minigame2valve_2");
            ValveAnimationAttatched.Play("minigame2valve_3");
            ValveAnimationAttatched.Play("minigame2valve_4");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
