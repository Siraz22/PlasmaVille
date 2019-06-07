using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayMission : MonoBehaviour
{
    public static int currentMission=0;

    public SequenceHandler sequence;
    public GameObject GameOverCanvas;
    [Header("Mission1")]
    public GameObject WBC;
    public GameObject enemySpwaner;

    [Header("Mission3")]
    public GameObject populationManager;
    public GameObject looseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        currentMission = 0;
        sequence = FindObjectOfType<SequenceHandler>();

    }

    // Update is called once per frame
    public void ReplayCurrentMission()
    {
        if (currentMission == 1)
        {
            WBC.transform.position = sequence.Wbc.transform.position;
            WBC.transform.rotation = sequence.Wbc.transform.rotation;
            enemySpwaner.GetComponent<EnemySpwaner>().currentWave = 0;
            WBCHealthMiniGame1.HealthWBC = 100;
            GameOverCanvas.SetActive(false);
        }
        else if (currentMission == 3)
        {
            PopulationManager.generation = 1;
            populationManager.GetComponent<PopulationManager>().enabled = true;
            TD_Stats.Lives = 5;
            TD_Stats.Experience = 10;
            looseCanvas.SetActive(false);
            Singletons.Instance.SequenceScript.quesScript.enabled = true;
        }
        else
            return;
        
    }

    public void ExitMission()
    {
        if (currentMission == 1)
        {
            StartCoroutine(sequence.FinishMiniGame1(false));
            GameOverCanvas.SetActive(false);
        }
        else if (currentMission == 3)
        {
            TD_Stats.Lives = 5;
            TD_Stats.Experience = 10;
            looseCanvas.SetActive(false);
            StartCoroutine(sequence.FinishMiniGame3(false));

        }
    }
}
