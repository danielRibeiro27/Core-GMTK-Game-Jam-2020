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
    [SerializeField] private float timeToEnd;
    private float seconds = 0;
    private void Start()
    {
        seconds = 0;
    }
    private void Update()
    {
        seconds += Time.deltaTime;
        if(seconds > timeToEnd)
        {
            EndGame();
        }

    }

    public void EndGame()
    {
        endGameScreen.SetActive(true);
    }
}
