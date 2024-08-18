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
        // Don't read inputs if time is frozen (i.e. we are paused).
        if (Time.timeScale <= 0)
        {
            return;
        }

        bool isPlayerWalking = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A));
        animator.SetBool("isPlayerWalking", isPlayerWalking);
        animator.SetBool("isOnGround", jump.IsPlayerOnGround());
    }
}
