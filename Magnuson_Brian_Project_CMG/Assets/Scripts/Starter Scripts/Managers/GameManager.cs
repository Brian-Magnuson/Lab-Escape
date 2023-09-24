using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //This component should be placed on a gameobject in your scene

    [HideInInspector]
    public Vector3 RespawnPlace;

    [Tooltip("Place your player game object in here so this knows where to handle respawns")]
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        if (Player == null)
        {
            Player = FindObjectOfType<PlayerMovement>().gameObject;
        }
        RespawnPlace = Player.transform.position;
    }

    public void Respawn(GameObject Player)//This is just where we respawn the player
    {
        Player.transform.position = RespawnPlace;
    }
    public void Respawn()
    {
        Player.transform.position = RespawnPlace;
    }

    public void SetNewRespawnPlace(GameObject newPlace)//This is 
    {
        RespawnPlace = newPlace.transform.position;
    }
}
