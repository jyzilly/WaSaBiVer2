using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class EndingGood : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup room;
    [SerializeField]
    private CanvasGroup roomSub;
    [SerializeField]
    private TextMeshProUGUI allText;

    [SerializeField]
    private CanvasGroup desk;
    [SerializeField]
    private CanvasGroup deskSub;


    private string[] dialogueLines;  // 대화 문장 배열
    private int currentLineIndex = 0;  // 현재 대화 인덱스

    private void Awake()
    {
        
    }

    private void Start()
    {
        roomSub.alpha = 0;
        dialogueLines = new string[]
        {
            "내 방이다! 살아남은 건가..?",
            "어라..?"
        };
    }

    private void Update()
    {
        if (roomSub.alpha <= 1)
        {
            roomSub.alpha += Time.deltaTime;
            if (roomSub.alpha == 1)
            {
                // 캔버스 그룹 로드 완료되면 대화 시작
                
                StartCoroutine(FadeOut());
                //StartCoroutine(StartDialogue());
            }

        }
    }

    IEnumerator FadeOut()
    {
        while (room.alpha > 0)
        {
            room.alpha -= Time.deltaTime * 2f;
            allText.text = dialogueLines[0];
            yield return StartDialogue();
        }
    }


    IEnumerator StartDialogue()
    {

        //yield return new WaitForSeconds(0.2f); ;  // 시작 대기시간

        // 대화 시작
        while (currentLineIndex < dialogueLines.Length)
        {

            yield return new WaitForSeconds(3f);
            allText.text = "";
           

            if (room.alpha <= 0.3)
            {
               // Debug.Log(currentLineIndex);
                yield return new WaitForSeconds(0.2f);
                allText.text = dialogueLines[1];
                yield return new WaitForSeconds(4f);  // 시작 대기시간
                SceneManager.LoadScene("Wasabi 1");

            }
           
        }
    }
   
}
