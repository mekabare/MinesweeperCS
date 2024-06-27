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

        public Image MineImage { get; private set; }
        public Image FlagImage { get; private set; }

        public bool IsMine { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int AdjacentMines { get; set; }
        #endregion

        #region Constructor
        public TileButton()
        {
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

            this.IsMine = false;
            this.IsRevealed = false;
            this.IsFlagged = false;
            this.Margin = new Thickness(1);
            AdjacentMines = 0;

            this.Background = c1;
            this.BorderBrush = c3;
            this.VerticalAlignment = VerticalAlignment.Stretch;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.BorderThickness = new Thickness(1);
            this.Cursor = Cursors.Hand;
        }
        #endregion

        #region Event Handlers
        private void TileButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFlagged)
            {
                IsRevealed = true;
                SetBackground();
            }
        }

        private void TileButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsRevealed)
            {
                IsFlagged = !IsFlagged;
                SetBackground();
                e.Handled = true; // Prevents further handling of the event
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
        }

        public void SetAdjacentMines(int adjacentMines)
        {
            AdjacentMines = adjacentMines;
            this.Content = AdjacentMines > 0 ? AdjacentMines.ToString() : string.Empty;
        }
        #endregion
    }

}