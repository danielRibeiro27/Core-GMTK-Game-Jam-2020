using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
                GameObject player = GameObject.Find("Player");
                if(player != null)
                {
                    player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        //player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {

    }
    public void TrocarCena(int index)
    {
        SceneManager.LoadScene(index);
    }
}
