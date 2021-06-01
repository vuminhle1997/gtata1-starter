using System.Collections.Generic;
using UnityEngine;

namespace Handlers
{
    /// <summary>
    /// Struct for state handling
    /// </summary>
    public interface IStateHandler
    {
        void OnEnter(Dictionary<string, object> payload = null);
        void OnExit();
    }
    /// <summary>
    /// Classes which inherits this class needs to implement OnEnter and OnExit
    /// </summary>
    public abstract class StateHandler : MonoBehaviour, IStateHandler
    {
        public abstract void OnEnter(Dictionary<string, object> payload = null);
        public abstract void OnExit();
    }
}