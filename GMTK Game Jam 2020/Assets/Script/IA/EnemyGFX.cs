using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGFX : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    public void Blink(bool piscar)
    {
        anim.SetBool("Blink", piscar);
    }
}
