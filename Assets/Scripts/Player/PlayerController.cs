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
    
    //health & energy
    [SerializeField] private float playerHealth = 200f;
    [SerializeField] private float presentHealth;
    [SerializeField] private float playerEnergy = 100f;
    [SerializeField] private float presentEnergy;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private EnergyBar _energyBar;
    [SerializeField] private GameObject _damageIndicator;


    private void Awake()
    {
        presentHealth = playerHealth;
        presentEnergy = playerEnergy;
        _healthBar.GiveFullHealth(presentHealth);
        _energyBar.GiveFullEnergy(presentEnergy);
    }

    private void Update()
    {
        if (presentEnergy<=0)
        {
            movementSpeed = 2f;
            if (!Input.GetButton("Horizontal") || !Input.GetButton("Vertical"))
            {
                _animator.SetFloat("movementValue", 0f);
            }
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                _animator.SetFloat("movementValue", 0.5f);
                StartCoroutine(SetEnergy());
            }
        }

        if (presentEnergy >= 1)
        {
            movementSpeed = 4f;
        }

        if (_animator.GetFloat("movementValue")>=0.9999)
        {
            PlayerEnergyDecrease(0.02f);
        }
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

    public bool HasPlayerControl
    {
        get => playerControl;
        set => playerControl = value;
    }

    public void PlayerHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        _healthBar.SetHealth(presentHealth);
        StartCoroutine(ShowDamage());
        if (presentHealth<=0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject,1.0f);
    }

    public void PlayerEnergyDecrease(float energyDecrease)
    {
        presentEnergy -= energyDecrease;
        _energyBar.SetEnergy(presentEnergy);
    }

    IEnumerator SetEnergy()
    {
        presentEnergy = 0f;
        yield return new WaitForSeconds(5f);
        _energyBar.GiveFullEnergy(presentEnergy);
        presentEnergy = 100f;
    }

    IEnumerator ShowDamage()
    {
        _damageIndicator.SetActive(true);
        yield return new WaitForSeconds(1f);
        _damageIndicator.SetActive(false);
    }
   
}