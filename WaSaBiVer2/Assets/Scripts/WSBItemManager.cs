using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class WSBItemManager : MonoBehaviour
{
    //������ �����ϴ� �Լ�
    //���࿡ ���콺�� �������� Ŭ���ϸ� �ش� �������� �̸���  ã�ư��� �ش� �������� ������ +1

    //������. �׻� �޼տ� ��� �ְ�. on/off InputŰ�� �߰��ؼ� ���� ���� Ű�� ����� �߰� 
    //����. use�� bool�� �����ؼ� ���࿡ Ŭ�������� ���� -1, 2�ʵ��� ����Ÿ��ȿ���� Ȱ��ȭ��Ŵ, �̰� ����ߴٴ� �� �Ҿƹ������� �����ϱ� .
    //if������ ���࿡ 0���̻��̸� Ŭ�������� ���� -1,���� ����ߴ� �� �Ҿƹ������� ����,0���̸� ������ư�� ��Ȱ��ȭ��Ų��.  
    //��. if������ ���࿡ 0�� �̻��̸� ��ư Ȱ��ȭ ��Ű�� Ŭ������ �� ���� -1,�� ȿ���� 2�ʵ��� Ȱ��ȭ��Ŵ, �Ź��� distroy�Լ��� ����ؼ� �ı���Ű��. 0���̸� ��ư ��Ȱ��ȭ�� ��Ŵ.  
    //����. if������ ���࿡ 0�� �̻��̸� ��ư Ȱ��ȭ ��Ű�� Ŭ������ �� ���� -1,�Ҹ����, ����ߴٴ� �� ũ��ó2���� ����. 0���̸� ��ư ��Ȱ��ȭ�� ��Ŵ.
    //��豸��. if������ ���࿡ 0�� �̻��̸� ��ư Ȱ��ȭ ��Ű�� Ŭ������ �� ���� -1 , ������ �÷��̾� �ٶ󺸴� �ü��� �����(�ƴϸ� ��ǥ�� �÷��̾� �տ� ���ڷ� �����ϱ�), ���޾���


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

    private int CharmCnt = 0;
    private int FireCnt = 0;
    private int FirecrackerCnt = 0;
    private int BlockballCnt = 0;


    private void Start()
    {
        fire.onClick.AddListener(PressedFireButton);
        fire.onClick.AddListener(PressedCharm);
        fire.onClick.AddListener(PressedFirecracker);
        fire.onClick.AddListener(PressedBlockball);
    }

    private void Update()
    {
        GetItem();
    }

    private void GetItem()
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
                CharmCnt += 1;
                getItem = true;
            }
            //��
            else if (hit.transform.gameObject.tag == "fire")
            {
                FireCnt += 1;
                getItem = true;
            }
            //����
            else if (hit.transform.gameObject.tag == "firecracker")
            {
                FirecrackerCnt += 1;
                getItem = true;
            }
            //��豸��
            else if (hit.transform.gameObject.tag == "blockball")
            {
                BlockballCnt += 1;
                getItem = true;
            }
            if (getItem == true)
            {
                UpdateItemCnt();
            }

        }
    }


    private void UpdateItemCnt()
    {
        charmCnt.text = CharmCnt.ToString();
        fireCnt.text = FireCnt.ToString();
        firecrackerCnt.text = FirecrackerCnt.ToString();
        blockballCnt.text = BlockballCnt.ToString();
    }

    private void PressedFireButton()
    {
        if (FireCnt > 0)
        {
            --FireCnt;
            UpdateItemCnt();
        }
    }

    private void PressedCharm()
    {
        if (CharmCnt > 0)
        {
            --CharmCnt;
            UpdateItemCnt();
        }
    }

    private void PressedFirecracker()
    {
        if (FirecrackerCnt > 0)
        {
            --FirecrackerCnt;
            UpdateItemCnt();
        }
    }

    private void PressedBlockball()
    {
        if (BlockballCnt > 0)
        {
            --BlockballCnt;
            UpdateItemCnt();
        }
    }







}

