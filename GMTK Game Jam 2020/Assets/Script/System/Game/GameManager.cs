using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void TrocarCena(int index)
    {
        SceneManager.LoadScene(index);
    }
}
