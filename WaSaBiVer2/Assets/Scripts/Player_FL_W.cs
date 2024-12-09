using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private RaycastHit hit; //ray의 충돌정보를 저장하는 구조체
    private Ray ray;

    void Update()
    {
        //ObjectHit();
    }

    void ObjectHit()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //ray생성

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "FlashLight")
            {
                Debug.Log("손전등입니다."); //오브젝트의 태그가 FlashLight일 경우           
                FlashLight.GetLight();
            }

        }
    }

}