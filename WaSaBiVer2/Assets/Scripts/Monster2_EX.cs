using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster2_EX : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    int m_CurrentWaypointIndex;
    private Animator animator;
    AudioSource m_AudioSource;

    public AudioClip footstep;
    //public AudioClip snowstep;

    //public Image image;
    //private float alpha = 0f;

    //public Transform mazepoint;

    private void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
        //navMeshAgent.speed = 120;
        animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
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
            //StartCoroutine(monsterAttackAni());
            //animator.SetBool("isCrash", false);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isCrash", false);

            navMeshAgent.isStopped = false;
        }    
    }

    //IEnumerator monsterAttackAni()
    //{
    //    GameObject.Find("Canvas").transform.Find("Panel").transform.gameObject.SetActive(true);

    //    yield return new WaitForSeconds(1.0f);

    //    while (alpha < 1.0f)
    //    {
    //        alpha += Time.deltaTime;
    //        image.color = new Color(1, 1, 1, alpha);
    //    }

    //    yield return new WaitForSeconds(0.5f);

    //    transform.position = mazepoint.position;

    //}

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
        navMeshAgent.isStopped = true;
    }

    public void FootStep()
    {
        //AudioSource.PlayClipAtPoint(footstep, Camera.main.transform.position);
        m_AudioSource.PlayOneShot(footstep);
    }

}
