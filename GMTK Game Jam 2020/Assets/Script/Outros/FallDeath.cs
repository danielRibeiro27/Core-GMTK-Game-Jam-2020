using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerCombat pc = collision.GetComponent<PlayerCombat>();
            if(pc != null)
            {
                pc.TomarDano(9999999, Vector2.zero, 0, 0, 0);
            }
        }
    }
}
