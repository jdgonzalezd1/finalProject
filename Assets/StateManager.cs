using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : StateMachineBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] public float distance;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        player = GameObject.FindWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = agent.remainingDistance;
        animator.transform.LookAt(player);
        agent.destination = player.position;
        CheckDistance(distance, animator);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private void CheckDistance(float distance, Animator animator)
    {
        if (distance < 2f)
        {
            agent.isStopped = true;
            animator.SetBool("isMoving", false);
            animator.SetTrigger("PunchAttack");
            animator.SetBool("isAttacking", true);
        }

        if (distance > 2f && distance < 8f)
        {
            agent.isStopped = false;
            animator.SetBool("isMoving", true);
        }

        if (distance > 8f)
        {
            agent.isStopped = true;
            animator.SetBool("isMoving", false);
            animator.SetBool("CastSpell", true);
            animator.SetTrigger("JumpAttack");
        }

    }

}
