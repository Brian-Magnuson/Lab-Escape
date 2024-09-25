using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.maxHealth = 100;
        CollectibleManager.collected = 0;
        KillCountManager.killScore = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
