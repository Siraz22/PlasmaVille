using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreditLaneTalk : MonoBehaviour
{
    public Camera IsoCam;
    public DialogueManager dialogueSript;

    public string NotesPop_text;
    public TextMeshProUGUI NotesPopTEXT;

    public TextMeshProUGUI CurrentDialoguetextTEXT;
    public Button DialogueBoxUI;

    public bool TalkedWithNpc = false;

    //Specify the dialogues in the inspector view from Unity

    [Header("Dialogues")]
    public string[] Dialogues_ToPass; //Contains array of dialogues that will be displayed on the Dialogue UI

    [Header("Handle Duplicates across Map")]
    public GameObject Duplicate;

    private void Start()
    {
        IsoCam = Singletons.Instance.Isocam;
        DialogueBoxUI = Singletons.Instance.DialogueUI;
        CurrentDialoguetextTEXT = Singletons.Instance.DialogueTEXT;
        dialogueSript = Singletons.Instance.dialoguescript;
        NotesPopTEXT = Singletons.Instance.NotesPopTMProTEXT;
    }

    public void TalkwithNPC()
    {
        TalkedWithNpc = true;

        //Dialogue box gets activated once therefore
        DialogueBoxUI.gameObject.SetActive(true);

        Singletons.Instance.NotesPopTMProTEXT.text = NotesPop_text;
        
        ActivateDialogue();
    }

    public void ActivateDialogue()
    {

        DialogueManager.PhotoID = 1;
        dialogueSript.NextNoteDialogueText(Dialogues_ToPass);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + IsoCam.transform.rotation * Vector3.forward, IsoCam.transform.rotation * Vector3.up);
    }
}
