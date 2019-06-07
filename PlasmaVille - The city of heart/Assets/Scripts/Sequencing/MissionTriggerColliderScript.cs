using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTriggerColliderScript : MonoBehaviour
{
    public int TriggerId;
    public Button missionButton;
    public GameObject mission1DetailCanvas;
    public GameObject mission2DetailCanvas;
    public GameObject mission3DetailCanvas;
    public GameObject EndGame;
    public GameObject fadeinTransition;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            missionButton.interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           missionButton.interactable = false;
        }
    }


    public void StartMission()
    {
        StartCoroutine(LoadDelay());
        //Collect the notes of that cinematic
        if (TriggerId == 1)
        {
            //First mission : Neutrophil and germs, ie 9 and 8

            Singletons.Instance.Notescript.noofNotesCollected += 3;
            Singletons.Instance.Notebook.GetComponent<Book>().bookPages[8] = Singletons.Instance.Notescript.NotePages[8];
            Singletons.Instance.Notebook.GetComponent<Book>().bookPages[9] = Singletons.Instance.Notescript.NotePages[9];
            Singletons.Instance.Notebook.GetComponent<Book>().bookPages[10] = Singletons.Instance.Notescript.NotePages[10];
            mission1DetailCanvas.SetActive(false);
        }
        else if (TriggerId == 2)
        {
            //Second mission : Pulmonary and Systemic circulation, Arteries and Veins, ie 15 and 16

            Singletons.Instance.Notescript.noofNotesCollected += 2;
            Singletons.Instance.Notebook.GetComponent<Book>().bookPages[15] = Singletons.Instance.Notescript.NotePages[15];
            Singletons.Instance.Notebook.GetComponent<Book>().bookPages[16] = Singletons.Instance.Notescript.NotePages[16];
            mission2DetailCanvas.SetActive(false);
        }
        else if (TriggerId == 3)
        {
            mission3DetailCanvas.SetActive(false);
        }
        else if (TriggerId == 4)
        {
            EndGame.SetActive(false);
        }
       
    }

    public IEnumerator LoadDelay()
    {
        fadeinTransition.SetActive(true);
        fadeinTransition.GetComponent<Animation>().Play("FadeInFadeout");
        yield return new WaitForSeconds(3f);
        Singletons.Instance.SequenceScript.LoadCinematic(TriggerId);
    }

    public void OnMIssionButtonPressed()
    {
        if (TriggerId == 1)
            mission1DetailCanvas.SetActive(true);
        else if (TriggerId == 2)
            mission2DetailCanvas.SetActive(true);
        else if (TriggerId == 3)
            mission3DetailCanvas.SetActive(true);
        else if (TriggerId == 4)
            EndGame.SetActive(true);
    }

    public void OnLaterPressed()
    {
        mission1DetailCanvas.SetActive(false);
        mission2DetailCanvas.SetActive(false);
        mission3DetailCanvas.SetActive(false);
        EndGame.SetActive(false);
    }
}
