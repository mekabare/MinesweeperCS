﻿using Minesweeper;
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

        private bool isMine, isRevealed, isFlagged;     //Hat Mine, Ist Aufgedeckt, Hat Flagge

        private int adjacentMines;                      //umliegende Minen

        private Tile[] adjacentTiles;                       //umliegende Tiles
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

        public int AdjacentMines
        {
            get
            {
                {
                    int count = 0;
                    foreach (Tile tile in AdjacentTiles)
                    {
                        if (tile.IsMine) { count++; }
                    }
                    adjacentMines = count;
                }
                return adjacentMines;
            }
                set
            {
                if (value >= 0) { adjacentMines = value; }
                else { throw new ArgumentException("value must be larger than or equal to zero!", "adjacentMines"); }
            }
        }
        public Tile[] AdjacentTiles
        {
            get { 
                Tile Ntile = new Tile(row - 1, column);
                Tile NEtile = new Tile(row - 1, column + 1);
                Tile Etile = new Tile(row, column + 1);
                Tile SEtile = new Tile(row + 1, column + 1);
                Tile Stile = new Tile(row + 1, column);
                Tile SWtile = new Tile(row + 1, column - 1);
                Tile Wtile = new Tile(row, column - 1);
                Tile NWtile = new Tile(row - 1, column - 1);

                adjacentTiles = new Tile[] { Ntile, NEtile, Etile, SEtile, Stile, SWtile, Wtile, NWtile };
                return adjacentTiles;
            }
            set { value = adjacentTiles; }
        }//AdjacentTiles

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
            IsMine = false;
            IsRevealed = false;
            IsFlagged = false;

        }
        #endregion       

    }//Klasse
}//Namespace

