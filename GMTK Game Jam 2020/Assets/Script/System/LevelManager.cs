using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Esse script é recriado em todo o level e gerencia o seu escopo
/// </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject endGameScreen;

    public float seconds = 0;
    private void Start()
    {
    }
    private void Update()
    {
        seconds += Time.deltaTime;
        if(seconds > 10)
        {
            EndGame();
        }

    }

    public void EndGame()
    {
        endGameScreen.SetActive(true);
    }
}
