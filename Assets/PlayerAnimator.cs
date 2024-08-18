using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Jump jump;

    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(body.velocity.x));
        animator.SetBool("isOnGround", jump.IsPlayerOnGround());
    }
}
