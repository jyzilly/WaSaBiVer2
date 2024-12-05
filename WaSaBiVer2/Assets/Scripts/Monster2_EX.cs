using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster2_EX : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    int m_CurrentWaypointIndex;
    private Animator animator;


    private void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
        //navMeshAgent.speed = 120;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 닿임");
            monsterIsAttack();
            animator.SetBool("isCrash", false);
        }
    }

    void monsterIsWalk()
    {
        navMeshAgent.speed = 2;

    }

    void monsterIsRun()
    {
        animator.SetBool("isRun", true);
        
    }

    void monsterIsAttack()
    {
        animator.SetBool("isCrash", true);
        navMeshAgent.speed = 0;
    }

}
