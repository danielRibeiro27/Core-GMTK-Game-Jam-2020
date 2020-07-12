using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private int vidaInicial;
    public int dano = 1;

    #region Propriedades

    [SerializeField]
    private int _vida;
    public int Vida
    {
        get
        {
            return _vida;
        }

        set
        {
            _vida = value;

            if(_vida <= 0)
            {
                Morrer();
            }
        }
    }

    #endregion

    private void Start()
    {
        vidaInicial = Vida;
    }

    private void Update()
    {

    }

    private void Morrer()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        Vida -= damage;
    }


}
