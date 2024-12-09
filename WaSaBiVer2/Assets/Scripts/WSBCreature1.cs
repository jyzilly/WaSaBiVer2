using System.Collections;
using UnityEngine;
using UnityEngine.AI;
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



    public NavMeshAgent navMeshAgent;

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
    private int distance = 5;
    /*������� -------------------------------------*/


    /*ũ��ó1 �̵��� ���� ������ -----------*/
    private float moveSpeed = 2f;

    /*������� --------------------------*/

    Animator Cture1animator;
    CharacterController Cture1controller;
    

    //test��
    //[SerializeField] private GameObject Test1 = null;
    [SerializeField] private Button Testbt = null;


    /*�̵��Լ� �ڷ�ƾ*/
    private Coroutine moveOnCoroutine = null;


    private bool isMoving = false;




    private void Awake()
    {
        OriginCreature1Tr = Cture1.transform.position;

        Cture1animator = GetComponent<Animator>();
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

        Debug.Log("Cture1.position" + transform.position);

        //�÷��̾��� ��ó�� �̵��ϱ� 
        transform.position = Player.transform.position + (new Vector3(Random.insideUnitSphere.x, 0f, Random.insideUnitSphere.z) * distance);

        Debug.Log("�̵� �Ϸ� Cture1�� ��ġ" + transform.position);

        moveOnCoroutine = StartCoroutine(moveOn());
    }

    //�÷��̾� �ε�ġ�� �÷��̾��� hp �����Ѵ�. ȭ���� ���� ȿ���� ����. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Cture1animator.SetFloat("Blend", 1f, 0.5f, Time.deltaTime);
            //navMeshAgent.isStopped = true;
            //Player.Damage(damage);
            //Debug.Log("������� ����");
            Debug.Log("ũ��ó1���� Damage�Ծ��� ���� hp : " + Player.CurHp);
            //hpBar.UpdateHpBar(Player.MaxHp, Player.CurHp);
            //ȿ������ �ڵ带 ���⼭ �߰�
        }
    }


    //�÷��̾����� ���������� �̵� & �÷��̾��ϰ� ����ġ�� ���߰�
    public IEnumerator moveOn()
    {
        isMoving = true;

        while (true)
        {
            if (isMoving)
            {
                Vector3 moveDirection = (Player.transform.position - Cture1.transform.position).normalized;
                //Vector3 targetPosition = Vector3.MoveTowards(Cture1.transform.position, Player.transform.position, moveSpeed * Time.deltaTime);
                //Cture1.transform.position = targetPosition;
                Cture1.transform.Translate(moveDirection * moveSpeed  * Time.deltaTime);

                // ȸ���ϴ� �κ�. Point 1.
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * moveSpeed);

                //Vector3 velocity = moveDirection * moveSpeed * Time.deltaTime;
                //Cture1controller.Move(velocity);
                Cture1animator.SetFloat("Blend", 0.5f,0f,Time.deltaTime);
                //Cture1animator.speed = 2f;
            }
            else
            {
                Cture1animator.SetFloat("Blend", 0f);

            }

            yield return null;
        }
    }

    public void SetMoving(bool _isMoving)
    {
        isMoving = _isMoving;
    }

    public void StopmoveOnCoroutine()
    {
        //if (moveOnCoroutine != null)
        //{
        //    StopCoroutine(moveOnCoroutine);
        //    moveOnCoroutine = null;
        //    Debug.Log("moveOnCorourine been Stopped");
        //}
    }
}

