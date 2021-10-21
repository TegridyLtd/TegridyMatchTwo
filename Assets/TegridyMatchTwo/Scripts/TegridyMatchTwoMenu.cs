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
using TMPro;
namespace Tegridy.MatchTwo 
{
    public class TegridyMatchTwoMenu : MonoBehaviour
    {
        public TegridyMatchTwoGUIMenu gui;
        Tegridy1024MatchTwoInterface game;
        AudioSource audioSource;

        private int lvl;
        void Start()
        {
            //get our data
            game = new Tegridy1024MatchTwoInterface();
            audioSource = gameObject.AddComponent<AudioSource>();
            lvl = gui.levels.Length;

            //set the level names to the language file if the size matches.
            if (TegridyMatchTwoLanguage.levelName.Length == gui.levels.Length)
            {
                for (int i = 0; i < gui.levels.Length; i++)
                {
                    gui.levels[i].levelName = TegridyMatchTwoLanguage.levelName[i];
                }
            }

            //setup the gui components
            gui.changeLvl.onClick.AddListener(() => ChangeLevel());
            if (gui.changeLvl.GetComponentInChildren<TextMeshProUGUI>() != null)
                gui.changeLvl.GetComponentInChildren<TextMeshProUGUI>().text = TegridyMatchTwoLanguage.change;

            gui.quit.onClick.AddListener(() => Close());
            if (gui.quit.GetComponentInChildren<TextMeshProUGUI>() != null)
                gui.quit.GetComponentInChildren<TextMeshProUGUI>().text = TegridyMatchTwoLanguage.exit;

            gui.start.onClick.AddListener(() => StartLevel());
            if (gui.start.GetComponentInChildren<TextMeshProUGUI>() != null)
                gui.start.GetComponentInChildren<TextMeshProUGUI>().text = TegridyMatchTwoLanguage.start;

            //Enable the menu
            gui.gameObject.SetActive(true);
            gui.menu.SetActive(true);
            ChangeLevel();
        }
        private void ChangeLevel()
        {
            lvl++;
            //resest the count if we have gone over the number of levels
            if (lvl >= gui.levels.Length) lvl = 0;
            gui.lvlName.text = gui.levels[lvl].levelName;
            gui.lvlPic.sprite = gui.levels[lvl].lvlPic;
        }
        private void StartLevel()
        {
            //tell the controller to start the game
            game.StartGame(gui.levels[lvl], gui.menu, audioSource, lvl);
        }
        private void Close()
        {
            //shut the game down
            gui.changeLvl.onClick.RemoveAllListeners();
            gui.quit.onClick.RemoveAllListeners();
            gui.start.onClick.RemoveAllListeners();
            Application.Quit();
        }
    }
}
