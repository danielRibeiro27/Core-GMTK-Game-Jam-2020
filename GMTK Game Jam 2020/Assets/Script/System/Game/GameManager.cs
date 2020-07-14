using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Rigidbody2D player;

    private static bool canInput = false;
    public static bool CanInput
    {
        get
        {
            return canInput;
        }

        set
        {
            canInput = value;

            //se ir para falso resetar as velocidades por 1 frame
            if (!CanInput)
            {
                GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    private static bool canMove = false;
    public static bool CanMove
    {
        get
        {
            return canMove;
        }

        set
        {
            canMove = value;

        }
    }

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
    }
    private void FixedUpdate()
    {
        if (!CanMove)
            player.velocity = Vector2.zero;
    }
    public void TrocarCena(int index)
    {
        SceneManager.LoadScene(index);
    }
}
