using UnityEngine;
using UnityEngine.Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

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
    [SerializeField] private LayerMask Creature1;

    //������ �þ߰� ǥ�ð���
    //[SerializeField, Range(0.1f, 1f)] private float angle;
    //�� ��������Ʈ
    [SerializeField] private List<CastInfo> lineList;
    //��ġ������ ����
    //[SerializeField] private Vector3 offset;
    /*������� --------------------------------------*/

    [SerializeField] private WSBCreature1 Cture1;


    private void Awake()
    {

    }

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        controller = this.GetComponent<CharacterController>();

        //hp �ʱ�ȭ��Ű�� ����
        curHp = maxHp;
        isDead = false;

        /*�þ߰� ���� �ڵ��*/
        lineList = new();

        StartCoroutine(DrawRayLine());
        StartCoroutine(CheckTarget());
        /*�������*/

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

        //���࿡ �þ߰��� ũ��ó1 ������ ũ��ó1 �̵��ϴ� �Լ� ȣ��

    }

    //�÷��̾� �̵��ϴ� �Լ�
    private void InputMovement()
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

    //ũ��ó�� �Ź��� ������ �� ȣ���ϴ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "")
        {

        }
    }

    //�÷��̾� Hp ���� �Լ�. ������ �Ծ��� �� hp�����ϴ� �Լ�
    public void Damage(float _dmg)
    {
        curHp -= _dmg;
        if (curHp < 0f)
        {
            curHp = 0f;
            isDead = true;
            Debug.Log("Player is Dead");
        }
    }

    //�÷��̾ �Ҽ��� ������ hpȸ���ϴ� �Լ�
    public void Heal(float _heal)
    {
        if (isDead) return;

        curHp += _heal;
        if (curHp > maxHp) curHp = maxHp;
    }


    /*�þ߰� �Լ���*/
    private IEnumerator CheckTarget()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.1f);
        while (true)
        {

            float tmpAngle = viewAngle * 0.5f;
            float tmpDist = 3f;
            Vector3 playerRot = transform.rotation.eulerAngles;
            int rayCount = Mathf.RoundToInt(viewAngle);
            bool isCatch = false;
            for (int i = 0; i < rayCount; ++i)
            {
                Vector3 dir = new Vector3(Mathf.Cos(((tmpAngle - i) + 90f - playerRot.y) * Mathf.Deg2Rad), 0.0f, Mathf.Sin(((tmpAngle - i) + 90f - playerRot.y) * Mathf.Deg2Rad));
                if (Physics.Raycast(transform.position + transform.up, dir, tmpDist, Creature1))
                {
                    //Debug.Log("Hit");
                    isCatch = true;
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





}

