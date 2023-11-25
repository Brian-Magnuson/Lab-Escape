using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [Header("Collectible Manager: Collectible Options")]
    public bool isCollectible = false;
    public int collectibleValue = 1;
    private CollectibleManager cManager;

    [Tooltip("Optional sound effect for when the item is collected.")]
    [SerializeField] private AudioClip pickupSFX;

    private void Start()
    {
        if (isCollectible)
        {
            cManager = FindObjectOfType<CollectibleManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isCollectible)
            {
                cManager.Collected(collectibleValue);

                if (pickupSFX)
                {
                    AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
                }

                Destroy(gameObject);
            }
        }
    }
}
