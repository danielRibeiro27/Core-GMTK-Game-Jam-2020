using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gerencia a vida do player, e suas ações de ataque
/// </summary>
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private BoxCollider2D swordCollider;
    private Animator anim;
    private int vidaInicial;

    #region Propriedades

    [SerializeField] private int _vida;
    public int Vida
    {
        get
        {
            return _vida;
        }

        set
        {
            _vida = value;
        }
    }

    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();

        vidaInicial = _vida;
    }

    private void Update()
    {
        if (CustomInputManager.instance.GetInput("Acao") > 0)
        {
            anim.SetTrigger("Attack");
        }
    }

    public void EnableSwordCollisor(int ativar)
    {
        swordCollider.enabled = ativar == 1;
    }
}
