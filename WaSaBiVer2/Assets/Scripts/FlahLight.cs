using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlashLight : MonoBehaviour
{
    bool PlayerGetLight;
    static bool useLight; //������ ȹ�� ���θ� Ȯ���� ����
    Light myLight;
    // Start is called before the first frame update
    void Start()
    {
        PlayerGetLight = false;
        useLight = false;
        myLight = this.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        lightOnOFF();
        if (PlayerGetLight == false)
        {
            myLight.intensity = 0;
        }
        else if (PlayerGetLight == true)
        {
            myLight.intensity = 300;
        }
    }

    static internal void GetLight()
    {
       
            useLight = true; //�������� ȹ�������� �˸�
          //  Destroy(GameObject.FindGameObjectWithTag("FlashLight")); //�ʿ� ���� ������ ����          
        
    }

    void lightOnOFF()
    {
        if (useLight == true) //�������� ȹ������ ��쿡�� ����ǵ���
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerGetLight = PlayerGetLight ? false : true;
            }
        }
    }
}