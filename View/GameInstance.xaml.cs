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
    /// Interaction logic for GameInstance.xaml
    /// </summary>
    public partial class GameInstance : UserControl
    {
        FieldGrid fieldGrid;
        StackPanel StackPanel = new StackPanel();
        Spieler spieler = new Spieler();


        event EventHandler GameOver;
        event EventHandler GameWon;

        public GameInstance(GameDifficulty gameDifficulty)
        {
            InitializeComponent();

            MineField mineField = new MineField(gameDifficulty);
            fieldGrid = new FieldGrid(mineField);
            fieldGrid.AddTiles();

            //fieldGrid = new FieldGrid(difficulty.RowSize,difficulty.ColumnSize );
            //fieldGrid.Width = fieldGrid.Columns * 45 ;
            //fieldGrid.Height = fieldGrid.Rows * 45;
            //fieldGrid.AddTiles();
            //fieldGrid.SetMines(difficulty.TotalMines);

            MainGrid.Children.Add(fieldGrid);

            MainGrid.Children.Add(StackPanel);
        }

        protected virtual void OnGameOver()
        {
            GameOver?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnGameWon() {

            GameWon?.Invoke(this, EventArgs.Empty);
        }



    }

    public partial class FieldGrid : Grid
    {
        private int rows;
        private int columns;
        public int Rows { get; set; }
        public int Columns { get; set; }

        private MineField mineFieldModel;

        private GameDifficulty gameDifficulty;

        public MineField MineFieldModel
        {
            get => mineFieldModel;
            set { mineFieldModel = value; }
        }

        public GameDifficulty GameDifficulty
        {
            get => gameDifficulty;
            set { gameDifficulty = value; }
        }

        public FieldGrid(MineField mineFieldModel)
        {
            for (int i = 0; i < rows; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                this.RowDefinitions.Add(row);
            }

            for (int i = 0; i < columns; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(1, GridUnitType.Star);
                this.ColumnDefinitions.Add(column);
            }
        }
        public void AddTiles() {
            MineFieldModel = new MineField(gameDifficulty);
            for (int i = 0; i < MineFieldModel.MaxRow; i++)
            {
                for (int j = 0; j < MineFieldModel.MaxColumn; j++)
                {
                    TileButton tileView = new TileButton();
                    Tile tileModel = new Tile(i,j);
                    tileView.Row = tileModel.Row;
                    tileView.Column = tileModel.Column;
                    

                    MineFieldModel.Field[i, j] = tileModel;
                    tileView.Margin = new Thickness(0);
                    this.Children.Add(tileView);
                    Grid.SetRow(tileView, i);
                    Grid.SetColumn(tileView, j);

                }
            }
        }
    }
}