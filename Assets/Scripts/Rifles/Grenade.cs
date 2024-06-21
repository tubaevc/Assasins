using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float grenadeTimer = 3f;
    private float countDown;
    private float giveDamage = 120f;
    private bool hasExploded = false;
    private float radius = 10f;
    [SerializeField] private GameObject explosionEffect;
    private void Start()
    {
        countDown = grenadeTimer;
    }

    private void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position,radius);
        foreach (Collider nearbyObject in colliders)
        {
            //throwing all
            
            //damage
            Object obj = nearbyObject.GetComponent<Object>();
            if (obj != null)
            {
                obj.ObjectHitDamage(giveDamage);
            }
        }
        Destroy(gameObject);
    }
}
