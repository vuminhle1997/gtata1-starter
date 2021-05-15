using ScriptableObjects;
using UnityEngine;

namespace Test
{
    public class EventTesterClass : MonoBehaviour
    {
        private void Update()
        {
            // test score change
            if (Input.GetKeyDown(KeyCode.A))
            {
                GlobalEvents.TriggerScoreChange(10);
            }
            
            // test pause menu
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GlobalEvents.TriggerGamePause();
            }
        }
    }
}