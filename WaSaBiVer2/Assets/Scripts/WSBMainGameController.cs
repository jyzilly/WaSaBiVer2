using UnityEngine;


public class WSBMainGameController : MonoBehaviour
{
    private WSBPlayerController PlayerController;
    private WSBItemManager ItemManager;

    public bool isSameItem_1 = false;
    public bool isSameItem_2 = false;
    public bool isSameItem_3 = false;
    public bool isSameItem_4 = false;

    public bool isRun = false;



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
                Debug.Log(ItemManager.item1Able);

                if (PlayerController.isSpider)
                {
                    Debug.Log(PlayerController.isSpider);
                    // �°� ���� ��ƼŬ ȿ��
                    isRun = true;
                }
            }
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
                
                if (PlayerController.isCreature2)
                {
                    
                    // �°� ���� ��ƼŬ ȿ��
                    isRun = true;
                }
                else if (PlayerController.isCreature2_1)
                {

                    // �°� ���� ��ƼŬ ȿ��
                    isRun = true;
                }
                else if (PlayerController.isCreature2_2)
                {

                    // �°� ���� ��ƼŬ ȿ��
                    isRun = true;
                }
            }


        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            // ũ���� 1
            ItemManager.PressedBlockballButton();
            if (ItemManager.item4Able)
            {
                
                if (PlayerController.isCreature1)
                {
                    
                    // �°� ���� ��ƼŬ ȿ��
                    isRun = true;
                }
            }


        }
    }

}
