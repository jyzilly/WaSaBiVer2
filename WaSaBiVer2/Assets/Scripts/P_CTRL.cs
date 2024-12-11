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


    [SerializeField] private float CountDown = 240f; // 카운트다운 시간
    [SerializeField] private TextMeshProUGUI CountDownDisplay = null; // 카운트 ui
    private bool countdownStarted = false; // 카운트다운 시작 확인 기본은 off

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
            Debug.Log("닿임");
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
        Debug.Log("끝");
        //transform.position = waypoint.position;
        control.SetPosition(waypoint.position);
        control.isMovable = true;
        StartCountdown();

        //teleportCnt += 1;
    }

    private void StartCountdown() //버튼 활성화 함수
    {
        if (!countdownStarted)  //만약에 카운트 다운 실행이 안되었다면
        {
            countdownStarted = true; // 이제 실행 

            //코루틴 실행
            StartCoroutine(CountDownToStart());
        }
    }
    private IEnumerator CountDownToStart()
    {
        float time = CountDown;
        //while 반복문 돌릴 변수는 카운트 다운 타임
        while (time > 0)
        {
            CountDownDisplay.text = Mathf.Ceil(time).ToString(); // 카운트다운 UI 업데이트
            yield return new WaitForSeconds(1f); //1초를 기다리고
            time--; //1초씩 뺀다.
        }
        if(time == 0)
        {
            SceneManager.LoadScene("Wasabi 6");
        }
    }

}
