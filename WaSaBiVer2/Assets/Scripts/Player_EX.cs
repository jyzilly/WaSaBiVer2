using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_EX : MonoBehaviour
{
    [SerializeField]
    private float smoothRotationTime;
    [SerializeField]
    private float smoothMoveTime;
    [SerializeField]
    private float moveSpeed;
    private float rotationVelocity;
    private float speedVelocity;
    private float currentSpeed;
    private float targetSpeed;

    private void Update()
    {
        Vector2 input = new Vector2(-Input.GetAxisRaw("Horizontal"), -Input.GetAxisRaw("Vertical"));

        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float rotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;

            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation, ref rotationVelocity, smoothRotationTime);
        }

        targetSpeed = moveSpeed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, smoothMoveTime);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        }
    }
