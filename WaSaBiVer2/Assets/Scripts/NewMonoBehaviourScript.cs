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

        float h = Input.GetAxis("Horizontal"); // [����] -1 ���� 0 ��� 1 ������
        float v = Input.GetAxis("Vertical"); // [����]
        Vector3 dir = new Vector3(h, 0f, v);

        // �밢�� �̵� �ӵ��� ���� ( ��Ʈ2�� )
        transform.position = transform.position + (dir.normalized * moveSpeed * Time.deltaTime);

    }
}
