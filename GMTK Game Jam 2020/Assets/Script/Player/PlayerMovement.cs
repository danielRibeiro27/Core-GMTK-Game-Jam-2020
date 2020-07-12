using System.Collections;
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
    private bool estaNoChao = true;
    private Vector2 tamanhoPlayer;


    [Space]
    [Header("Outros")]
    [HideInInspector] public bool atordoado = false;

    private void Awake()
    {
        tamanhoPlayer = GetComponent<BoxCollider2D>().size;
    }
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input = GetInput();
        GetJumpInput();
    }

    void FixedUpdate()
    {
        //É preciso estar no FixedUpdate pois esse método trata melhor os calculos relacionados a física
        Mover();
        Jump();
        JumpPyshics();
    }

    /// <summary>
    /// Pega o input do usuário
    /// </summary>
    private Vector2 GetInput()
    {
        Vector2 input = new Vector2(CustomInputManager.instance.GetInputAxisRaw("Horizontal") * direcao, 0);

        return input;
    }

    /// <summary>
    /// Move o personagem baseado no input
    /// </summary>
    /// <param name="direcao">o parâmetro direcao irá definir se a direção estará invertida ou não</param>
    private void Mover()
    {
        Vector2 velocidade = new Vector2(input.x * speed, rig.velocity.y); //gera um vetor de velocidade mantendo a velocidade do Y do corpo rígido
        rig.velocity = velocidade;
    }

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
        }
    }

    private void Jump()
    {
        if (pulou == true)
        {
            rig.AddForce(Vector2.up * velocidadeDoPulo, ForceMode2D.Impulse);
            pulou = false;
            estaNoChao = false;
        }
        else
        {
            estaNoChao = Physics2D.OverlapBox(overlapPivot.position, overlapSize, 0, chao);
        }
    }

    #endregion


    #region Gizmos

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(overlapPivot.position, overlapSize);
    }

    #endregion
}
