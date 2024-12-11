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

    public bool isRun = false;
    public bool isRun2 = false;
    public bool isRun3 = false;
    public bool isRun4 = false;
    public bool isRun5 = false;



    private void Start()
    {
        ItemManager = GameObject.Find("ItemManager").GetComponent<WSBItemManager>();
        PlayerController = GameObject.Find("Ch46_nonPBR").GetComponent<WSBPlayerController>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
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
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            //�Ҿƹ���
            ItemManager.PressedCharmButton();
            Debug.Log(ItemManager.item2Able);


        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            //ũ���� 2
            ItemManager.PressedFirecrackerButton();
            if (ItemManager.item3Able)
            {
                GameObject.Find("Ch46_nonPBR").transform.Find("firecake").transform.gameObject.SetActive(true);
                ItemManager.item3Able = false;
                
                if (PlayerController.isCreature2)
                {
                    // �°� ���� ��ƼŬ ȿ��
                    isRun2 = true;
                }
                else if (PlayerController.isCreature2_1)
                {
               
                    // �°� ���� ��ƼŬ ȿ��
                    isRun3 = true;
                }
                else if (PlayerController.isCreature2_2)
                {
                   
                    // �°� ���� ��ƼŬ ȿ��
                    isRun4 = true;
                }
            }
            Invoke("OffItemPb", 1.5f);


        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            // ũ���� 1
            ItemManager.PressedBlockballButton();
            if (ItemManager.item4Able)
            {
                //GameObject.Find("Ch46_nonPBR").transform.Find("Blood").transform.gameObject.SetActive(true);
                Debug.Log("����" + ItemManager.item1Able.ToString());
                ItemManager.item4Able = false;

                if (PlayerController.isCreature1)
                {
                    
                    // �°� ���� ��ƼŬ ȿ��
                    isRun5 = true;
                }
            }
            //Invoke("OffItemPb", 1.0f);

        }
    }

    void OffItemPb()
    {
       GameObject.Find("Ch46_nonPBR").transform.Find("FirePrefab").transform.gameObject.SetActive(false);
        GameObject.Find("Ch46_nonPBR").transform.Find("firecake").transform.gameObject.SetActive(false);
        GameObject.Find("Ch46_nonPBR").transform.Find("Blood").transform.gameObject.SetActive(false);

    }

}
