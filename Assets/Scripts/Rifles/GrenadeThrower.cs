using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private Transform grenadeArea;
    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private Animator _animator;

    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     //left click throw
        //     StartCoroutine(GrenadeAnim());
        // }
        
        
    }

    private void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, grenadeArea.transform.position, grenadeArea.transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(grenadeArea.transform.forward*throwForce,ForceMode.VelocityChange);
    }

    private IEnumerator GrenadeAnim()
    {
        _animator.SetBool("GrenadeInAir",true);
        yield return new WaitForSeconds(0.5f);
        ThrowGrenade();
        yield return new WaitForSeconds(1f);
        _animator.SetBool("GrenadeInAir",false);


    }
}
