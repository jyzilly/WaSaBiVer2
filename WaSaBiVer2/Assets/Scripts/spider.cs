using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class spider : MonoBehaviour
{
    [SerializeField] private WSBPlayerController Player = null;
    [SerializeField] private WSBHpBar hpBar = null;

    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints; // 경로 배열 설정
    public Transform target; // 타겟 설정

    private Animator animator;
    int m_CurrentWaypointIndex; // 최근 경로 번호

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position); // 거미 시작점
    }

    private void Update()
    {

        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", true);
            //navMeshAgent.SetDestination(target.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
            animator.SetBool("isWalk", true);
            animator.SetBool("isAttack", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            navMeshAgent.SetDestination(target.position);
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", true);
        }
        

    }

    //private void OnTriggerStay(Collider other) 
    //{
    //    float damage = Random.Range(5f, 11f);

    //    if (other.CompareTag("Player"))
    //    {
    //        //navMeshAgent.isStopped = true;
    //        //animator.ResetTrigger("isAttack");
    //        animator.SetBool("isWalk", false);
    //        animator.SetBool("isAttack", true);
    //        Debug.Log(animator.name);

    //        navMeshAgent.SetDestination(target.position);
    //        //navMeshAgent.isStopped = false;
    //        //if (animator.OpenAnimationStream == "rig|attack_01")
    //        //{
    //        //    Player.Damage(damage);
    //        //    Debug.Log(damage);
    //        //}
    //    }

    //    if(!(other.CompareTag("Player")))
    //    {
    //        animator.SetBool("isAttack", false);
    //        animator.SetBool("isWalk", true);
    //    }




    //}

    public void attack()
    {
        float damage = Random.Range(5f, 11f);
        Player.Damage(damage);
        Debug.Log(Player.CurHp);
    }

}
