using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Minesweeper
{
    /*
    /// <summary>
    /// Die Grenzen des Spielfelds, um zu überprüfen, ob eine Zelle innerhalb des Spielfelds liegt
    /// </summary>
    public struct Bounds
    {
        private int rows;
        private int columns;

        public int Rows { get => rows; set {  rows = value; } }
        public int Columns { get => columns; set { columns = value; } }

        public Bounds(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }

        public bool IsWithin(int row, int column)
        {
            return row >= 0 && row < Rows && column >= 0 && column < Columns;
        }
    }//Struct
    */




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
        private int maxRow = 0, maxColumn = 0;  //Groesse des Spielfleds

        private Tile[,] field;                  //Das Spielfeld

        private GameDifficulty difficulty;      //Schwierigkeitsgrad
        #endregion


        //Hier muss noch was gemacht werden
        #region Getter und Setter
        
        public int MaxRow
        {
            get => maxRow;
            set
            {
                if (value >= 0) { maxRow = value; }
                else { throw new ArgumentOutOfRangeException("maxRow", "Value must be larger or equal than zero!"); }
            }
        }//MaxRow

        public int MaxColumn
        {
            get => maxColumn;
            set
            {
                if (value >= 0) { maxColumn = value; }
                else { throw new ArgumentOutOfRangeException("maxColumn", "Value must be larger or equal than zero!"); }
            }
        }//MaxColumn

        public Tile[,] Field
        {
            get => field;
            set
            { 
                field = value;
            }
        }//Field
        
        public GameDifficulty Difficulty
        {
            get => difficulty;
            set
            {
                if (value == null) { throw new ArgumentNullException("difficulty", "Difficulty object cannot be null!"); }
                else if (value is Easy ||
                    value is Medium ||
                    value is Hard ||
                    value is Custom)
                {
                    difficulty = value;
                }
                else { throw new ArgumentException("Difficulty object must be an Easy-, Medium-, Hard-, or Custom-Objekt.", "Difficulty"); }
            }
        }//Difficulty
        #endregion Getter und Setter



        // In MineField class
        //public Bounds Bounds => new Bounds(Rows, Columns);

        

        #region Konstruktoren

        /// <summary>
        /// Allg. Konstruktor. Erzeugt ein leeres Feld.
        /// </summary>
        /// <param name="difficulty"></param>
        public MineField(GameDifficulty difficulty)
        {
            Difficulty = difficulty;
            MaxRow = difficulty.RowSize;
            MaxColumn = difficulty.ColumnSize;

            Field = new Tile[difficulty.RowSize, difficulty.ColumnSize];    //Arraygroesse setzen

            for (int i = 0; i < difficulty.RowSize; i++)
            {
                for (int j = 0; j < difficulty.ColumnSize; j++)
                {
                    Field[i, j] = new Tile(i, j);                           //Alle Felder einsetzen
                }//for Column
            }//for Row

        }//Allg.

        #endregion Konstruktoren



        #region Methoden

        private void PlaceMines(int Row, int Column)
        {
            //Zufallsgenerator
            Random random = new Random(2349);

            //Feld durchlaufen
            //  Wenn RandGen über Schwellenwert, Bomb platzieren; Mitzählen
            for (int i = 0; i < difficulty.RowSize; i++)
            {
                for (int j = 0; j < difficulty.ColumnSize; j++)
                {
                    //pruefen, ob man in der Umgebung des Coursors ist
                    //  wenn nein, Generator laufen lassen.
                    //    Wenn über Schwelle laufen lassen Bombe platzieren, Bombe abziehen
                    //

                }//for Column
            }//for Row

            //AdjacentMines zählen
        }


        #endregion Methoden

        /*
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
        }//PlaceMines
        */


        /// <summary>   
        /// Zählt die Minen in den benachbarten Zellen für jede Zelle im Spielfeld
        /// </summary>
        private void CalculateAdjacentMines(Tile cell)
        {
            int row = cell.Row;
            int column = cell.Column;
            int adjacentMines = 0;

            foreach (Tile SingleTile in Field)
            {
                if (Field[row,column].IsMine)
                {
                    adjacentMines++;
                }
            }

            // Check all 8 directions for mines

        }//CalculateAdjacentMines



    }//Klasse
  
}//Namespace


