using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionGenerator : MonoBehaviour
{
    private bool[] QuestionBools;

    [Header("Question Elements")]
    public TextMeshProUGUI Ques;
    public Image QuesCounter;
    public Button[] Options;

    public int correct_ans_index;
    int randomquesnum = 0;

    [Header("All notes Ques Blueprint")]
    public QuestionBlueprint RBC;
    public QuestionBlueprint Plasma;
    public QuestionBlueprint VenousValve;
    public QuestionBlueprint Electrolytes;
    public QuestionBlueprint Mitochondria;
    public QuestionBlueprint VascularEndothelialCells;
    public QuestionBlueprint Dendrite;
    public QuestionBlueprint Germs;
    public QuestionBlueprint Neutrophil;
    public QuestionBlueprint KillerTCell;
    public QuestionBlueprint MemoryTCell;
    public QuestionBlueprint Heart;
    public QuestionBlueprint CardioVascularMuscle;
    public QuestionBlueprint Lungs;
    public QuestionBlueprint ArteriesandVeins;
    public QuestionBlueprint PulmonaryAndSystemicCirculation;
    public QuestionBlueprint Platelets;
    public QuestionBlueprint BloodClot;
    public QuestionBlueprint Unity;

    float countdown = 20f; // 5 seconds given to answer a question

    private void Start()
    {
        QuestionBools = Singletons.Instance.Notescript.NoteCollected;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;

        if(countdown<=0f)
        {
            TD_Stats.Lives--;

            if(TD_Stats.Lives<=0)
            {
                //Gameover
            }

            countdown = 10f;
            GenerateRandomQues();
        }

        QuesCounter.fillAmount = Mathf.Lerp(QuesCounter.fillAmount, countdown / 10f, Time.deltaTime * 10f);
        
    }

    public void GenerateRandomQues()
    {
        countdown = 10f;

        int RandomTopicNum = Random.Range(0,20);

        Debug.Log(RandomTopicNum);

        switch(RandomTopicNum)
        {
            case 1: //RBC
                AssignOptions(Random.Range(0, RBC.QuestionsMCQ.Count), RBC);
                break;
            case 2: //RBC
                AssignOptions(Random.Range(0, Plasma.QuestionsMCQ.Count), Plasma);
                break;
            case 3: //RBC
                AssignOptions(Random.Range(0, VenousValve.QuestionsMCQ.Count), VenousValve);
                break;
            case 4: //RBC
                AssignOptions(Random.Range(0, Electrolytes.QuestionsMCQ.Count), Electrolytes);
                break;
            case 5: //RBC
                AssignOptions(Random.Range(0, Mitochondria.QuestionsMCQ.Count), Mitochondria);
                break;
            case 6: //RBC
            
                AssignOptions(Random.Range(0, VascularEndothelialCells.QuestionsMCQ.Count), VascularEndothelialCells);
                break;
            case 7: //RBC
             
                AssignOptions(Random.Range(0, Dendrite.QuestionsMCQ.Count), Dendrite);
                break;
            case 8: //RBC
                
                AssignOptions(Random.Range(0, Germs.QuestionsMCQ.Count), Germs);
                break;
            case 9: //RBC
                
                AssignOptions(Random.Range(0, Neutrophil.QuestionsMCQ.Count), Neutrophil);
                break;
            case 10: //RBC
                
                AssignOptions(Random.Range(0, KillerTCell.QuestionsMCQ.Count), KillerTCell);
                break;
            case 11: //RBC
                
                AssignOptions(Random.Range(0, MemoryTCell.QuestionsMCQ.Count), MemoryTCell);
                break;
            case 12: //RBC
                AssignOptions(Random.Range(0, Heart.QuestionsMCQ.Count), Heart);
                break;
            case 13: //RBC
                
                AssignOptions(Random.Range(0, CardioVascularMuscle.QuestionsMCQ.Count), CardioVascularMuscle);
                break;
            case 14: //RBC
                
                AssignOptions(Random.Range(0, Lungs.QuestionsMCQ.Count), Lungs);
                break;
            case 15: //RBC
                
                AssignOptions(Random.Range(0, ArteriesandVeins.QuestionsMCQ.Count), ArteriesandVeins);
                break;
            case 16: //RBC
                
                AssignOptions(Random.Range(0, PulmonaryAndSystemicCirculation.QuestionsMCQ.Count), PulmonaryAndSystemicCirculation);
                break;
            case 17: //RBC
                
                AssignOptions(Random.Range(0, Platelets.QuestionsMCQ.Count), Platelets);
                break;
            case 18: //RBC
                
                AssignOptions(Random.Range(0, BloodClot.QuestionsMCQ.Count), BloodClot);
                break;
            case 19: //RBC
                
                AssignOptions(Random.Range(0, Unity.QuestionsMCQ.Count), Unity);
                break;
        }
    }

    void AssignOptions(int questionindex, QuestionBlueprint blueprint_passed)
    {
        string answer = blueprint_passed.QuestionsMCQ[questionindex].Answer;
        Ques.text = blueprint_passed.QuestionsMCQ[questionindex].QuesionStatement;

        correct_ans_index = Random.Range(0,4);

        Options[correct_ans_index].GetComponentInChildren<TextMeshProUGUI>().text = answer;

        //Assign rest
        Options[(correct_ans_index+1)%4].GetComponentInChildren<TextMeshProUGUI>().text = blueprint_passed.QuestionsMCQ[questionindex].Option1;
        Options[(correct_ans_index+2)%4].GetComponentInChildren<TextMeshProUGUI>().text = blueprint_passed.QuestionsMCQ[questionindex].Option2;
        Options[(correct_ans_index+3)%4].GetComponentInChildren<TextMeshProUGUI>().text = blueprint_passed.QuestionsMCQ[questionindex].Option3;
    }

    public void SelectThisAns(int ButtonID)
    {

        if(ButtonID==correct_ans_index)
        {
            TD_Stats.Experience++;
            GenerateRandomQues();
        }
        else
        {
            TD_Stats.Lives--;

            if(TD_Stats.Lives<=0)
            {
                //GameOver
            }
            else
            {
                GenerateRandomQues();
            }
        }
    }
}
