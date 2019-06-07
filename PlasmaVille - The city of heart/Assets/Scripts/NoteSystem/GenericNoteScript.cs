using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericNoteScript : MonoBehaviour
{

    public GameObject ChildButton;

    //To determine if the player has collected the Note before
    public bool Collected = false;
    
    //To set an ID to the note number
    //public int NoteNum;

    private void Start()
    {
        ChildButton = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bhida");

        if (other.CompareTag("Player"))
        {
            
            ChildButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            ChildButton.GetComponent<Button>().interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!Collected)
            {
                ChildButton.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
                ChildButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                ChildButton.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
                ChildButton.GetComponent<Button>().interactable = false;
            }
            
        }
    }

}
