using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotesManager : MonoBehaviour {

    //Actual Notes Collection
    public Sprite[] NotePages; //Has contents of the notes

    //Trigger colliders local to NotesManager
    public GameObject[] MissionTriggers;

    public GameObject BookHolder;

    public bool[] NoteCollected;

    public int noofNotesCollected=0;

    public TextMeshProUGUI notecountTEXT;

    public void SatOnBenchNotesDisplay()
    {
        //EDIT

        BookHolder.gameObject.SetActive(true);
    }

    void Update()
    {
        notecountTEXT.text = noofNotesCollected.ToString() + " /19";
    }

}
