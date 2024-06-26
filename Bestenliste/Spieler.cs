using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Minesweeper
{
    /// <summary>
    /// Ein Spieler kann eine Bestenzeit haben und so in die Bestenliste einsortiert werden
    /// </summary>
    public class Spieler
    {
        #region Felder
        private string name;                //Namen des Spielers

        private int time;                   //Zeit, die er für ein Spiel gebraucht hat

        private GameDifficulty difficulty;  //Schwierigkeit, auf die der Spieler gespielt hat

        //private int score;                //Score, den der Spieler erziehlt hat. Errechneter Wert, kein Set.
        #endregion Felder



        #region Konstruktoren
        /// <summary>
        /// Default-Konstruktor
        /// </summary>
        public Spieler()
        {
            Name = "NUL";
            Time = 0;
            difficulty = new Easy();
        }//Default


        /// <summary>
        /// Allg. Konstruktor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="time"></param>
        /// <param name="difficulty"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Spieler(string name, int time, GameDifficulty difficulty)
        {
            Name = name;
            Time = time;
            Difficulty = difficulty;
        }//Allg.


        /// <summary>
        /// Kopier-Konstruktor
        /// </summary>
        /// <param name="player"></param>
        public Spieler(Spieler player)
        {
            Name = player.Name;
            Time = player.Time;
            Difficulty = player.Difficulty;
        }//Kopier
        #endregion Konstruktoren



        #region Methoden
        /*
        public void calculateScore()    //Wenn score implementiert wird
        {
            int diffValue = 0;

            switch(Difficulty.ToString())   //Bestimmung des Rechenfaktors
            {
                case "Easy": {
                        diffValue = 1;
                        break;
                    }
                case "Medium": {
                        diffValue = 2;
                        break;
                    }
                case "Hard": {
                        diffValue = 3;
                        break;
                    }
                default: { break; }
            }//switch

            Score = diffValue / Time;   //Berechnung des Scores
        }*///calculateScore()
        

        #endregion Methoden



        #region Getter und Setter
        public string Name
        {
            get => name;
            set
            {
                int strlen = value.Length;
                bool ok = true;

                for(int i = 0; i < strlen; i++)
                {
                    if (value[i] == '0' ||
                        value[i] == '1' ||
                        value[i] == '2' ||
                        value[i] == '3' ||
                        value[i] == '4' ||
                        value[i] == '5' ||
                        value[i] == '6' ||
                        value[i] == '7' ||
                        value[i] == '8' ||
                        value[i] == '9' ||
                        value[i] == '^' ||
                        value[i] == '°' ||
                        value[i] == '!' ||
                        value[i] == '"' ||
                        value[i] == '§' ||
                        value[i] == '$' ||
                        value[i] == '%' ||
                        value[i] == '&' ||
                        value[i] == '/' ||
                        value[i] == '(' ||
                        value[i] == ')' ||
                        value[i] == '=' ||
                        value[i] == '?' ||
                        value[i] == '`' ||
                        value[i] == '´' ||
                        value[i] == '+' ||
                        value[i] == '*' ||
                        value[i] == '@' ||
                        value[i] == '€' ||
                        value[i] == '<' ||
                        value[i] == '>' ||
                        value[i] == '|' ||
                        value[i] == '.' ||
                        value[i] == ',' ||
                        value[i] == ';' ||
                        value[i] == ':' ||
                        value[i] == '-' ||
                        value[i] == '_' ||
                        value[i] == '#' ||
                        value[i] == '~' ||
                        value[i] == '²' ||
                        value[i] == '³' ||
                        value[i] == '{' ||
                        value[i] == '[' ||
                        value[i] == ']' ||
                        value[i] == '}' ||
                        value[i] == '\n' ||
                        value[i] == '\t')
                    { 
                        ok = false;
                        throw new ArgumentException("Name is not allowed to contain numbers or special characters!", "Name");
                    }//if
                }//for
                if (ok) { Name = value; }
            }//set
        }//Name

        public int Time
        {
            get => time;
            set
            {
                if (value >= 0) { time = value; }
                else { throw new ArgumentOutOfRangeException("time", "A Player cannot achive less than zero time!"); }
            }
        }//Time

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

    }//Klasse
}//Namespace
