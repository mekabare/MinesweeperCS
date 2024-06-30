using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using 

namespace Minesweeper
{
    
    /// <summary>
    /// Klasse, die das Spielfeld repräsentiert
    /// </summary>
    /// <param name="Field">2D-Array von Zellen, die das Spielfeld repräsentieren</param>
    /// <param name="MaxRow">Anzahl der Zeilen des Spielfelds</param>
    /// <param name="MaxColumn">Anzahl der Spalten des Spielfelds</param>
    /// <param name="Difficulty">Schwierigkeitsgrad des Felds</param>
    public class MineField : INotifyPropertyChanged
    {
        #region Felder
        private int _maxRow = 0, maxColumn = 0;  //Groesse des Spielfleds

        private Tile[,] _field;                  //Das Spielfeld

        private GameDifficulty _difficulty;      //Schwierigkeitsgrad

        private int _totalMines;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion



        #region Getter und Setter

        public int MaxRow
        {
            get => _maxRow;
            set
            {
                if (value >= 0) { _maxRow = value; }
                else { throw new ArgumentOutOfRangeException("_maxRow", "Value must be larger or equal than zero!"); }
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
            get => _field;
            set
            {
                if (value != null)
                {
                    _field = value;
                }
                //else { throw new NullReferenceException("Field cannot be null!"); }
            }
        }//Field

        public int TotalMines
        {

            get => _totalMines;
            set
            {
                if (value >= 0) { _totalMines = value; }
                else { throw new ArgumentOutOfRangeException("_totalMines", "Value must be larger or equal than zero!"); }
            }
        }
        
        internal GameDifficulty Difficulty
        {
            get => _difficulty;
            set
            {
              if (_difficulty is Easy || _difficulty is Medium || _difficulty is Hard || _difficulty is Custom) { _difficulty = value; }
                else { throw new ArgumentException("Difficulty object must be an Easy-, Medium-, Hard-, or Custom-Objekt.", "Difficulty"); }

            }
        }//Difficulty
        #endregion Getter und Setter

        

        #region Konstruktoren

        /// <summary>
        /// Allg. Konstruktor. Erzeugt ein leeres Feld.
        /// </summary>
        /// <param name="difficulty">Schwierigkeitsgrad des Felds</param>
        public MineField()
        {
           this.Difficulty = _difficulty;
           AddTiles();
        }//Allg.

        #endregion Konstruktoren



        #region Methoden

        /// <summary>
        /// Methode um Field mit Tiles zu fuellen
        /// </summary>
        /// <exception cref="ArgumentException"></exception>

        public void AddTiles()
        {

            switch (_difficulty)
            {
                case Easy e:
                    Difficulty = new Easy();
                    break;
                case Medium m:
                    Difficulty = new Medium();
                    break;
                case Hard h:
                    Difficulty = new Hard();
                    break;
                case Custom c:
                    Difficulty = new Custom(MaxRow, MaxColumn, TotalMines);
                    break;
                default:
                    throw new ArgumentException("Difficulty object must be an Easy-, Medium-, Hard-, or Custom-Objekt.", "Difficulty");
            }

            MaxRow = _difficulty.RowSize;
            MaxColumn = _difficulty.ColumnSize;

            Field = new Tile[_difficulty.RowSize, _difficulty.ColumnSize];    //Arraygroesse setzen

            for (int i = 0; i < _difficulty.RowSize; i++)
            {
                for (int j = 0; j < _difficulty.ColumnSize; j++)
                {
                    // populate Field with Mines
                    Field[i, j] = new Tile(i, j);

                }//for Column
            }//for Row

        }

        /// <summary>
        /// Platziert die Minen auf dem Spielfeld und verteilt die Zahlen im Feld. 
        /// ACHTUNG: Deckt keine Felder auf. OpenTile(...) muss trotzdem aufgerufen werden!
        /// </summary>
        ///
       /* public void PlaceMines(int Row, int Column)
        {
            int minesLeft = this.Difficulty.TotalMines;
            bool finished = false;

            //Zufallsgenerator
            var rand = new Random(2349);

            while (!finished)   //Spielfeld solange durchlaufen, bis alle Minen platziert worden sind.
            {
                //Feld durchlaufen
                for (int i = 0; i < _difficulty.RowSize; i++)
                {
                    for (int j = 0; j < _difficulty.ColumnSize; j++)
                    {
                        if (minesLeft == 0) { finished = true; break; } //Wenn alle Bomben platziert wurden

                        else if (Field[i, j] != null)    //Existiert das Feld?
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
                                        minesLeft--;                //Mitzählen
                                    }
                                }//IsMine
                            }//Coursor
                        }//Exist
                    }//for Column
                    if (finished) { break; }
                }//for Row
            }//while(!finished)

        }//PlaceMines(...) */


        /// <summary>
        /// Platziert Mienen, ignoriert das geklickte Tile
        /// </summary>
        public void PlaceMines(Tile clickedTile)
        {
            int row, column;
            int minesLeft = this.Difficulty.TotalMines;

            //Zufallsgenerator
            var seed = new Random(2349);
            var rand = new Random(seed.Next());

            while (minesLeft > 0)
            {
                row = rand.Next(0, MaxRow);
                column = rand.Next(0, MaxColumn);

                if (Field[row, column].IsMine == false || Field[row, column] != clickedTile)
                {
                    Field[row, column].IsMine = true;
                    minesLeft--;
                }
            }
        }

        /// <summary>
        /// Rekursive Methode, der mindestens eine Miene oeffnet
        /// </summary>
        /// <param name="clickedTile"></param>
        public void BucketFill (Tile clickedTile)
        {
            if (clickedTile != null)
            {
                if (clickedTile.AdjacentMines == 0)
                {
                    clickedTile.IsRevealed = true;
                    for (int i = 0; i < clickedTile.AdjacentTiles.Length; i++)
                    {
                        if (clickedTile.AdjacentTiles[i] != null)
                        {
                            if (clickedTile.AdjacentTiles[i].IsRevealed == false)
                            {
                                BucketFill(clickedTile.AdjacentTiles[i]);
                            }
                        }
                    }
                }
                else
                {
                    clickedTile.IsRevealed = true;
                }
            }
        }



        /// <summary>
        /// Toggles the IsFlagged bool of a Tile in the Field
        /// </summary>
        /// <param name="row">x-pos of the Tile</param>
        /// <param name="column">y-pos of the Tile</param>
        /// <exception cref="ArgumentException">If the Bool couldn't be determined</exception>
        public void ToggleFlag(int row, int column)
        {
            if (Field[row, column] != null) //Existiert das Feld?
            {
                if (Field[row, column].IsRevealed == false) //Ist es noch verdeckt?
                {
                    if (Field[row, column].IsFlagged == false) { Field[row, column].IsFlagged = true; }
                    else if (Field[row, column].IsFlagged == true) { Field[row, column].IsFlagged = false; }
                    else { throw new ArgumentException("Couldn't determine state of bool.", "IsFlagged"); }
                }//ff (IsRevealed)
            }//if (!= null)
        }//ToggleFlag(...)
        

        #endregion Methoden

    }//Klasse
}//Namespace


