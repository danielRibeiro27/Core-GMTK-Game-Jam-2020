using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_esquerda_direita : MonoBehaviour
{
    public Transform pos1, pos2;
    public float velocidade = 1f;
    public GameObject player;
    private Vector3 proximaPos;

    private void Start()
    {
        proximaPos = pos1.position;
    }

    void Update()
    {
        if(transform.position == pos1.position)
        {
            proximaPos = pos2.position;
        }
        if(transform.position == pos2.position)
        {
            proximaPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, proximaPos, velocidade * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            player.transform.parent = null;
        }
    }

}
