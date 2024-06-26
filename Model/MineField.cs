using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Minesweeper
{
    /// <summary>
    /// Klasse, die das Spielfeld repräsentiert
    /// <param name= "Cell">Objektinstanz für Kästchen</field>
    /// <param name="Field">Liste von Zellen, die das Spielfeld repräsentieren</param>
    /// <param name="Rows">Anzahl der Zeilen des Spielfelds</param>
    /// <param name="Columns">Anzahl der Spalten des Spielfelds</param>
    /// <param name="Difficulty">Schwierigkeitsgrad der Session</param>
    /// <param name="Bounds">Objektinstanz für die Grenzen des Spielfelds</param>
    /// </summary>
    public class MineField
    {
        #region Felder
        private int[,] size;                    //Groesse des Spielfelds
        private int maxRow = 0, maxColumn = 0;  //Groesse des Spielfleds

        private Tile[,] field;                  //Das Spielfeld

        private GameDifficulty difficulty;      //Schwierigkeitsgrad
        #endregion


        #region Getter und Setter
        public int[,] Size
        {
            get => Size;
            set { }
        }
                    //ANTO: HIER WEITERMACHEN -ANTO


        public List<List<Tile>> Field { get; set; }
        
        public GameDifficulty Difficulty { get; set; }
        #endregion

        /// <summary>
        /// Places mines randomly on field in accordance to difficulty
        /// </summary>
        private void PlaceMines()
        {
            Random random = new Random();
            int minesPlaced = 0;
            while (minesPlaced < Difficulty.Mines)
            {
                // Randomly select a cell, tries again if cell already has a mine, ends when all mines are placed
                int row = random.Next(Rows);
                int column = random.Next(Columns);
                if (!Field[row][column].IsMine)
                {
                    Field[row][column].IsMine = true;
                    minesPlaced++;
                }
            }
        }

        /// <summary>   
        /// Zählt die Minen in den benachbarten Zellen für jede Zelle im Spielfeld
        /// </summary>
        private void CalculateAdjacentMines(Tile cell)
        {
            int row = cell.Row;
            int column = cell.Column;
            int adjacentMines = 0;

            foreach SingleTile in Field
            {
                if (Field[row][column].IsMine)
                {
                    adjacentMines++;
                }
            }

            // Check all 8 directions for mines

        }



    }//Klasse
  
}//Namespace


