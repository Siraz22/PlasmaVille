using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInteractable : MonoBehaviour {

    public Camera IsoCam;
    public NotesManager NoteScript;
    public DialogueManager dialogueSript;
    public GameObject Notebook;

    [Header("Notes Pop")]
    public Animation NotedownAnimation;
    public string NotesPop_text;
    public TextMeshProUGUI NotesPopTEXT;

    public TextMeshProUGUI CurrentDialoguetextTEXT;
    public Button DialogueBoxUI;
    //Specify the dialogues in the inspector view from Unity

    [Header("Dialogues")]
    public string[] Dialogues_ToPass; //Contains array of dialogues that will be displayed on the Dialogue UI

    [Header("Handle Duplicates across Map")]
    public GameObject Duplicate;

    private void Start()
    {
        IsoCam = Singletons.Instance.Isocam;
        NoteScript = Singletons.Instance.Notescript;
        DialogueBoxUI = Singletons.Instance.DialogueUI;
        CurrentDialoguetextTEXT = Singletons.Instance.DialogueTEXT;
        dialogueSript = Singletons.Instance.dialoguescript;
        Notebook = Singletons.Instance.Notebook;
        NotedownAnimation = Singletons.Instance.DropDownAnimObj.GetComponent<Animation>();
        NotesPopTEXT = Singletons.Instance.NotesPopTMProTEXT;
    }

    public void CollectNote(int NoteNum)
    {
        if(transform.GetComponentInParent<GenericNoteScript>().Collected!=true)
        {
            NoteScript.noofNotesCollected++;
            Singletons.Instance.SequenceScript.CheckNoOfNotes();
        }

        transform.GetComponentInParent<GenericNoteScript>().Collected = true;

        if (Duplicate!=null)
        {
            Duplicate.GetComponent<GenericNoteScript>().Collected = true;
        }

        //Setting the pages
        Notebook.GetComponent<Book>().bookPages[NoteNum] = NoteScript.NotePages[NoteNum];

        //Dialogue box gets activated once therefore
        DialogueBoxUI.gameObject.SetActive(true);

        //Display Notedropdown Anim
        //Debug.Log("Play Hogi");
        Singletons.Instance.NotesPopTMProTEXT.text = NotesPop_text;

        NotedownAnimation.Play("NoteDropDown");

        ActivateDialogue();     
    }

    public void ActivateDialogue()
    {
        DialogueManager.PhotoID = 0;
        dialogueSript.NextNoteDialogueText(Dialogues_ToPass);
        
    }

    // Update is called once per frame
    void Update ()
    {
        transform.LookAt(transform.position+IsoCam.transform.rotation*Vector3.forward,IsoCam.transform.rotation*Vector3.up);
	}
}
