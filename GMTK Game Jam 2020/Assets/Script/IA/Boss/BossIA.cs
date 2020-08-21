using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossIA : MonoBehaviour
{
    public float velocity = 10;
    private Vector2 direction = new Vector2(1, 0);
    private Rigidbody2D rig;
    private BossCombat bossCombat;

    [Space]
    [Header("IA Settings")]
    [SerializeField] private float playerDetectionRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask oqEPlayer;
    [SerializeField] private State currentState = State.Null;
    [SerializeField] private Transform player;
    public bool canMove = true;

    [Space]
    [Header("Animação")]
    private Animator anim;

    private enum State { Null, Patrol, Chase };
    private Vector3 lookin_right;
    private Vector3 lookin_left;
    private float cooldownAttack = 0f;
    private bool isAttacking = false;
    private bool som_passos_tocando = false;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        bossCombat = GetComponent<BossCombat>();

        lookin_right = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        lookin_left = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    private void Update()
    {
        SetAnimations();

        if (!canMove)
            return;

        cooldownAttack -= Time.deltaTime;

        if (currentState == State.Chase && !isAttacking)
        {
            if (cooldownAttack <= 0)
            {
                if (IsCloseToAttack())
                {
                    cooldownAttack = bossCombat.Attack();
                }
                else
                {
                    Chase();
                }
            }
        }

        if (rig.velocity.x > .1f || rig.velocity.x < -.1f)
        {
            if (!som_passos_tocando)
            {
                AudioManager.instance.PlayByName("BossWalk");
                som_passos_tocando = true;
            }
        }
        else
        {
            AudioManager.instance.StopByName("BossWalk");
            som_passos_tocando = false;
        }
    }

    private void Chase()
    {
        Vector2 dir_to_player = Vector2.zero;
        Collider2D[] player_detection = Physics2D.OverlapCircleAll(transform.position, playerDetectionRadius, oqEPlayer);
        foreach (Collider2D col in player_detection)
        {
            if (col.name == "Player")
            {
                dir_to_player = player.position.x > transform.position.x ? Vector2.right : Vector2.left;
                dir_to_player.y = rig.velocity.y; //mantem o Y do rb
                dir_to_player.x = dir_to_player.x * velocity; //multiplica o X para a velocidade
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

    private void SetAnimations()
    {
        if(player != null)
        {
            transform.localScale = player.position.x > transform.position.x ? lookin_right : lookin_left;
        }

        bool isMoving = rig.velocity.x != 0;
        anim.SetBool("IsMoving", isMoving);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
