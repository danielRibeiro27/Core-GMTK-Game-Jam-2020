using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Script destinado a tratar a movimentação do player
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    private Vector2 input;
    private Rigidbody2D rig;
    public int direcao = 1;
    public float multiplicadorCaída = 2.5f;
    public float multiplicadorPulo = 2f;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input = GetInput();

        if (CustomInputManager.instance.GetStaticButton("Cancel"))
        {
            CustomInputManager.instance.EmbaralharInput();
        }
    }

    void FixedUpdate()
    {
        //É preciso estar no FixedUpdate pois esse método trata melhor os calculos relacionados a física
        Mover();
        Pulo();
    }

    /// <summary>
    /// Pega o input do usuário
    /// </summary>
    private Vector2 GetInput()
    {
        Vector2 input = new Vector2(CustomInputManager.instance.GetInput("Horizontal") * direcao, 0);

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
    private void Pulo()
    {

        if (rig.velocity.y < 0)
        {
            rig.gravityScale = multiplicadorCaída;
        }
        else if (rig.velocity.y > 0 && CustomInputManager.instance.GetInput("Pulo") <= 0 )
        {
            rig.gravityScale = multiplicadorPulo;  
        }
        else
        {
            rig.gravityScale = 1;
        }
    }
}
