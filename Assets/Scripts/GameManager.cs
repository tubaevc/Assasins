using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int numOfGrenades;
    public int numOfHealth;
    public int numOfEnergy;

    public Text GrenadeStock1;
    public Text GrenadeStock2;
    public Text HealthStock;
    public Text EnergyStock;

    public GameObject healthSlot;
    public GameObject energySlot;

    public Rifle Rifle;
    public Text RifleAmmoText;
    public Text RifleMagText;

    private void Update()
    {
        //ammo & mag for rifle
        RifleAmmoText.text = "" + Rifle.presentAmmo;
        RifleMagText.text = "" + Rifle.mag;

        //stock grenade & energy
        GrenadeStock1.text = "" + numOfGrenades;
        GrenadeStock2.text = "" + numOfGrenades;
        HealthStock.text = "" + numOfHealth;
        EnergyStock.text = "" + numOfEnergy;
        if (numOfHealth > 0)
        {
            healthSlot.SetActive(true);
        }
        else if (numOfHealth <= 0)
        {
            healthSlot.SetActive(false);
        }

        if (numOfEnergy > 0)
        {
            energySlot.SetActive(true);
        }
        else if (numOfEnergy <= 0)
        {
            energySlot.SetActive(false);
        }
    }
}