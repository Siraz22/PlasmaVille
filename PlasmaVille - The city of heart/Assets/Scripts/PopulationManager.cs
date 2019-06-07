using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class PopulationManager : MonoBehaviour {
    public GameObject personPrefab;
    public int populationSize = 10;
    public List<GameObject> population = new List<GameObject>();
    public static float elasped = 0;
    public static int populationCount;

    public Transform spwanPoint1;
    public Transform spwanPoint2;

    //For GUI on Wave Number
    public TextMeshProUGUI WaveNumText;
    public TextMeshProUGUI SurvivalTime;

    int trialTime = 100;
    public static int generation = 1;

    public GameObject WinningCanvas;
    private bool WonGame = false;

    //GUIStyle gUIStyle = new GUIStyle();

    /*
    public void OnGUI()
    {
        gUIStyle.fontSize = 50;
        gUIStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "germs_Generation : " + populationCount,gUIStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "survival_Time : " + generation, gUIStyle);
    }
    */

    public void UpdateGUIElem()
    {
        WaveNumText.text = generation.ToString();
    }

    // Use this for initialization
    void Start () {
        populationCount = populationSize;
        spwanPoint1.GetComponentInChildren<ParticleSystem>().Play();
        spwanPoint2.GetComponentInChildren<ParticleSystem>().Play();
        for (int i = 0; i < populationSize; i++)
        {
            if (Random.Range(1,3) == 1)
            {
                Vector3 pos = new Vector3(spwanPoint1.position.x + Random.Range(-0.5f, 0.5f),0f, spwanPoint1.position.z + Random.Range(-0.5f,0.5f));
                GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
                go.GetComponent<DNABott>().spwanPoint = spwanPoint1;
                population.Add(go);
            }
            else
            {
                Vector3 pos = new Vector3(spwanPoint2.position.x + Random.Range(-0.5f, 0.5f), 0f, spwanPoint2.position.z + Random.Range(-0.5f, 0.5f));
                GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
                go.GetComponent<DNABott>().spwanPoint = spwanPoint2;
                population.Add(go);
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
        elasped += Time.deltaTime;
        if (populationCount<=0)
        {
            BreedNewPopulation();
            elasped = 0;
        }
		
	}

    public void FinishdGame3()
    {

    }

    private void BreedNewPopulation()
    {
        if (generation >= 5 && !WonGame)
        {
            //Finish Tower Defence Mission, find the Winning Canvas

            //The code below is done by a Button
            //Singletons.Instance.SequenceScript.FinishMiniGame3(true);

            WinningCanvas.gameObject.SetActive(true);
            WonGame = true;
        }
        else
        {
            List<GameObject> newPopulation = new List<GameObject>();
            List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNABott>().timeToDie).ToList();

            spwanPoint1.GetComponentInChildren<ParticleSystem>().Play();
            spwanPoint2.GetComponentInChildren<ParticleSystem>().Play();


            population.Clear();
            for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
            {
                Breed(sortedList[i], sortedList[i + 1]);
                Breed(sortedList[i + 1], sortedList[i]);
            }
            for (int i = 0; i < sortedList.Count; i++)
            {
                Destroy(sortedList[i]);
            }
            populationCount = population.Count;
            generation++;
        }
    }

    private void Breed(GameObject gameObject1, GameObject gameObject2)
    {
        DNABott dna1 = gameObject1.GetComponent<DNABott>();
        DNABott dna2 = gameObject2.GetComponent<DNABott>();
        GameObject offspring;
        if (Random.Range(0, 1000) > 100f)
        {
            if (Random.Range(0, 10) <= 5f)
            {
                Debug.Log("Called1");
                Vector3 pos = new Vector3(dna1.spwanPoint.position.x + Random.Range(-0.5f, 0.5f), 0f, dna1.spwanPoint.position.z + Random.Range(-0.5f, 0.5f));
                offspring = Instantiate(personPrefab, pos, Quaternion.identity);
                offspring.GetComponent<DNABott>().spwanPoint = dna1.spwanPoint;
                population.Add(offspring);
            }
            else
            {
                Debug.Log("Called2");
                Vector3 pos = new Vector3(dna2.spwanPoint.position.x + Random.Range(-0.5f, 0.5f), 0f, dna2.spwanPoint.position.z + Random.Range(-0.5f, 0.5f));
                offspring = Instantiate(personPrefab, pos, Quaternion.identity);
                offspring.GetComponent<DNABott>().spwanPoint = dna2.spwanPoint;
                population.Add(offspring);
            }
            
        }
        else
        {
            if (Random.Range(1, 3) == 1)
            {
                Vector3 pos = new Vector3(spwanPoint1.position.x + Random.Range(-0.5f, 0.5f), 0f, spwanPoint1.position.z + Random.Range(-0.5f, 0.5f));
                offspring = Instantiate(personPrefab, pos, Quaternion.identity);
                offspring.GetComponent<DNABott>().spwanPoint = spwanPoint1;
                population.Add(offspring);
            }
            else
            {
                Vector3 pos = new Vector3(spwanPoint2.position.x + Random.Range(-0.5f, 0.5f), 0f, spwanPoint2.position.z + Random.Range(-0.5f, 0.5f));
                offspring = Instantiate(personPrefab, pos, Quaternion.identity);
                offspring.GetComponent<DNABott>().spwanPoint = spwanPoint2;
                population.Add(offspring);
            }
        }
    }
}
