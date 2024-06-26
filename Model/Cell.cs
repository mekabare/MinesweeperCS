using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Cell
    {
        //Felder
        private int[,] position;                        //Position (2D-Array)

        private int row, column;                        //Reihe, Spalte

        private bool isMine, isRevealed, isFlagged;     //Hat Mine, Ist Aufgedeckt, Hat Flagge

        //---------------------------------------------------------------------------------------------------------------
        //Kontruktoren

        /// <summary>
        /// Reihe, Spalte - Konstruktor
        /// </summary>
        /// <param name="row">Reihe</param>
        /// <param name="column">Spalte</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Cell(int row, int column)
        {
            Position = new int[row, column];
            IsMine = false;
            IsRevealed = false;
            IsFlagged = false;
        }//public Cell

        //---------------------------------------------------------------------------------------------------------------
        //Getter und Setter

        public int[,] Position
        {
            get => position;
            set { value = position; }
        }//Position

        public int Row
        {
            get => row;
            set
            {
                if (value < 0) { throw new ArgumentOutOfRangeException(nameof(value), "row of Cell cannot be negative!"); }
                else { row = value; }
            }
        }//Row

        public int Column
        {
            get => column;
            set
            {
                if (value < 0) { throw new ArgumentOutOfRangeException(nameof(value), "column of Cell cannot be negative!"); }
                else { column = value; }
            }
        }//Column

        public bool IsMine
        {
            get => isMine;
            set { isMine = value; }
        }//IsMine

        public bool IsRevealed
        {
            get => isRevealed;
            set { isRevealed = value; }
        }//IsRevealed

        public bool IsFlagged
        {
            get => isFlagged;
            set { isFlagged = value; }
        }//IsFlagged
        
    }//Class
}//Namespace
