using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WSBItemDialog : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    private WSBItemManager ItemManager;

    private string[] dialogueLines;  // ��ȭ ���� �迭
    int currentLineIndex = 0;

    private void Awake()
    {
        ItemManager = GameObject.Find("ItemManager").GetComponent<WSBItemManager>();
    }

    private void Update()
    {
        bool isTalk = false;

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
        if (isTalk)
        {
            // ���� �� ��ȭ ���� ��Ȱ��ȭ
            dialogueBox.SetActive(false);

            // ���� �� ��ȭ ����
            StartCoroutine(StartDialogue());
        }
        ItemManager.isMemory1 = false;
        ItemManager.isMemory2 = false;
        ItemManager.isMemory3 = false;
        ItemManager.isMemory4 = false;

    }

    private void Start()
    {
        
       
    }

    IEnumerator StartDialogue()
    {
        //yield return new WaitForSeconds(1f);  // ���� ���ð� 

        // ��ȭ ���� Ȱ��ȭ
        dialogueBox.SetActive(true);
        // ��ȭâ.�ؽ�Ʈ = �� ���� ��� ���[�����ε���]
        dialogueText.text = dialogueLines[currentLineIndex];
        yield return new WaitForEndOfFrame();
        // 3�� ���
        yield return new WaitForSeconds(2f);
        // ���� ������ ������
        dialogueBox.SetActive(false);
       

    }
}
