using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotesCounter : MonoBehaviour
{
    private void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = Singletons.Instance.Notescript.noofNotesCollected.ToString() +" /19";
    }
}
