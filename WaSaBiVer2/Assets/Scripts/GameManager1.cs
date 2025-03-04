using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class GameManager1 : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    
    [SerializeField]
    private TextMeshProUGUI enterText;

    private string[] dialogueLines;  // ��ȭ ���� �迭
    private int currentLineIndex = 0;  // ���� ��ȭ �ε���

    public AudioClip enterSD;

    private void Start()
    {
        // ��� ���
        dialogueLines = new string[]
        {
            "�ȳ�!",
            "������ 12�� 24���̾�",
            "�Ϸ��� �ð��� �ٰ� ���ʹٸ� Ż���غ�",
            "��!",
            "WASD�� ������ �� �ְ� Shift�� �޸� �� �����ž�",
            "����� ���� Ű�е� 1~5�� �������� ��� �� �� �־�",
            "�̸� �޸�ũ��������~"
        };

        enterText.enabled = false; // ���� �ȳ� ���� ��Ȱ��ȭ


        // ���� �� ��ȭ ����
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(0.5f);  // ���� ���ð�


        // ��ȭ ����
        while (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
            enterText.enabled = true;
            yield return new WaitUntil(() =>
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    soundEnter();
                    return true;
                }
                return false;
            });  // Ű���� ���� ���



            currentLineIndex++;  // ���� ��ȭ�� �Ѿ
            yield return new WaitForSeconds(0.1f);  // ���ð� (


            // ��� ��ȭ�� ������ �� ���� �� �ε�
            if (currentLineIndex >= dialogueLines.Length)
            {
                SceneManager.LoadScene("Wasabi 3");
            }
        }
    }

    private void soundEnter()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(enterSD);
    }
}
