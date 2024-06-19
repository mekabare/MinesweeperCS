using System;
using System.Collections.Generic;

/// <summary>
/// This class automates the process of getting the adjacent cells of a cell
/// </summary>
namespace Minesweeper
{
static class CellHelper
    {
        // Dictionary of offsets for each direction from enum DirectionType
        private static readonly Dictionary<CellHelper, (int rowOffset, int colOffset)> DirectionOffsets = new Dictionary<CellHelper, (int, int)>
        {
            { CellHelper.N, (-1, 0) },
            { CellHelper.NE, (-1, 1) },
            { CellHelper.E, (0, 1) },
            { CellHelper.SE, (1, 1) },
            { CellHelper.S, (1, 0) },
            { CellHelper.SW, (1, -1) },
            { CellHelper.W, (0, -1) },
            { CellHelper.NW, (-1, -1) }
        };

        // Enum for shorthand names for directions
        public enum CellHelper
        {
            N, NE, E, SE, S, SW, W, NW
        }

        // Method to get adjacent cells based on a given cell
        public static List<Cell> GetAdjacentCells(Cell cell, MineField)
        {
            var adjacentCells = new List<Cell>();

            foreach (var direction in DirectionOffsets)
            {
                int newRow = cell.Row + direction.Value.rowOffset;
                int newColumn = cell.Column + direction.Value.colOffset;

                // Check if the new position is within the bounds of the field
                if (newRow >= 0 && newRow < maxRows && newColumn >= 0 && newColumn < maxColumns)
                {
                    adjacentCells.Add(new Cell(newRow, newColumn));
                }
            }

            return adjacentCells;
        }
    }

}