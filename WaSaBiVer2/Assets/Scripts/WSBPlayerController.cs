using UnityEngine;
using UnityEngine.Animations;
using System.Collections;
using System.Collections.Generic;

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
    /*이동함수에 필요하는 변수들 --------------------*/
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float smoothness = 10f;
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
    [SerializeField] private LayerMask creature1;

    //선으로 시야각 표시각도
    //[SerializeField, Range(0.1f, 1f)] private float angle;
    //선 정보리스트
    [SerializeField] private List<CastInfo> lineList;
    //위치정보용 벡터
    //[SerializeField] private Vector3 offset;
    /*여기까지 --------------------------------------*/

    private WSBCreature1 Cture1;



    private void Awake()
    {

    }

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        controller = this.GetComponent<CharacterController>();

        //hp 초기화시키는 설정
        curHp = maxHp;
        isDead = false;

        /*시야각 관련 코드들*/
        lineList = new();

        StartCoroutine(DrawRayLine());
        StartCoroutine(CheckTarget());
        /*여기까지*/

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

        //만약에 시야각에 크리처1 들어오면 크리처1 이동하는 함수 호출

    }

    //플레이어 이동하는 함수
    private void InputMovement()
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

    //크리처나 거미줄 만났을 때 호출하는 함수
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "")
        {

        }
    }

    //플레이어 Hp 관한 함수. 데미지 입었을 때 hp감소하는 함수
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

    //플레이어가 소세지 먹으면 hp회복하는 함수
    public void Heal(float _heal)
    {
        if (isDead) return;

        curHp += _heal;
        if (curHp > maxHp) curHp = maxHp;
    }


    /*시야각 함수들*/
    private IEnumerator CheckTarget()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.1f);
        while (true)
        {
            //원 범위 내 대상을 검출
            Collider[] cols = Physics.OverlapSphere(transform.position, viewRange, creature1);
            foreach (var i in cols)
            {
                Vector3 direction = (i.transform.position - transform.position).normalized;
                Debug.Log("target in range");

                //대상과의 각도가 설정한 각도 이내에 있는지 확인
                //viewAngle은 전체 각도이라서 0.5 곱함
                if (Vector3.Angle(transform.forward, direction) < (viewAngle * 0.5f))
                {
                    Debug.Log("target in angle");
                    //크리처1 시야 안에 들어와서 크리처1의 이동함수 끝기
                    //Cture1.StopmoveOnCoroutine();

                }
            }

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


}

