using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] public float knockbackForce;
    [SerializeField] public float knockbackForceUp;
    [SerializeField] public float knockbackDuration;
    public int dano = 1;

    private int vidaInicial;
    
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
        if (GameObject.Find("Level01Manager"))
        {
            Level01Manager lv = GameObject.Find("Level01Manager").GetComponent<Level01Manager>();
            lv.criaturas_derrotadas++;
        }
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        Vida -= damage;
    }

    public void Attack()
    {
        Debug.Log("Attacking !");
    }
}
