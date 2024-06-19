using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsMine { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
            IsMine = false;
            IsRevealed = false;
            IsFlagged = false;
        }
    }
}
