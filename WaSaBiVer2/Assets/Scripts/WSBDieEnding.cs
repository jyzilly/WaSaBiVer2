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
        currentLineIndex = Random.Range(0, 10); // ���� �ε���

        // ��� ���
        dialogueLines = new string[]
        {
            "�ܼ��� �� ��ƾ��ҰŰ���...",
            "�������� �� ã�ƺ���..?",
            "��� �ݺ��ǰ� �ִ� �� ���� ����� ���...",
            "������ �´� �������� ���� �ִ°ɱ�..?",
            "�Ź̴� ��Ű���������� �Ѿƿ����ʴ� �� ����",
            "���� ���� ���� �� ������....",
            "����� �������� ��� �ִ� �ɱ�?",
            "� ������ ������ ���� �׸��� ������ �������� �� ����",
            "���� ��¼�� ���⿡ ������ �Ȱɱ�.. ������ ����;�..."
        };

        // ���� �� ��ȭ ���� ��Ȱ��ȭ
        dialogueBox.SetActive(false);

        // ���� �� ��ȭ ����
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(0.5f);  // ���� ���ð� 

        // ��ȭ ���� Ȱ��ȭ
        dialogueBox.SetActive(true);
        // ��ȭâ.�ؽ�Ʈ = �� ���� ��� ���[�����ε���]
        dialogueText.text = dialogueLines[currentLineIndex];
        // 3�� ���
        yield return new WaitForSeconds(1.5f);
        // ���� ������ ������
        SceneManager.LoadScene("Wasabi 3");
    }
}