using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinghtAI2 : MonoBehaviour
{
    //info
    [SerializeField] private float movingSpeed;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float currMovingSpeed;
    private float maxHealth = 120f;
    private float currentHealth;

    //knight ai
    [SerializeField] private GameObject playerBody;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float visionRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private bool playerInvisionRadius;
    [SerializeField] private bool playerInattackRadius;

    [SerializeField] private int SingleMeleeVal;
    [SerializeField] private Transform attackArea;
    [SerializeField] private float giveDamage;
    [SerializeField] private float attackingRadius;
    [SerializeField] private bool prevAttack;
    [SerializeField] private float timebtwAttack;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        currMovingSpeed = movingSpeed;
        currentHealth = maxHealth;
        playerBody = GameObject.Find("Player");
    }

    private void Update()
    {
        //check player in vision radius
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerInattackRadius = Physics.CheckSphere(transform.position, attackRadius, playerLayer);

        if (!playerInvisionRadius && !playerInattackRadius)
        {
            Idle();
        }

        if (playerInvisionRadius && !playerInattackRadius)
        {
            _animator.SetBool("Idle", false);
            ChasePlayer();
        }

        if (playerInvisionRadius && playerInattackRadius)
        {
            _animator.SetBool("Idle", true);
            SingleMeleeModes();
            //attack
        }
    }

    private void Idle()
    {
        _animator.SetBool("Run", false);
    }

    private void ChasePlayer()
    {
        currMovingSpeed = runningSpeed;
        transform.position += transform.forward * currMovingSpeed * Time.deltaTime;
        transform.LookAt(playerBody.transform);
        _animator.SetBool("Attack", false);
        _animator.SetBool("Run", true);
    }

    private void SingleMeleeModes()
    {
        if (!prevAttack)
        {
            SingleMeleeVal = Random.Range(1, 5);

            if (SingleMeleeVal == 1)
            {
                Attack();
                StartCoroutine(Attack1());
            }

            if (SingleMeleeVal == 2)
            {
                Attack();
                StartCoroutine(Attack2());
            }

            if (SingleMeleeVal == 3)
            {
                Attack();
                StartCoroutine(Attack3());
            }

            if (SingleMeleeVal == 4)
            {
                Attack();
                StartCoroutine(Attack4());
            }
        }
    }

    private void Attack()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(attackArea.position, attackingRadius, playerLayer);
        //damage
        foreach (Collider player in hitPlayer)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                //hit player
            }
        }

        prevAttack = true;
        Invoke(nameof(ActiveAttack), timebtwAttack);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackArea == null)
        {
            return;
        }
        else
        {
            Gizmos.DrawWireSphere(attackArea.position, attackingRadius);
        }
    }

    private void ActiveAttack()
    {
        prevAttack = false;
    }

    IEnumerator Attack1()
    {
        _animator.SetBool("Attack1", true);
        movingSpeed = 0f;
        runningSpeed = 0f;
        yield return new WaitForSeconds(0.2f);
        _animator.SetBool("Attack1", false);
        movingSpeed = 2f;
        runningSpeed = 4f;
    }

    IEnumerator Attack2()
    {
        _animator.SetBool("Attack2", true);
        movingSpeed = 0f;
        runningSpeed = 0f;
        yield return new WaitForSeconds(0.2f);
        _animator.SetBool("Attack2", false);
        movingSpeed = 2f;
        runningSpeed = 4f;
    }

    IEnumerator Attack3()
    {
        _animator.SetBool("Attack3", true);
        movingSpeed = 0f;
        runningSpeed = 0f;
        yield return new WaitForSeconds(0.2f);
        _animator.SetBool("Attack3", false);
        movingSpeed = 2f;
        runningSpeed = 4f;
    }

    IEnumerator Attack4()
    {
        _animator.SetBool("Attack4", true);
        movingSpeed = 0f;
        runningSpeed = 0f;
        yield return new WaitForSeconds(0.2f);
        _animator.SetBool("Attack4", false);
        movingSpeed = 2f;
        runningSpeed = 4f;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        _animator.SetTrigger("GetHit");
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        _animator.SetBool("isDead", true);
        this.enabled = false;
        GetComponent<Collider>().enabled = false; //when knight die remove collider
    }
}