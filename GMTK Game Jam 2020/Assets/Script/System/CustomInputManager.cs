using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// Esse script será o intermediário entre o InputManager do Unity e os outros scripts que precisarem utilizar uma entrada de input.
/// Ele terá seus próprios métodos para devolver quando o usuário apertou a tecla de movimento, pulo ou ação, porém, irá re-mapear essas ações.
/// Todo script que precisar pegar input deve chamar o método GetInput.
/// </summary>
public class CustomInputManager : MonoBehaviour
{
    #region Variáveis

    /// <summary>
    /// Esse é um vetor contendo todas as ações do player
    /// </summary>
    public CustomInput[] inputs = { 
        new CustomInput("Horizontal", 0, "Horizontal", "movimento do player na horizontal", "Movimentar", "AxisRaw"),
        new CustomInput("Acao", 0, "Fire", "botao de ação", "Ação", "ButtonDown"),
        new CustomInput("Pulo", 0, "Jump", "botao de pular", "Pular", "Button")
    };

    /// <summary>
    /// Esse é um vetor contendo o nome de todos os Input Axes disponíveis no InputManager
    /// </summary>
    private readonly string[] Axis = new string[]{
        "Axis0",
        "Axis1",
        "Axis2",
        "Axis3",
        "Axis4",
        "Axis5",
        "Axis6",
        "Axis7"
    };

    private List<string> allAxisEmUso = new List<string>();

    private List<string> avaliableAxis = new List<string>();

    private CustomInputManagerUI inputManagerUI;

    #endregion

    public static CustomInputManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        inputManagerUI = GetComponent<CustomInputManagerUI>();
        inputManagerUI.AtualizarInputsUI();
    }

    private void Update()
    {
        //atualiza os valores do input
        foreach(CustomInput i in inputs)
        {
            if (i.type == "Axis")
                i.value = Input.GetAxis(i.target);
            else if (i.type == "ButtonDown")
                i.value = Input.GetButtonDown(i.target) ? 1 : 0;
            else if (i.type == "AxisRaw")
                i.value = Input.GetAxisRaw(i.target);
            else if (i.type == "Button")
                i.value = Input.GetButton(i.target) ? 1 : 0 ;
        }
    }

    #region Static Methods

    /// <summary>
    /// Procura o input pelo name e retorna seu valor
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Retorna 1 se pressionou positivo, 0 se não pressionou nada e -1 se pressionou negativo</returns>
    public float GetInput(string name)
    {
        CustomInput input = Array.Find(inputs, i => i.name == name);
        return input.value; 
    }

    /// <summary>
    /// Retorna um input direto do InputManager, útil para usar quando se precisar de um input não embaralhado
    /// </summary>
    /// <param name="name">Nome do input</param>
    /// <returns>Retorna 1 se pressionou positivo, 0 se não pressionou nada, e -1 se pressionou negativo</returns>
    public float GetStaticAxis(string name)
    {
        return Input.GetAxis(name);
    }

    /// <summary>
    /// Retorna um input direto do InputManager, útil para usar quando se precisar de um input não embaralhado
    /// </summary>
    /// <param name="name">Nome do input</param>
    /// <returns>Retorna true se pressionou o botão, ou false</returns>
    public bool GetStaticButton(string name)
    {
        return Input.GetButton(name);
    }

    /// <summary>
    /// Percorre cada input e atribui um target aleatório dentro do vetor
    /// </summary>
    public void EmbaralharInput()
    {
        allAxisEmUso = GetAxisEmUso();

        foreach(CustomInput i in inputs)
        {
            avaliableAxis = GetAvaliableAxis(); //pega os targets disponiveis

            //sorteia um target disponivel aleatorio e atribui ao input
            int randomAxisIndex = UnityEngine.Random.Range(0, avaliableAxis.Count);
            string randomAxis = avaliableAxis[randomAxisIndex];
            i.target = randomAxis;

            //atualiza os targets disponiveis
            allAxisEmUso = GetAxisEmUso();
        }

        inputManagerUI.AtualizarInputsUI();
    }

    /// <summary>
    /// Define o target de cada input para seu default.
    /// </summary>
    public void ResetarInput()
    {
        foreach(CustomInput i in inputs)
        {
            i.target = i.default_target;
        }

        inputManagerUI.AtualizarInputsUI();
    }

    /// <summary>
    /// Essa função irá percorrer todos os inputs e retornará uma lista contendo todos os axis em uso pelos input
    /// </summary>
    /// <returns>Retorna uma lista de string contendo todos os axis em uso</returns>
    private List<string> GetAxisEmUso()
    {
        List<string> allAxisEmUso = new List<string>();
        foreach (CustomInput i in inputs)
        {
            allAxisEmUso.Add(i.target);
        }

        return allAxisEmUso;
    }

    /// <summary>
    /// Essa função irá percorrer todos os Axis disponíveis, e devolverá somente aqueles que não estão sendo utilizados
    /// </summary>
    /// <returns>Retorna uma lista de string contendo todos os axis que podem ser usados por não estarem em uso</returns>
    private List<string> GetAvaliableAxis()
    {
        List<string> avaliableAxis = new List<string>();
        for (int j = 0; j < Axis.Length; j++)
        {
            string currentName = Axis[j];
            if (!allAxisEmUso.Contains(currentName))
                avaliableAxis.Add(Axis[j]);
        }

        return avaliableAxis;
    }

    #endregion
}
