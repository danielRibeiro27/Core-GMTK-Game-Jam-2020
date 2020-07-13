using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    [Space]
    [Header("Atributos de Combate")]
    [SerializeField] private float cooldownAtks;
    [SerializeField] public int dano = 1;
    [SerializeField] public float knockbackForce;
    [SerializeField] public float knockbackForceUp;
    [SerializeField] public float knockbackDuration;

    private Animator anim;
    private Rigidbody2D rig;
    private int vidaInicial;

    #region Propriedades

    [SerializeField]
    private int _vida;
    public int Vida
    {
        get
        {
            return _vida;
        }

        set
        {
            _vida = value;

            if (_vida <= 0)
            {
                Morrer();
            }
        }
    }

    #endregion

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rig = GetComponentInChildren<Rigidbody2D>();
        vidaInicial = Vida;
    }

    private void Update()
    {

    }

    private void Morrer()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        Vida -= damage;
    }

    public float Attack()
    {
        //zera a velocidade
        rig.velocity = Vector2.zero;

        //seta a animacao de ataque
        anim.SetTrigger("Attack");

        return cooldownAtks;
    }
}
