using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditLaneDialogue : MonoBehaviour
{
    public Button CreditButton;


    [Header("Notes Pop")]
    public Animation NotedownAnimation;

    private void Start()
    {
        NotedownAnimation = Singletons.Instance.DropDownAnimObj.GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CreditButton.GetComponent<Button>().interactable = true;
            CreditButton.GetComponent<Image>().color = new Color32(255,255,255,255);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CreditButton.GetComponent<Button>().interactable = false;
            CreditButton.GetComponent<Image>().color = new Color32(255, 255, 255, 155);
        }

        if(CreditButton.GetComponent<CreditLaneTalk>().TalkedWithNpc)
        {
            NotedownAnimation.Play("NoteDropDown");
            CreditButton.GetComponent<CreditLaneTalk>().TalkedWithNpc = false;
        }
    }

}
