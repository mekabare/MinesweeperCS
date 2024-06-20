using System;

namespace Minesweeper
{/// <summary>
/// Difficulty als Klasse, die die Schwierigkeit des Spiels definiert und die Feldgröße und die Anzahl der Minen festlegt
/// </summary>
    public class Difficulty
    {
        public int Rows;
        public int Columns;
        public int Mines;

        // Enum der Schwierigkeitsstufen für developerfreundliche Auswahl
        public enum Level
        {
            Easy = '0',
            Medium = '1',
            Hard = '2'

        }

        // Konstruktor, der die Feldgroesse des Spiels festlegt
        public Difficulty(Level lvl)
        {
            switch (lvl)
            {
                case Level.Easy:
                    Rows = 9;
                    Columns = 9;
                    Mines = 10;
                    break;
                case Level.Medium:
                    Rows = 16;
                    Columns = 16;
                    Mines = 40;
                    break;
                case Level.Hard:
                    Rows = 16;
                    Columns = 30;
                    Mines = 99;
                    break;
            }
        }
    }
  
}



