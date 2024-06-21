using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fistfight : MonoBehaviour
{
    [SerializeField] private float timer = 0f;
    private int FistFightVal;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerController;

    //attack knight
    [SerializeField] private Transform attackArea;
    [SerializeField] private float giveDamage = 10f;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask knightLayer;

    [SerializeField] private Transform LeftHandPunch;
    [SerializeField] private Transform RightHandPunch;
    [SerializeField] private Transform LeftLegKick;


    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            timer += Time.deltaTime;
        }
        else
        {
            _playerController.movementSpeed = 2f;
            _animator.SetBool("FistFightActive", true);
            timer = 0f;
        }

        if (timer > 5f) //not active 5 sec 
        {
            _playerController.movementSpeed = 4f;
            _animator.SetBool("FistFightActive", false);
        }

        FistFightModes();
    }

    private void FistFightModes()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FistFightVal = Random.Range(1, 6);

            if (FistFightVal == 1)
            {
                attackArea = LeftHandPunch;
                attackRadius = 0.7f;
                Attack();
                StartCoroutine(SingleFist());
            }

            if (FistFightVal == 2)
            {
                attackArea = RightHandPunch;
                attackRadius = 0.6f;
                Attack();
                StartCoroutine(DoubleFist());
            }

            if (FistFightVal == 3)
            {
                attackArea = LeftHandPunch;
                attackArea = LeftLegKick;
                attackRadius = 0.7f;
                Attack();
                StartCoroutine(FirstFistKick());
            }

            if (FistFightVal == 4)
            {
                attackArea = LeftLegKick;
                attackRadius = 0.9f;
                Attack();
                StartCoroutine(KickCombo());
            }

            if (FistFightVal == 5)
            {
                attackArea = LeftLegKick;
                attackRadius = 0.9f;
                Attack();
                StartCoroutine(LeftKick());
            }
        }
    }

    private void Attack()
    {
        Collider[] hitKnight = Physics.OverlapSphere(attackArea.position, attackRadius, knightLayer);
        //damage
        foreach (Collider knight in hitKnight)
        {
            KnightAI knightAI = knight.GetComponent<KnightAI>();
            if (knightAI != null)
            {
                knightAI.TakeDamage(giveDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackArea == null)
        {
            return;
        }
        else
        {
            Gizmos.DrawWireSphere(attackArea.position, attackRadius);
        }
    }

    IEnumerator SingleFist()
    {
        _animator.SetBool("SingleFist", true);
        _playerController.movementSpeed = 0f;
        _animator.SetFloat("movementValue",0f);
        yield return new WaitForSeconds(0.8f);
        _animator.SetBool("SingleFist", false);
        _playerController.movementSpeed = 4f;
        _animator.SetFloat("movementValue",0f);
    }

    IEnumerator DoubleFist()
    {
        _animator.SetBool("DoubleFist", true);
        _playerController.movementSpeed = 0f;
        _animator.SetFloat("movementValue",0f);
        yield return new WaitForSeconds(0.4f);
        _animator.SetBool("DoubleFist", false);
        _playerController.movementSpeed = 4f;
        _animator.SetFloat("movementValue",0f);
    }

    IEnumerator FirstFistKick()
    {
        _animator.SetBool("FirstFistKick", true);
        _playerController.movementSpeed = 0f;
        _animator.SetFloat("movementValue",0f);
        yield return new WaitForSeconds(0.4f);
        _animator.SetBool("FirstFistKick", false);
        _playerController.movementSpeed = 4f;
        _animator.SetFloat("movementValue",0f);
    }

    IEnumerator KickCombo()
    {
        _animator.SetBool("KickCombo", true);
        _playerController.movementSpeed = 0f;
        _animator.SetFloat("movementValue",0f);
        yield return new WaitForSeconds(0.4f);
        _animator.SetBool("KickCombo", false);
        _playerController.movementSpeed = 4f;
        _animator.SetFloat("movementValue",0f);
    }

    IEnumerator LeftKick()
    {
        _animator.SetBool("LeftKick", true);
        _playerController.movementSpeed = 0f;
        _animator.SetFloat("movementValue",0f);
        yield return new WaitForSeconds(0.4f);
        _animator.SetBool("LeftKick", false);
        _playerController.movementSpeed = 4f;
        _animator.SetFloat("movementValue",0f);
    }
}