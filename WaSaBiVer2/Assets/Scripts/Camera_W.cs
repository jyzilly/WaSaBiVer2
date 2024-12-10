using UnityEngine;
using UnityEngine.InputSystem;

public class Camera_W : MonoBehaviour
{
 // private float mouseX;
  private float mouseY = 0f;
    [SerializeField] float mouseSpeed = 8f;

    private void Update()
    {
       // mouseX += Input.GetAxis("Mouse X") * mouseSpeed;

        mouseY += Input.GetAxis("Mouse Y") * mouseSpeed;
        mouseY = Mathf.Clamp(mouseY, -30f, 10f);
        this.transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
    }
}
