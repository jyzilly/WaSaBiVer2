using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WSBCreature1 : MonoBehaviour
{
    //만약에 눈알 아이템의 count시간이 == 3이면 (눈알 아이템의 dount 값이 가져와야 한다.)
    //플레이어의 위치를 받아오고, 그 위치보다 떨이진 곳으로 이동하기 
    //민약에 결계구슬이랑 Ontrigger하면 앞에 가지 못하게 하고
    //플레이어가 시야각에 사라지면 다시 원래 위치로 복귀(원래위치를 저장하기)
    //만약에 플레이어의 콜라이더와 충돌했을 때 플레이어한테 damage를 준다
    //플레이어hp -40
    //플레이어향해 쫓아가는 함수/ 작동방식 생각하기

    //순서는 눈알아이템 작동하면 플레이어 주변 위치로 이동하고 플레이어 쫓아가는 함수를 작동하면서 단 조건이 앞에 아무것도 없는 상황.만약에 결계구슬있으면 그 앞에 멈춰야 한다.



    public NavMeshAgent navMeshAgent;

    /*플레이어 hp 관련 변수들 ---------------------------*/
    [SerializeField] private WSBPlayerController Player = null;
    [SerializeField] private WSBHpBar hpBar = null;

    [SerializeField, Range(0f, 50f)] private float damage = 30f;
    /*여기 까지 ---------------------------------------*/


    /*크리처1 위치 이동관한 변수들 ------------------*/
    [SerializeField] private GameObject Cture1 = null;

    //원 위치를 저장
    private Vector3 OriginCreature1Tr;

    ////이동할 위치를 넣어
    //private Transform Creature1Tr = null;

    //플레이어 근처 갔을 때 반경거리
    private int distance = 5;
    /*여기까지 -------------------------------------*/


    /*크리처1 이동에 관한 변수들 -----------*/
    private float moveSpeed = 2f;

    /*여기까지 --------------------------*/

    Animator Cture1animator;
    CharacterController Cture1controller;
    

    //test용
    //[SerializeField] private GameObject Test1 = null;
    [SerializeField] private Button Testbt = null;


    /*이동함수 코로틴*/
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


    //눈알아이템 작동하면 호출하게 함
    public void TrChanged()
    {
        //원래의 위피를 저장하고
        //OriginCreature1Tr = Creature1Tr.transform;

        Debug.Log("Cture1.position" + transform.position);

        //플레이어의 근처로 이동하기 
        transform.position = Player.transform.position + (new Vector3(Random.insideUnitSphere.x, 0f, Random.insideUnitSphere.z) * distance);

        Debug.Log("이동 완료 Cture1의 위치" + transform.position);

        moveOnCoroutine = StartCoroutine(moveOn());
    }

    //플레이어 부딪치면 플레이어의 hp 감소한다. 화면이 붉은 효과를 낸다. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Cture1animator.SetFloat("Blend", 1f, 0.5f, Time.deltaTime);
            //navMeshAgent.isStopped = true;
            //Player.Damage(damage);
            //Debug.Log("여기까지 왔음");
            Debug.Log("크리처1한테 Damage입었다 현재 hp : " + Player.CurHp);
            //hpBar.UpdateHpBar(Player.MaxHp, Player.CurHp);
            //효과내는 코드를 여기서 추가
        }
    }


    //플레이어향해 지속적으로 이동 & 플레이어하고 마주치면 멈추고
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

                // 회전하는 부분. Point 1.
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

