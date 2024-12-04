using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    
    [SerializeField]
    private TextMeshProUGUI enterText;

    private string[] dialogueLines;  // 대화 문장 배열
    private int currentLineIndex = 0;  // 현재 대화 인덱스

    private void Start()
    {
        // 대사 목록
        dialogueLines = new string[]
        {
            "안녕!",
            "오늘은 12월 24일이야",
            "하루의 시간을 줄게 살고싶다면 탈출해봐"
        };

        enterText.enabled = false; // 엔터 안내 문구 비활성화


        // 시작 시 대화 시작
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(0.5f);  // 시작 대기시간


        // 대화 시작
        while (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
            enterText.enabled = true;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));  // 키보드 엔터 대기

            currentLineIndex++;  // 다음 대화로 넘어감
            yield return new WaitForSeconds(0.1f);  // 대기시간 (


            // 모든 대화가 끝났을 때 다음 씬 로드
            if (currentLineIndex >= dialogueLines.Length)
            {
                SceneManager.LoadScene("Wasabi 3");
            }
        }
    }
}
