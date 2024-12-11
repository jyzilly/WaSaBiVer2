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

    bool isTeleport = false;

    public AudioClip[] Creature2Sound;

    //public AudioClip footstep;
    //public AudioClip snowstep;

    public Image image;
    private float alpha = 0f;
    private Color colorCreature;

    public Transform mazepoint;

    public WSBMainGameController GM;
    private WSBPlayerController PC;

    GameObject m1;
    public NavMeshAgent m1Nav;

    GameObject m2;
    public NavMeshAgent m2Nav;

    GameObject m3;
    public NavMeshAgent m3Nav;

    public AudioClip m2Scream;

    private void Awake()
    {
        //m_AudioSource = GameObject.Find("Demon_damaged").transform.Find("Demon").GetComponent<AudioSource>();
        //navMeshAgent.speed = 120;
    }


    private void Start()
    {

        player = GameObject.Find("Ch46_nonPBR").GetComponent<Transform>();
        m_AudioSource = GetComponent<AudioSource>();
        image = GameObject.Find("Canvas").transform.Find("ImageMain").GetComponent<Image>();
        animator = GetComponent<Animator>();
        navMeshAgent.SetDestination(waypoints[0].position);
        colorCreature = image.color;
        GM = GameObject.Find("GameManager").GetComponent<WSBMainGameController>();
        PC = GameObject.Find("Ch46_nonPBR").GetComponent<WSBPlayerController>();

        GameObject m1 = GameObject.FindGameObjectWithTag("Creature2");
        NavMeshAgent m1Nav = m1.GetComponent<NavMeshAgent>();

        GameObject m2 = GameObject.FindGameObjectWithTag("Creature2_1");
        NavMeshAgent m2Nav = m2.GetComponent<NavMeshAgent>();

        GameObject m3 = GameObject.FindGameObjectWithTag("Creature2_2");
        NavMeshAgent m3Nav = m3.GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }

        if(GM.isRun2)
        {
            AudioClip Cture2shout = Creature2Sound[0];
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(Cture2shout, 0.8f);
            Debug.Log("ÆøÁ×»ç¿ë ¼º°ø");
            //navMeshAgent.isStopped = true;
            m1Nav.isStopped = true;
            Debug.Log("¸ØÃã: " + navMeshAgent.isStopped);
            animator.SetBool("isItemUse", true);
            Debug.Log("¾Ö´Ï¸ÞÀÌ¼Ç");

            Invoke("monster2_again", 3f);
        }
        else if(GM.isRun3)
        {
            AudioClip Cture2shout = Creature2Sound[0];
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(Cture2shout, 0.8f);
            Debug.Log("ÆøÁ×»ç¿ë ¼º°ø");
            //navMeshAgent.isStopped = true;
            m2Nav.isStopped = true;
            Debug.Log("¸ØÃã: " + navMeshAgent.isStopped);
            animator.SetBool("isItemUse", true);
            Debug.Log("¾Ö´Ï¸ÞÀÌ¼Ç");

            Invoke("monster2_again", 3f);
        }
        else if(GM.isRun4)
        {
            AudioClip Cture2shout = Creature2Sound[0];
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(Cture2shout, 0.8f);
            Debug.Log("ÆøÁ×»ç¿ë ¼º°ø");
            //navMeshAgent.isStopped = true;
            m3Nav.isStopped = true;
            Debug.Log("¸ØÃã: " + navMeshAgent.isStopped);
            animator.SetBool("isItemUse", true);
            Debug.Log("¾Ö´Ï¸ÞÀÌ¼Ç");

            Invoke("monster2_again", 3f);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ÇÃ·¹ÀÌ¾î ´êÀÓ");
            monsterIsAttack();
            if (isTeleport) return;
            StartCoroutine(monsterAttackAni());
            animator.SetBool("isCrash", false);
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

        PC.SetPosition(mazepoint.position);
        
        
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
        PC.Damage(30f);
        navMeshAgent.isStopped = true;
    }

    public void FootStep()
    {

        AudioClip footStep = Creature2Sound[1];
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(footStep);
        //AudioSource.PlayClipAtPoint(footstep, Camera.main.transform.position);
        //m_AudioSource.PlayOneShot(footstep);
    }

    void monster2_again()
    {
        Debug.Log("µµ¸Á ¼º°ø");
        navMeshAgent.isStopped = false;
        animator.SetBool("isItemUse", false);
        GM.isRun2 = false;
        GM.isRun3 = false;
        GM.isRun4 = false;
    }

    private void monster_Scream()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(m2Scream);
    }
}
