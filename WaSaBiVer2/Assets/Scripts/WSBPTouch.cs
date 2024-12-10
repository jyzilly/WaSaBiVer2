using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WSBPTouch : MonoBehaviour
{
    private WSBCreature1 Cture1;
    //private Coroutine moveOnCoroutine = null;

    private void Start()
    {
        Cture1 = GameObject.Find("BookHeadMonster").GetComponent<WSBCreature1>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("여기까지");
            StartCoroutine(Cture1.moveOn());
            
        }
    }


}

    //palyer.gettarget()