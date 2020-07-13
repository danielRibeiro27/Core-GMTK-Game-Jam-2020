using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationEvents : MonoBehaviour
{
    [SerializeField] private BoxCollider2D attackCollider;
    public void EnableAttackCollision(int enable)
    {
        attackCollider.enabled = enable == 1;
    }
}

