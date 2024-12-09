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
    public Transform[] waypoints; // 경로 배열 설정
    public Transform target; // 타겟 설정

    private Animator animator;
    int m_CurrentWaypointIndex; // 최근 경로 번호
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
        navMeshAgent.SetDestination(waypoints[0].position); // 거미 시작점
   
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


        // 아이템 사용 조건
        /*
            1. 거미 콜라이더에 플레이어가 있어야함
            2. 플레이어가 조건에 맞는 아이템을 사용해야
            3. 거미가 웨이포인트 0번으로 감
         
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
