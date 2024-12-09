using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WSBItemManager : MonoBehaviour
{
    //������ �����ϴ� �Լ�
    //���࿡ ���콺�� �������� Ŭ���ϸ� �ش� �������� �̸���  ã�ư��� �ش� �������� ������ +1

    //������. �׻� �޼տ� ��� �ְ�. on/off InputŰ�� �߰��ؼ� ���� ���� Ű�� ����� �߰� 
    //����. use�� bool�� �����ؼ� ���࿡ Ŭ�������� ���� -1, 2�ʵ��� ����Ÿ��ȿ���� Ȱ��ȭ��Ŵ, �̰� ����ߴٴ� �� �Ҿƹ������� �����ϱ� .
    //if������ ���࿡ 0���̻��̸� Ŭ�������� ���� -1,���� ����ߴ� �� �Ҿƹ������� ����,0���̸� ������ư�� ��Ȱ��ȭ��Ų��.  
    //��. if������ ���࿡ 0�� �̻��̸� ��ư Ȱ��ȭ ��Ű�� Ŭ������ �� ���� -1,�� ȿ���� 2�ʵ��� Ȱ��ȭ��Ŵ, �Ź��� distroy�Լ��� ����ؼ� �ı���Ű��. 0���̸� ��ư ��Ȱ��ȭ�� ��Ŵ.  
    //����. if������ ���࿡ 0�� �̻��̸� ��ư Ȱ��ȭ ��Ű�� Ŭ������ �� ���� -1,�Ҹ����, ����ߴٴ� �� ũ��ó2���� ����. 0���̸� ��ư ��Ȱ��ȭ�� ��Ŵ.
    //��豸��. if������ ���࿡ 0�� �̻��̸� ��ư Ȱ��ȭ ��Ű�� Ŭ������ �� ���� -1 , ������ �÷��̾� �ٶ󺸴� �ü��� �����(�ƴϸ� ��ǥ�� �÷��̾� �տ� ���ڷ� �����ϱ�), ���޾���


    //������ ��ư �������� ǥ��
    [SerializeField] private Button flash;
    [SerializeField] private Button charm;
    [SerializeField] private Button fire;
    [SerializeField] private Button firecracker;
    [SerializeField] private Button blockball;

    //UIText�� ���� ǥ���ϱ�
    [SerializeField] private TextMeshProUGUI charmCnt;
    [SerializeField] private TextMeshProUGUI fireCnt;
    [SerializeField] private TextMeshProUGUI firecrackerCnt;
    [SerializeField] private TextMeshProUGUI blockballCnt;


    //������ �̹��� 
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


   // private bool PlayerGetLight; //true�� ��� ������on
   // private Light myLight; //light ������Ʈ�� ��� ����

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

       // PlayerGetLight = false; //�ʱ⿡�� �������� �Һ��� ���� ����
       // myLight = this.GetComponent<Light>(); //������Ʈ�� ���� light ������Ʈ�� ������.
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
        //    PlayerGetLight = PlayerGetLight ? false : true; //rŰ�� ���� �������� �Һ��� on/off
        //}
        //if (PlayerGetLight == false)
        //{
        //    myLight.intensity = 0; //������ off
        //    flashImg.enabled = false;
        //}


        //if (PlayerGetLight == true)
        //{
        //    myLight.intensity = 10; //������ on
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
                //�±׷� �������� �ν��ϱ�. �����۸��� �±׸� �߰��ϱ�
                //����
                if (hit.transform.gameObject.tag == "charm")
                {
                    //if (Input.GetMouseButtonDown(0))
                    {
                        CharmCnt += 1;
                        getItem = true;
                        //Destroy(GameObject.FindGameObjectWithTag("charm"));                        
                    }
                }
                //��
                else if (hit.transform.gameObject.tag == "fire")
                {
                    //if (Input.GetMouseButtonDown(0))
                    {
                        FireCnt += 1;
                        getItem = true;
                        //Destroy(GameObject.FindGameObjectWithTag("fire"));

                    }
                }
                //����
                else if (hit.transform.gameObject.tag == "firecracker")
                {
                    //if (Input.GetMouseButtonDown(0))
                    {
                        FirecrackerCnt += 1;
                        getItem = true;
                        //Destroy(GameObject.FindGameObjectWithTag("firecracker"));

                    }
                }
                //��豸��
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
        Debug.Log("�����Ϸ�");
    }



}

