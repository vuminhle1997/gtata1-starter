using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleHandler : StateHandler
{
    [SerializeField] private PlayerController playerController;
    public override void OnExit()
    {
        Debug.Log("Exit State: "+$"{PlayerState.Idle}");
    }

    public override void OnEnter(Dictionary<string, object> payload = null)
    {
        Debug.Log("Enter State: "+$"{PlayerState.Idle}");
    }
}
