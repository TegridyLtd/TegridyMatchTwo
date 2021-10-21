using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Tegridy.MatchTwo
{
    public class TegridyMatchTwoController
    {
        [Header("Info")]
        public int gameState = 0;  //0= Idle // 1=GameStarted // 2=NoMoreRoom // 3=NoMoreTime // 4=Won
        public int score;
        public GameGrid[] gameGrid;

        int maxValue;
        int rows; 
        int colums;
        bool winMaxTile;

        public void StartGameGUI(TegridyMatchTwoGUIGame gui)
        {
            //get the settings from the gui
            maxValue = gui.values.Length;
            colums = gui.grid.Length;
            rows = gui.grid[0].row.Length;
            winMaxTile = gui.winMaxTile;
            gameGrid = new GameGrid[colums];
            score = 0;
            
            //create the game board
            for (int i = 0; i < gameGrid.Length; i++)
            {
                gameGrid[i] = new GameGrid();
                gameGrid[i].row = new int[rows];
            }

            //start the game and add the first tiles
            gameState = 1;
            AddBlock();
            AddBlock();
        }
        public void MoveBlocks(int direction) //1 = up //2 = down //3 = left //4 = right
        {
            //decide the direction to move in if the game is active
            if (gameState == 1)
            {
                switch (direction)
                {
                    case 1:
                        MoveVerticle(true);
                        CheckVerticalMatches(true);
                        break;
                    case 2:
                        MoveVerticle(false);
                        CheckVerticalMatches(false);
                        break;
                    case 3:
                        MoveHorizontal(true);
                        CheckHorizontalMatches(true);
                        break;
                    case 4:
                        MoveHorizontal(false);
                        CheckHorizontalMatches(false);
                        break;
                }
                //check if we have met the win conditions and that the game can continue for another move
                CheckWin();
                CheckEmpty();
                AddBlock();
            }
        }
        private void MoveVerticle(bool up) 
        { 
            for(int i = 0; i < gameGrid[0].row.Length; i++)
            {
                //sort the tiles for the move
                List<int> tempList = new List<int>();
                List<int> padding = new List<int>();
                for(int i2 = 0; i2 < gameGrid.Length; i2++)
                {
                    if (gameGrid[i2].row[i] != 0) tempList.Add(gameGrid[i2].row[i]);
                    else padding.Add(0);
                }
                if (up) tempList = tempList.Concat(padding).ToList();
                else tempList = padding.Concat(tempList).ToList();

                //rewrite the array with the update positions
                for (int i2 = 0; i2 < gameGrid.Length; i2++)
                {
                    gameGrid[i2].row[i] = tempList[i2];
                }
            }
        }
        private void MoveHorizontal(bool left)
        {
            for(int i2 = 0; i2 < gameGrid.Length; i2++)
            {
                //sort the tiles for the move
                List<int> ints = new List<int>();
                List<int> padding = new List<int>();
                for (int i = 0; i < gameGrid[i2].row.Length; i++)
                {
                    if (gameGrid[i2].row[i] != 0)
                    {
                        ints.Add(gameGrid[i2].row[i]);
                    }
                    else padding.Add(0);
                }

                //rewrite the array with the update positions
                if (left) gameGrid[i2].row = ints.Concat(padding).ToArray();
                else gameGrid[i2].row = padding.Concat(ints).ToArray();
            }
        }
        private void CheckVerticalMatches(bool up)
        {
            //check for any verticle matches in the desired direction
            for (int i = 1; i < gameGrid.Length; i++)
            {
                for (int i2 = 0; i2 < gameGrid[i].row.Length; i2++)
                {
                    if (gameGrid[i].row[i2] != 0 && gameGrid[i].row[i2] == gameGrid[i - 1].row[i2])
                    {
                        if (up) { score += gameGrid[i - 1].row[i2]; gameGrid[i - 1].row[i2]++; gameGrid[i].row[i2] = 0; }
                        else { gameGrid[i - 1].row[i2] = 0; score += gameGrid[i].row[i2]; gameGrid[i].row[i2]++; }
                    }
                }
            }
        }
        private void CheckHorizontalMatches(bool left)
        {
            //check for matches in the desired direction 
            foreach(GameGrid row in gameGrid)
            {
                for (int i = 1; i < row.row.Length; i++)
                {
                    //if we have a match move to the next tile and increase the score
                    if (row.row[i] != 0 && row.row[i - 1] == row.row[i])
                    {
                        if (left) { row.row[i] = 0; row.row[i - 1]++; score += row.row[i - 1]; }
                        else { row.row[i]++; row.row[i - 1] = 0; }
                    }
                }
            }
        }
        private void CheckWin()
        {
            //Check if we have reached the max tile if we are ending the game then.
            if (winMaxTile)
            foreach(GameGrid row in gameGrid)
            {
                foreach(int check in row.row)
                {
                    if (check == maxValue) gameState = 4;
                }
            }
        }
        private void CheckEmpty()
        {
            //make sure we havent filled the game board with peices
            bool empty = false;
            foreach(GameGrid row in gameGrid)
            {
                foreach(int value in row.row)
                {
                    if (value == 0) empty = true;
                }
            }
            if (empty == false) gameState = 2;
        }
        private void AddBlock()
        {
            //add another tile to the game board
            if (gameState == 1)
            {
                int tmpCol = UnityEngine.Random.Range(0, gameGrid.Length);
                int tmpRow = UnityEngine.Random.Range(0, gameGrid[0].row.Length);

                if (gameGrid[tmpCol].row[tmpRow] == 0) gameGrid[tmpCol].row[tmpRow]++;
                else if (!FindSpace(tmpCol, tmpRow)) FindSpace(0,0);
            }
        }
        private bool FindSpace(int c, int r)
        {
            //make sure our space is empty and if not find the next one that is free
            bool found = false;
            for (int i = c; i < gameGrid.Length; i++)
            {
                for(int i2 = r; i2 < gameGrid[i].row.Length; i2++)
                {
                    if(gameGrid[i].row[i2] == 0)
                    {
                        gameGrid[i].row[i2]++;
                        found = true;
                        break;
                    }
                }
                if (found) break;
            }
            return found;
        }
    }
}