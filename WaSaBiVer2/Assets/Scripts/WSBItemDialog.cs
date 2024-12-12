using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WSBItemDialog : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    public TextMeshProUGUI LetterText;
    public GameObject LetterBox; 
    
    
    public TextMeshProUGUI InfoText;
    public GameObject InfoBox;

    public WSBItemManager ItemManager;


    public WSBMoveToSquare MoveToSquare;

    private string[] dialogueLines;  // 대화 문장 배열
    int currentLineIndex = 0;

    bool isTalk = false;
    bool isLetterTalk = false;
    bool isInfoTalk = false;

    private void Awake()
    {
        ItemManager = GameObject.Find("ItemManager").GetComponent<WSBItemManager>();
    }

    private void Update()
    {
       

        if (ItemManager.isMemory1)
        {
            Debug.Log("Memory");
            dialogueLines = new string[]
            {
                "어?......"
            };
            isTalk = true;
        }
        else if (ItemManager.isMemory2)
        {
            dialogueLines = new string[]
            {
                "케이크... 생일인가?"
            };
            isTalk = true;

        }
        else if (ItemManager.isMemory3)
        {
            dialogueLines = new string[]
            {
                "누구지? 뭔가 익숙한 느낌인걸"
            };
            isTalk = true;

        }
        else if (ItemManager.isMemory4)
        {
            dialogueLines = new string[]
            {
                "누구한테 주려 했던 것 같아"
            };
            isTalk = true;

        }
        else if (ItemManager.isLetter)
        {
            isLetterTalk = true;
        }
        else if (MoveToSquare.isReturn)
        {
            isInfoTalk = true;
        }
        if (isTalk)
        {
            // 시작 시 대화 상자 비활성화
            dialogueBox.SetActive(false);

            // 시작 시 대화 시작
            StartCoroutine(StartDialogue());
        }
        if (isLetterTalk)
        {
            LetterBox.SetActive(false);
            LetterText.enabled = false;


            // 시작 시 대화 시작
            StartCoroutine(StartDialogue());
        }
        if (isInfoTalk)
        {
            InfoBox.SetActive(false);
            InfoText.enabled = false;


            // 시작 시 대화 시작
            StartCoroutine(StartDialogue());
        }

        ItemManager.isMemory1 = false;
        ItemManager.isMemory2 = false;
        ItemManager.isMemory3 = false;
        ItemManager.isMemory4 = false;
        ItemManager.isLetter = false;
        MoveToSquare.isReturn = false;

    }

    private void Start()
    {
        
       
    }

    IEnumerator StartDialogue()
    {
        //yield return new WaitForSeconds(1f);  // 시작 대기시간 

        if (isTalk)
        {
            // 대화 상자 활성화
            dialogueBox.SetActive(true);
            
            // 대화창.텍스트 = 저 위에 대사 목록[랜덤인덱스]
            dialogueText.text = dialogueLines[currentLineIndex];
            yield return new WaitForEndOfFrame();
            // 3초 기둘
            yield return new WaitForSeconds(2f);
            // 메인 씬으로 재전송
            dialogueBox.SetActive(false);
            isTalk = false;
        }

        else if (isLetterTalk)
        {
            // 대화 상자 활성화
            LetterBox.SetActive(true);
            // 대화창.텍스트 = 저 위에 대사 목록[랜덤인덱스]
            LetterText.enabled = true;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            // 3초 기둘

            // 메인 씬으로 재전송
            LetterBox.SetActive(false);
            isLetterTalk = false;
        }
        
        else if (isInfoTalk)
        {
            // 대화 상자 활성화
            InfoBox.SetActive(true);
            // 대화창.텍스트 = 저 위에 대사 목록[랜덤인덱스]
            InfoText.enabled = true;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            // 3초 기둘

            // 메인 씬으로 재전송
            InfoBox.SetActive(false);
            isInfoTalk = false;
        }
     
       

    }
}
