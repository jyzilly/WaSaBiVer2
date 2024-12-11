using UnityEngine;
using UnityEngine.Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

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


    public Vector3 _dir;
    private Vector3 OriginTr;



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

        OriginTr = CamTr.transform.position;


    }

    private void FixedUpdate()
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

        if(Input.GetKey(KeyCode.F))
        {
            animator.SetBool("Down", true);
            //CamTr.transform.position += _dir;
            
        }
        else
        {
            animator.SetBool("Down", false);
            //CamTr.transform.position = OriginTr;
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

            Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical");// + right * Input.GetAxisRaw("Horizontal");

            //if (moveDirection.magnitude > 0)
            //if (moveDirection.x > 0f || moveDirection.x < 0f)
            //{
            //    Quaternion targetRotation = Quaternion.LookRotation(moveDirection.normalized);
            //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothness);
            //}
            float axisH = 0f;
            if (Input.GetKey(KeyCode.A)) axisH = -1f *5;
            else if (Input.GetKey(KeyCode.D)) axisH = 1f * 5;
            transform.Rotate(transform.up, axisH);

            controller.SimpleMove(moveDirection.normalized * finalSpeed * Time.deltaTime);
            //controller.SimpleMove(moveDirection.normalized * finalSpeed); // FixedUpdate -> Update


            float percent = ((run) ? 1 : 0.75f) * moveDirection.magnitude;
            animator.SetFloat("Blend", percent, 0.25f, Time.deltaTime);

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
        GameObject.Find("Ch46_nonPBR").transform.Find("Blood").transform.gameObject.SetActive(true);

        if (curHp < 0f)
        {
            curHp = 0f;
            isDead = true;
            Debug.Log("Player is Dead");
            SceneManager.LoadScene("Wasabi 6");
        }
        MainGM.Invoke("OffItemPb", 1.5f);
    }

    public void Heal(float _heal)
    {
        if (isDead) return;

        curHp += _heal;
        if (curHp > maxHp) curHp = maxHp;
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
                    Debug.Log("ũ��ó2 �߰�");
                    break;

                }
                else if (Physics.Raycast(transform.position + transform.up, dir, tmpDist, Creature2_1))
                {
                    isCreature2_1 = true;
                    Debug.Log("ũ��ó2_1 �߰�");
                    break;

                }
                else if (Physics.Raycast(transform.position + transform.up, dir, tmpDist, Creature2_2))
                {
                    isCreature2_2 = true;
                    Debug.Log("ũ��ó2_2 �߰�");

                    break;
                }
                else
                {
                    isSpider = false;
                    isCreature1 = false;
                    isCreature2 = false;
                    isCreature2_1 = false;
                    isCreature2_2 = false;
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



    public void SetPosition(Vector3 _newPos)
    {
        controller.enabled = false;
        transform.position = _newPos;
        controller.enabled = true;
    }

}
