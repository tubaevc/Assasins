using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float movementSpeed = 4f;
    [SerializeField] private MainCameraController _mainCameraController;
    [SerializeField] public float rotSpeed = 600f;
    private Quaternion requiredRotation;
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float surfaceCheckRadius = 0.1f;
    [SerializeField] private Vector3 surfaceCheckOffset;
    [SerializeField] private LayerMask surfaceMask;
    private bool onSurface;
    [SerializeField] private float fallingSpeed;
    [SerializeField] private Vector3 moveDir;

    [SerializeField] private bool playerControl = true;

    private void Update()
    {
        PlayerMovement();
        if (!playerControl)
        {
            return;
        }

        if (onSurface)
        {
            fallingSpeed = 0.5f;
        }
        else
        {
            fallingSpeed += Physics.gravity.y * Time.deltaTime;
        }

        var velocity = moveDir * movementSpeed;
        velocity.y = fallingSpeed;
        SurfaceCheck();
    }

    private void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float movementAmount =
            Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical)); //clamp 0-1 animator blendtree treshold

        var movementInput = new Vector3(horizontal, 0f, vertical).normalized;

        var movementDirection = _mainCameraController.flatRotation * movementInput;

        _characterController.Move(movementDirection * movementSpeed * Time.deltaTime);
        if (movementAmount > 0)
        {
            requiredRotation = Quaternion.LookRotation(movementDirection);
        }

        movementDirection = moveDir;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, requiredRotation, rotSpeed * Time.deltaTime);

        _animator.SetFloat("movementValue", movementAmount, 0.2f, Time.deltaTime);
    }

    private void SurfaceCheck()
    {
        onSurface = Physics.CheckSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius, surfaceMask);
    }

    public void SetControl(bool hasControl)
    {
        this.playerControl = hasControl;
        _characterController.enabled = hasControl;
        if (!hasControl)
        {
            _animator.SetFloat("movementValue", 0f);
            requiredRotation = transform.rotation;
        }
    }
}