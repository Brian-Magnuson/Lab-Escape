using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShooting : MonoBehaviour
{
    private float breakTime;
    public float ShootingRate;
    public GameObject projectile;
    


    // Start is called before the first frame update
    void Start()
    {
        breakTime = ShootingRate;

    }

    // Update is called once per frame
    void Update()
    {
        if(breakTime<=0 && gameObject.GetComponent<EnemyAnimController>().isVisible == true)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            breakTime = ShootingRate;
        }
        else
        {
            breakTime -= Time.deltaTime;
        }
    }
}
