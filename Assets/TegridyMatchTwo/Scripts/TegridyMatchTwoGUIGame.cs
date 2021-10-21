/////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2021 Tegridy Ltd                                          //
// Author: Darren Braviner                                                 //
// Contact: db@tegridygames.co.uk                                          //
/////////////////////////////////////////////////////////////////////////////
//                                                                         //
// This program is free software; you can redistribute it and/or modify    //
// it under the terms of the GNU General Public License as published by    //
// the Free Software Foundation; either version 2 of the License, or       //
// (at your option) any later version.                                     //
//                                                                         //
// This program is distributed in the hope that it will be useful,         //
// but WITHOUT ANY WARRANTY.                                               //
//                                                                         //
/////////////////////////////////////////////////////////////////////////////
//                                                                         //
// You should have received a copy of the GNU General Public License       //
// along with this program; if not, write to the Free Software             //
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,              //
// MA 02110-1301 USA                                                       //
//                                                                         //
/////////////////////////////////////////////////////////////////////////////

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
