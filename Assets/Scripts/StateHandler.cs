using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateHandler : MonoBehaviour, IStateHandler
{
    public abstract void OnEnter(Dictionary<string, object> payload = null);
    public abstract void OnExit();
}