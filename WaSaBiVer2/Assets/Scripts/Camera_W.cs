using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera_W : MonoBehaviour
{
    // private float mouseX;
    private float mouseY = 0f;
    [SerializeField] float mouseSpeed = 8f;

    //public float ShakeAmount;
    //float ShakeTime;
    //Vector3 initialPosition;
    //Vector3 OriginalPos;

    private float shakeTime = 0.1f;

    private float shakeStrength = 0.1f;

    //public void VivrateForTime(float time)
    //{
    //    ShakeTime = time;
    //}

    private void Start()
    {
        // OriginalPos = Camera.main.transform.position;
    }

    private void Update()
    {
        // mouseX += Input.GetAxis("Mouse X") * mouseSpeed;

        mouseY += Input.GetAxis("Mouse Y") * mouseSpeed;
        mouseY = Mathf.Clamp(mouseY, -30f, 10f);
        this.transform.localEulerAngles = new Vector3(-mouseY, 0, 0);

        //initialPosition = transform.position;



        //if (ShakeTime > 0)
        //{
        //    transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;
        //    ShakeTime -= Time.deltaTime;
        //    Camera.main.transform.position = OriginalPos;
        //}
        //else
        //{
        //    ShakeTime = 0.0f;
        //    transform.position = initialPosition;
        //}

        //StartCoroutine(Shake());
    }

    public void ShakeCoroutine()
    {
        StartCoroutine(Shake());

    }

    IEnumerator Shake()
    {

        // 카메라의 원래 위치를 저장합니다.

        Vector3 originalPosition = transform.localPosition;



        float countTime = 0.0f;



        // 경과 시간이 지정된 지속 시간보다 작을 동안 반복됩니다.

        while (countTime < shakeTime)

        {

            // x와 y축으로 무작위 흔들림을 생성합니다.

            float x = Random.Range(-1f, 1f) * shakeStrength;

            float y = Random.Range(-1f, 1f) * shakeStrength;



            // 카메라의 위치를 흔들림 값으로 업데이트합니다.

            // z축 위치는 원래 위치를 유지합니다.

            transform.localPosition = new Vector3(x, y, originalPosition.z);



            // 경과 시간을 갱신합니다.

            countTime += Time.deltaTime;



            // 다음 프레임까지 대기합니다.

            yield return null;

        }



        // 흔들림이 끝난 후 카메라를 원래 위치로 되돌립니다.

        transform.localPosition = originalPosition;

    }

}
