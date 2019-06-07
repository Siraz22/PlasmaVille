using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveUpdate : MonoBehaviour
{
    public TextMeshProUGUI WaveTEXT;
    public GameObject winCanvas;
    public GameObject start;
    void Update()
    {
        WaveTEXT.text = "WAVE:"+PopulationManager.generation;

        if (PopulationManager.generation >= 4)
        {
            start.GetComponent<PopulationManager>().enabled = false;

            Singletons.Instance.SequenceScript.quesScript.enabled = false;
            winCanvas.SetActive(true);
        }
    }
}
