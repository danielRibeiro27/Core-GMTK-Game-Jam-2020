using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomInputManagerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txtMovimento;
    [SerializeField]
    private TextMeshProUGUI txtAcao;
    [SerializeField]
    private TextMeshProUGUI txtPulo;

    /// <summary>
    /// Esse método atualiza a UI de esquema de controles.
    /// </summary>
    public void AtualizarInputsUI()
    {
        List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>() { txtMovimento, txtAcao, txtPulo };
        CustomInput[] inputs = CustomInputManager.instance.inputs;
        for(int i = 0; i < inputs.Length; i++)
        {
            CustomInput input = inputs[i];
            string axisKey = GetKeyOfAxis(input.target, input.type);
            texts[i].text = input.label + ": " + axisKey;
        }
    }

    /// <summary>
    /// O método recebe o nome do axis e retorna quais sao as teclas associadas.
    /// É preciso fazer um "mapeamento" dos axis e suas teclas direto do InputManager e dar um switch para achar o crrespondente
    /// </summary>
    /// <param name="axisName">O nome do axis</param>
    /// <returns>Retorna uma string descrevendo a tecla correspondente</returns>
    private string GetKeyOfAxis(string axisName, string type)
    {
        string key = "";
        switch (axisName)
        {
            case "Horizontal":
                key = type == "AxisRaw" ? "A e D" : "A";
                break;
            case "Fire":
                key = "Left Shift";
                break;
            case "Jump":
                key = "Space";
                break;
            case "Axis0":
                key = type == "AxisRaw" ? "Q e E" : "Q";
                break;
            case "Axis1":
                key = type == "AxisRaw" ? "Z e C" : "Z";
                break;
            case "Axis2":
                key = type == "AxisRaw" ? "J e L" : "J";
                break;
            case "Axis3":
                key = type == "AxisRaw" ? "U e O" : "U";
                break;
            case "Axis4":
                key = type == "AxisRaw" ? "1 e 2" : "1";
                break;
            case "Axis5":
                key = type == "AxisRaw" ? "M e ." : "M";
                break;
            case "Axis6":
                key = type == "AxisRaw" ? "X e V" : "X";
                break;
            case "Axis7":
                key = type == "AxisRaw" ? "9 e 0" : "9";
                break;
        }

        return key;
    }
}
