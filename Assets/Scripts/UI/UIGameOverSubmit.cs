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

        public void OnClick(int i)
        {
            HighScoreLadder highScoreLadder;
            var loadedHighScoreLadder = ScorePointSerializer.LoadHighScoreLadder(ScorePointSerializer.Path);
            pointsTracker.playerScore.Name = uiInputFieldController.GetName();
            if (loadedHighScoreLadder != null)
            {
                highScoreLadder = loadedHighScoreLadder;
            }
            else
            {
                highScoreLadder = new HighScoreLadder();
            }
            switch (i)
            {
                case 1:
                    ScorePointSerializer.AddPlayerToLadder(highScoreLadder,pointsTracker.playerScore);
                    SceneManager.LoadScene("GameMenu", LoadSceneMode.Single);
                    break;
            }
        }
    }
}