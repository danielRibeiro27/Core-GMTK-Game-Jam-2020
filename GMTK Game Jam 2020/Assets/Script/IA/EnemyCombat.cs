using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private EnemyGFX gfx;
    private EnemyIA IA;

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
        gfx = GetComponent<EnemyGFX>();
        IA = GetComponent<EnemyIA>();
    }

    private void Update()
    {

    }

    private void Morrer()
    {
        AudioManager.instance.PlayByName("InimigoMorte");
        AudioManager.instance.StopByName("EnemyWalk");

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
        AudioManager.instance.PlayByName("AtaqueAcertouInimigo");
        AudioManager.instance.PlayByName("InimigoAcertado");
        StartCoroutine(EfeitosTakeDamage(.5f));
    }

    IEnumerator EfeitosTakeDamage(float duration)
    {
        gfx.Blink(true);
        IA.velocity = 0f;

        yield return new WaitForSeconds(duration);

        IA.velocity = IA.startVelocity;
        gfx.Blink(false);
    }

    public void Attack()
    {
        Debug.Log("Attacking !");
    }
}
