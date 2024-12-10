using UnityEngine;

public class WSBMoveToSquare : MonoBehaviour
{
    [SerializeField] private WSBItemManager itemManager;
    [SerializeField] private WSBPlayerController player;
    [SerializeField] private Transform SquareTr;


    private void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(itemManager.keyCnt == 3)
        {
            player.transform.position = SquareTr.position; ;
        }
        else
        {
            Debug.Log("���� 3�� ��Ƽ� �ٽ� ����� ������.");
        }
    }



}
