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

        private bool isMine, isRevealed, isFlagged;     //Hat Mine, Ist Aufgedeckt, Hat Flagge

        private int adjacentMines;                      //umliegende Minen
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
            get => adjacentMines;
            set
            {
                if (value >= 0) { adjacentMines = value; }
                else { throw new ArgumentException("value must be larger than or equal to zero!", "adjacentMines"); }
            }
        }

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



    abstract class TileHelper
    {

        // Enum for shorthand names for directions
        public enum Abkuerzung
        {
            N, NE, E, SE, S, SW, W, NW
        }
        // rowOffset, colOffset => Verschiebung in Zeile, Verschiebung in Spalte
        private static readonly Dictionary<Abkuerzung, (int rowOffset, int colOffset)> DirectionOffsets = new Dictionary<Abkuerzung, (int, int)>
            {
                { Abkuerzung.N, (-1, 0) },
                { Abkuerzung.NE, (-1, 1) },
                { Abkuerzung.E, (0, 1) },
                { Abkuerzung.SE, (1, 1) },
                { Abkuerzung.S, (1, 0) },
                { Abkuerzung.SW, (1, -1) },
                { Abkuerzung.W, (0, -1) },
                { Abkuerzung.NW, (-1, -1) }
            };

        // Method to get adjacent cells based on a given tile
        public static List<Tile> GetNeighboringTiles(Tile tile)
        {
            var neighboringTiles = new List<Tile>();

            foreach (var direction in DirectionOffsets)
            {
                int newRow = tile.Row + direction.Value.rowOffset;
                int newColumn = tile.Column + direction.Value.colOffset;

                // Check if the new position is within the bounds of the field
                if (/*tile.*/ true)                                                 //UNFERTIG!!!!!!!!!!!!!
                {
                    neighboringTiles.Add(new Tile(newRow, newColumn));
                }
            }
            return neighboringTiles;
        }//GetNeighboringTiles

    }//Klasse


}//Namespace

