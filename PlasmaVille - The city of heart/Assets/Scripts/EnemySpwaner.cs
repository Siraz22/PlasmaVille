using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{

    public int populationSize = 8;
    public static int populationCount;
    public GameObject zombiePrefab;
    public int totalWaves = 3;
    public int currentWave = 0;
    public GameObject DialogueBox;
    public string[] WBCEndDialogues;
    public GameObject gun;
    public ParticleSystem spwanEffect;

    bool FinalWave = false;

    
    // Start is called before the first frame update
    void Start()
    {
        spwanEffect = GetComponent<ParticleSystem>();
        currentWave = 0;
        Spwan();
    }

    // Update is called once per frame
    void Update()
    {
        if (populationCount <= 0 && !FinalWave)
        {
            if (currentWave == totalWaves)
            {
   
                DialogueBox.SetActive(true);
                DialogueManager.PhotoID = 3;

                Singletons.Instance.dialoguescript.NextNoteDialogueText(WBCEndDialogues);
                
                Cursor.lockState = CursorLockMode.None;

                FinalWave = true;

                //Disable WBC controller
                Singletons.Instance.SequenceScript.WBCActual.GetComponent<WBCController>().enabled = false;
                Singletons.Instance.SequenceScript.WBCActual.GetComponent<Animator>().SetFloat("VelX",0);
                Singletons.Instance.SequenceScript.WBCActual.GetComponent<Animator>().SetFloat("VelY",0);
                gun.GetComponent<GunScript>().enabled = false;

            }
            else
            {
                Spwan();
            }
        }
        
    }

    void Spwan()
    {
        ++currentWave;
        populationCount = populationSize;
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 spwanPosition = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f),transform.position.y, transform.position.z + Random.Range(-0.5f, 0.5f));
            GameObject go = Instantiate(zombiePrefab, spwanPosition, Quaternion.identity);
        }
        spwanEffect.Play();
    }

   
}
