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
    [Header("Movimento")]
    [SerializeField] private float speed = 2;
    private Vector2 input;
    private Rigidbody2D rig;
    public int direcao = 1;


    [Space]
    [Header("Pulo")]
    [Range(0, 16)]
    public float velocidadeDoPulo;
    private Vector2 tamanhoPlayer;
    public LayerMask chao;
    public float friccao = 0.05f;
    private bool pulou;
    private bool estaNoChao;
    public bool atordoado = false;
    public float multiplicadorCaída = 2.5f;
    public float multiplicadorPulo = 2f;

    private void Awake()
    {
        tamanhoPlayer = GetComponent<BoxCollider2D>().size;
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (atordoado)
        {
            input = Vector2.zero;
            return;
        }

        input = GetMovementInput();
        GetJumpInput();

        if(input.x < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        else if(input.x > 0){
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
    }

    void FixedUpdate()
    {
        //É preciso estar no FixedUpdate pois esse método trata melhor os calculos relacionados a física
        Mover();
        Pulo();
        JumpPhysics();
    }



    #region Pulo
    private void GetJumpInput()
    {
        if (CustomInputManager.instance.GetInputDown("Pulo") && estaNoChao == true)
        {
            pulou = true;
        }
    }

    private void Pulo()
    {
        if (pulou == true)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * velocidadeDoPulo, ForceMode2D.Impulse);
            pulou = false;
            estaNoChao = false;
        }
        else
        {
            Vector2 comecoRaio = (Vector2)transform.position + Vector2.down * tamanhoPlayer * 0.5f;
            estaNoChao = Physics2D.Raycast(comecoRaio, Vector2.down, friccao, chao);
        }
    }

    private void JumpPhysics()
    {
        if (rig.velocity.y < 0)
        {
            rig.gravityScale = multiplicadorCaída;
        }
        else if (rig.velocity.y > 0 && !CustomInputManager.instance.GetInput("Pulo"))
        {
            rig.gravityScale = multiplicadorPulo;
        }
        else
        {
            rig.gravityScale = 1;
        }
    }

    #endregion

    #region Movement

    /// <summary>
    /// Move o personagem baseado no input
    /// </summary>
    /// <param name="direcao">o parâmetro direcao irá definir se a direção estará invertida ou não</param>
    private void Mover()
    {
        Vector2 velocidade = new Vector2(input.x * speed, rig.velocity.y); //gera um vetor de velocidade mantendo a velocidade do Y do corpo rígido
        rig.velocity = velocidade;
    }

    /// <summary>
    /// Pega o input do usuário
    /// </summary>
    private Vector2 GetMovementInput()
    {
        Vector2 input = new Vector2(CustomInputManager.instance.GetInputAxisRaw("Horizontal") * direcao, 0);

        return input;
    }

    #endregion


    #region Outros


    #endregion
}
