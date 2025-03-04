using UnityEngine;

public class WSBMoveToSquare : MonoBehaviour
{
    [SerializeField] private WSBItemManager itemManager;
    [SerializeField] private WSBPlayerController player;
    [SerializeField] private Transform SquareTr;

    public bool isReturn = false;


    private void Start()
    {
     //  GameObject.FindGameObjectWithTag("CutScene").SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(itemManager.keyCnt == 3)
        {
            isReturn = false;
            GameObject.FindGameObjectWithTag("CutScene").SetActive(false);
            player.SetPosition(SquareTr.position);

            //player.transform.position = SquareTr.position; ;
        }
        else if(other.tag == "Player")
        {
            isReturn = true;
            Debug.Log("���� 3�� ��Ƽ� �ٽ� ����� ������.");
        }
    }



}
