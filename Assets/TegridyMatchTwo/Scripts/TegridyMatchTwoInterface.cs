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
using System.Collections.Generic;
using Tegridy.AudioTools;
using TMPro;
namespace Tegridy.MatchTwo
{
    public class Tegridy1024MatchTwoInterface
    {
        TegridyMatchTwoController controller;
        TegridyMatchTwoGUIGame gui;
        [HideInInspector] public GameObject hostMenu;
        AudioSource audioSource;

        int currentLevel;
        public List<Result> results = new List<Result>();
        public void StartGame(TegridyMatchTwoGUIGame gameUI, GameObject host, AudioSource audioOut, int levelID)
        {
            //setup the variables
            hostMenu = host;
            gui = gameUI;
            audioSource = audioOut;
            currentLevel = levelID;

            //if we came from another menu make sure its disabled
            if (hostMenu != null) hostMenu.SetActive(false);

            //configure the GUI and set any text fields we find
            gui.up.onClick.AddListener(() => MakeMove(1));
            if (gui.up.GetComponentInChildren<TextMeshProUGUI>() != null)
                gui.up.GetComponentInChildren<TextMeshProUGUI>().text = TegridyMatchTwoLanguage.up;

            gui.down.onClick.AddListener(() => MakeMove(2));
            if (gui.down.GetComponentInChildren<TextMeshProUGUI>() != null)
                gui.down.GetComponentInChildren<TextMeshProUGUI>().text = TegridyMatchTwoLanguage.down;

            gui.left.onClick.AddListener(() => MakeMove(3));
            if (gui.left.GetComponentInChildren<TextMeshProUGUI>() != null)
                gui.left.GetComponentInChildren<TextMeshProUGUI>().text = TegridyMatchTwoLanguage.left;

            gui.right.onClick.AddListener(() => MakeMove(4));
            if (gui.right.GetComponentInChildren<TextMeshProUGUI>() != null)
                gui.right.GetComponentInChildren<TextMeshProUGUI>().text = TegridyMatchTwoLanguage.right;

            gui.restart.onClick.AddListener(() => RestartGame());
            if (gui.restart.GetComponentInChildren<TextMeshProUGUI>() != null)
                gui.restart.GetComponentInChildren<TextMeshProUGUI>().text = TegridyMatchTwoLanguage.restart;

            gui.quit.onClick.AddListener(() => CloseGame());
            if (gui.quit.GetComponentInChildren<TextMeshProUGUI>() != null)
                gui.quit.GetComponentInChildren<TextMeshProUGUI>().text = TegridyMatchTwoLanguage.exit;

            //enable the GUI and start the game.
            gui.gameObject.SetActive(true);
            TegridyAudioTools.PlayRandomClip(gui.levelMusic, audioSource);
            RestartGame();
        }
        private void MakeMove(int direction)
        {
            //tell the controller to make a move in the desired direction
            controller.MoveBlocks(direction);
            UpdateDisplay();

            //check the game state and see if we can continue
            switch (controller.gameState)
            {
                case 1:
                    TegridyAudioTools.PlayOneShot(gui.moveSound, audioSource);
                    break;
                case 2:
                    TegridyAudioTools.PlayOneShot(gui.loseSound, audioSource);
                    CloseGame();
                    break;
                case 3:
                    TegridyAudioTools.PlayOneShot(gui.loseSound, audioSource);
                    CloseGame();
                    break;
                case 4:
                    TegridyAudioTools.PlayOneShot(gui.winSound, audioSource);
                    CloseGame();
                    break;
            }
        }
        private void UpdateDisplay()
        {
            //update the game grid display
            for(int i = 0; i < controller.gameGrid.Length; i++)
            {
                for(int i2 = 0; i2 < controller.gameGrid[i].row.Length; i2++)
                {
                    gui.grid[i].row[i2].sprite = gui.values[controller.gameGrid[i].row[i2]];
                }
            }
            gui.score.text = controller.score.ToString();
        }
        private void CloseGame()
        {
            //add the results to the list
            Result result = new Result();
            result.level = currentLevel;
            result.gameState = controller.gameState;
            result.score = controller.score;
            results.Add(result);

            //if we have a ui to display them...
            if(gui.results != null)
            {
                string newString;
                if (controller.gameState != 4) newString = "Loser";
                else newString = "<b>Winner</b><br>Score: " + result.score;
                gui.results.text = newString;
            }

            //remove the listeners so they arent added twive if we play the level again
            gui.up.onClick.RemoveAllListeners();
            gui.down.onClick.RemoveAllListeners();
            gui.left.onClick.RemoveAllListeners();
            gui.right.onClick.RemoveAllListeners();

            gui.restart.onClick.RemoveAllListeners();
            gui.quit.onClick.RemoveAllListeners();

            //disable the GUI and go back to the host menu if we have one
            gui.gameObject.SetActive(false);
            if (hostMenu != null) hostMenu.SetActive(true);
            else Application.Quit();
        }
        private void RestartGame()
        {
            //tell the controller we want to restart and update the gui
            controller = new TegridyMatchTwoController();
            controller.StartGameGUI(gui);
            UpdateDisplay();
        }
    }
}
