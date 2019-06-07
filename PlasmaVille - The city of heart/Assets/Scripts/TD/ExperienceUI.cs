using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceUI : MonoBehaviour
{
    public TextMeshProUGUI ExperienceTEXT;

    // Update is called once per frame
    void Update()
    {
        ExperienceTEXT.text = "XP:"+TD_Stats.Experience.ToString();
    }
}
