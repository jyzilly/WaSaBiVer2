using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class WSBDieEnding : MonoBehaviour
{

    public TextMeshProUGUI dialogueText; 
    public GameObject dialogueBox; 

    private string[] dialogueLines;  // ��ȭ ���� �迭
    int currentLineIndex = 0;  

    void Start()
    {
        currentLineIndex = Random.Range(0, 3); // ���� �ε���

        // ��� ���
        dialogueLines = new string[]
        {
            "�ܼ��� �� ��ƾ��ҰŰ���...",
            "�������� �� ã�ƺ���..?",
            "...ũ��������..."
        };

        // ���� �� ��ȭ ���� ��Ȱ��ȭ
        dialogueBox.SetActive(false);

        // ���� �� ��ȭ ����
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(1f);  // ���� ���ð� 

        // ��ȭ ���� Ȱ��ȭ
        dialogueBox.SetActive(true);
        // ��ȭâ.�ؽ�Ʈ = �� ���� ��� ���[�����ε���]
        dialogueText.text = dialogueLines[currentLineIndex];
        // 3�� ���
        yield return new WaitForSeconds(3f);
        // ���� ������ ������
        SceneManager.LoadScene("Wasabi 3");
    }
}