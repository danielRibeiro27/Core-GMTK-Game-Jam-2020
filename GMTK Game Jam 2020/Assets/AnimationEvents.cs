using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Esse script tem as funções de evento da animação
/// </summary>
public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private BoxCollider2D swordCollider;
    public void EnableAttackCollision(int enable)
    {
        swordCollider.enabled = enable == 1;
    }
}
