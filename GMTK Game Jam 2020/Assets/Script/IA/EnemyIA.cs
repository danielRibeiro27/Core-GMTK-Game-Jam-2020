using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    public float velocity = 10;
    private Vector2 direction;
    private Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Walk();
    }

    private void Walk()
    {
        rig.velocity = direction * velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlocoMudarDirecao")
        {
            direction = new Vector2(-direction.x, direction.y);
        }
    }
}
