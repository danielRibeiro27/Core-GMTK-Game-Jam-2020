using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Esse script é recriado em todo o level e gerencia o seu escopo
/// </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject tela_morte;
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
        if(SceneManager.GetActiveScene().name != "Boss")
            endGameScreen.SetActive(true);
    }

    public void PlayerDied()
    {
        //abrir tela de morte
        if (SceneManager.GetActiveScene().name != "Boss")
            tela_morte.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
