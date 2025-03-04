using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class EndingGood : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup room;
    [SerializeField]
    private CanvasGroup roomSub;
    [SerializeField]
    private TextMeshProUGUI allText;

    [SerializeField]
    private CanvasGroup desk;
    [SerializeField]
    private CanvasGroup deskSub;


    private string[] dialogueLines;  // ��ȭ ���� �迭
    private int currentLineIndex = 0;  // ���� ��ȭ �ε���

    private void Awake()
    {
        
    }

    private void Start()
    {
        roomSub.alpha = 0;
        dialogueLines = new string[]
        {
            "�� ���̴�! ��Ƴ��� �ǰ�..?",
            "���..?"
        };
    }

    private void Update()
    {
        if (roomSub.alpha <= 1)
        {
            roomSub.alpha += Time.deltaTime;
            if (roomSub.alpha == 1)
            {
                // ĵ���� �׷� �ε� �Ϸ�Ǹ� ��ȭ ����
                
                StartCoroutine(FadeOut());
                //StartCoroutine(StartDialogue());
            }

        }
    }

    IEnumerator FadeOut()
    {
        while (room.alpha > 0)
        {
            room.alpha -= Time.deltaTime * 2f;
            allText.text = dialogueLines[0];
            yield return StartDialogue();
        }
    }


    IEnumerator StartDialogue()
    {

        //yield return new WaitForSeconds(0.2f); ;  // ���� ���ð�

        // ��ȭ ����
        while (currentLineIndex < dialogueLines.Length)
        {

            yield return new WaitForSeconds(3f);
            allText.text = "";
           

            if (room.alpha <= 0.3)
            {
               // Debug.Log(currentLineIndex);
                yield return new WaitForSeconds(0.2f);
                allText.text = dialogueLines[1];
                yield return new WaitForSeconds(4f);  // ���� ���ð�
                SceneManager.LoadScene("Wasabi 1");

            }
           
        }
    }
   
}
