using System;

namespace Minesweeper
{/// <summary>
/// GameDifficulty als Klasse, die die Schwierigkeit des Spiels definiert und die Feldgröße und die Anzahl der Minen festlegt
/// </summary>
    public abstract class GameDifficulty
    {
        private int[,] fieldSize;
        private int totalMines;
        private string nameString;

        public int[,] FieldSize { get => fieldSize; set => fieldSize = value; }
        public int TotalMines { get => totalMines; set => totalMines = value; }
        public string NameString { get => nameString; set => nameString = value; }

        public GameDifficulty()
        {
            FieldSize = new int[0, 0];
            TotalMines = 0;
            NameString = "";
        }
    }

    public class Easy : GameDifficulty
    {
        public Easy() : base()
        {
            FieldSize = new int[9, 9];
            TotalMines = 10;
            NameString = "Easy";
        }
    }

    public class Medium : GameDifficulty
    {
        public Medium() : base()
        {
            FieldSize = new int[16, 16];
            TotalMines = 40;
            NameString = "Medium";
        }
    }

    public class Hard : GameDifficulty
    {
        public Hard() : base()
        {
            FieldSize = new int[16, 30];
            TotalMines = 99;
            NameString = "Hard";
        }
    }
}


#region
// IDEE: Man kann den User eigene Schwierigkeitsgrade erstellen lassen

/* 
 * public class Custom : GameDifficulty 
 * {
 *  public Custom(int rows, int columns, int mines) : base()
 *  {
 *      this.FieldSize = new int[rows, columns];
 *      this.TotalMines = mines;
 *      this.NameString = "Custom";
 *  }
 * 
 * }
 */
#endregion
