using UnityEngine;
using TMPro;

public class CirculationNotesPop : MonoBehaviour
{

    [Header("Notes Pop")]
    public Animation NotedownAnimation;
    public string NotesPop_text;
    public TextMeshProUGUI NotesPopTEXT;

    void Start()
    {
        NotedownAnimation = Singletons.Instance.DropDownAnimObj.GetComponent<Animation>();
        NotesPopTEXT = Singletons.Instance.NotesPopTMProTEXT;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Singletons.Instance.NotesPopTMProTEXT.text = NotesPop_text;

            NotedownAnimation.Play("NoteDropDown");

            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
