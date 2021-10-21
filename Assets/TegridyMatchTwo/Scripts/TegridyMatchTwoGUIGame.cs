using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tegridy.MatchTwo
{
    public class TegridyMatchTwoGUIGame : MonoBehaviour
    {
        [Header("MenuDisplay")]
        public string levelName;
        public Sprite lvlPic;
        public TextMeshProUGUI results;

        [Header("Level Config")]
        public bool winMaxTile;
        public TextMeshProUGUI score;
        public Button up;
        public Button down;
        public Button left;
        public Button right;
        public Button restart;
        public Button quit;
        public ImageGrid[] grid;
        public Sprite[] values;

        [Header("SoundFx")]
        public AudioClip[] levelMusic;
        public AudioClip[] moveSound;
        public AudioClip[] winSound;
        public AudioClip[] loseSound;
    }
}
