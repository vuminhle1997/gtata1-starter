using System.Collections.Generic;
using System.Linq;
using Score;

namespace Utils
{
    public static class SortHighScores
    {
        public static List<HighScore> GetDescendingHighScores(HighScoreLadder ladder)
        {
            var highScores = ladder.highScores;

            return highScores.OrderByDescending(highScore => highScore.score).ToList();
        }

        public static List<HighScore> GetAscendingHighScores(HighScoreLadder ladder)
        {
            var highScores = ladder.highScores;

            return highScores.OrderBy(highScore => highScore.score).ToList();
        }
    }
}