using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI livesTEXT;
    public GameObject looseCanvas;
    public GameObject start;
    void Update()
    {
        livesTEXT.text = TD_Stats.Lives.ToString() + " LIVES";

        if(TD_Stats.Lives<=0)
        {
            livesTEXT.text = "OVER";
            looseCanvas.SetActive(true);
            start.GetComponent<PopulationManager>().enabled = false;
            Singletons.Instance.SequenceScript.quesScript.enabled = false;
            //
        }
    }
}
