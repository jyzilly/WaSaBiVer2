using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spider : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public Transform target;

    private Animator animator;
    int m_CurrentWaypointIndex;

    private void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        //transform.rotation = Quaternion.Euler(0, -180, 0);

        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
        
    }

    private void OnTriggerStay(Collider _collider) 
    {
        if (_collider.CompareTag("Player"))
        {
            //navMeshAgent.isStopped = true;
            //animator.ResetTrigger("isAttack");
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", true);
            navMeshAgent.SetDestination(target.position);
            //navMeshAgent.isStopped = false;
        }

        if(!(_collider.CompareTag("Player")))
        {
            animator.SetBool("isAttack", false);
            animator.SetBool("isWalk", true);

        }


    }

}
