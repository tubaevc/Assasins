using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
   [SerializeField] private int itemRadius;
   [SerializeField] private string itemTag;
   [SerializeField] private GameObject ItemToPick;

   [SerializeField] private Transform player;
   [SerializeField] private Inventory inventory;
   [SerializeField] private GameManager _gameManager;
   private void Start()
   {
      ItemToPick = GameObject.FindWithTag(itemTag);
      
   }

   private void Update()
   {
      if (Vector3.Distance(transform.position,player.transform.position)<itemRadius)
      {
         if (Input.GetKeyDown("f"))
         {
            if (itemTag=="Sword")
            {
               inventory.isWeapon1Picked = true;
            }

            else if (itemTag=="Rifle")
            {
               inventory.isWeapon2Picked = true;
            }
            else if (itemTag=="Grenade")
            {
               _gameManager.numOfGrenades += 5;
               inventory.isWeapon3Picked = true;
            }
            else if (itemTag=="Energy")
            {
               _gameManager.numOfHealth += 1;
            }
            else if (itemTag=="Health")
            {
               _gameManager.numOfEnergy += 1;
            }
            Destroy(gameObject);
         }
      }
   }
}
