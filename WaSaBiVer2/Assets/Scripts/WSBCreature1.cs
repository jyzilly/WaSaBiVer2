using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WSBCreature1 : MonoBehaviour
{
    //���࿡ ���� �������� count�ð��� == 3�̸� (���� �������� dount ���� �����;� �Ѵ�.)
    //�÷��̾��� ��ġ�� �޾ƿ���, �� ��ġ���� ������ ������ �̵��ϱ� 
    //�ξ࿡ ��豸���̶� Ontrigger�ϸ� �տ� ���� ���ϰ� �ϰ�
    //�÷��̾ �þ߰��� ������� �ٽ� ���� ��ġ�� ����(������ġ�� �����ϱ�)
    //���࿡ �÷��̾��� �ݶ��̴��� �浹���� �� �÷��̾����� damage�� �ش�
    //�÷��̾�hp -40
    //�÷��̾����� �Ѿư��� �Լ�/ �۵���� �����ϱ�

    //������ ���˾����� �۵��ϸ� �÷��̾� �ֺ� ��ġ�� �̵��ϰ� �÷��̾� �Ѿư��� �Լ��� �۵��ϸ鼭 �� ������ �տ� �ƹ��͵� ���� ��Ȳ.���࿡ ��豸�������� �� �տ� ����� �Ѵ�.


    /*�÷��̾� hp ���� ������ ---------------------------*/
    [SerializeField] private WSBPlayerController Player = null;
    [SerializeField] private WSBHpBar hpBar = null;

    [SerializeField, Range(0f, 50f)] private float damage = 30f;
    /*���� ���� ---------------------------------------*/


    /*ũ��ó1 ��ġ �̵����� ������ ------------------*/
    [SerializeField] private GameObject Cture1 = null;

    //�� ��ġ�� ����
    private Vector3 OriginCreature1Tr;

    ////�̵��� ��ġ�� �־�
    //private Transform Creature1Tr = null;

    //�÷��̾� ��ó ���� �� �ݰ�Ÿ�
    private int distance = 10;
    /*������� -------------------------------------*/


    /*ũ��ó1 �̵��� ���� ������ -----------*/
    private float moveSpeed = 5f;

    /*������� --------------------------*/



    //test��
    [SerializeField] private GameObject Test1 = null;
    [SerializeField] private Button Testbt = null;


    /*�̵��Լ� �ڷ�ƾ*/
    private Coroutine moveOnCoroutine = null;




    private void Awake()
    {
        //Player = GetComponent<PlayerController>();
        //Creature1Tr = GetComponent<Transform>();

        OriginCreature1Tr = Cture1.transform.position;
    }

    private void Start()
    {
        Testbt.onClick.AddListener(TrChanged);
    }

    private void Update()
    {

    }


    //���˾����� �۵��ϸ� ȣ���ϰ� ��
    public void TrChanged()
    {
        //������ ���Ǹ� �����ϰ�
        //OriginCreature1Tr = Creature1Tr.transform;

        //�÷��̾��� ��ó�� �̵��ϱ� 
        //Cture1.transform.position = Player.transform.position + (new Vector3(Random.insideUnitSphere.x, 0f, Random.insideUnitSphere.z) * distance);

        //test��
        Cture1.transform.position = Test1.transform.position + (new Vector3(Random.insideUnitSphere.x, 0f, Random.insideUnitSphere.z) * distance);
        Debug.Log("�̵� �Ϸ�");
        StartCoroutine(moveOn());
    }

    //�÷��̾� �ε�ġ�� �÷��̾��� hp �����Ѵ�. ȭ���� ���� ȿ���� ����. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player.Damage(damage);
            Debug.Log("������� ����");
            Debug.Log("ũ��ó1���� Damage�Ծ��� ���� hp : " + Player.CurHp);
            //hpBar.UpdateHpBar(Player.MaxHp, Player.CurHp);
            //ȿ������ �ڵ带 ���⼭ �߰�
        }
    }


    //�÷��̾����� ���������� �̵� & �÷��̾��ϰ� ����ġ�� ���߰�
    public IEnumerator moveOn()
    {
        Cture1.transform.position = Vector3.MoveTowards(Cture1.transform.position, Player.transform.position, moveSpeed * Time.deltaTime);
        yield return null;
    }

    public void StopmoveOnCoroutine()
    {
        if (moveOnCoroutine != null)
        {
            StopCoroutine(moveOnCoroutine);
            moveOnCoroutine = null;
            Debug.Log("moveOnCorourine been Stopped");
        }
    }
}

