using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tegridy.MatchTwo
{
    public class TegridyMatchTwoGUIMenu : MonoBehaviour
    {
        public GameObject menu;
        public TextMeshProUGUI Title;
        public TextMeshProUGUI lvlName;
        public Image lvlPic;
        public Button changeLvl;
        public Button start;
        public Button quit;
        public TegridyMatchTwoGUIGame[] levels;
    }
}
