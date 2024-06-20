namespace Minesweeper
{
    /// <summary>
    /// Die Grenzen des Spielfelds, um zu überprüfen, ob eine Zelle innerhalb des Spielfelds liegt
    /// </summary>
    public struct Bounds
    {
        public int Rows { get; }
        public int Columns { get; }

        public Bounds(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }

        public bool IsWithin(int row, int column)
        {
            return row >= 0 && row < Rows && column >= 0 && column < Columns;
        }
    }
}
