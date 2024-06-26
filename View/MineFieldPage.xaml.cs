using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minesweeper.View
{
    /// <summary>
    /// Interaction logic for MineFieldPage.xaml
    /// </summary>
    public partial class MineFieldPage : Page
    {
        GameDifficulty difficulty = new Easy();
        FieldGrid fieldGrid;
        StackPanel StackPanel = new StackPanel();
        public MineFieldPage(GameDifficulty difficulty)
        {
            InitializeComponent();

            fieldGrid = new FieldGrid(difficulty.RowSize,difficulty.ColumnSize );
            fieldGrid.Width = 500;
            fieldGrid.Height = 500;
            fieldGrid.AddTiles();
            fieldGrid.SetMines(difficulty.TotalMines);

            MainGrid.Children.Add(fieldGrid);

            MainGrid.Children.Add(StackPanel);
        }
       
        
    }

    public partial class FieldGrid : Grid
    {
        private int rows;
        private int columns;
        public int Rows { get; set; }
        public int Columns { get; set; }


        public FieldGrid(int rows, int columns)
        {
            this.Rows = rows; // Set the instance property
            this.Columns = columns; // Set the instance property

            for (int i = 0; i < this.Rows; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                this.RowDefinitions.Add(row);
            }
            for (int j = 0; j < this.Columns; j++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(1, GridUnitType.Star);
                this.ColumnDefinitions.Add(column);
            }
        }
        public void AddTiles() { 
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    TileButton tile = new TileButton();
                    tile.Row = i;
                    tile.Column = j;
                    this.Children.Add(tile);
                    Grid.SetRow(tile, i);
                    Grid.SetColumn(tile, j);
                }
            }
        }

        public void SetMines(int totalMines)
        {
            Random random = new Random();
            int mines = 0;
            while (mines < totalMines)
            {
                int row = random.Next(0, Rows);
                int column = random.Next(0, Columns);
                TileButton tile = (TileButton)this.Children.Cast<TileButton>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
                if (!tile.IsMine)
                {
                    tile.IsMine = true;
                    mines++;
                }
            }
        }
    }

}