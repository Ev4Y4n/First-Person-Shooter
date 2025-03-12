using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : StateMachineBehaviour
{
    //Ctrl+K+U para descomentar varias líneas de golpe

    float timer;
    public float idleTimer = 0f;

    Transform player;

    public float detectionAreaRadius = 18f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // --- Transition to Patrol State --- //

        timer += Time.deltaTime;
        if (timer > idleTimer)
        {
            animator.SetBool("isPatroling", true);
        }

        // --- Transition to Patrol State --- //

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if(distanceFromPlayer< detectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }
    }

}
