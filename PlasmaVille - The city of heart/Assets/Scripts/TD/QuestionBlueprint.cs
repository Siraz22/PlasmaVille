using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionBlueprint
{
    public List<CollectionOfQues> QuestionsMCQ;
}

[System.Serializable]
public class CollectionOfQues
{
    public string QuesionStatement;
    public string Answer;
    public string Option1;
    public string Option2;
    public string Option3;
}
