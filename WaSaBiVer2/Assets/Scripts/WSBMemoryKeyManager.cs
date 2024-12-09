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
                    //key1�� �̹����� Ȱ��ȭ�ϱ�
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
            //i��° ��ġ���� i�� Ű or �޸� ���� �����ϱ�
            Instantiate(MandK[i], MKTr[i].position, Quaternion.identity);
        }
    }

    private void CheckKeyCnt()
    {
        if (keyCnt == 3)
        {
            //�� Ȱ��ȭ

            //�޸� ���� üũ�ؼ� ������ �Ѿ��
            CheckResult();
        }
    }

    //private void CheckMemoryCnt()
    //{
    //    if (memoryCnt == 2 || memoryCnt == 3)
    //    {
    //        //�޸� ����1 �޼�
    //    }
    //    else if (memoryCnt == 4)
    //    {
    //        //�޸� ����2 �޼�
    //    }
    //    else
    //    {
    //        //�̴޼�
    //    }
    //}

    private void CheckResult()
    {
        if(keyCnt == 3 && memoryCnt == 2 || memoryCnt == 3 || memoryCnt == 4)
        {
            //�¿���
        }
        else if(keyCnt == 3 && memoryCnt == 5)
        {
            //������
        }
    }

}
