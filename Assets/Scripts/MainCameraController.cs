using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float gap = 3f;
    [SerializeField] private Vector2 framingBalance;
    private float rotationX;
    private float rotationY;
    private float rotSpeed = 2.5f; //for smooth
    private float minVerAngle = -14f;
    private float maxVerAngle = 45f;

    [SerializeField] private bool invertX;
    [SerializeField] private bool invertY;

    private float invertXValue;
    private float invertYValue;
    
    private void Update()
    {

        invertXValue = (invertX) ? -1 : 1;
        invertYValue = (invertY) ? -1 : 1;
        
        rotationX += Input.GetAxis("Mouse Y")*invertYValue*rotSpeed;
        
        rotationX = Mathf.Clamp(rotationX, minVerAngle, maxVerAngle);
        rotationY += Input.GetAxis("Mouse X")*invertXValue*rotSpeed;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        var focusPos = target.position + new Vector3(framingBalance.x, framingBalance.y);
        transform.position = focusPos - targetRotation * new Vector3(0, 0, gap);
        transform.rotation = targetRotation;
    }

     public Quaternion flatRotation => Quaternion.Euler(0, rotationY, 0);

}