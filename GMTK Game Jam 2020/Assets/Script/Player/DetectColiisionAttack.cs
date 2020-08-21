using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectColiisionAttack : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemys")
        {
            EnemyCombat ec = collision.gameObject.GetComponent<EnemyCombat>();
            if (ec != null)
            {
                ec.TakeDamage(damage);
            }
        }

        if (collision.gameObject.tag == "Boss")
        {
            BossCombat ec = collision.gameObject.GetComponent<BossCombat>();
            if (ec != null)
            {
                ec.TakeDamage(damage);
            }
        }
    }
}
