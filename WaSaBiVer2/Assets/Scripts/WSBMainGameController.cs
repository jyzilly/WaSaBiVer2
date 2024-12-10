using System;
using UnityEngine;


public class WSBMainGameController : MonoBehaviour
{
    private WSBPlayerController PlayerController;
    private WSBItemManager ItemManager;

    //public GameObject firePb;

    //public GameObject[] Prefabs;

    public bool isSameItem_1 = false;
    public bool isSameItem_2 = false;
    public bool isSameItem_3 = false;
    public bool isSameItem_4 = false;

    
    public bool isCreture2 = false;
    public bool isCreture2_1 = false;
    public bool isCreture2_2 = false;


  
    public bool isRun = false;

    private void Start()
    {
        ItemManager = GameObject.Find("ItemManager").GetComponent<WSBItemManager>();
        PlayerController = GameObject.Find("Ch46_nonPBR").GetComponent<WSBPlayerController>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //�Ź�
            ItemManager.PressedFireButton();
            if(ItemManager.item1Able)
            {
                GameObject.Find("Ch46_nonPBR").transform.Find("FirePrefab").transform.gameObject.SetActive(true);
               
                Debug.Log("������");
                ItemManager.item1Able = false;
               // Debug.Log(ItemManager.item1Able);
                if (PlayerController.isSpider)
                {
                    Debug.Log(PlayerController.isSpider);

                    // �°� ���� ��ƼŬ ȿ��
                    isRun = true;
                }
            }
            Invoke("OffItemPb", 1.5f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //�Ҿƹ���
            ItemManager.PressedCharmButton();
            Debug.Log(ItemManager.item2Able);


        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //ũ���� 2
            ItemManager.PressedFirecrackerButton();
            if (ItemManager.item3Able)
            {
                GameObject.Find("Ch46_nonPBR").transform.Find("firecake").transform.gameObject.SetActive(true);
                Debug.Log("������ ũ��ó ������� 3��" + ItemManager.item3Able.ToString());

                ItemManager.item3Able = false;
                
                if (PlayerController.isCreature2)
                {
                    isCreture2 = true;
                    Debug.Log("isCreature2");
                    // �°� ���� ��ƼŬ ȿ��
                    isRun = true;
                  
                }
                else if (PlayerController.isCreature2_1)
                {
                    isCreture2_1 = true;
                    Debug.Log("isCreature2_1");
                    // �°� ���� ��ƼŬ ȿ��
                    isRun = true;
                   
                }
                else if (PlayerController.isCreature2_2)
                {
                    isCreture2_2 = true;
                    Debug.Log("isCreature2_2");
                    // �°� ���� ��ƼŬ ȿ��
                    isRun = true;
                   
                }
            }
            Invoke("OffItemPb", 1.5f);


        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // ũ���� 1
            ItemManager.PressedBlockballButton();
            if (ItemManager.item4Able)
            {
                GameObject.Find("Ch46_nonPBR").transform.Find("Blood").transform.gameObject.SetActive(true);
                Debug.Log("����" + ItemManager.item1Able.ToString());
                ItemManager.item4Able = false;

                if (PlayerController.isCreature1)
                {
                    
                    // �°� ���� ��ƼŬ ȿ��
                    isRun = true;
                }
            }
            Invoke("OffItemPb", 1.0f);

        }
    }

    void OffItemPb()
    {
       GameObject.Find("Ch46_nonPBR").transform.Find("FirePrefab").transform.gameObject.SetActive(false);
        GameObject.Find("Ch46_nonPBR").transform.Find("firecake").transform.gameObject.SetActive(false);
        GameObject.Find("Ch46_nonPBR").transform.Find("Blood").transform.gameObject.SetActive(false);

    }

}
