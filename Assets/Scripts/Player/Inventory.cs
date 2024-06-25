using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject Weapon1;
    public bool isWeapon1Picked = false;
    public bool isWeapon1Active = false;
    [SerializeField] private SingleMeleeAttack _singleMeleeAttack;


    [SerializeField] public bool fistFightMode = false;

    [SerializeField] private GameObject Weapon2;
    public bool isWeapon2Picked = false;
    public bool isWeapon2Active = false;
    [SerializeField] private Rifle _rifle;

    [SerializeField] private GameObject Weapon3;
    public bool isWeapon3Picked = false;
    public bool isWeapon3Active = false;
    [SerializeField] private GrenadeThrower grenadeThrower;

    [SerializeField] private Fistfight _fistfight;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameManager _gameManager;

    //current weapons
    [SerializeField] private GameObject NoWeapon;
    [SerializeField] private GameObject CurrWeapon1;
    [SerializeField] private GameObject CurrWeapon2;
    [SerializeField] private GameObject CurrWeapon3;

    private void Update()
    {
        if (isWeapon1Active == false && isWeapon2Active == false &&
            isWeapon3Active == false && fistFightMode == false)
        {
            NoWeapon.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0) && isWeapon1Active == false && isWeapon2Active == false &&
            isWeapon3Active == false && fistFightMode == false)
        {
            fistFightMode = true;
            isRifleActive();
        }

        if (Input.GetKeyDown("1") && isWeapon1Active == false && isWeapon2Active == false &&
            isWeapon3Active == false && isWeapon1Picked == true)
        {
            isWeapon1Active = true;
            isRifleActive();
            CurrWeapon1.SetActive(true);
            NoWeapon.SetActive(false);
        }
        else if (Input.GetKeyDown("1") && isWeapon1Active == true)
        {
            isWeapon1Active = false;
            isRifleActive();
            CurrWeapon1.SetActive(false);
        }

        if (Input.GetKeyDown("2") && isWeapon1Active == false && isWeapon2Active == false &&
            isWeapon3Active == false && isWeapon2Picked == true)
        {
            isWeapon2Active = true;
            isRifleActive();
            CurrWeapon2.SetActive(true);
            NoWeapon.SetActive(false);
        }
        else if (Input.GetKeyDown("2") && isWeapon2Active == true)
        {
            isWeapon2Active = false;
            isRifleActive();
            CurrWeapon2.SetActive(false);
        }

        if (Input.GetKeyDown("3") && isWeapon1Active == false && isWeapon2Active == false &&
            isWeapon3Active == false && isWeapon3Picked == true)
        {
            isWeapon3Active = true;
            isRifleActive();
            CurrWeapon3.SetActive(true);
            NoWeapon.SetActive(false);
        }
        else if (Input.GetKeyDown("3") && isWeapon3Active == true)
        {
            isWeapon3Active = false;
            isRifleActive();
            CurrWeapon3.SetActive(false);
        }
        if (_gameManager.numOfGrenades<=0 && isWeapon3Active==true)
        {
            Weapon3.SetActive(false);
            isWeapon3Active = false;
            CurrWeapon3.SetActive(false);
            isRifleActive();
        }
        if (Input.GetKeyDown("4") && isWeapon1Active == false && isWeapon2Active == false &&
            isWeapon3Active == false && _gameManager.numOfHealth > 0 && _playerController.presentHealth<95)
        {
            StartCoroutine(IncreaseHealth());
        }

        if (Input.GetKeyDown("5") && isWeapon1Active == false && isWeapon2Active == false &&
            isWeapon3Active == false && _gameManager.numOfEnergy > 0 && _playerController.presentEnergy<95)
        {
            StartCoroutine(IncreaseEnergy());
        }
    }

    private void isRifleActive()
    {
        if (fistFightMode == true)
        {
            _fistfight.GetComponent<Fistfight>().enabled = true; //fistfight script on for fist fight mode active 
        }

        if (isWeapon1Active == true)
        {
            StartCoroutine(Weapon1Go());
            _singleMeleeAttack.GetComponent<SingleMeleeAttack>().enabled = true;
            _animator.SetBool("SingleHandAttackActive", true);
        }

        if (isWeapon1Active == false)
        {
            StartCoroutine(Weapon1Go());
            _singleMeleeAttack.GetComponent<SingleMeleeAttack>().enabled = false;
            _animator.SetBool("SingleHandAttackActive", false);
        }

        if (isWeapon2Active == true)
        {
            StartCoroutine(Weapon2Go());
            _rifle.GetComponent<Rifle>().enabled = true;
            _animator.SetBool("RifleActive", true);
        }

        if (isWeapon2Active == false)
        {
            StartCoroutine(Weapon2Go());
            _rifle.GetComponent<Rifle>().enabled = false;
            _animator.SetBool("RifleActive", false);
        }

        if (isWeapon3Active == true)
        {
            StartCoroutine(Weapon3Go());
            grenadeThrower.GetComponent<GrenadeThrower>().enabled = true;
        }

        if (isWeapon3Active == false)
        {
            StartCoroutine(Weapon3Go());
            grenadeThrower.GetComponent<GrenadeThrower>().enabled = false;
        }
    }

    [NotNull]
    IEnumerator Weapon1Go()
    {
        if (!isWeapon1Active)
        {
            Weapon1.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f); // kılıcı arkadan alma animasyonunda 0.5 iyi 
        if (isWeapon1Active)
        {
            Weapon1.SetActive(true);
        }
    }

    IEnumerator Weapon2Go()
    {
        if (!isWeapon2Active)
        {
            Weapon2.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f); // kılıcı arkadan alma animasyonunda 0.5 iyi 
        if (isWeapon2Active)
        {
            Weapon2.SetActive(true);
        }
    }

    IEnumerator Weapon3Go()
    {
        if (!isWeapon3Active)
        {
            Weapon3.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f); // kılıcı arkadan alma animasyonunda 0.5 iyi 
        if (isWeapon3Active)
        {
            Weapon3.SetActive(true);
        }
    }

    IEnumerator IncreaseHealth()
    {
        _animator.SetBool("Drink", true);
        yield return new WaitForSeconds(1.5f);
        _animator.SetBool("Drink", false);
        _gameManager.numOfHealth -= 1;
        _playerController.presentHealth = 200f;
        _playerController._healthBar.GiveFullHealth(200f);
    }

    IEnumerator IncreaseEnergy()
    {
        _animator.SetBool("Drink", true);
        yield return new WaitForSeconds(1.5f);
        _animator.SetBool("Drink", false);
        _gameManager.numOfEnergy -= 1;
        _playerController.presentEnergy = 100f;
        _playerController._energyBar.GiveFullEnergy(100f);
    }
}