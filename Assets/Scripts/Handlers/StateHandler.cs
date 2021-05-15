using System.Collections.Generic;
using UnityEngine;

namespace Handlers
{
    public interface IStateHandler
    {
        void OnEnter(Dictionary<string, object> payload = null);
        void OnExit();
    }
    public abstract class StateHandler : MonoBehaviour, IStateHandler
    {
        public abstract void OnEnter(Dictionary<string, object> payload = null);
        public abstract void OnExit();
    }
}