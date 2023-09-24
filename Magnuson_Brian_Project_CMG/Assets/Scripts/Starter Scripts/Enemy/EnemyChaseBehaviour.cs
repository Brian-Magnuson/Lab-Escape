using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseBehaviour : StateMachineBehaviour
{
    private GameObject player;
    private Transform playerPos;
    public float speed;
    private Animator anim;
    private EnemyAnimController cont;
    private PlayerStealth playerStealth;
    private Vector3 StartPos;

    private bool hasStealth = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform; // load in the player's position
        player.TryGetComponent<PlayerStealth>(out playerStealth);
        if (playerStealth)
        {
            hasStealth = true;
        }
        anim = animator;
        cont = anim.gameObject.GetComponent<EnemyAnimController>();
        cont.SetStateChase();
        StartPos = cont.GetComponent<Transform>().position;


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (hasStealth && !playerStealth.isHidden)
        {
            cont.SetStateChase();
            Transform enemyPos = animator.transform;
            float step = speed * Time.deltaTime;
            enemyPos.position = Vector2.MoveTowards(enemyPos.transform.position, playerPos.position, step); // move towards the player

            if (enemyPos.transform.position.x > playerPos.position.x)
            {
                enemyPos.localScale = new Vector3(Mathf.Abs(enemyPos.localScale.x) * -1, enemyPos.localScale.y, enemyPos.localScale.z);
                //cont.transform.position = Vector3.MoveTowards(cont.transform.position, StartPos, speed * Time.deltaTime);

            }
            else
            {
                enemyPos.localScale = new Vector3(Mathf.Abs(enemyPos.localScale.x), enemyPos.localScale.y, enemyPos.localScale.z);
            }
        }
        else if (hasStealth && playerStealth.isHidden && cont.usePlayerStealth)
        {

            cont.SetStateReturn();
        }
        else //Does not have stealth
        {
            cont.SetStateChase();
            Transform enemyPos = animator.transform;
            float step = speed * Time.deltaTime;
            enemyPos.position = Vector2.MoveTowards(enemyPos.transform.position, playerPos.position, step); // move towards the player

            if (enemyPos.transform.position.x > playerPos.position.x)
            {
                enemyPos.localScale = new Vector3(Mathf.Abs(enemyPos.localScale.x) * -1, enemyPos.localScale.y, enemyPos.localScale.z);
            }
            else
            {
                enemyPos.localScale = new Vector3(Mathf.Abs(enemyPos.localScale.x), enemyPos.localScale.y, enemyPos.localScale.z);
            }

        }
        
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        Vector2 lineToObject = (anim.transform.position - other.transform.position).normalized;
        anim.transform.position = Vector2.MoveTowards(anim.transform.position, -1 * lineToObject, speed * Time.deltaTime); // don't get too stuck on objects (barely noticeable)
    }
}
