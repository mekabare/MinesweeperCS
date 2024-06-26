using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minesweeper.View
{
    /// <summary>
    /// Interaction logic for EasyMode.xaml
    /// </summary>
    public partial class EasyMode : Page
    {
        GameDifficulty difficulty = new Easy();
        public EasyMode()
        {
            InitializeComponent();

            FieldGrid fieldGrid = new FieldGrid();
            fieldGrid.Rows = difficulty.FieldSize[0, 0];
            fieldGrid.Columns = difficulty.FieldSize[0, 1];
            fieldGrid.AddTiles();
        }
    }

    public partial class TileButton : Button
    {
        DependencyProperty IsMineProperty = DependencyProperty.Register("IsMine", typeof(bool), typeof(TileButton));
        DependencyProperty IsFlaggedProperty = DependencyProperty.Register("IsFlagged", typeof(bool), typeof(TileButton));
        DependencyProperty IsRevealedProperty = DependencyProperty.Register("IsRevealed", typeof(bool), typeof(TileButton));



        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsMine { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsRevealed { get; set; }
        public int AdjacentMines { get; set; }

        public TileButton()
        {
            IsMine = false;
            IsFlagged = false;
            IsRevealed = false;
            AdjacentMines = 0;

            this.Height = 46;
            this.Width = 46;
            this.Background = Brushes.Beige;
            this.BorderBrush = Brushes.Wheat;
            this.BorderThickness = new Thickness(1);
        }
    }

    public partial class FieldGrid : Grid
    {
        private int rows;
        private int columns;

        public int Rows { get; set; }
        public int Columns { get; set; }

        public FieldGrid()
        {
            for (int i = 0; i < Rows; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                this.RowDefinitions.Add(row);
            }
            for (int j = 0; j < Columns; j++)
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
    }

}