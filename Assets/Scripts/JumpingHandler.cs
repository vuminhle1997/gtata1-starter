using System;
using System.Collections.Generic;
using UnityEngine;

public class JumpingHandler : StateHandler
{
    [SerializeField] private PlayerStateMachine playerStateMachine;
    [SerializeField] private PlayerController playerController;
    
    public override void OnEnter(Dictionary<string, object> payload)
    {
        Debug.Log("Exit State: " + $"{PlayerState.Jump}");
    }

    public override void OnExit()
    {
        Debug.Log("Enter State: " + $"{PlayerState.Jump}");
    }

    // private void Update()
    // {
    //     playerController.isGrounded = Physics2D.OverlapCircle(playerController.groundCheck.position,
    //         0.2f, playerController.groundLayer);
    // }

    private void FixedUpdate()
    {
        if (playerController.isGrounded && playerStateMachine.GetCurrentState() == PlayerState.Jump)
        {
            playerController.Jump();    
        }
    }
}