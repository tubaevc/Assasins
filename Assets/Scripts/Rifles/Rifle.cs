using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [SerializeField] private Transform shootingArea;
    [SerializeField] private float giveDamage = 10f;
    [SerializeField] private float shootingRange = 100f;
    [SerializeField] private bool isMoving;
    [SerializeField] private PlayerController _playerController;

    //ammo
    [SerializeField] private int maxAmmo = 1;
    [SerializeField] private int presentAmmo;
    [SerializeField] private int mag;
    [SerializeField] private float reloadingTime;
    [SerializeField] private bool setReloading;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject crossHair;
    private void Start()
    {
        presentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (_animator.GetFloat("movementValue") > 0.001f) //moving
        {
            isMoving = true;
        }
        else if (_animator.GetFloat("movementValue") < 0.099999f) //not moving
        {
            isMoving = false;
        }

        if (setReloading)
        {
            return;
        }

        if (presentAmmo <= 0 && mag > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0) && isMoving==false)
        {
            _animator.SetBool("RifleActive",true);
            _animator.SetBool("Shooting",true);
            Shoot();
        }
        else if (!Input.GetMouseButtonDown(0))
        {
            _animator.SetBool("Shooting",false);
        }

        if (Input.GetMouseButton(1))
        {
            _animator.SetBool("RifleAim",true);
            crossHair.SetActive(true);
        }
       else if (!Input.GetMouseButton(1))
        {
            _animator.SetBool("RifleAim",false);
            crossHair.SetActive(false);
        }

    }

    private void Shoot()
    {
        if (mag <= 0)
        {
            //show ammo ui
            return;
        }

        presentAmmo--;
        if (presentAmmo == 0)
        {
            mag--;
        }

        RaycastHit hitInfo;

        if (Physics.Raycast(shootingArea.position, shootingArea.forward, out hitInfo, shootingRange))
        {
            KnightAI knightAI = hitInfo.transform.GetComponent<KnightAI>();
            if (knightAI != null)
            {
                knightAI.TakeDamage(giveDamage);
            }
        }
    }

    IEnumerator Reload()
    {
        setReloading = true;
        _animator.SetFloat("movementValue",0);
        _playerController.movementSpeed = 0f;
        _animator.SetBool("ReloadRifle",true);
        yield return new WaitForSeconds(reloadingTime);
        _animator.SetBool("ReloadRifle",false);
        presentAmmo = maxAmmo;
        setReloading = false;
        _animator.SetFloat("movementValue",0);
        _playerController.movementSpeed = 4f;
    }
}