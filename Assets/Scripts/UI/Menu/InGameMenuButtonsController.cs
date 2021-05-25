using StateMachines;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class InGameMenuButtonsController : MonoBehaviour
    {
        [SerializeField] private GameStateMachine gameStateMachine;
        public void OnClick(int i)
        {
            switch (i)
            {
                case 1:
                    gameStateMachine.Trigger(GameTransition.ResumePlaying);
                    break;
                case 2:
                    SceneManager.LoadScene("GameMenu", LoadSceneMode.Single);
                    break;
            }
        }
    }
}
