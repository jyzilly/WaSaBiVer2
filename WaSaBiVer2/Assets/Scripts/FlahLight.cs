using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlashLight : MonoBehaviour
{
    bool PlayerGetLight;
    static bool useLight; //손전등 획득 여부를 확인할 변수
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
       
            useLight = true; //손전등을 획득했음을 알림
          //  Destroy(GameObject.FindGameObjectWithTag("FlashLight")); //맵에 놓인 손전등 삭제          
        
    }

    void lightOnOFF()
    {
        if (useLight == true) //손전등을 획득했을 경우에만 실행되도록
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerGetLight = PlayerGetLight ? false : true;
            }
        }
    }
}