using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] private TMP_Text collectibles;
    [SerializeField] private static int collected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        collectibles.text = "Honey :  " + collected;
    }

    public void Collected(int value)
    {
        collected += value;
    }

}
