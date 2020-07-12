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

    int minutes = 0;
    public int seconds = 0;
    int hours = 0;
    private void Start()
    {
    }
    private void Update()
    {

        string hoursTxt = String.Format("00", hours);
        string minutesTxt = String.Format("00", minutes);
        string secondsTxt = String.Format("00", seconds);

        GameObject.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = hoursTxt + ":" + minutesTxt + ":" + secondsTxt;

        seconds += (int)Time.deltaTime;
        if(seconds >= 60)
        {
            seconds = 0;
            minutes++;
        }

        if(minutes > 60)
        {
            minutes = 0;
            hours++;
        }

        if (minutes >= 10)
        {
            EndGame();
        }

    }

    public void EndGame()
    {
        endGameScreen.SetActive(true);
    }
}
