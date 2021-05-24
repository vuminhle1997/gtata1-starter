using System.Collections;
using Game;
using Score;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
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
                    HighScore player = new HighScore(pointsTracker.playerScore.CurrentScore,
                        uiInputFieldController.GetName());
                    scorePointSerializerController.AddPlayerToLadder(player);
                    scorePointSerializerController.SaveSettings(ScorePointSerializerController.Path);
                    SceneManager.LoadScene("GameMenu", LoadSceneMode.Single);
                    break;
            }
        }
    }
}