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
        currentLineIndex = Random.Range(0, 10); // 랜덤 인덱스

        // 대사 목록
        dialogueLines = new string[]
        {
            "단서를 더 모아야할거같아...",
            "아이템을 더 찾아볼까..?",
            "계속 반복되고 있는 것 같은 기분이 들어...",
            "괴물에 맞는 아이템이 따로 있는걸까..?",
            "거미는 들키지만않으면 쫓아오지않는 것 같아",
            "내가 빠져 나갈 수 있을까....",
            "열쇠는 괴물들이 들고 있는 걸까?",
            "어떤 물건을 만지면 가끔 그리운 생각이 떠오르는 것 같아",
            "내가 어쩌다 여기에 갇히게 된걸까.. 엄마가 보고싶어..."
        };

        // 시작 시 대화 상자 비활성화
        dialogueBox.SetActive(false);

        // 시작 시 대화 시작
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(0.5f);  // 시작 대기시간 

        // 대화 상자 활성화
        dialogueBox.SetActive(true);
        // 대화창.텍스트 = 저 위에 대사 목록[랜덤인덱스]
        dialogueText.text = dialogueLines[currentLineIndex];
        // 3초 기둘
        yield return new WaitForSeconds(1.5f);
        // 메인 씬으로 재전송
        SceneManager.LoadScene("Wasabi 3");
    }
}