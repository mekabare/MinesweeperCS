using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Minesweeper
{
    public class MineField
    {
        private int maxRows;
        private int maxColumns;
        private Tile[,] tiles;
        private int totalMines;
        private int flaggedTiles;

        /// <summary>
        /// Erstellt ein Feld mit der angegebenen Anzahl an Reihen und Spalten und initialisiert alle Tiles
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public MineField(int rows, int columns)
        {
            this.maxRows = rows;
            this.maxColumns = columns;
            tiles = new Tile[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    tiles[i, j] = new Tile(i, j);
                }
            }
        }

        public Tile[,] Tiles => tiles;


        /// <summary>
        /// Randomisiert die Positionen der Minen auf dem Spielfeld und platziert sie
        /// </summary>
        /// <param name="mineCount"></param>
        /// <param name="selectedRow"></param>
        /// <param name="selectedCol"></param>
        public void PlaceMines(int mineCount, int selectedRow, int selectedCol)
        {
            Random rand = new Random();
            int placedMines = 0;
            totalMines = mineCount;

            while (placedMines < mineCount)
            {
                int row = rand.Next(maxRows);
                int col = rand.Next(maxColumns);

                if (!tiles[row, col].HasMine && tiles[row, col] != tiles[selectedRow, selectedCol])
                {
                    tiles[row, col].HasMine = true;
                    placedMines++;
                    UpdateAdjacentMines(row, col);
                }
            }
        }

        /// <summary>
        /// Simple rechnung um die Anzahl der verbleibenden Minen zu berechnen
        /// </summary>
        public int RemainingMines => totalMines - flaggedTiles;


        /// <summary>
        /// Toggelt den Flag-Status eines Tiles und erhöht oder verringert die Anzahl der geflaggten Tiles im Feld
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void ToggleFlag(int row, int col)
        {
            if (tiles[row, col].IsFlagged)
            {
                tiles[row, col].IsFlagged = false;
                flaggedTiles--;
            }
            else
            {
                tiles[row, col].IsFlagged = true;
                flaggedTiles++;
            }
        }

        /// <summary>
        /// Holt die benachbarten Tiles eines bestimmten Tiles
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public List<Tile> GetAdjacentTiles(int row, int column)
        {
            List<Tile> adjacentTiles = new List<Tile>();

            if (row > 0)
            {
                adjacentTiles.Add(tiles[row - 1, column]); // North
                if (column > 0)
                    adjacentTiles.Add(tiles[row - 1, column - 1]); // North-West
                if (column < maxColumns - 1)
                    adjacentTiles.Add(tiles[row - 1, column + 1]); // North-East
            }

            if (row < maxRows - 1)
            {
                adjacentTiles.Add(tiles[row + 1, column]); // South
                if (column > 0)
                    adjacentTiles.Add(tiles[row + 1, column - 1]); // South-West
                if (column < maxColumns - 1)
                    adjacentTiles.Add(tiles[row + 1, column + 1]); // South-East
            }

            if (column > 0)
                adjacentTiles.Add(tiles[row, column - 1]); // West

            if (column < maxColumns - 1)
                adjacentTiles.Add(tiles[row, column + 1]); // East

            return adjacentTiles;
        }

        /// <summary>
        /// Getter der AdjacentMines eines bestimmten Tiles
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int GetAdjacentMines(int row, int column)
        {
            int count = 0;
            foreach (Tile tile in GetAdjacentTiles(row, column))
            {
                if (tile.HasMine) { count++; }
            }
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mineRow"></param>
        /// <param name="mineCol"></param>
        private void UpdateAdjacentMines(int mineRow, int mineCol)
        {
            for (int i = mineRow - 1; i <= mineRow + 1; i++)
            {
                for (int j = mineCol - 1; j <= mineRow + 1; j++)
                {
                    if (i >= 0 && i < maxRows && j >= 0 && j < maxColumns && !(i == mineRow && j == mineCol))
                    {
                        tiles[i, j].AdjacentMines++;
                    }
                }
            }
        }


        #region Getter und Setter der Tile-States

        public bool HasMine(int row, int col)
        {
            return tiles[row, col].HasMine;
        }

        public bool IsRevealed(int row, int col)
        {
            return tiles[row, col].IsRevealed;
        }

        public void SetRevealed(int row, int col, bool value)
        {
            tiles[row, col].IsRevealed = value;
        }

        public bool IsFlagged(int row, int col)
        {
            return tiles[row, col].IsFlagged;
        }

        public void SetFlagged(int row, int col, bool value)
        {
            tiles[row, col].IsFlagged = value;
        }

        #endregion

        /// <summary>
        /// Algorithmus zum Aufdecken von benachbarten Tiles ohne Minen, WIP
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void FloodFill(int row, int col)
        {
            // Base case: if the tile is out of bounds, revealed, flagged, or has a mine, stop the recursion
            if (row < 0 || row >= maxRows || col < 0 || col >= maxColumns || tiles[row, col].IsRevealed || tiles[row, col].IsFlagged || tiles[row, col].HasMine)
            {
                return;
            }

            // Reveal the current tile
            tiles[row, col].IsRevealed = true;

            // TODO - Implement the flood fill algorithm
        }

        /// <summary>
        /// Untersucht, ob die Gewinnbedingung erfüllt ist
        /// </summary>
        /// <returns></returns>
        internal bool CheckWinCondition()
        {
            if (flaggedTiles == totalMines)
            {
                foreach (Tile tile in tiles)
                {
                    if (tile.HasMine && !tile.IsFlagged)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        internal void RevealAllMines()
        {
            foreach (Tile tile in tiles)
            {
                if (tile.HasMine)
                {
                    tile.IsRevealed = true;
                }

            }
        }
    }
}
