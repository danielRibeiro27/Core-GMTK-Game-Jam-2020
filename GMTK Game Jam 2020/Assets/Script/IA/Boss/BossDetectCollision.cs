using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDetectCollision : MonoBehaviour
{
    [SerializeField] BossCombat bossCombat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerCombat ec = collision.gameObject.GetComponent<PlayerCombat>();
            if (ec != null)
            {
                Vector2 dir = (collision.transform.position - bossCombat.gameObject.transform.position);
                ec.TomarDano(bossCombat.dano, dir, bossCombat.knockbackForce, bossCombat.knockbackForceUp, bossCombat.knockbackDuration);
            }
        }
    }
}
