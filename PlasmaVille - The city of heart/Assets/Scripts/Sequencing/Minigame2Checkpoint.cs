using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame2Checkpoint : MonoBehaviour
{
    public GameObject[] DeactivateBarriers;
    public GameObject[] ActivateBarriers;

    public Button DialogueBoxUI;
    
    public string[] Dialogues;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(DeactivateBarriers.Length!=0)
            {
                foreach (GameObject barricade in DeactivateBarriers)
                {
                    barricade.gameObject.SetActive(false);
                }
            }
            
            if(Dialogues!=null)
            {
                DialogueBoxUI.gameObject.SetActive(true);

                DialogueManager.PhotoID = 0;
                Singletons.Instance.dialoguescript.NextNoteDialogueText(Dialogues);

                
            }
            
            if(ActivateBarriers!=null)
            {
                foreach(GameObject barricade in ActivateBarriers)
                {
                    barricade.gameObject.SetActive(true);
                }
            }

            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DialogueBoxUI = Singletons.Instance.DialogueUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
