using System;
using System.Collections.Generic;

/// <summary>
/// Automatisiert die Lokalisierung der benachbarten Zellen
/// </summary>
namespace Minesweeper
{
static class CellHelper
    {
        // Dictionary of offsets for each direction from enum DirectionType
        private static readonly Dictionary<Direction, (int rowOffset, int colOffset)> DirectionOffsets = new Dictionary<Direction, (int, int)>
        {
            { Direction.N, (-1, 0) },
            { Direction.NE, (-1, 1) },
            { Direction.E, (0, 1) },
            { Direction.SE, (1, 1) },
            { Direction.S, (1, 0) },
            { Direction.SW, (1, -1) },
            { Direction.W, (0, -1) },
            { Direction.NW, (-1, -1) }
        };

        // Enum for shorthand names for directions
        public enum Direction
        {
            N, NE, E, SE, S, SW, W, NW
        }

        // Method to get adjacent cells based on a given cell
        public static List<Cell> GetAdjacentCells(Cell cell)
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
            return adjacentCells;
        }
    }

}