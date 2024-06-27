using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private bool startGame;
   public static MainMenu instance;

   private void Awake()
   {
      Cursor.lockState = CursorLockMode.None;
      instance = this;
   }

   public void OnStartButton()
   {
      Debug.Log("Starting");
      startGame = true;
      SceneManager.LoadScene("MainScene");
   }

   public void OnQuitButton()
   {
      Debug.Log("Quit Game");
      Application.Quit();
   }
}
