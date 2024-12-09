using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster2_EX : MonoBehaviour
{
    public Transform player = null;
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    int m_CurrentWaypointIndex;
    private Animator animator;
    AudioSource m_AudioSource;

    //public GameObject m2;

    private WSBMainGameController MainGM;


    bool isTeleport = false;

    public AudioClip footstep;
    //public AudioClip snowstep;

    public Image image;
    private float alpha = 0f;
    private Color colorCreature;

    public Transform mazepoint;

    private void Awake()
    {
        image = GameObject.Find("Canvas").transform.Find("ImageMain").GetComponent<Image>();
        m_AudioSource = GameObject.Find("Demon_damaged").transform.Find("Demon").GetComponent<AudioSource>();
        //navMeshAgent.speed = 120;
        player = GameObject.Find("Ch46_nonPBR").GetComponent<Transform>();
        MainGM = GetComponent<WSBMainGameController>();
    }


    private void Start()
    {

        animator = GetComponent<Animator>();
        navMeshAgent.SetDestination(waypoints[0].position);
       // m_AudioSource = GetComponent<AudioSource>();
        colorCreature = image.color;
        
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
            if (isTeleport) return;
            StartCoroutine(monsterAttackAni());
           // animator.SetBool("isCrash", false);
        }
        else if(other.CompareTag("Monster2_attack"))
        {
            if (MainGM.isRun)
            {
                navMeshAgent.isStopped = true;
                animator.SetBool("isItemUse", true);

                Invoke("monster2_again", 3f);
            }
            return;
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

    IEnumerator monsterAttackAni()
    {
        isTeleport = true;
        GameObject.Find("Canvas").transform.Find("Panel").transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        image.gameObject.SetActive(true);
        yield return new WaitForSeconds(.3f);

        while (colorCreature.a < 1f)
        {
            
            colorCreature.a += (Time.deltaTime)*1.2f / 1f;
            image.color = colorCreature;
            yield return new WaitForEndOfFrame();
            //break;
        }

        GameObject.Find("Canvas").transform.Find("Panel").transform.gameObject.SetActive(false);
        yield return new WaitForSeconds(.3f);

        colorCreature.a = 0;
        image.color = Color.black;
        image.gameObject.SetActive(false);

        Debug.Log(colorCreature.a);

        player.transform.position = mazepoint.position;
        isTeleport = false;
        yield return null;


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
        navMeshAgent.isStopped = true;
    }

    public void FootStep()
    {
        //AudioSource.PlayClipAtPoint(footstep, Camera.main.transform.position);
        //m_AudioSource.PlayOneShot(footstep);
    }

    void monster2_again()
    {
        navMeshAgent.isStopped = false;
        animator.SetBool("isItemUse", false);
    }
}
