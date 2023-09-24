using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //This is a component that we put on an object with collision to act as a checkpoint for the player
    //This REQUIRES a gamemanager be set in the world if you want it to work properly
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager temp = FindObjectOfType<GameManager>();
            if (temp != null)
            {
                temp.SetNewRespawnPlace(collision.gameObject);
            }
            else
            {
                Debug.Log("Checkpoint: ERROR no GameManager found!");
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager temp = FindObjectOfType<GameManager>();
            if (temp != null)
            {
                temp.SetNewRespawnPlace(collision.gameObject);
            }
            else
            {
                Debug.Log("Checkpoint: ERROR no GameManager found!");
            }
            Destroy(gameObject);
        }
    }


}
