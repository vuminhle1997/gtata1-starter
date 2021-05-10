using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverHandler : StateHandler
{
    public override void OnEnter(Dictionary<string, object> payload = null)
    {
        Debug.Log("Enter State: " + $"{GameState.GameOver}");
    }

    public override void OnExit()
    {
        Debug.Log("Exit State: " + $"{GameState.GameOver}");
    }
}
