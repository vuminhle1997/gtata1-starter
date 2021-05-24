using System;
using Handlers;
using Score;
using TMPro;
using UnityEngine;
using Utils;

namespace UI
{
    public class UIHighScoreListController : MonoBehaviour
    {
        [SerializeField] private HighScoreHandler highScoreLadder;
        [SerializeField] private GameObject highScoreLabel; // lazy UI component for HighScore
        private void Start()
        {
            InitializeLadder();
        }

        private void InitializeLadder()
        {
            var highScores = highScoreLadder.highScores;
            for (int i = 0; i < highScores.Count; i++)
            {
                var index = i + 1;
                Debug.Log(index);
                var highScore = highScores[i];
                var newGameObject = Instantiate(highScoreLabel);
                newGameObject.GetComponent<TextMeshProUGUI>().text =
                    $"{index}:" + $"{highScore.name}," + $"{highScore.score} PTS";

                newGameObject.transform.parent = gameObject.transform;
            }
            
            foreach (Transform child in transform)
            {
                var x = child.transform.localPosition.x;
                var y = child.transform.localPosition.y;
                child.transform.localPosition = new Vector3(x, y, 0);
                child.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
