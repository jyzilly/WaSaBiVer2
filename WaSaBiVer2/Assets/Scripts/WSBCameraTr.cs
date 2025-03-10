using UnityEngine;
using UnityEngine.InputSystem;

public class WSBCameraTr : MonoBehaviour
{
    [SerializeField] float mouseSpeed = 8f;
    //private float mouseX;
    private float mouseY = 0f;

    private void Update()
    {
        // mouseX += Input.GetAxis("Mouse X") * mouseSpeed;

        mouseY += Input.GetAxis("Mouse Y") * mouseSpeed;
        mouseY = Mathf.Clamp(mouseY, -50f, 30f);
        this.transform.localEulerAngles = new Vector3(-mouseY, 0, 0);

    }
}

