using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class spider : MonoBehaviour
{
    private WSBItemManager ItemManager;


    [SerializeField] private WSBPlayerController PlayerController = null;
    [SerializeField] private WSBHpBar hpBar = null;

    private WSBMainGameController GameManager = null;

    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints; // ��� �迭 ����
    public Transform target; // Ÿ�� ����

    private Animator animator;
    int m_CurrentWaypointIndex; // �ֱ� ��� ��ȣ
    private Transform searchTarget = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Ch46_nonPBR").GetComponent<Transform>();
        PlayerController = GameObject.Find("Ch46_nonPBR").GetComponent<WSBPlayerController>();
        GameManager = GameObject.Find("GameManager").GetComponent<WSBMainGameController>();

        searchTarget = this.GetComponent<Transform>();
    }

    private void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position); // �Ź� ������
   
    }

    private void Update()
    {

        setDistance();
        if (GameManager.isRun)
        {
            RunAway();
            GameManager.isRun = false;
        }
        setDistance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = GameObject.Find("Ch46_nonPBR").GetComponent<Transform>();
            navMeshAgent.SetDestination(target.position);
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

    private void RunAway()
    {
        target = null;
        animator.SetBool("isWalk", true);
        animator.SetBool("isAttack", false);

    }

    public void attack()
    {
        float damage = Random.Range(5f, 11f);
        PlayerController.Damage(damage);
        Debug.Log(PlayerController.CurHp);
    }

    private void setDistance()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }

}
