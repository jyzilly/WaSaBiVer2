using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class WSBItemManager : MonoBehaviour
{
    //아이템 습득하는 함수
    //만약에 마우스로 아이템을 클릭하면 해당 아이템의 이름에  찾아가서 해당 아이템의 개수를 +1

    //손전등. 항상 왼손에 들고 있고. on/off Input키를 추가해서 조명 끄고 키는 기능을 추가 
    //부적. use를 bool로 설정해서 만약에 클릭했으면 개수 -1, 2초동안 부적타는효과를 활성화시킴, 이걸 사용했다는 걸 할아버지한테 전달하기 .
    //if문으로 만약에 0개이상이면 클릭했을때 개수 -1,동시 사용했는 걸 할아버지한테 전달,0개이면 부적버튼을 비활성화시킨다.  
    //불. if문으로 만약에 0개 이상이면 버튼 활성화 시키고 클릭했을 때 개수 -1,불 효과를 2초동안 활성화시킴, 거미줄 distroy함수를 사용해서 파괴시키기. 0개이면 버튼 비활성화로 시킴.  
    //폭죽. if문으로 만약에 0개 이상이면 버튼 활성화 시키고 클릭했을 때 개수 -1,소리재생, 사용했다는 걸 크리처2한테 전달. 0개이면 버튼 비활성화로 시킴.
    //결계구슬. if문으로 만약에 0개 이상이면 버튼 활성화 시키고 클릭했을 때 개수 -1 , 구슬이 플레이어 바라보는 시선에 생기기(아니면 좌표로 플레이어 앞에 일자로 나열하기), 전달없음


    //아이템 버튼 형식으로 표현
    [SerializeField] private Button flash;
    [SerializeField] private Button charm;
    [SerializeField] private Button fire;
    [SerializeField] private Button firecracker;
    [SerializeField] private Button blockball;

    //UIText로 개수 표현하기
    [SerializeField] private TextMeshProUGUI charmCnt;
    [SerializeField] private TextMeshProUGUI fireCnt;
    [SerializeField] private TextMeshProUGUI firecrackerCnt;
    [SerializeField] private TextMeshProUGUI blockballCnt;

    private int CharmCnt = 0;
    private int FireCnt = 0;
    private int FirecrackerCnt = 0;
    private int BlockballCnt = 0;


    private void Start()
    {
        fire.onClick.AddListener(PressedFireButton);
        fire.onClick.AddListener(PressedCharm);
        fire.onClick.AddListener(PressedFirecracker);
        fire.onClick.AddListener(PressedBlockball);
    }

    private void Update()
    {
        GetItem();
    }

    private void GetItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.gameObject);

            bool getItem = false;
            //태그로 아이템을 인식하기. 아이템마다 태그를 추가하기
            //부적
            if (hit.transform.gameObject.tag == "charm")
            {
                CharmCnt += 1;
                getItem = true;
            }
            //불
            else if (hit.transform.gameObject.tag == "fire")
            {
                FireCnt += 1;
                getItem = true;
            }
            //폭죽
            else if (hit.transform.gameObject.tag == "firecracker")
            {
                FirecrackerCnt += 1;
                getItem = true;
            }
            //경계구슬
            else if (hit.transform.gameObject.tag == "blockball")
            {
                BlockballCnt += 1;
                getItem = true;
            }
            if (getItem == true)
            {
                UpdateItemCnt();
            }

        }
    }


    private void UpdateItemCnt()
    {
        charmCnt.text = CharmCnt.ToString();
        fireCnt.text = FireCnt.ToString();
        firecrackerCnt.text = FirecrackerCnt.ToString();
        blockballCnt.text = BlockballCnt.ToString();
    }

    private void PressedFireButton()
    {
        if (FireCnt > 0)
        {
            --FireCnt;
            UpdateItemCnt();
        }
    }

    private void PressedCharm()
    {
        if (CharmCnt > 0)
        {
            --CharmCnt;
            UpdateItemCnt();
        }
    }

    private void PressedFirecracker()
    {
        if (FirecrackerCnt > 0)
        {
            --FirecrackerCnt;
            UpdateItemCnt();
        }
    }

    private void PressedBlockball()
    {
        if (BlockballCnt > 0)
        {
            --BlockballCnt;
            UpdateItemCnt();
        }
    }







}

