using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] private TMP_Text collectibles;
    [SerializeField] public static int collected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        collectibles.text = "Score   " + collected;
    }

    public void Collected(int value)
    {
        collected += value;
    }

}
