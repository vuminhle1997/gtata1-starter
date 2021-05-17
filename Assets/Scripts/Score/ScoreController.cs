using System;
using Persistence;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Score
{
    /**
     * Controller to listen on OnScoreChange events
     */
    // public class ScoreController : MonoBehaviour
    // {
    //     [SerializeField] private TextMeshProUGUI scoreTextGUI;
    //     private Score score;
    //
    //     [Header("Workarounds")] [SerializeField]
    //     private bool loadScore;
    //
    //     #region Score Getter / Setter
    //
    //     /**
    //      * Get current score value
    //      */
    //     public int GetScore()
    //     {
    //         return score.CurrentScore;
    //     }
    //
    //     /**
    //      * Set score to new value.
    //      * <param name="value">New score value</param>
    //      */
    //     public void SetScore(int value)
    //     {
    //         score.CurrentScore = value;
    //     }
    //
    //     #endregion
    //     
    //     
    //     #region Awake Behaviour
    //     
    //     private void Awake()
    //     {
    //         PersistenceHandler<Score>.SetupDirectories();
    //         var startScore = PersistenceHandler<Score>.LoadData("currentScore") ?? new Score();
    //         score = loadScore ? startScore : new Score();
    //         // update score text on GUI once at awakening
    //         scoreTextGUI.text = score.ToString();
    //     }
    //
    //     #endregion
    //     
    //     #region OnEnable / OnDisable
    //
    //     private void OnEnable()
    //     {
    //         GlobalEvents.OnScoreChanged += ExecuteScoreChanged;
    //     }
    //
    //     private void OnDisable()
    //     {
    //         GlobalEvents.OnScoreChanged -= ExecuteScoreChanged;
    //     }
    //
    //     #endregion
    //     
    //     /**
    //      * Add score change to current score
    //      * convert current score to string and
    //      * update score gui text
    //      * <param name="value">Integer change to score</param>
    //      */
    //     private void ExecuteScoreChanged(int value)
    //     {
    //         SetScore(GetScore() + value);
    //         scoreTextGUI.text = score.ToString();
    //     }
    //     
    //     
    //     private void OnApplicationQuit()
    //     {
    //         PersistenceHandler<Score>.SaveData(score, "currentScore");
    //     }
    // }
}