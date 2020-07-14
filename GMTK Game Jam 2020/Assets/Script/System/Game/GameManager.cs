using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool canInput = false;
    public static bool CanInput
    {
        get
        {
            return canInput;
        }

        set
        {
            canInput = value;

            if(CanInput == false)
            {
                GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }
    private void Update()
    {
        Debug.Log(CanInput);
    }
    public void TrocarCena(int index)
    {
        SceneManager.LoadScene(index);
    }
}
