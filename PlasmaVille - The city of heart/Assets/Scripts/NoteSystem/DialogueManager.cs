using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

    public Button DialogueBoxUI;
    public TextMeshProUGUI CurrentDialoguetextTEXT;
    public int current_Dialogue = -1;
    int Num_of_Dialogues;
    public Image DialogueIMG;

    public static int PhotoID;
    public Sprite[] AllSpeakersIMGID;
    //0 - Player
    //1 - Credit
    //2 - WBC Weak
    //3 - Killer T Cell
    //4 - Teacher
    //5 - Police

    public GameObject PlayerRef;
    public SequenceHandler sequencescript;

    string[] DialoguesPassedTemp;

    public void SetAptButton(Button button_passed)
    {
        DialogueBoxUI = button_passed;
    }

	// Use this for initialization
	void Start ()
    {
        DialogueBoxUI = Singletons.Instance.DialogueUI;
        CurrentDialoguetextTEXT = Singletons.Instance.DialogueTEXT;
        sequencescript = Singletons.Instance.SequenceScript;
    }

    //Buttons can't call functions that have a array so this is indirect call of the req function
    public void NextNoteAlternateButton()
    {
        NextNoteDialogueText(DialoguesPassedTemp);
    }

    public void NextNoteDialogueText(string[] dialogues_passed)
    {
        Debug.Log("Photo ID is "+ PhotoID.ToString());
        DialogueIMG.sprite = AllSpeakersIMGID[PhotoID];

        current_Dialogue++;

        DialoguesPassedTemp = dialogues_passed;

        //Check if we have ran out of dialogues
        if (current_Dialogue >= dialogues_passed.Length)
        {
            if(CurrentDialoguetextTEXT.text== "I should check on the other cells and get the alarm off")
            {
                //Passed by the WBC Character
                Debug.Log("Enteres here");

                //Switch Camera and Controls
                DialogueBoxUI.gameObject.SetActive(false);
                StartCoroutine(sequencescript.FinishMiniGame1(true));
                
            }

            PlayerRef.GetComponent<NavMeshPlayerMvt>().enabled = true;
            Singletons.Instance.AudioScript.CharacterAS.volume = 1f; //To mute running sound

            //Reset everything if the user decides to click on the Button again
            DialogueBoxUI.gameObject.SetActive(false);
            CurrentDialoguetextTEXT.text = " ";
            current_Dialogue = -1;
            DialoguesPassedTemp = null;
        }
        else
        {
            //Dialogues haven't finished
            PlayerRef.GetComponent<NavMeshPlayerMvt>().playerAgent.ResetPath(); //Stop Agent midway and clear his calculated path
            PlayerRef.GetComponent<Animator>().SetFloat("speedPercent",0f); //To stop the running animation
            Singletons.Instance.AudioScript.CharacterAS.volume = 0f; //To mute running sound
            PlayerRef.GetComponent<NavMeshPlayerMvt>().enabled = false;
            Num_of_Dialogues = dialogues_passed.Length;
            DialogueBoxUI.gameObject.SetActive(true); //For the start level
            CurrentDialoguetextTEXT.text = dialogues_passed[current_Dialogue];         
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
