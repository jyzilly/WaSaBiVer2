using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class EndingBad : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    private TextMeshProUGUI dialogueText; // ��� 

    [SerializeField]
    private TextMeshProUGUI enterText; // ���� ����

    [SerializeField]
    private AudioClip bbi; // ��- �ϴ� ����� Ŭ��

    [SerializeField]
    private CanvasGroup SantaBaby; // ĵ���� �׷�

    private string[] dialogueLines;  // ��ȭ ���� �迭
    private int currentLineIndex = 0;  // ���� ��ȭ �ε���

    // ���� ���̵� �ƿ�
    double fadeOutSeconds = 1.7;
    bool isFadeOut = true;
    double fadeDeltaTime = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.PlayOneShot(bbi);
        SantaBaby.alpha = 0;
        // ��� ���
        dialogueLines = new string[]
        {
            "20XX�� X�� X�� XX�� XX��, OOO�Բ��� ����ϼ̽��ϴ�"
        };

        enterText.enabled = false; // ���� �ȳ� ���� ��Ȱ��ȭ

    }

    private void Update()
    {
        if (isFadeOut)
        {
            fadeDeltaTime += Time.deltaTime;
            if (fadeDeltaTime >= fadeOutSeconds)
            {
                fadeDeltaTime = fadeOutSeconds;
                isFadeOut = false;
            }
            audioSource.volume = (float)(1.0 - (fadeDeltaTime / fadeOutSeconds));
        }

        if (SantaBaby.alpha < 1)
        {
            SantaBaby.alpha += Time.deltaTime;
            if (SantaBaby.alpha >= 1)
            {
                // ĵ���� �׷� �ε� �Ϸ�Ǹ� ��ȭ ����
                StartCoroutine(StartDialogue());
            }

        }
    }



    IEnumerator StartDialogue()
    {
        
        yield return new WaitForSeconds(0.2f);  // ���� ���ð�
        


        // ��ȭ ����
        while (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
            enterText.enabled = true;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));  // Ű���� ���� ���

            currentLineIndex++;  // ���� ��ȭ�� �Ѿ
            yield return new WaitForSeconds(0.1f);  // ���ð� (


            // ���� ������ ���� �����
            if (currentLineIndex >= dialogueLines.Length)
            {
                SceneManager.LoadScene("Wasabi 1");
            }
        }
    }


  


}
