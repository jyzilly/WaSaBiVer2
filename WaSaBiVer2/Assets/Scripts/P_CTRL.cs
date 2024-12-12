using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class P_CTRL : MonoBehaviour
{
    private WSBPlayerController control;
    private PlayableDirector pd;
    public TimelineAsset[] ta;
    public Transform waypoint;

    public Image image;
    private float alpha = 0f;
    private float fadeTime = 3.0f;

    public Transform mazepoint;


    [SerializeField] private float CountDown = 240f; // ī��Ʈ�ٿ� �ð�
    [SerializeField] private TextMeshProUGUI CountDownDisplay = null; // ī��Ʈ ui
    private bool countdownStarted = false; // ī��Ʈ�ٿ� ���� Ȯ�� �⺻�� off

   // public int teleportCnt = 0;

    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
        control = GetComponent<WSBPlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CutScene")
        {
            Debug.Log("����");
            //other.gameObject.SetActive(false);
            pd.Play(ta[0]);
            control.isMovable = false;
            StartCoroutine(Pteleport());
        }

        //if (other.tag == "monster2")
        //{
        //    //StartCoroutine(monsterAttackAni());
        //}
        
    }

    IEnumerator monsterAttackAni()
    {
        GameObject.Find("Canvas").transform.Find("Panel").transform.gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("ImageMain").transform.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime / fadeTime;
            image.color = new Color(0, 0, 0, alpha);
        }

        yield return new WaitForSeconds(0.5f);

        transform.position = mazepoint.position;
        GameObject.Find("ImageMain").SetActive(false);
        GameObject.Find("Panel").SetActive(false);
    }

    IEnumerator Pteleport()
    {
        yield return new WaitForSeconds(4.0f);
        Debug.Log("��");
        //transform.position = waypoint.position;
        control.SetPosition(waypoint.position);
        control.isMovable = true;
        StartCountdown();

        //teleportCnt += 1;
    }

    private void StartCountdown() //��ư Ȱ��ȭ �Լ�
    {
        if (!countdownStarted)  //���࿡ ī��Ʈ �ٿ� ������ �ȵǾ��ٸ�
        {
            countdownStarted = true; // ���� ���� 

            //�ڷ�ƾ ����
            StartCoroutine(CountDownToStart());
        }
    }
    private IEnumerator CountDownToStart()
    {
        float time = CountDown;
        //while �ݺ��� ���� ������ ī��Ʈ �ٿ� Ÿ��
        while (time > 0)
        {
            CountDownDisplay.text = Mathf.Ceil(time).ToString(); // ī��Ʈ�ٿ� UI ������Ʈ
            yield return new WaitForSeconds(1f); //1�ʸ� ��ٸ���
            time--; //1�ʾ� ����.
        }
        if(time == 0)
        {
            SceneManager.LoadScene("Wasabi 6");
        }
    }

}
