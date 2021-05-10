using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseHandler : StateHandler
{
    public override void OnEnter(Dictionary<string, object> payload = null)
    {
        Debug.Log("Enter State: " + $"{GameState.Pause}");
    }

    public override void OnExit()
    {
        Debug.Log("Exit State: " + $"{GameState.Pause}");
    }
}
