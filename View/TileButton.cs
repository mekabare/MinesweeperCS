using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Minesweeper.View
{
    public partial class TileButton : Button
    {
        #region Properties 
        Brush c1 = new SolidColorBrush(Color.FromRgb(250, 243, 240)); // Light Pink
        Brush c2 = new SolidColorBrush(Color.FromRgb(212, 226, 212)); // Teal
        Brush c3 = new SolidColorBrush(Color.FromRgb(255, 202, 204)); // Deep Pink
        Brush c4 = new SolidColorBrush(Color.FromRgb(219, 196, 240)); // Purple
        Brush c5 = new SolidColorBrush(Brushes.LightCoral.Color);

        private Tile tileModel;
        private MineField mineField; // Reference to the MineField object

        public Tile TileModel
        {
            get => tileModel;
            set { tileModel = value; }
        }

        public Image MineImage { get; private set; }
        public Image FlagImage { get; private set; }

        public bool IsMine => mineField.HasMine(Row, Column); // Property to check if the tile has a mine
        public bool IsRevealed => mineField.IsRevealed(Row, Column); // Property to check if the tile is revealed
        public bool IsFlagged => mineField.IsFlagged(Row, Column); // Property to check if the tile is flagged
        public int Row { get; set; }
        public int Column { get; set; }
        public int AdjacentMines => mineField.GetAdjacentMines(Row, Column); // Property to get adjacent mines
        #endregion

        #region Constructor
        public TileButton(MineField sharedMineField)
        {
            this.mineField = sharedMineField;
            // Initialize the content grid and images within the constructor
            Grid contentGrid = new Grid();
            MineImage = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Mine.png")),
                Visibility = Visibility.Collapsed // Initially hidden
            };
            FlagImage = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Flagged.png")),
                Visibility = Visibility.Collapsed // Initially hidden
            };

            contentGrid.Children.Add(MineImage);
            contentGrid.Children.Add(FlagImage);

            // Set the content of the button to the grid
            this.Content = contentGrid;

            this.Click += TileButton_Click;
            this.MouseRightButtonDown += TileButton_MouseRightButtonDown;

            this.Margin = new Thickness(1);

            this.Background = c1;
            this.BorderBrush = c3;
            this.VerticalAlignment = VerticalAlignment.Stretch;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.BorderThickness = new Thickness(1);
            this.Cursor = Cursors.Hand;
        }
        #endregion

        #region Event Handlers
        public void TileButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFlagged)
            {
                mineField.FloodFill(Row, Column);
                SetBackground();
            }
        }

        public void TileButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsRevealed)
            {
                mineField.SetFlagged(Row, Column, !IsFlagged);
                SetBackground();
            }
        }
        #endregion

        #region Methods
        public void SetBackground()
        {
            this.Background = IsRevealed ? c3 : c1;
            this.BorderBrush = IsRevealed ? c3 : c3;
            MineImage.Visibility = IsRevealed && IsMine ? Visibility.Visible : Visibility.Collapsed;
            FlagImage.Visibility = IsFlagged ? Visibility.Visible : Visibility.Collapsed;
            if (IsRevealed && !IsMine)
            {
                this.Content = AdjacentMines > 0 ? AdjacentMines.ToString() : string.Empty;
            }
        }
        #endregion
    }

}