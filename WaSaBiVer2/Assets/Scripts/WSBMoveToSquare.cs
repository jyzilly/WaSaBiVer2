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
            Debug.Log("열쇠 3개 모아서 다시 여기로 오세요.");
        }
    }



}
