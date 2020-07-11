using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulo : MonoBehaviour
{
    [Range(1,100)]
    public float velocidadeDoPulo;
    public LayerMask chao;
    public float friccao;
    bool pulou;
    bool estaNoChao;

    Vector2 tamanhoPlayer;

    private void Awake()
    {
        tamanhoPlayer = GetComponent<BoxCollider2D>().size;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && estaNoChao == true) {

            pulou = true;
        }
    }

    private void FixedUpdate()
    {
        if (pulou == true )
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * velocidadeDoPulo, ForceMode2D.Impulse);
            pulou = false;
            estaNoChao = false;
        }
        else
        {
            Vector2 comecoRaio = (Vector2)transform.position + Vector2.down * tamanhoPlayer * 0.5f;
            estaNoChao = Physics2D.Raycast(comecoRaio, Vector2.down, friccao, chao);
        }
        
    }
}
