using UnityEngine;
using UnityEngine.Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct CastInfo
{
    public bool Hit;  // 맞았는지 확인
    public Vector3 Point;  // 맞았다면, 맞은 위치를 출력, 아니면 range거리
    public float Distance;  // 거리
    public float Angle; //각도
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

    /*이동함수에 필요하는 변수들 --------------------*/
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float smoothness = 5f;
    [SerializeField] private float finalSpeed;
    [SerializeField] private bool run;
    [SerializeField] public Transform CamTr;

    Animator animator;
    CharacterController controller;
    /*여기까지 -------------------------------------*/


    /*플레이어 HP관련 변수들 ------------------------*/
    [SerializeField] private float maxHp = 100f;

    private float curHp = 100f;
    private bool isDead = false;

    public float MaxHp { get { return maxHp; } }
    public float CurHp { get { return curHp; } }
    public bool IsDead { get { return isDead; } }
    /*여기까지 -------------------------------------*/


    /*시야각 관련 변수들 -----------------------------*/
    //시야범위
    [SerializeField, Range(0f, 30f)] private float viewRange;
    //시야각도
    [SerializeField, Range(0f, 360f)] private float viewAngle;

    //크리처1 레이어로 설정해서 -> 타겟
    [SerializeField] private LayerMask Spider;
    [SerializeField] private LayerMask Creature1;
    [SerializeField] private LayerMask Creature2;
    [SerializeField] private LayerMask Creature2_1;
    [SerializeField] private LayerMask Creature2_2;

    

    //선으로 시야각 표시각도
    //[SerializeField, Range(0.1f, 1f)] private float angle;
    //선 정보리스트
    [SerializeField] private List<CastInfo> lineList;
    //위치정보용 벡터
    //[SerializeField] private Vector3 offset;
    /*여기까지 --------------------------------------*/

    [SerializeField] private WSBCreature1 Cture1;
    private WSBMainGameController mainGameManager;

    public bool isSpider = false;
    public bool isCreature1 = false;
    public bool isCreature2 = false;
    public bool isCreature2_1 = false;
    public bool isCreature2_2 = false;

    //public bool isMovable = false;

    //구슬 던지는 것
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

        //hp 초기화시키는 설정
        curHp = maxHp;
        isDead = false;

        /*시야각 관련 코드들*/
        lineList = new();

        StartCoroutine(DrawRayLine());
        StartCoroutine(CheckTarget());
        /*여기까지*/

        //controller = GetComponent<CharacterController>();
        //mov = Vector3.zero;
        // gravity = 10f;

        //구슬 던지는 것
        rx = CamTr.transform.eulerAngles.x;
        ry = CamTr.transform.eulerAngles.y;

 
    }

    private void Update()
    {



        //달리기 조작키
        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }

        //이동하는  함수호출
        InputMovement();

        //뛰기 조작키
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

        //만약에 시야각에 크리처1 들어오면 크리처1 이동하는 함수 호출

    }

    //플레이어 이동하는 함수
    private void InputMovement()
    {
        if (isMovable)
        {
            //run true이면 run속도로 바꾸기
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

 

    //플레이어 Hp 관한 함수. 데미지 입었을 때 hp감소하는 함수
    public void Damage(float _dmg)
    {
        //MainGM.Prefabs[0].SetActive(true);
        //GameObject.Find("Ch46_nonPBR").transform.Find("Blood").transform.gameObject.SetActive(true);
        //Debug.Log("들어옴");
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


    /*시야각 함수들*/
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

    



    /*여기까지*/

    //public void CallBlockball()
    //{
    //    //구슬 던지는 것
    //    blockball.transform.position = 
    //    _dir = transform.TransformDirection(throwPower);
    //    Debug.Log("_dir 좌표 : " + _dir);
    //    blockball.GetComponent<Rigidbody>().useGravity = true;
    //    blockball.GetComponent<Rigidbody>().AddForce(-dir, ForceMode.Impulse);
    //    Debug.Log("blockball 위치" + blockball.transform.position);

    //}



}
