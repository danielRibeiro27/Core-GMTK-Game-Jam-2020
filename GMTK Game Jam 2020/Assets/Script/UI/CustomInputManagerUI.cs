using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomInputManagerUI : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> texts;

    /// <summary>
    /// Esse método atualiza a UI de esquema de controles.
    /// </summary>
    public void AtualizarInputsUI()
    {
        CustomInput[] inputs = CustomInputManager.instance.inputs;
        for(int i = 0; i < inputs.Length; i++)
        {
            if(i > texts.Count)
            {
                Debug.LogWarning("Existem mais inputs do que input text.");
                return;
            }


            CustomInput input = inputs[i];
            string axisKey = GetKeyOfAxis(input.target);
            texts[i].text = input.label + ": " + axisKey;
        }
    }

    /// <summary>
    /// O método recebe o nome do axis e retorna quais sao as teclas associadas.
    /// É preciso fazer um "mapeamento" dos axis e suas teclas direto do InputManager e dar um switch para achar o crrespondente
    /// </summary>
    /// <param name="axisName">O nome do axis</param>
    /// <returns>Retorna uma string descrevendo a tecla correspondente</returns>
    private string GetKeyOfAxis(string axisName)
    {
        string key = "";
        switch (axisName)
        {
            case "Horizontal":
                key = "A e D";
                break;
            case "Axis0":
                key = "Q e E";
                break;
            case "Axis1":
                key = "Z e C";
                break;
            case "Axis2":
                key = "J e L";
                break;
            case "Axis3":
                key = "U e O";
                break;
            case "Axis4":
                key = "1 e 2";
                break;
            case "Axis5":
                key = "M e .";
                break;
            case "Axis6":
                key = "X e V";
                break;
            case "Axis7":
                key = "9 e 0";
                break;
        }

        return key;
    }
}
