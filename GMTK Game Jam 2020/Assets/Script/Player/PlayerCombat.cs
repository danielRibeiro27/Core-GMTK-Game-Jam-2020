using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gerencia a vida do player, e suas ações de ataque
/// </summary>
public class PlayerCombat : MonoBehaviour
{
    [Space]
    [Header("Slots")]

    [SerializeField] private BoxCollider2D swordCollider;

    [Space]
    [Header("Settings")]

    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private float tempoInvencivel = 1f;
    [SerializeField] private float tempo_atordoado = 0.2f;


    [Space]
    [Header("Atributos")]

    [SerializeField] private int _vida;
    private int vidaInicial;
    public int Vida
    {
        get
        {
            return _vida;
        }

        set
        {
            _vida = value;

            if(_vida <= 0)
            {
                Morrer();
            }
        }
    }

    [Space]
    [Header("Others")]
    private PlayerMovement playerMov;
    private Rigidbody2D rig;
    private Animator anim;
    private bool invencivel = false;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerMov = GetComponent<PlayerMovement>();
        rig = GetComponent<Rigidbody2D>();

        vidaInicial = _vida;
    }

    private void Update()
    {
        if (CustomInputManager.instance.GetInputDown("Acao"))
        {
            anim.SetTrigger("Attack");
        }
    }

    public void TomarDano(int dano, Vector2 dir_to_enemy)
    {
        Vida -= dano;

        StartCoroutine(Knockback(dir_to_enemy));
        StartCoroutine(Invencivel());
        StartCoroutine(Atordoado());
    }

    IEnumerator Atordoado()
    {
        playerMov.atordoado = true;
        yield return new WaitForSeconds(tempo_atordoado);
        playerMov.atordoado = false;
    }
    IEnumerator Invencivel()
    {
        invencivel = true;
        anim.SetBool("Blink", true);
        yield return new WaitForSeconds(tempoInvencivel);
        anim.SetBool("Blink", false);
        invencivel = false;
    }
    IEnumerator Knockback(Vector2 dir)
    {
        float timer = 0;

        while(knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            rig.AddForce(dir * knockbackForce);
        }

        yield return 0;
    }

    private void Morrer()
    {
        Destroy(gameObject);
    }

    public void EnableSwordCollisor(int ativar)
    {
        swordCollider.enabled = ativar == 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemys" && !invencivel)
        {
            EnemyCombat ec = collision.gameObject.GetComponent<EnemyCombat>();

            Vector2 dir_player_to_enemy = (transform.position - collision.transform.position).normalized;
            dir_player_to_enemy.y = 0;

            TomarDano(ec.dano, dir_player_to_enemy);
        }
    }
}
