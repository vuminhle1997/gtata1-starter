using ScriptableObjects;
using UnityEngine;

namespace Test
{
    public class EventTesterClass : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                GlobalEvents.TriggerScoreChange(10);
            }
        }
    }
}