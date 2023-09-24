using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ===== Attach this script to Player. Dependecies - Blast.cs on your Bullet prefab ===== */
public class EnergyBlaster : MonoBehaviour
{
    public Transform firePoint;
    public GameObject blastPrefab;
    public Vector2 direction;
    public Blast bullet;

    // Optional to play sound when firing projectile
    public AudioSource shootingSound;

    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y + 1);
            direction = target - myPos;
            direction.Normalize();
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
            Shoot(direction, myPos, rotation);

            // Play whatever audio clip for firing projectiles
            shootingSound.Play();
        }
    }

    void Shoot(Vector2 direction, Vector2 myPos, Quaternion rotation)
    {
        bullet.path = direction;
        Instantiate(blastPrefab, firePoint.position, firePoint.rotation);
        //Instantiate(blastPrefab, myPos, rotation);

    }
}
