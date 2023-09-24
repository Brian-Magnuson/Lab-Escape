using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    //This should be placed on any item that heals the player
    //This game object though should have some kind of collider so it can work
    public int HealAmount = 0;

    public bool DestroyOnContact = false;
}
