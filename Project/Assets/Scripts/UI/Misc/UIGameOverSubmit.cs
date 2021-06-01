using Game;
using Score;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Misc
{
    public class UIGameOverSubmit: MonoBehaviour
    {
        [SerializeField] private UIInputFieldController uiInputFieldController;
        [SerializeField] private PointsTracker pointsTracker;
        [SerializeField] private ScorePointSerializerController scorePointSerializerController;

        public void OnClick(int i)
        {
            switch (i)
            {
                case 1:
                    if (uiInputFieldController.GetName().Length > 0)
                    {
                        var player = new Score.HighScore(pointsTracker.playerScore.CurrentScore,
                            uiInputFieldController.GetName());
                        scorePointSerializerController.AddPlayerToLadder(player);
                        
                        #if UNITY_EDITOR
                            scorePointSerializerController.SaveHighScore(ScorePointSerializerController.PATH);
                        #else
                            scorePointSerializerController.SaveHighScore(ScorePointSerializerController.BIN_PATH);
                        #endif
                    }
                    
                    SceneManager.LoadScene("GameMenu", LoadSceneMode.Single);
                    break;
            }
        }
    }
}