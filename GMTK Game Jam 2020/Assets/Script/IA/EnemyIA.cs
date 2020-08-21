using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    public float velocity = 10;
    public float startVelocity;
    private Vector2 direction = new Vector2(1, 0);
    private Rigidbody2D rig;
    private EnemyCombat enemyCombat;

    [Space]
    [Header("IA Settings")]
    [SerializeField] private float playerDetectionRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask oqEPlayer;
    [SerializeField] private State currentState = State.Null;

    private enum State { Null, Patrol, Chase };
    private Vector3 lookin_right;
    private Vector3 lookin_left;
    private bool som_passos_tocando = false;

    private void Start()
    {
        if (currentState == State.Null)
            currentState = State.Patrol;

        rig = GetComponent<Rigidbody2D>();
        enemyCombat = GetComponent<EnemyCombat>();


        lookin_right = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        lookin_left = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        startVelocity = velocity;
    }
    private void Update()
    {
        if (currentState == State.Patrol)
        {
            WalkForward();
        }
        else if(currentState == State.Chase)
        {
            if (IsCloseToAttack())
            {
                enemyCombat.Attack();
            }
            else
            {
                Chase();
            }
        }

        transform.localScale = direction.x > 0 ? lookin_right : lookin_left;

        if(rig.velocity.x > .1f || rig.velocity.x < -.1f)
        {
            if (!som_passos_tocando)
            {
                AudioManager.instance.PlayByName("EnemyWalk");
                som_passos_tocando = true;
            }
        }
        else
        {
            AudioManager.instance.StopByName("EnemyWalk");
            som_passos_tocando = false;
        }
    }

    private void WalkForward()
    {
        rig.velocity = direction * velocity;
    }

    private void Chase()
    {
        Vector2 dir_to_player = Vector2.zero;
        Collider2D[] player_detection = Physics2D.OverlapCircleAll(transform.position, playerDetectionRadius, oqEPlayer);
        foreach (Collider2D col in player_detection) { 
            if(col.name == "Player")
            {
                dir_to_player = (col.transform.position - transform.position);
                dir_to_player.y = rig.velocity.y;
                dir_to_player.x = dir_to_player.x * velocity;
            }
        }

        rig.velocity = dir_to_player;
    }

    private bool IsCloseToAttack()
    {
        Vector2 dir_to_player = Vector2.zero;
        Collider2D[] player_detection = Physics2D.OverlapCircleAll(transform.position, attackRadius, oqEPlayer);
        foreach (Collider2D col in player_detection)
        {
            if (col.name == "Player")
            {
                return true;
            }
        }

        return false;
    }

    private void Attack()
    {
        Debug.Log("Attacking !");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState != State.Patrol)
            return;

        if (collision.gameObject.tag == "BlocoMudarDirecao")
        {
            direction = new Vector2(-direction.x, direction.y);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
