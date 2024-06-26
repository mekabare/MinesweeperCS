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
        }//Size
        
        public int MaxRow
        {
            get => maxRow;
            set
            {
                maxRow = value;
            }
        }//MaxRow

        public int MaxColumn
        {
            get => maxColumn;
            set
            {
                maxColumn = value;
            }
        }//MaxColumn

        public Tile[,] Field
        {

        }


        public List<List<Tile>> Field { get; set; }
        
        public GameDifficulty Difficulty { get; set; }
        #endregion


        // In MineField class
        public Bounds Bounds => new Bounds(Rows, Columns);

        // Konstruktor, der das Spielfeld erstellt
        public MineField(GameDifficulty difficulty)
        {
            Difficulty = difficulty;
            
            Field = new List<List<Tile>>();
            for (int i = 0; i < Rows; i++)
            {
                Field.Add(new List<Tile>());
                for (int j = 0; j < Columns; j++)
                {
                    Field[i].Add(new Tile(i, j));
                }
            }
            // TODO: Erst nach dem ersten Klicken werden die Minen platziert!! 
            PlaceMines();
            CalculateAdjacentMines();
        }

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

            foreach Tile SingleTile in Field
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


