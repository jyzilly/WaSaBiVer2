using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private RaycastHit hit; //ray�� �浹������ �����ϴ� ����ü
    private Ray ray;

    void Update()
    {
        //ObjectHit();
    }

    void ObjectHit()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //ray����

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "FlashLight")
            {
                Debug.Log("�������Դϴ�."); //������Ʈ�� �±װ� FlashLight�� ���           
                FlashLight.GetLight();
            }

        }
    }

}