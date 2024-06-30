using Minesweeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Tile
    {
        #region Properties
        private int[,] position;                        //Position (2D-Array)

        private int row, column;                        //Reihe, Spalte

        private bool hasMine, isRevealed, isFlagged;     //Hat Mine, Ist Aufgedeckt, Hat Flagge
    
           
        private int adjacentMines;

        public int AdjacentMines
        {
            get => adjacentMines;
            set => adjacentMines = value;
        }
       


        #endregion Properties



        #region Getter/Setter
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
                if (value < 0) { throw new ArgumentOutOfRangeException(nameof(value), "row of Tile cannot be negative!"); }
                else { row = value; }
            }
        }//Row

        public int Column
        {
            get => column;
            set
            {
                if (value < 0) { throw new ArgumentOutOfRangeException(nameof(value), "column of Tile cannot be negative!"); }
                else { column = value; }
            }
        }//Column

        public bool HasMine
        {
            get => hasMine;
            set { hasMine = value; }
        }//HasMine

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

        #endregion



        #region Konstruktoren

        /// <summary>
        /// Reihe, Spalte - Konstruktor
        /// </summary>
        /// <param name="row">Reihe</param>
        /// <param name="column">Spalte</param>
        /// <exception cref="ArgumentOutOfRangeException">Reihe und Spalte müssen positiv sein!</exception>
        public Tile(int row, int column)
        {
            Position = new int[row, column];
            HasMine = false;
            IsRevealed = false;
            IsFlagged = false;

        }
        #endregion       

    }//Klasse
}//Namespace

