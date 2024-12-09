using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class WSBDieEnding : MonoBehaviour
{

    public TextMeshProUGUI dialogueText; 
    public GameObject dialogueBox; 

    private string[] dialogueLines;  // 대화 문장 배열
    int currentLineIndex = 0;  

    void Start()
    {
        currentLineIndex = Random.Range(0, 3); // 랜덤 인덱스

        // 대사 목록
        dialogueLines = new string[]
        {
            "단서를 더 모아야할거같아...",
            "아이템을 더 찾아볼까..?",
            "...크리스마스..."
        };

        // 시작 시 대화 상자 비활성화
        dialogueBox.SetActive(false);

        // 시작 시 대화 시작
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(1f);  // 시작 대기시간 

        // 대화 상자 활성화
        dialogueBox.SetActive(true);
        // 대화창.텍스트 = 저 위에 대사 목록[랜덤인덱스]
        dialogueText.text = dialogueLines[currentLineIndex];
        // 3초 기둘
        yield return new WaitForSeconds(3f);
        // 메인 씬으로 재전송
        SceneManager.LoadScene("Wasabi 3");
    }
}