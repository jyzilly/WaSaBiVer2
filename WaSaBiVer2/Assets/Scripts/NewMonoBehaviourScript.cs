using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    private float moveSpeed = 10f;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position +
                (transform.forward * -1f * moveSpeed * Time.deltaTime);
        }

        float h = Input.GetAxis("Horizontal"); // [수평] -1 왼쪽 0 가운데 1 오른쪽
        float v = Input.GetAxis("Vertical"); // [수직]
        Vector3 dir = new Vector3(h, 0f, v);

        // 대각선 이동 속도가 빠름 ( 루트2라 )
        transform.position = transform.position + (dir.normalized * moveSpeed * Time.deltaTime);

    }
}
