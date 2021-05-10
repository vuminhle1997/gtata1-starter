using System;
using UnityEngine;

namespace ScriptableObjects
{
    /**
     * Scriptable Object class responsible to communicate static events which can be triggered globally, to enact
     * a very loose 'observer-pattern' between event creation, event listening and event triggering
     */
    [CreateAssetMenu(menuName = "Scriptable Objects/GlobalEvents")]
    public class GlobalEvents : ScriptableObject
    {
        #region Events

        /**
         * Static event to trigger a score change. Can be called globally. Event does not care where it comes from.
         * Event Parameters are of type int.
         */
        public static event Action<int> OnScoreChanged;

        #endregion

        #region Event Triggering

        /**
         * Static trigger to execute score change. The value for change can bei either +/-, event does not care where
         * it comes from or whether the change is positive or negative.
         */
        public static void TriggerScoreChange(int value)
        {
            OnScoreChanged?.Invoke(value);
        }

        #endregion
    }
}