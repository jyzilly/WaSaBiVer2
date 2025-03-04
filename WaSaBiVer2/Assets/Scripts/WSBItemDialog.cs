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

    private string[] dialogueLines;  // ��ȭ ���� �迭
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
                "��?......"
            };
            isTalk = true;
        }
        else if (ItemManager.isMemory2)
        {
            dialogueLines = new string[]
            {
                "����ũ... �����ΰ�?"
            };
            isTalk = true;

        }
        else if (ItemManager.isMemory3)
        {
            dialogueLines = new string[]
            {
                "������? ���� �ͼ��� �����ΰ�"
            };
            isTalk = true;

        }
        else if (ItemManager.isMemory4)
        {
            dialogueLines = new string[]
            {
                "�������� �ַ� �ߴ� �� ����"
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
            // ���� �� ��ȭ ���� ��Ȱ��ȭ
            dialogueBox.SetActive(false);

            // ���� �� ��ȭ ����
            StartCoroutine(StartDialogue());
        }
        if (isLetterTalk)
        {
            LetterBox.SetActive(false);
            LetterText.enabled = false;


            // ���� �� ��ȭ ����
            StartCoroutine(StartDialogue());
        }
        if (isInfoTalk)
        {
            InfoBox.SetActive(false);
            InfoText.enabled = false;


            // ���� �� ��ȭ ����
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
        //yield return new WaitForSeconds(1f);  // ���� ���ð� 

        if (isTalk)
        {
            // ��ȭ ���� Ȱ��ȭ
            dialogueBox.SetActive(true);
            
            // ��ȭâ.�ؽ�Ʈ = �� ���� ��� ���[�����ε���]
            dialogueText.text = dialogueLines[currentLineIndex];
            yield return new WaitForEndOfFrame();
            // 3�� ���
            yield return new WaitForSeconds(2f);
            // ���� ������ ������
            dialogueBox.SetActive(false);
            isTalk = false;
        }

        else if (isLetterTalk)
        {
            // ��ȭ ���� Ȱ��ȭ
            LetterBox.SetActive(true);
            // ��ȭâ.�ؽ�Ʈ = �� ���� ��� ���[�����ε���]
            LetterText.enabled = true;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            // 3�� ���

            // ���� ������ ������
            LetterBox.SetActive(false);
            isLetterTalk = false;
        }
        
        else if (isInfoTalk)
        {
            // ��ȭ ���� Ȱ��ȭ
            InfoBox.SetActive(true);
            // ��ȭâ.�ؽ�Ʈ = �� ���� ��� ���[�����ε���]
            InfoText.enabled = true;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            // 3�� ���

            // ���� ������ ������
            InfoBox.SetActive(false);
            isInfoTalk = false;
        }
     
       

    }
}
