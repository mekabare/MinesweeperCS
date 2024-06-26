using System;

namespace Minesweeper
{   
    /// <summary>
    /// GameDifficulty als Klasse, die die Schwierigkeit des Spiels definiert und die Feldgröße und die Anzahl der Minen festlegt
    /// </summary>
    public abstract class GameDifficulty
    {
        #region Felder
        private int[,] fieldSize;
        private int totalMines;
        //private string nameString;    //wird durch ToString() Methode ersetzt, für die abstract Programmierung.
        #endregion Felder


        #region Getter und Setter

        public int[,] FieldSize
        {
            get => fieldSize;
            set { fieldSize = value; }      //pot. set Leer setzen und durch extra Funktion ersetzen.
        }//FieldSize

        public int TotalMines
        {
            get => totalMines;
            set { totalMines = value; }     //Eingabepruefung einfügen, oder set Leer setzen und durch extra Funktion ersetzen
        }//TotalMines

        #endregion Getter und Setter



        #region Konstruktoren

        /// <summary>
        /// default - Konstruktor
        /// </summary>
        public GameDifficulty()
        {
            FieldSize = new int[0, 0];
            TotalMines = 0;
        }//default

        /// <summary>
        /// Allg. Konstruktor
        /// </summary>
        /// <param name="fieldSize"></param>
        /// <param name="totalMines"></param>
        public GameDifficulty(int[,] fieldSize, int totalMines)
        {
            FieldSize = fieldSize;
            TotalMines = totalMines;
        }//Allg.

        #endregion Konstruktoren



        #region abstract Methoden

        /// <summary>
        /// Gibt den Schwierigkeitsgrad als String zurück.
        /// </summary>
        /// <returns></returns>
        public abstract override string ToString();

        #endregion abstract Methoden

    }//abstract Class

    //---------------------------------------------------------------------------------------------------

    public class Easy : GameDifficulty
    {
        public Easy() : base()
        {
            FieldSize = new int[9, 9];
            TotalMines = 10;
        }

        public override string ToString() { return "EasyMode"; }

    }//Klasse


    public class Medium : GameDifficulty
    {
        public Medium() : base()
        {
            FieldSize = new int[16, 16];
            TotalMines = 40;
        }

        public override string ToString() { return "Medium"; }

    }//Klasse


    public class Hard : GameDifficulty
    {
        public Hard() : base()
        {
            FieldSize = new int[16, 30];
            TotalMines = 99;
        }

        public override string ToString() { return "Hard"; }

    }//Klasse


    public class Custom : GameDifficulty
    {
        /// <summary>
        /// Allg. Konstruktor
        /// </summary>
        /// <param name="rows">Menge an Reihen des Spielfelds</param>
        /// <param name="columns">Menge an Spalten des Spielfelds</param>
        /// <param name="mines">Menge an Minen</param>
        public Custom(int rows, int columns, int mines) : base(new int[rows, columns], mines) { }

        public override string ToString() { return "Custom"; }

    }//Klasse

}//Namespace
