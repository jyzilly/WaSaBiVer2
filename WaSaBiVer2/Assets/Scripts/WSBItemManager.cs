using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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


    //아이템 이미지 
    [SerializeField] private Image flashImg;
    [SerializeField] private Image charmImg;
    [SerializeField] private Image fireImg;
    [SerializeField] private Image firecrackerImg;
    [SerializeField] private Image blockballImg;


    private int CharmCnt = 0;
    private int FireCnt = 0;
    private int FirecrackerCnt = 0;
    private int BlockballCnt = 0;

    public bool item1Able = false;
    public bool item2Able = false;
    public bool item3Able = false;
    public bool item4Able = false;


   // private bool PlayerGetLight; //true일 경우 손전등on
   // private Light myLight; //light 컴포넌트를 담는 변수

    [SerializeField] private GameObject[] Items;


    [SerializeField] private Transform[] CreatItemTrs;


    private void Awake()
    {
    }

    private void Start()
    {
        CreatItem();

        //fire.onClick.AddListener(PressedFireButton);
        //charm.onClick.AddListener(PressedCharmButton);
        //firecracker.onClick.AddListener(PressedFirecrackerButton);
        //blockball.onClick.AddListener(PressedBlockballButton);

       // PlayerGetLight = false; //초기에는 손전등의 불빛이 꺼진 상태
       // myLight = this.GetComponent<Light>(); //오브젝트가 가진 light 컴포넌트를 가져옴.
    }

    private void Update()
    {
        GetItem();
        SetItemImg();

        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    PressedFireButton();
        //}
        //if(Input.GetKeyDown(KeyCode.Z))
        //{
        //    PressedCharmButton();
        //}
        //if(Input.GetKeyDown(KeyCode.C))
        //{
        //    PressedFirecrackerButton();
        //}
        //if(Input.GetKeyDown(KeyCode.V))
        //{
        //    PressedBlockballButton();
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    PlayerGetLight = PlayerGetLight ? false : true; //r키를 눌러 손전등의 불빛을 on/off
        //}
        //if (PlayerGetLight == false)
        //{
        //    myLight.intensity = 0; //손전등 off
        //    flashImg.enabled = false;
        //}


        //if (PlayerGetLight == true)
        //{
        //    myLight.intensity = 10; //손전등 on
        //    flashImg.enabled = true;
        //}


    }

    private void GetItem()
    {
        if (Input.GetMouseButtonDown(0))
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
                    //if (Input.GetMouseButtonDown(0))
                    {
                        CharmCnt += 1;
                        getItem = true;
                        //Destroy(GameObject.FindGameObjectWithTag("charm"));                        
                    }
                }
                //불
                else if (hit.transform.gameObject.tag == "fire")
                {
                    //if (Input.GetMouseButtonDown(0))
                    {
                        FireCnt += 1;
                        getItem = true;
                        //Destroy(GameObject.FindGameObjectWithTag("fire"));

                    }
                }
                //폭죽
                else if (hit.transform.gameObject.tag == "firecracker")
                {
                    //if (Input.GetMouseButtonDown(0))
                    {
                        FirecrackerCnt += 1;
                        getItem = true;
                        //Destroy(GameObject.FindGameObjectWithTag("firecracker"));

                    }
                }
                //경계구슬
                else if (hit.transform.gameObject.tag == "blockball")
                {
                    //if (Input.GetMouseButtonDown(0))
                    {
                        BlockballCnt += 1;
                        getItem = true;
                        //Destroy(GameObject.FindGameObjectWithTag("blockball"));

                    }
                }

                if (getItem == true)
                {
                    UpdateItemCnt();
                    Destroy(hit.transform.gameObject);
                }

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

    public void PressedFireButton()
    {
        if (FireCnt > 0)
        {
            --FireCnt;
            UpdateItemCnt();
            item1Able = true;
        }

        //else if(FireCnt > 0 && GameObject.Find("itemCollider"))
        //{
        //    --FireCnt;
        //    UpdateItemCnt();
        //}

    }

    public void PressedCharmButton()
    {
        if (CharmCnt > 0)
        {
            --CharmCnt;
            UpdateItemCnt();

            item2Able = true;

        }
    }

    public void PressedFirecrackerButton()
    {
        if (FirecrackerCnt > 0)
        {
            --FirecrackerCnt;
            UpdateItemCnt();
            item3Able = true;

        }
    }

    public void PressedBlockballButton()
    {
        if (BlockballCnt > 0)
        {
            --BlockballCnt;
            UpdateItemCnt();
            item4Able = true;

        }
    }

    private void SetItemImg()
    {
        if (CharmCnt == 0)
        {
            charmImg.enabled = false;
        }
        else
        {
            charmImg.enabled = true;
        }
        if(FireCnt == 0)
        {
            fireImg.enabled = false;
        }
        else
        {
            fireImg.enabled = true;
        }
        if(FirecrackerCnt == 0)
        {
            firecrackerImg.enabled = false;
        }
        else
        {
            firecrackerImg.enabled = true;
        }
        if(BlockballCnt == 0)
        {
            blockballImg.enabled = false;
        }
        else
        {
            blockballImg.enabled = true;
        }
    }

    private void CreatItem()
    {
        for(int i = 0; i < 7; ++i)
        {
            
            GameObject randomItem = Items[Random.Range(0, Items.Length)];
            //GameObject randomItem = Items[2];
            Instantiate(randomItem, CreatItemTrs[i].position,Quaternion.identity);

        }
        Debug.Log("생성완료");
    }



}

