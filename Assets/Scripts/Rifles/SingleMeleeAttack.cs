using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMeleeAttack : MonoBehaviour
{
    private int SingleMeleeVal;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerController;

    //attack knight
    [SerializeField] private Transform attackArea;
    [SerializeField] private float giveDamage = 10f;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask knightLayer;
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SingleMeleeModes();
        }
    }

    private void SingleMeleeModes()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SingleMeleeVal = Random.Range(1, 6);

            if (SingleMeleeVal == 1)
            {
               
                Attack();
                StartCoroutine(SingleAttack1());
            }

            if (SingleMeleeVal == 2)
            {
               
                Attack();
                StartCoroutine(SingleAttack2());
            }

            if (SingleMeleeVal == 3)
            {
                
                Attack();
                StartCoroutine(SingleAttack3());
            }

            if (SingleMeleeVal == 4)
            {
               
                Attack();
                StartCoroutine(SingleAttack4());
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

    IEnumerator SingleAttack1()
    {
        _animator.SetBool("SingleAttack1", true);
        _playerController.movementSpeed = 0f;
        _animator.SetFloat("movementValue",0f);
        yield return new WaitForSeconds(0.2f);
        _animator.SetBool("SingleAttack1", false);
        _playerController.movementSpeed = 4f;
        _animator.SetFloat("movementValue",0f);
    }

    IEnumerator SingleAttack2()
    {
        _animator.SetBool("SingleAttack2", true);
        _playerController.movementSpeed = 0f;
        _animator.SetFloat("movementValue",0f);
        yield return new WaitForSeconds(0.2f);
        _animator.SetBool("SingleAttack2", false);
        _playerController.movementSpeed = 4f;
        _animator.SetFloat("movementValue",0f);
    }

    IEnumerator SingleAttack3()
    {
        _animator.SetBool("SingleAttack3", true);
        _playerController.movementSpeed = 0f;
        _animator.SetFloat("movementValue",0f);
        yield return new WaitForSeconds(0.2f);
        _animator.SetBool("SingleAttack3", false);
        _playerController.movementSpeed = 4f;
        _animator.SetFloat("movementValue",0f);
    }

    IEnumerator SingleAttack4()
    {
        _animator.SetBool("SingleAttack4", true);
        _playerController.movementSpeed = 0f;
        _animator.SetFloat("movementValue",0f);
        yield return new WaitForSeconds(0.2f);
        _animator.SetBool("SingleAttack4", false);
        _playerController.movementSpeed = 4f;
        _animator.SetFloat("movementValue",0f);
    }

   
}
