using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TD_Stats : MonoBehaviour
{
    public static int Experience;
    public int startMoney = 2;

    public static int Lives;
    public int startLives = 5;

    private void Start()
    {
        Experience = startMoney;
        Lives = startLives;
    }
    
}
