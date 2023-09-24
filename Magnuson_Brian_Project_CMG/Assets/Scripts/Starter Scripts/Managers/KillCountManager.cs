using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KillCountManager : MonoBehaviour
{
    public TMP_Text killCountText;
    public static int killScore = 0;

    // Start is called before the first frame update
       void Update()
    {
        killCountText.text = "Defeated " + killScore.ToString();
    }
    
    public void AddKillCount(int value)
    {
        killScore += value;
        killCountText.text = killScore.ToString();
    }

    public void ResetScore()
    {
        killScore = 0;
    }



}
