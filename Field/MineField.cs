using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Minesweeper
{
    public class MineField
    {
        public Cell Cell { get; set; } 
        public List<List<Cell>> Field { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public Difficulty Difficulty { get; set; }

        // In MineField class
        public Bounds Bounds => new Bounds(Rows, Columns);

        public MineField(Difficulty difficulty)
        {
            Difficulty = difficulty;
            Rows = difficulty.Rows;
            Columns = difficulty.Columns;
            Field = new List<List<Cell>>();
            for (int i = 0; i < Rows; i++)
            {
                Field.Add(new List<Cell>());
                for (int j = 0; j < Columns; j++)
                {
                    Field[i].Add(new Cell(i, j));
                }
            }
            PlaceMines();
            CalculateAdjacentMines();
        }

        /// <summary>
        /// Places mines randomly on field in accordance to difficulty
        /// </summary>
        private void PlaceMines()
        {
            Random random = new Random();
            int minesPlaced = 0;
            while (minesPlaced < Difficulty.Mines)
            {
                // Randomly select a cell, tries again if cell already has a mine, ends when all mines are placed
                int row = random.Next(Rows);
                int column = random.Next(Columns);
                if (!Field[row][column].IsMine)
                {
                    Field[row][column].IsMine = true;
                    minesPlaced++;
                }
            }
        }

        /// <summary>   
        /// Checks for mines directly adjacent to a cell and returns number to display
        /// </summary>
        private void CalculateAdjacentMines(Cell cell)
        {
            int row = cell.Row;
            int column = cell.Column;
            int adjacentMines = 0;

            foreach 

            // Check all 8 directions for mines

        }



    }
  
}



