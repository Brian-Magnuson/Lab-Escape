using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealth : MonoBehaviour
{
    [HideInInspector]
    public bool isHidden;
    public GameManager gameManager;
    public bool useGSMFade = false;
    

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(7, 8);
        if (!gameManager)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cover"))
        {
            isHidden = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        // I couldn't think of a better tag name than GuardView. Feel free to change it if you come up with a better name
        if (collision.CompareTag("Enemy"))
        {
            if (!isHidden)
            {
                if (useGSMFade)
                {
                    GetComponent<PlayerMovement>().TimeToDie();
                }
                else
                {
                    gameManager.Respawn();
                }
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cover"))
        {
            Debug.Log("Trigger Exit", collision.gameObject);
            isHidden = false;
        }
    }


}
