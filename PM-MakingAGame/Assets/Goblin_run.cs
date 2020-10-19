using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_run : StateMachineBehaviour
{
    
    public float attackRange = 2f;
    public float speed = 2.5f;

    Transform Player;
    Rigidbody2D rb;
    Goblinmain goblin;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        goblin = animator.GetComponent<Goblinmain>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        goblin.LookAtPlayer();

        Vector2 target = new Vector2(Player.position.x, Player.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(Player.position,rb.position)<=attackRange)
        {
            animator.SetTrigger("Attack");
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
