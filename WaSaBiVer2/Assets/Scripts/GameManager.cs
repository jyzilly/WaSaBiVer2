using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //닉네임 입력을 받아서 저장하고 버튼을 클릭할 때 다음 씬으로 이동하기

    [SerializeField] private TMP_InputField  nickName;
    [SerializeField] private Button startButton;


    private void Start()
    {
        PlayerPrefs.SetString("nickName", nickName.text);        
        startButton.onClick.AddListener(() => SceneManager.LoadScene("Wasabi 2"));
    }











}
