using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingHandler : StateHandler
{
    [SerializeField] private PlayerController playerController;
    public override void OnEnter(Dictionary<string, object> payload = null)
    {
        // throw new System.NotImplementedException();
        Debug.Log("Exit State: " + $"{PlayerState.Walk}");
    }

    public override void OnExit()
    {
        // throw new System.NotImplementedException();
        Debug.Log("Enter State: " + $"{PlayerState.Walk}");
    }
}
