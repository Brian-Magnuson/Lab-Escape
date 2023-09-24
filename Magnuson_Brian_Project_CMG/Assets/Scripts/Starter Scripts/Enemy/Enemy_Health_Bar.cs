using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health_Bar : MonoBehaviour
{   
    public Slider Slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;



    public void SetHealth(int currentHealth, int maxHealth)
    {
        Slider.gameObject.SetActive(currentHealth < maxHealth);
        Slider.value = currentHealth;
        Slider.maxValue = maxHealth;

        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue);
    }


    // Update is called once per frame
    void Update()
    {
       Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset); 
    }
}
