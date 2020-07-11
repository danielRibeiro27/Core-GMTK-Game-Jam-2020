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
    [Header("")]
    private PlayerMovement playerMov;

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
        if (CustomInputManager.instance.GetInput("Acao") > 0)
        {
            CurtoCircuitoVal += 0.2f;
        }
    }

    private void AplicarEfeitosCurtoCircuito()
    {
        //inverte a direcao que o player anda
        playerMov.direcao = -1;
        CustomInputManager.instance.EmbaralharInput();
    }

    /// <summary>
    /// Essa coroutine espera 3 segundos para encerrar os efeitos do curto circuito e resetar seu valor
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoroutineEncerrarEfeitosCurtoCircuito()
    {
        yield return new WaitForSeconds(3f);

        EncerrarEfeitosCurtoCircuito();
        CurtoCircuitoVal = 0;
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
