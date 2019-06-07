using UnityEngine;
using UnityEngine.UI;

public class GenericBenchScript : MonoBehaviour {

    public GameObject ChildButton;

    public string DialogueText;

    private void Start()
    {
        ChildButton = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChildButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            ChildButton.GetComponent<Button>().interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChildButton.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
            ChildButton.GetComponent<Button>().interactable = false;

        }
    }
}
