using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomInput 
{
    public string name { get; set; } //nome do input
    public float value { get; set; } //valor do input pegado do Input.GetAxis
    public string target { get; set; } //nome do axis que o input ira usar
    public string descricao { get; set; } //descrição do uso do input
    public string label { get; set; } //rótulo do input

    public CustomInput(string _name, float _value, string _target, string _descricao, string _label)
    {
        name = _name;
        value = _value;
        target = _target;
        descricao = _descricao;
        label = _label;
    }
}
