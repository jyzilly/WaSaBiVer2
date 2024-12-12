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

        // ī�޶��� ���� ��ġ�� �����մϴ�.

        Vector3 originalPosition = transform.localPosition;



        float countTime = 0.0f;



        // ��� �ð��� ������ ���� �ð����� ���� ���� �ݺ��˴ϴ�.

        while (countTime < shakeTime)

        {

            // x�� y������ ������ ��鸲�� �����մϴ�.

            float x = Random.Range(-1f, 1f) * shakeStrength;

            float y = Random.Range(-1f, 1f) * shakeStrength;



            // ī�޶��� ��ġ�� ��鸲 ������ ������Ʈ�մϴ�.

            // z�� ��ġ�� ���� ��ġ�� �����մϴ�.

            transform.localPosition = new Vector3(x, y, originalPosition.z);



            // ��� �ð��� �����մϴ�.

            countTime += Time.deltaTime;



            // ���� �����ӱ��� ����մϴ�.

            yield return null;

        }



        // ��鸲�� ���� �� ī�޶� ���� ��ġ�� �ǵ����ϴ�.

        transform.localPosition = originalPosition;

    }

}
