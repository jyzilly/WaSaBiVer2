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
            //거미
            ItemManager.PressedFireButton();
            if(ItemManager.item1Able)
            {
                Debug.Log(ItemManager.item1Able);

                if (PlayerController.isSpider)
                {
                    Debug.Log(PlayerController.isSpider);
                    // 맞게 사용된 파티클 효과
                    isRun = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            //할아버지
            ItemManager.PressedCharmButton();
            Debug.Log(ItemManager.item2Able);


        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            //크리쳐 2
            ItemManager.PressedFirecrackerButton();
            if (ItemManager.item3Able)
            {
                
                if (PlayerController.isCreature2)
                {
                    
                    // 맞게 사용된 파티클 효과
                    isRun = true;
                }
                else if (PlayerController.isCreature2_1)
                {

                    // 맞게 사용된 파티클 효과
                    isRun = true;
                }
                else if (PlayerController.isCreature2_2)
                {

                    // 맞게 사용된 파티클 효과
                    isRun = true;
                }
            }


        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            // 크리쳐 1
            ItemManager.PressedBlockballButton();
            if (ItemManager.item4Able)
            {
                
                if (PlayerController.isCreature1)
                {
                    
                    // 맞게 사용된 파티클 효과
                    isRun = true;
                }
            }


        }
    }

}
