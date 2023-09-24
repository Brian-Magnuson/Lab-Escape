using UnityEngine;
using System.Collections;

public class PlaySound2D : MonoBehaviour
{
    public AudioClip Chirp;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(Chirp, transform.position);
        }
    }
}