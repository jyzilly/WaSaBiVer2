using UnityEngine;


public class WSBMainGameController : MonoBehaviour
{
    private WSBPlayerController PlayerController;
    private WSBItemManager ItemManager;

    public bool isSameItem_1 = false;

    private void Start()
    {
        ItemManager = GameObject.Find("ItemManager").GetComponent<WSBItemManager>();
        //ItemManager = GetComponent<WSBItemManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            //�Ź�
            ItemManager.PressedFireButton();
            isSameItem_1 = true;
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
            Debug.Log(ItemManager.item3Able);


        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            // ũ���� 1
            ItemManager.PressedBlockballButton();
            Debug.Log(ItemManager.item4Able);


        }
    }

}
