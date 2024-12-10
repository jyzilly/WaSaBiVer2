using UnityEngine;

public class WSBMemoryKeyManager : MonoBehaviour
{

    [SerializeField] private Transform[] MKTr;

    [SerializeField] private GameObject[] MandK;

    private int keyCnt = 0;
    private int memoryCnt = 0;

    private void Start()
    {
        SetMK();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                bool getItem = false;

                if(hit.transform.gameObject.tag == "Key")
                {
                    //key1의 이미지를 활성화하기
                    //
                    keyCnt += 1;
                    getItem = true;
                }
                else if(hit.transform.gameObject.tag == "Memory")
                {
                    memoryCnt += 1;
                    getItem = true;
                }

                if (getItem == true)
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }

        CheckKeyCnt();
        
    }

    private void SetMK()
    {
        for(int i = 0; i < 7; ++i)
        {
            //i번째 위치에서 i번 키 or 메모리 퍼즐 생성하기
            Instantiate(MandK[i], MKTr[i].position, Quaternion.identity);
        }
    }

    private void CheckKeyCnt()
    {
        if (keyCnt == 3)
        {
            //문 활성화

            //메모리 퍼즐 체크해서 엔딩씬 넘어가기
            CheckResult();
        }
    }

    //private void CheckMemoryCnt()
    //{
    //    if (memoryCnt == 2 || memoryCnt == 3)
    //    {
    //        //메모리 조건1 달성
    //    }
    //    else if (memoryCnt == 4)
    //    {
    //        //메모리 조건2 달성
    //    }
    //    else
    //    {
    //        //미달성
    //    }
    //}

    private void CheckResult()
    {
        if(keyCnt == 3 && memoryCnt == 2 || memoryCnt == 3 || memoryCnt == 4)
        {
            //굿엔딩
        }
        else if(keyCnt == 3 && memoryCnt == 5)
        {
            //진엔딩
        }
    }

}
