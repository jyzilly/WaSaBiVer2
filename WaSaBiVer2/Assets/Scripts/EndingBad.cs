using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class EndingBad : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    private TextMeshProUGUI dialogueText; // 대사 

    [SerializeField]
    private TextMeshProUGUI enterText; // 엔터 문구

    [SerializeField]
    private AudioClip bbi; // 삐- 하는 오디오 클립

    [SerializeField]
    private CanvasGroup SantaBaby; // 캔버스 그룹

    private string[] dialogueLines;  // 대화 문장 배열
    private int currentLineIndex = 0;  // 현재 대화 인덱스

    // 음량 페이드 아웃
    double fadeOutSeconds = 1.7;
    bool isFadeOut = true;
    double fadeDeltaTime = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.PlayOneShot(bbi);
        SantaBaby.alpha = 0;
        // 대사 목록
        dialogueLines = new string[]
        {
            "20XX년 X월 X일 XX시 XX분, OOO님께서 사망하셨습니다"
        };

        enterText.enabled = false; // 엔터 안내 문구 비활성화

    }

    private void Update()
    {
        if (isFadeOut)
        {
            fadeDeltaTime += Time.deltaTime;
            if (fadeDeltaTime >= fadeOutSeconds)
            {
                fadeDeltaTime = fadeOutSeconds;
                isFadeOut = false;
            }
            audioSource.volume = (float)(1.0 - (fadeDeltaTime / fadeOutSeconds));
        }

        if (SantaBaby.alpha < 1)
        {
            SantaBaby.alpha += Time.deltaTime;
            if (SantaBaby.alpha >= 1)
            {
                // 캔버스 그룹 로드 완료되면 대화 시작
                StartCoroutine(StartDialogue());
            }

        }
    }



    IEnumerator StartDialogue()
    {
        
        yield return new WaitForSeconds(0.2f);  // 시작 대기시간
        


        // 대화 시작
        while (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
            enterText.enabled = true;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));  // 키보드 엔터 대기

            currentLineIndex++;  // 다음 대화로 넘어감
            yield return new WaitForSeconds(0.1f);  // 대기시간 (


            // 게임 오버라서 게임 재시작
            if (currentLineIndex >= dialogueLines.Length)
            {
                SceneManager.LoadScene("Wasabi 1");
            }
        }
    }


  


}
