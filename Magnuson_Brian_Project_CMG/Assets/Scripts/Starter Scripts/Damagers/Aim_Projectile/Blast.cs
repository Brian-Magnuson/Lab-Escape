using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* === Put this on your Blast/Bullet Object Prefab === */
public class Blast : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    EnergyBlaster eb;
    public int damage = 20;
    //private int count;

    public Vector2 path;
    void Start()
    {
        //count = 0;
        rb.velocity = path * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("shooting collisiom");
        // Destroys Projectile On Collision
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Platform")
        {
           
            //Destroy(other.gameObject);
            Destroy(gameObject);
            
        }
        else
        {
            Destroy(gameObject, 5);
        }
    }
}
