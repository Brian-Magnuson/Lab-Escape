using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    public float speed;
    public int maxDistanceDestroy;
    private Transform player;
    private Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.y == target.y || 
            Mathf.Abs(transform.position.x - target.x) > maxDistanceDestroy || 
            Mathf.Abs(transform.position.y - target.y) > maxDistanceDestroy)
        {
            DestroyProjectile();
        }
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Platform")
        {
            DestroyProjectile();
        }
    }
}
