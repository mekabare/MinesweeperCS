﻿using System;
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
                if (field != null)
                {
                    field = value;
                }
                else { throw new NullReferenceException("Field cannot be null!"); }
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
            Difficulty = difficulty;            //Variablen aus difficulty übernehmen
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

        /// <summary>
        /// Platziert die Minen auf dem Spielfeld und verteilt die Zahlen im Feld. 
        /// ACHTUNG: Deckt keine Felder auf. L_Click(...) muss trotzdem aufgerufen werden!
        /// </summary>
        /// <param name="Row">x-pos des Coursors</param>
        /// <param name="Column">y-pos des Coursors</param>
        public void PlaceMines(int Row, int Column)
        {
            int bombsLeft = Difficulty.TotalMines;
            bool finished = false;

            //Zufallsgenerator
            var rand = new Random(2349);

            while (!finished)   //Spielfeld solange durchlaufen, bis alle Minen platziert worden sind.
            {
                //Feld durchlaufen
                for (int i = 0; i < difficulty.RowSize; i++)
                {
                    for (int j = 0; j < difficulty.ColumnSize; j++)
                    {
                        if (bombsLeft == 0) { finished = true; break; } //Wenn alle Bomben platziert wurden

                        if (Field[i, j] != null)    //Existiert das Feld?
                        {
                            if (i != Row && j != Column ||      //
                            i != Row && j - 1 != Column ||      //
                            i != Row && j + 1 != Column ||      //
                            i - 1 != Row && j != Column ||      //
                            i - 1 != Row && j - 1 != Column ||  //
                            i - 1 != Row && j + 1 != Column ||  //
                            i + 1 != Row && j != Column ||      //
                            i + 1 != Row && j - 1 != Column ||  //
                            i + 1 != Row && j + 1 != Column)    //Ist man auf dem Couror, oder in dessen Nähe?
                            {
                                if (Field[i, j].IsMine == false)    //Hat es schon eine Mine?
                                {
                                    if (rand.Next(1001) < 25)       //Generator in der Schwelle
                                    {
                                        Field[i, j].IsMine = true;  //Mine Platzieren
                                        bombsLeft--;                //Mitzählen
                                    }
                                }//IsMine
                            }//Coursor
                        }//Exist
                    }//for Column
                    if (finished) { break; }
                }//for Row
            }//while(!finished)

            MineCounter();  //Umliegende Minen zählen

        }//PlaceMines(...)


        /// <summary>
        /// Counts the Mines surrounding a Tile for every Tile and writes the number equal to the amount of counted Mines into the Tile
        /// </summary>
        private void MineCounter()
        {
            int umliegende = 0;

            //Feld durchlaufen
            for (int i = 0; i < difficulty.RowSize; i++)
            {
                for (int j = 0; j < difficulty.ColumnSize; j++)
                {
                    if (Field[i, j] != null) { if (Field[i, j].IsMine == true) { umliegende++; } }          //
                    if (Field[i, j-1] != null) { if (Field[i, j-1].IsMine == true) { umliegende++; } }      //
                    if (Field[i, j+1] != null) { if (Field[i, j+1].IsMine == true) { umliegende++; } }      //
                    if (Field[i-1, j] != null) { if (Field[i-1, j].IsMine == true) { umliegende++; } }      //
                    if (Field[i-1, j-1] != null) { if (Field[i-1, j-1].IsMine == true) { umliegende++; } }  //
                    if (Field[i-1, j+1] != null) { if (Field[i-1, j+1].IsMine == true) { umliegende++; } }  //
                    if (Field[i+1, j] != null) { if (Field[i+1, j].IsMine == true) { umliegende++; } }      //
                    if (Field[i+1, j-1] != null) { if (Field[i+1, j-1].IsMine == true) { umliegende++; } }  //
                    if (Field[i+1, j+1] != null) { if (Field[i+1, j+1].IsMine == true) { umliegende++; } }  //umliegende Bomben zählen

                    Field[i, j].AdjacentMines = umliegende; //gezählte Bomben in das Tile schreiben

                    umliegende = 0; //umliegende zurücksetzen
                }//for Column
            }//for Row
        }//MineCounter()

        #endregion Methoden


    }//Klasse
  
}//Namespace


