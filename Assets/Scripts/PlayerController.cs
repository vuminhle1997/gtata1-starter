using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private PlayerStateMachine stateMachine;
    // Start is called before the first frame update
    private float moveSpeed;
    private float dirX, dirY;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool originFlip = true;

    [SerializeField] public float jumpForce;

    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private void Awake()
    {
        moveSpeed = 100f;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        
        // triggers walking or idle transition
        if (dirX == 0)
        {
            stateMachine.Trigger(PlayerTransition.IsIdle, null);
        }
        else
        {
            stateMachine.Trigger(PlayerTransition.IsWalking, null);
        }

        // triggers jumping transition
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.Trigger(PlayerTransition.IsJumping, null);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        // transition to falling down state
        if (!isGrounded && stateMachine.GetCurrentState() == PlayerState.Jump)
        {
            stateMachine.Trigger(PlayerTransition.IsFallingDown, null);
        }
    }
    
    /// <summary>
    /// performs jump on player
    /// </summary>
    public void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    public float GetDirX()
    {
        return dirX;
    }
}
