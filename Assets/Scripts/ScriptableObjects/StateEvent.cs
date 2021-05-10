using System;
using FSM.State;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/StateEvent")]
    public class StateEvent : ScriptableObject
    {
        
        
        #region Event

        public static event Action<IBaseState> OnStateChange;

        #endregion

        #region Event Triggering

        public static void TriggerStateChange(IBaseState toState)
        {
            OnStateChange?.Invoke(toState);
        }

        #endregion
    }
}