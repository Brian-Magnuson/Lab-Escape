using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //This class should be placed on anything enemy related! Or anything that the player can damage
    
    public int currentHealth;
    public int maxHealth = 100;
    public SpriteRenderer sprite;
       
    public KillCountManager killCountManager;
    public int killScoreValue = 1;
    public Enemy_Health_Bar Healthbar;
    public bool spawnCollectibleOnDeath = false;
    public GameObject collectible;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Healthbar.SetHealth(currentHealth, maxHealth);
        if (!killCountManager)
        {
            killCountManager = FindObjectOfType<KillCountManager>();
        }
    }

    public void DecreaseHealth(int value)
    {
        currentHealth -= value;
        Healthbar.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isWeapon = false;
        bool isProjectile = false;
        Weapon playerWeapon = null;
        Projectile playerProjectile = null;

        // check if hit by weapon
        if (collision.gameObject.TryGetComponent(out Weapon weapon))
        {
            if (weapon.alignmnent == Weapon.Alignment.Player)
            {
                isWeapon = true;
                playerWeapon = weapon;
            }
        }

        // check if hit by projectile
        else if (collision.gameObject.TryGetComponent(out Projectile projectile))
        {
            isProjectile = true;
            playerProjectile = projectile;
        }

        // if hit by weapon or projectile, decrease player health
        if (isWeapon || isProjectile)
        {
            StartCoroutine(FlashRed());
            if (isWeapon && playerWeapon != null)
            {
                DecreaseHealth(playerWeapon.damageValue);
            }

            else if (isProjectile && playerProjectile != null)
            {
                DecreaseHealth(playerProjectile.damageValue);
            }

            else
            {
                Debug.Log("Player Weapon or Projectile is null");
            }

            // if enemy is dead, destroy enemy and spawn collectible if selected
            if (currentHealth == 0)
            {
                if (killCountManager)
                {
                    killCountManager.AddKillCount(killScoreValue);
                }
                if (spawnCollectibleOnDeath)
                {
                    Instantiate(collectible, transform.position, transform.rotation);
                }
                Destroy(this.gameObject);
            }
        }
    }


    private IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
