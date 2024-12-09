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
    public Transform[] waypoints; // ��� �迭 ����
    public Transform target; // Ÿ�� ����

    private Animator animator;
    int m_CurrentWaypointIndex; // �ֱ� ��� ��ȣ

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position); // �Ź� ������
   
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
            
        }

        // ������ ��� ����
        /*
            1. �Ź� �ݶ��̴��� �÷��̾ �־����
            2. �÷��̾ ���ǿ� �´� �������� ����ؾ�
            3. �Ź̰� ��������Ʈ 0������ ��
         
        */
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

    public void attack()
    {
        float damage = Random.Range(5f, 11f);
        Player.Damage(damage);
        Debug.Log(Player.CurHp);
    }

}
