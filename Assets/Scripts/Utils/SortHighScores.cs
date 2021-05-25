using System.Collections.Generic;
using System.Linq;
using Score;

namespace Utils
{
    public static class SortHighScores
    {
        /// <summary>
        /// Sorts the list in an descending order by the players score.
        /// </summary>
        /// <param name="ladder"></param>
        /// <returns></returns>
        public static List<HighScore> GetDescendingHighScores(HighScoreLadder ladder)
        {
            var highScores = ladder.highScores;

            return highScores.OrderByDescending(highScore => highScore.score).ToList();
        }

        /// <summary>
        /// Sorts the list in an ascending order by the players score.
        /// </summary>
        /// <param name="ladder"></param>
        /// <returns></returns>
        public static List<HighScore> GetAscendingHighScores(HighScoreLadder ladder)
        {
            var highScores = ladder.highScores;

            return highScores.OrderBy(highScore => highScore.score).ToList();
        }
    }
}