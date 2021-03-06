﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Script destinado a tratar a movimentação do player
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Space]
    [Header("Movimentação")]
    [SerializeField] private float speed = 2;
    private Vector2 input;
    private Rigidbody2D rig;
    public int direcao = 1;


    [Space]
    [Header("Jump")]
    [Range(1, 100)] 
    [SerializeField] private float velocidadeDoPulo;
    [SerializeField] private LayerMask chao;
    [SerializeField] private float multiplicadorCaída = 2.5f;
    [SerializeField] private float multiplicadorPulo = 2f;
    [SerializeField] private Vector2 overlapSize;
    [SerializeField] private Transform overlapPivot;
    private bool pulou;
    private bool pulouEvent = false;
    private bool estaNoChao = true;
    private bool estaNoAr = false;
    private bool estaCaindo = false;
    private bool playing_steps_audio = false;
    private int moving;

    [Space]
    [Header("Animacao")]
    private Animator anim;

    [Space]
    [Header("Outros")]
    [HideInInspector] public bool atordoado = false;
    private Vector3 lookin_right;
    private Vector3 lookin_left;

    [HideInInspector] public bool moverAuto = false;
    [HideInInspector] public Vector2 direcaoMoverAuto = new Vector2(1, 0);
    [HideInInspector] public float velocidadeMoverAuto;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        lookin_right = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        lookin_left = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {
        SetAnimations();
        input = GetInput();
        GetJumpInput();

        //se estive rno chao, entao nao esta no ar
        estaNoAr = !estaNoChao;

        if(estaCaindo && estaNoChao)
        {
            AudioManager.instance.PlayByName("PlayerLand");
            pulouEvent = false;
        }

        if (moving > 0 && !playing_steps_audio && !estaNoAr)
        {
            AudioManager.instance.PlayByName("PlayerSteps");
            playing_steps_audio = true;
        }

        if(moving == 0 || estaNoAr)
        {
            AudioManager.instance.StopByName("PlayerSteps");
            playing_steps_audio = false;
        }
    }

    void FixedUpdate()
    {
        //É preciso estar no FixedUpdate pois esse método trata melhor os calculos relacionados a física
        Mover();
        Jump();
        JumpPyshics();
    }

    #region Movement

    /// <summary>
    /// Pega o input do usuário
    /// </summary>
    private Vector2 GetInput()
    {
        Vector2 input = new Vector2(CustomInputManager.instance.GetInputAxisRaw("Horizontal") * direcao, 0);

        //set a a direcao que o personagem esta olhando
        if(input.x != 0)
        {
            transform.localScale = input.x > 0 ? lookin_right : lookin_left;
        }

        return input;
    }

    /// <summary>
    /// Move o personagem baseado no input
    /// </summary>
    /// <param name="direcao">o parâmetro direcao irá definir se a direção estará invertida ou não</param>
    public void Mover()
    {
        if (!GameManager.CanMove)
            return;

        Vector2 final_direcao = new Vector2(input.x * speed, rig.velocity.y); //gera um vetor de velocidade mantendo a velocidade do Y do corpo rígido

        if (moverAuto)
        {
            rig.velocity = new Vector2(direcaoMoverAuto.x * velocidadeMoverAuto, rig.velocity.y);
            return;
        }

        rig.velocity = final_direcao;
    }

    #endregion

    #region GFX

    private void SetAnimations()
    {
        //recebe 1 se estiver se movendo para a direita ou para a esquerda
        moving = rig.velocity.x > 0.1 || rig.velocity.x < -0.1 ? 1 : 0;

        anim.SetFloat("VelocityY", rig.velocity.y);
        anim.SetFloat("VelocityX", moving);
    }

    #endregion

    #region Jump

    private void JumpPyshics()
    {

        if (rig.velocity.y < 0)
        {
            rig.gravityScale = multiplicadorCaída; // adiciona o valor de um multiplicador pré definido ao valor da gravidade
        }
        else if (rig.velocity.y > 0 && !CustomInputManager.instance.GetInput("Pulo"))// compara se o jogador pulou e adiciona o valor pre definido do multiplicador de pulo ao valor da gravidade
        {
            rig.gravityScale = multiplicadorPulo; 
        }
        else
        {
            rig.gravityScale = 1;
        }
    }

    private void GetJumpInput()
    {
        if (CustomInputManager.instance.GetInputDown("Pulo") && estaNoChao == true)
        {
            pulou = true;
            estaNoAr = true;

            AudioManager.instance.PlayByName("PlayerJump");
        }
    }

    private void Jump()
    {
        if (!GameManager.CanMove)
            return;

        if (pulou == true)
        {
            rig.AddForce(Vector2.up * velocidadeDoPulo, ForceMode2D.Impulse);
            pulou = false;
            pulouEvent = true;
            estaNoChao = false;
        }
        else
        {
            estaNoChao = Physics2D.OverlapBox(overlapPivot.position, overlapSize, 0, chao);
        }

        //se estiver caindo, recebe true
        //o trecho precisa estar após capturar se esta no chao
        estaCaindo = rig.velocity.y < 0f;
    }

    #endregion

    #region Gizmos

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(overlapPivot.position, overlapSize);
    }

    #endregion
}
