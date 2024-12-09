using UnityEngine;
using UnityEngine.Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct CastInfo
{
    public bool Hit;  // �¾Ҵ��� Ȯ��
    public Vector3 Point;  // �¾Ҵٸ�, ���� ��ġ�� ���, �ƴϸ� range�Ÿ�
    public float Distance;  // �Ÿ�
    public float Angle; //����
}
public class WSBPlayerController : MonoBehaviour
{
    // [SerializeField] float speed = 5f;
    //[SerializeField] float mouseSpeed = 8f;
    //private float gravity;
    //private CharacterController controller;
    //private Vector3 mov;


    //private float mouseX;
    //private float mouseY = 0f;

    private WSBMainGameController MainGM;

    public bool isMovable = true;

    /*�̵��Լ��� �ʿ��ϴ� ������ --------------------*/
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float smoothness = 5f;
    [SerializeField] private float finalSpeed;
    [SerializeField] private bool run;
    [SerializeField] public Transform CamTr;

    Animator animator;
    CharacterController controller;
    /*������� -------------------------------------*/


    /*�÷��̾� HP���� ������ ------------------------*/
    [SerializeField] private float maxHp = 100f;

    private float curHp = 100f;
    private bool isDead = false;

    public float MaxHp { get { return maxHp; } }
    public float CurHp { get { return curHp; } }
    public bool IsDead { get { return isDead; } }
    /*������� -------------------------------------*/


    /*�þ߰� ���� ������ -----------------------------*/
    //�þ߹���
    [SerializeField, Range(0f, 30f)] private float viewRange;
    //�þ߰���
    [SerializeField, Range(0f, 360f)] private float viewAngle;

    //ũ��ó1 ���̾�� �����ؼ� -> Ÿ��
    [SerializeField] private LayerMask Spider;
    [SerializeField] private LayerMask Creature1;
    [SerializeField] private LayerMask Creature2;
    [SerializeField] private LayerMask Creature2_1;
    [SerializeField] private LayerMask Creature2_2;

    

    //������ �þ߰� ǥ�ð���
    //[SerializeField, Range(0.1f, 1f)] private float angle;
    //�� ��������Ʈ
    [SerializeField] private List<CastInfo> lineList;
    //��ġ������ ����
    //[SerializeField] private Vector3 offset;
    /*������� --------------------------------------*/

    [SerializeField] private WSBCreature1 Cture1;
    private WSBMainGameController mainGameManager;

    public bool isSpider = false;
    public bool isCreature1 = false;
    public bool isCreature2 = false;
    public bool isCreature2_1 = false;
    public bool isCreature2_2 = false;

    //public bool isMovable = false;

    //���� ������ ��
    [SerializeField] public GameObject blockball = null;
    private Vector3 dir;
    private float rx = 0f;
    private float ry = 0f;
    public float rotateSpeed ;
    public Vector3 throwPower;
    public Vector3 _dir;




    private void Awake()
    {

    }

    private void Start()
    {
        mainGameManager = GameObject.Find("GameManager").GetComponent<WSBMainGameController>();
        Cture1 = GameObject.Find("BookHeadMonster").GetComponent<WSBCreature1>();
        animator = this.GetComponent<Animator>();
        controller = this.GetComponent<CharacterController>();
        MainGM = GetComponent<WSBMainGameController>();

        //hp �ʱ�ȭ��Ű�� ����
        curHp = maxHp;
        isDead = false;

        /*�þ߰� ���� �ڵ��*/
        lineList = new();

        StartCoroutine(DrawRayLine());
        StartCoroutine(CheckTarget());
        /*�������*/

        //controller = GetComponent<CharacterController>();
        //mov = Vector3.zero;
        // gravity = 10f;

        //���� ������ ��
        rx = CamTr.transform.eulerAngles.x;
        ry = CamTr.transform.eulerAngles.y;

 
    }

    private void Update()
    {



        //�޸��� ����Ű
        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }

        //�̵��ϴ�  �Լ�ȣ��
        InputMovement();

        //�ٱ� ����Ű
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        //mouseX += Input.GetAxis("Mouse X") * mouseSpeed;

        //mouseY += Input.GetAxis("Mouse Y") * mouseSpeed;
        //mouseY = Mathf.Clamp(mouseY, -50f, 30f);
        //this.transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0);

        //if (controller.isGrounded)
        //{
        //    mov = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //    mov = controller.transform.TransformDirection(mov);
        //}
        //else
        //{
        //    mov.y -= gravity * Time.deltaTime;
        //}

        //controller.Move(mov * Time.deltaTime * speed);

        //���࿡ �þ߰��� ũ��ó1 ������ ũ��ó1 �̵��ϴ� �Լ� ȣ��

    }

    //�÷��̾� �̵��ϴ� �Լ�
    private void InputMovement()
    {
        if (isMovable)
        {
            //run true�̸� run�ӵ��� �ٲٱ�
            finalSpeed = (run) ? runSpeed : moveSpeed;

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");

            if (moveDirection.magnitude > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothness);
            }

            controller.Move(moveDirection.normalized * finalSpeed * Time.deltaTime);


            float percent = ((run) ? 1 : 0.5f) * moveDirection.magnitude;
            animator.SetFloat("Blend", percent, 0.1f, Time.deltaTime);

            CamTr = transform;
        }
        else
        {
            return;
        }
    }

 

    //�÷��̾� Hp ���� �Լ�. ������ �Ծ��� �� hp�����ϴ� �Լ�
    public void Damage(float _dmg)
    {
        //MainGM.Prefabs[0].SetActive(true);
        //GameObject.Find("Ch46_nonPBR").transform.Find("Blood").transform.gameObject.SetActive(true);
        //Debug.Log("����");
        curHp -= _dmg;
        if (curHp < 0f)
        {
            curHp = 0f;
            isDead = true;
            Debug.Log("Player is Dead");
            SceneManager.LoadScene("Wasabi 6");
        }
        //MainGM.Invoke("OffItemPb", 1.5f);
    }


    /*�þ߰� �Լ���*/
    public IEnumerator CheckTarget()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.1f);
        while (true)
        {

            float tmpAngle = viewAngle * 0.5f;
            float tmpDist = 3f;
            Vector3 playerRot = transform.rotation.eulerAngles;
            int rayCount = Mathf.RoundToInt(viewAngle);

            
            bool isCatch = false;
             

           ;

            for (int i = 0; i < rayCount; ++i)
            {
                Vector3 dir = new Vector3(Mathf.Cos(((tmpAngle - i) + 90f - playerRot.y) * Mathf.Deg2Rad), 0.0f, Mathf.Sin(((tmpAngle - i) + 90f - playerRot.y) * Mathf.Deg2Rad));
                if (Physics.Raycast(transform.position + transform.up, dir, tmpDist, Spider))
                {
                    isSpider = true;
                    //Debug.Log("this is Spider");


                }
                else if (Physics.Raycast(transform.position + transform.up, dir, tmpDist, Creature1))
                {
                    Debug.Log("Hit");
                    isCatch = true;
                    isCreature1 = true;

                    break;

                }
                else if (Physics.Raycast(transform.position + transform.up, dir, tmpDist, Creature2))
                {
                    isCreature2 = true;
                    break;

                }
                else if (Physics.Raycast(transform.position + transform.up, dir, tmpDist, Creature2_1))
                {
                    isCreature2_1 = true;

                    break;

                }
                else if (Physics.Raycast(transform.position + transform.up, dir, tmpDist, Creature2_2))
                {
                    isCreature2_2 = true;


                    break;
                }
                
            }

            if (isCatch) Cture1.SetMoving(false);
            else Cture1.SetMoving(true);

            yield return null;
        }
    }

    private IEnumerator DrawRayLine()
    {
        while (true)
        {

            lineList.Clear();

            float tmpAngle = viewAngle * 0.5f;
            float tmpDist = 3f;
            Vector3 playerRot = transform.rotation.eulerAngles;
            int rayCount = Mathf.RoundToInt(viewAngle);
            for (int i = 0; i < rayCount; ++i)
            {
                
                Vector3 dir = new Vector3(Mathf.Cos(((tmpAngle - i) + 90f - playerRot.y) * Mathf.Deg2Rad), 0.0f, Mathf.Sin(((tmpAngle - i) + 90f - playerRot.y) * Mathf.Deg2Rad));
                Debug.DrawLine(transform.position, transform.position + (dir * tmpDist), Color.green);
            }

            yield return null;

        }
    }

    



    /*�������*/

    //public void CallBlockball()
    //{
    //    //���� ������ ��
    //    blockball.transform.position = 
    //    _dir = transform.TransformDirection(throwPower);
    //    Debug.Log("_dir ��ǥ : " + _dir);
    //    blockball.GetComponent<Rigidbody>().useGravity = true;
    //    blockball.GetComponent<Rigidbody>().AddForce(-dir, ForceMode.Impulse);
    //    Debug.Log("blockball ��ġ" + blockball.transform.position);

    //}



}
