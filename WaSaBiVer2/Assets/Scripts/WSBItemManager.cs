using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WSBItemManager : MonoBehaviour
{

    //������ ��ư �������� ǥ��
    [SerializeField] private Button flash;
    [SerializeField] private Button charm;
    [SerializeField] private Button fire;
    [SerializeField] private Button firecracker;
    [SerializeField] private Button blockball;

    //UIText�� ���� ǥ���ϱ�
    [SerializeField] private TextMeshProUGUI charmCnt;
    [SerializeField] private TextMeshProUGUI fireCnt;
    [SerializeField] private TextMeshProUGUI firecrackerCnt;
    [SerializeField] private TextMeshProUGUI blockballCnt;
    [SerializeField] private TextMeshProUGUI sausageCnt;


    //������ �̹��� 
    [SerializeField] private Image sausageImg;
    [SerializeField] private Image charmImg;
    [SerializeField] private Image fireImg;
    [SerializeField] private Image firecrackerImg;
    [SerializeField] private Image blockballImg;



    private int CharmCnt = 0;
    private int FireCnt = 0;
    private int FirecrackerCnt = 0;
    private int BlockballCnt = 0;
    private int SausageCnt = 0;

    public bool item1Able = false;
    public bool item2Able = false;
    public bool item3Able = false;
    public bool item4Able = false;
    public bool item5Able = false;


    public bool isMemory1 = false;
    public bool isMemory2 = false;
    public bool isMemory3 = false;
    public bool isMemory4 = false;


    public bool isLetter = false;

    // private bool PlayerGetLight; //true�� ��� ������on
    // private Light myLight; //light ������Ʈ�� ��� ����

    //������
    [SerializeField] private GameObject[] Items;
    [SerializeField] private Transform[] CreatItemTrs;
    //�޸����� & Ű 
    [SerializeField] private GameObject[] MemoryKeys;
    [SerializeField] private Transform[] CreatMKTr;
    //Ű �̹��� & �޸����� �̹���
    [SerializeField] private GameObject[] KeyAndMemoryImgs;


    public int keyCnt = 0;
    private int memoryCnt = 0;

    [SerializeField] private GameObject ExitDoor;

    private Vector3 DestroyTr;

    public AudioClip[] itemsounds;

    private void Awake()
    {
    }

    private void Start()
    {
        CreatItem();
        SetMK();
       
    }

    private void Update()
    {
        GetItem();
        SetItemImg();
    }

    private void GetItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject);

                bool getItem = false;
                //�±׷� �������� �ν��ϱ�. �����۸��� �±׸� �߰��ϱ�
                //����
                if (hit.transform.gameObject.tag == "charm")
                {
                    //if (Input.GetMouseButtonDown(0))
                    AudioClip itemsound = itemsounds[4];
                    GetComponent<AudioSource>().Stop();
                    GetComponent<AudioSource>().PlayOneShot(itemsound, 0.8f);
                    {
                        CharmCnt += 1;
                        getItem = true;
                        //Destroy(GameObject.FindGameObjectWithTag("charm"));                        
                    }
                }
                //��
                else if (hit.transform.gameObject.tag == "fire")
                {
                    //if (Input.GetMouseButtonDown(0))
                    AudioClip itemsound = itemsounds[4];
                    GetComponent<AudioSource>().Stop();
                    GetComponent<AudioSource>().PlayOneShot(itemsound, 0.8f);
                    {
                        FireCnt += 1;
                        getItem = true;
                        //Destroy(GameObject.FindGameObjectWithTag("fire"));

                    }
                }
                //����
                else if (hit.transform.gameObject.tag == "firecracker")
                {
                    //if (Input.GetMouseButtonDown(0))
                    AudioClip itemsound = itemsounds[4];
                    GetComponent<AudioSource>().Stop();
                    GetComponent<AudioSource>().PlayOneShot(itemsound, 0.8f);
                    {
                        FirecrackerCnt += 1;
                        getItem = true;
                        //Destroy(GameObject.FindGameObjectWithTag("firecracker"));

                    }
                }
                //��豸��
                else if (hit.transform.gameObject.tag == "blockball")
                {
                    //if (Input.GetMouseButtonDown(0))
                    AudioClip itemsound = itemsounds[4];
                    GetComponent<AudioSource>().Stop();
                    GetComponent<AudioSource>().PlayOneShot(itemsound, 0.8f);
                    {
                        BlockballCnt += 1;
                        getItem = true;
                        //Destroy(GameObject.FindGameObjectWithTag("blockball"));

                    }
                }
                //�Ҽ���
                else if (hit.transform.gameObject.tag == "Sausage")
                {
                    AudioClip itemsound = itemsounds[4];
                    GetComponent<AudioSource>().Stop();
                    GetComponent<AudioSource>().PlayOneShot(itemsound, 0.8f);
                    {
                        SausageCnt += 1;
                        getItem = true;

                    }
                }
                else if (hit.transform.gameObject.tag == "Key")
                {
                    AudioClip itemsound = itemsounds[5];
                    GetComponent<AudioSource>().Stop();
                    GetComponent<AudioSource>().PlayOneShot(itemsound, 0.8f);
                    //key1�� �̹����� Ȱ��ȭ�ϱ�
                    //
                    keyCnt += 1;
                    getItem = true;
                }
                else if (hit.transform.gameObject.tag == "Memory")
                {
                    AudioClip itemsound = itemsounds[5];
                    GetComponent<AudioSource>().Stop();
                    GetComponent<AudioSource>().PlayOneShot(itemsound, 0.8f);
                    memoryCnt += 1;
                    getItem = true;
                    isMemory1 = true;
                    
                }
                else if (hit.transform.gameObject.tag == "Memory2")
                {
                    AudioClip itemsound = itemsounds[5];
                    GetComponent<AudioSource>().Stop();
                    GetComponent<AudioSource>().PlayOneShot(itemsound, 0.8f);
                    memoryCnt += 1;
                    getItem = true;
                    isMemory2 = true;

                }
                else if (hit.transform.gameObject.tag == "Memory3")
                {
                    AudioClip itemsound = itemsounds[5];
                    GetComponent<AudioSource>().Stop();
                    GetComponent<AudioSource>().PlayOneShot(itemsound, 0.8f);
                    memoryCnt += 1;
                    getItem = true;
                    isMemory3 = true;

                }
                else if (hit.transform.gameObject.tag == "Memory4")
                {
                    AudioClip itemsound = itemsounds[5];
                    GetComponent<AudioSource>().Stop();
                    GetComponent<AudioSource>().PlayOneShot(itemsound, 0.8f);
                    memoryCnt += 1;
                    getItem = true;
                    isMemory4 = true;

                }
                else if(hit.transform.gameObject.tag == "ExitDoor")
                {
                    if(memoryCnt > 2)
                    {
                        SceneManager.LoadScene("Wasabi 4");
                    }
                    else if(memoryCnt <=2)
                    {
                        SceneManager.LoadScene("Wasabi 5");
                    }
                }
                else if (hit.transform.gameObject.tag == "FlashLight")
                {
                    FlashLight.GetLight();
                    getItem = true;
                }

                else if (hit.transform.gameObject.tag == "Letter")
                {
                    Debug.Log(isLetter);
                    isLetter = true;
                }


                if (getItem == true)
                {
                    UpdateItemCnt();
                    Destroy(hit.transform.gameObject);

                    DestroyTr = hit.transform.position;
                    Invoke("ReCreatItem", 30f);
                }
                Debug.Log("Key : " + keyCnt);
                Debug.Log("Momory : " + memoryCnt);

            }
        }


        CheckKeyCnt();
    }

    private void ReCreatItem()
    {
        GameObject randomItem = Items[Random.Range(0, Items.Length)];
        Instantiate(randomItem, DestroyTr, Quaternion.identity);
    }
    private void UpdateItemCnt()
    {
        charmCnt.text = CharmCnt.ToString();
        fireCnt.text = FireCnt.ToString();
        firecrackerCnt.text = FirecrackerCnt.ToString();
        blockballCnt.text = BlockballCnt.ToString();
        sausageCnt.text = SausageCnt.ToString();

    }

    public void PressedFireButton()
    {
        if (FireCnt > 0)
        {
            AudioClip Fire = itemsounds[0];
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(Fire, 0.8f);
            --FireCnt;
            UpdateItemCnt();
            item1Able = true;
        }

        //else if(FireCnt > 0 && GameObject.Find("itemCollider"))
        //{
        //    --FireCnt;
        //    UpdateItemCnt();
        //}

    }

    public void PressedCharmButton()
    {
        if (CharmCnt > 0)
        {
            --CharmCnt;
            UpdateItemCnt();

            item2Able = true;

        }
    }

    public void PressedFirecrackerButton()
    {
        if (FirecrackerCnt > 0)
        {
            AudioClip Firecracker = itemsounds[1];
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(Firecracker, 0.3f);
            GetComponent<AudioSource>().Stop();
            --FirecrackerCnt;
            UpdateItemCnt();
            item3Able = true;

        }
    }

    public void PressedBlockballButton()
    {
        if (BlockballCnt > 0)
        {
            AudioClip Blockball = itemsounds[2];
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(Blockball, 0.8f);
            --BlockballCnt;
            UpdateItemCnt();
            item4Able = true;

        }
    }

    public void PressedSausageButton()
    {
        if(SausageCnt > 0)
        {
            AudioClip Sausage = itemsounds[3];
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(Sausage, 0.8f);
            --SausageCnt;
            UpdateItemCnt();
            item5Able = true;
        }
    }

    private void SetItemImg()
    {
        if (CharmCnt == 0)
        {
            charmImg.enabled = false;
        }
        else
        {
            charmImg.enabled = true;
        }
        if (FireCnt == 0)
        {
            fireImg.enabled = false;
        }
        else
        {
            fireImg.enabled = true;
        }
        if (FirecrackerCnt == 0)
        {
            firecrackerImg.enabled = false;
        }
        else
        {
            firecrackerImg.enabled = true;
        }
        if (BlockballCnt == 0)
        {
            blockballImg.enabled = false;
        }
        else
        {
            blockballImg.enabled = true;
        }
        if (SausageCnt == 0)
        {
            sausageImg.enabled = false;
        }
        else
        {
            sausageImg.enabled = true;
        }


        if (keyCnt == 1)
        {
            KeyAndMemoryImgs[0].SetActive(true);
        }
        if (keyCnt == 2)
        {
            KeyAndMemoryImgs[1].SetActive(true);
        }
        if (keyCnt == 3)
        {
            KeyAndMemoryImgs[2].SetActive(true);
        }
        if (memoryCnt == 1)
        {
            KeyAndMemoryImgs[3].SetActive(true);
        }
        if (memoryCnt == 2)
        {
            KeyAndMemoryImgs[4].SetActive(true);
        }
        if (memoryCnt == 3)
        {
            KeyAndMemoryImgs[5].SetActive(true);
        }
        if (memoryCnt == 4)
        {
            KeyAndMemoryImgs[6].SetActive(true);
        }
    }

    private void CreatItem()
    {
        for (int i = 0; i < 7; ++i)
        {

            GameObject randomItem = Items[Random.Range(0, Items.Length)];
            //GameObject randomItem = Items[2];
            Instantiate(randomItem, CreatItemTrs[i].position, Quaternion.identity);

        }
        Debug.Log("������ �����Ϸ�");
    }


    private void SetMK()
    {
        for (int i = 0; i < 7; ++i)
        {
            //i��° ��ġ���� i�� Ű or �޸� ���� �����ϱ�
            Instantiate(MemoryKeys[i], CreatMKTr[i].position, Quaternion.identity);
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

    private void CheckResult()
    {
        if (keyCnt == 3 && memoryCnt >= 3)
        {
            //�¿���
            ExitDoor.SetActive(true);
        }
        else if (keyCnt == 3 && memoryCnt == 5)
        {
            //������
            ExitDoor.SetActive(true);
        }
        else if (keyCnt == 3 && memoryCnt <= 2)
        {
            ExitDoor.SetActive(true);
        }

    }

}

