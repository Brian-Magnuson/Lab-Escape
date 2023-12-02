using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCredits : MonoBehaviour
{
    [SerializeField]
    private float initialDelay = 5f;

    [SerializeField]
    private float timeDelay = 5f;

    [SerializeField]
    private GameObject displayAfterCredits;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RunCredits());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && displayAfterCredits != null)
        {
            displayAfterCredits.SetActive(true);
        }
    }

    IEnumerator RunCredits()
    {
        yield return new WaitForSeconds(initialDelay);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeDelay);
            child.gameObject.SetActive(false);
        }
        if (displayAfterCredits != null)
            displayAfterCredits.SetActive(true);
    }
}
