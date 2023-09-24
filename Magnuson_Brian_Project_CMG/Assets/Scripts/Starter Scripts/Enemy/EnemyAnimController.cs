using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    //public float nakedAttackPower = 1; // the damage dealt on collision
    private GameObject player;
    private PlayerStealth stealth;
    public bool usePlayerStealth = false;
    [HideInInspector]
    public bool isVisible = false;
    public float playerCollidePauseDuration = 0f;
    private Animator animator;
    [Header("The max distance should match with Animator conditions")]
    public float maxChaseDistance = 0;
    private float distance;
    public float EnemyReturnSpeed;

    private Vector3 StartPos;
    
    private Collider2D collider;

    //ReturnStuff
    [HideInInspector]
    public Vector3 returnPosition;

    public enum EnemyState
    {
        NORMAL = 1,
        CHASE = 2,
        RETURN = 3
        
    }

    [SerializeField]
    private EnemyState state = EnemyState.NORMAL;

    
    [HideInInspector]
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // load in the player
        collider = gameObject.GetComponent<Collider2D>();
        animator = gameObject.GetComponent<Animator>(); // load in the attatched animator
        if (usePlayerStealth)
        {
            stealth = player.GetComponent<PlayerStealth>();
            animator.SetBool("playerDetected", isVisible);
        }
        
        StartPos = GetComponent<Transform>().position; // store the original position of this gameObject
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EnemyState.CHASE) //Turn on only when attacking
        {
            collider.enabled = true;
            isVisible = true;
            //animator.SetBool("playerDetected", true);
        }
        else
        {
            //animator.SetBool("playerDetected", false);
            isVisible = false;
        }
        animator.SetFloat("distance", distance); // set an animator float variable for 
        distance = (player.transform.position - transform.position).magnitude; // how far away this object is from the player
        animator.SetBool("playerDetected", isVisible);




        /*
         * if the enemy is visible (in either the main camera OR the scene camera in the editor)
         * AND the enemy is far enough away
         */
        if (state == EnemyState.CHASE && distance > maxChaseDistance) 
        {
            state = EnemyState.RETURN;              
            
        }
        if (state == EnemyState.RETURN)
        {
            animator.gameObject.transform.position = Vector3.MoveTowards(animator.gameObject.transform.position, StartPos, EnemyReturnSpeed * Time.deltaTime);
        }

        if (animator.gameObject.transform.position == StartPos)
        {
            state = EnemyState.NORMAL;
            
        }

    }

        
    

    public void SetStateChase()
    {
        state = EnemyState.CHASE;
    }
    public void SetStateReturn()
    {
        state = EnemyState.RETURN;
    }
    public void SetStateNormal()
    {
        state = EnemyState.NORMAL;
    }

    /// <summary>
    /// 1 = NORMAL  2 = CHASE   3 = RETURN
    /// </summary>
    /// <returns></returns>
    public int GetState()
    {
        return (int)state;
    }


   

    private void OnTriggerEnter2D(Collider2D other) // when this object enters another trigger
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            DisableAnimator(); // stop moving for 1 second
        }
        else if (other.gameObject.tag.Equals("Weapon"))
        {
            DisableAnimator(); // stop moving for 1 second
        }
    }

    void DisableAnimator()
    {
        animator.enabled = false;
        Invoke("EnableAnimator", playerCollidePauseDuration); // Enable the animator in 1 second
    }
    void EnableAnimator()
    {
        animator.enabled = true;
    }
}
