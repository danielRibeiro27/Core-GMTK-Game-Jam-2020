using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInput 
{
    public string name { get; set; } //nome do input
    public float value { get; set; } //valor do input pegado do Input.GetAxis
    public string target { get; set; } //nome do axis que o input ira usar
    public string descricao { get; set; } //descrição do uso do input
    public string label { get; set; } //rótulo do input
    public string type { get; set; } //o type do input, se é button down, axis, button up e etc...

    public CustomInput(string _name, float _value, string _target, string _descricao, string _label, string _type)
    {
        name = _name;
        value = _value;
        target = _target;
        descricao = _descricao;
        label = _label;
        type = _type;
    }
}
