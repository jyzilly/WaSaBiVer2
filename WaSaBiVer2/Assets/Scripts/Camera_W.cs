using UnityEngine;
using UnityEngine.InputSystem;

public class Camera_W : MonoBehaviour
{
 // private float mouseX;
  private float mouseY = 0f;
    [SerializeField] float mouseSpeed = 8f;

    public float ShakeAmount;
    float ShakeTime;
    Vector3 initialPosition;
    Vector3 OriginalPos;

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
    }

    
}
