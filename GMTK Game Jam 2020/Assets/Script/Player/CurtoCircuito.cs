using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Esse script é destinado a gerenciar a barra de curto-circuito, aplicar seus efeitos e etc...
/// </summary>
public class CurtoCircuito : MonoBehaviour
{
    [Space]
    [Header("Curto-Circuito slots")]
    [SerializeField] private Slider curtoCircuitoSlider;

    [Space]
    [Header("Settings")]
    [SerializeField] private float duracao = 10f;
    private PlayerMovement playerMov;

    private bool in_curto_circuito = false;

    #region Propriedades

    private float _curtoCircuitoVal = 0f;
    public float CurtoCircuitoVal
    {
        get
        {
            return _curtoCircuitoVal;
        }

        set
        {
            _curtoCircuitoVal = value;
            UpdateUI();

            if (_curtoCircuitoVal >= 1)
            {
                AplicarEfeitosCurtoCircuito();
                StartCoroutine(CoroutineEncerrarEfeitosCurtoCircuito());
            }
        }
    }

    #endregion

    private void Start()
    {
        playerMov = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (CustomInputManager.instance.GetInputDown("Acao") && !in_curto_circuito)
        {
            CurtoCircuitoVal += 0.2f;
        }
    }

    private void AplicarEfeitosCurtoCircuito()
    {
        //inverte a direcao que o player anda
        playerMov.direcao = -1;
        CustomInputManager.instance.EmbaralharInput();

        string[] audio_curto_circuito_names = new string[] {"CurtoCircuitoA", "CurtoCircuitoB", "CurtoCircuitoC"};
        AudioManager.instance.PlayByName(audio_curto_circuito_names[Random.Range(0, audio_curto_circuito_names.Length)]);

        in_curto_circuito = true;
    }

    /// <summary>
    /// Essa coroutine espera 3 segundos para encerrar os efeitos do curto circuito e resetar seu valor
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoroutineEncerrarEfeitosCurtoCircuito()
    {
        yield return new WaitForSeconds(duracao);

        EncerrarEfeitosCurtoCircuito();
        CurtoCircuitoVal = 0;

        in_curto_circuito = false;
    }

    private void EncerrarEfeitosCurtoCircuito()
    {
        //inverte a direcao que o player anda
        playerMov.direcao = 1;
        CustomInputManager.instance.ResetarInput();
    }

    private void UpdateUI()
    {
        curtoCircuitoSlider.value = CurtoCircuitoVal;
    }
}
