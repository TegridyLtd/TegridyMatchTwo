using UnityEngine.UI;

namespace Tegridy.MatchTwo
{
    [System.Serializable] public class GameGrid
    {
        public int[] row;
    }

    [System.Serializable] public class ImageGrid
    {
        public Image[] row;
    }
    [System.Serializable] public class Result
    {
        public int level;
        public int gameState;
        public int score;
    }
}
