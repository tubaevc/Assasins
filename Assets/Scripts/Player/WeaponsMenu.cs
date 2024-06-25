using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsMenu : MonoBehaviour
{
   [SerializeField] private GameObject weaponsMenuUI;
   [SerializeField] private bool weaponsMenuActive=true;
   [SerializeField] private GameObject mainCam;

   [SerializeField] private GameObject weapon1;
   [SerializeField] private GameObject weapon2;
   [SerializeField] private GameObject weapon3;
   [SerializeField] private GameObject weapon3StockUI;

   [SerializeField] private GameObject healthBottle;
   [SerializeField] private GameObject energyBottle;
   [SerializeField] private Inventory inventory;

   [SerializeField] private GameObject playerUI;
   [SerializeField] private GameObject minimapCanvas;
   [SerializeField] private GameObject currMenuUI;
   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Tab) && weaponsMenuActive==false)
      {
         playerUI.SetActive(false);
         minimapCanvas.SetActive(false);
         currMenuUI.SetActive(false);
         weaponsMenuUI.SetActive(true);
         weaponsMenuActive = true;
         Time.timeScale = 0; // to stop game
         mainCam.GetComponent<MainCameraController>().enabled = false;
      }
      else  if (Input.GetKeyDown(KeyCode.Tab) && weaponsMenuActive==true)
      {
         playerUI.SetActive(true);
         minimapCanvas.SetActive(true);
         currMenuUI.SetActive(true);
         weaponsMenuUI.SetActive(false);
         weaponsMenuActive = false;
         Time.timeScale = 1;
         mainCam.GetComponent<MainCameraController>().enabled = true;
      }
      WeaponsCheck();
   }

   private void WeaponsCheck()
   {
      if (inventory.isWeapon1Picked==true)
      {
         weapon1.SetActive(true);
      }
      if (inventory.isWeapon2Picked==true)
      {
         weapon2.SetActive(true);
      }
      if (inventory.isWeapon3Picked==true)
      {
         weapon3.SetActive(true);
         weapon3StockUI.SetActive(true);
      }
   }
}
