using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Minesweeper.View
{
    /// <summary>
    /// Interaction logic for GameInstance.xaml
    /// </summary>
    public partial class GameInstance : UserControl
    {
        private FieldGrid fieldGrid;
        private StackPanel stackPanel;
        Spieler spieler = new Spieler();
        private MineField _mineField;
        private bool firstClick = true;
        private DispatcherTimer timer;
        private DateTime startTime;
        private TextBlock timerTextBlock;

        private GameDifficulty _selectedDifficulty;

        public MineField MineField
        {
            get => _mineField;
            set
            {
                if (_mineField != value)
                {
                    _mineField = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public GameDifficulty SelectedDifficulty
        {
            get => _selectedDifficulty;
            set { _selectedDifficulty = value; }
        }

        DependencyPropertyChangedEventHandler PropertyChanged { get; set; }
        public FieldGrid FieldGrid { get => fieldGrid; set => fieldGrid = value; }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new DependencyPropertyChangedEventArgs());
        }

        event EventHandler GameOver;
        event EventHandler GameWon;

        public GameInstance(GameDifficulty selectedDifficulty)
        {
            InitializeComponent();

            SelectedDifficulty = selectedDifficulty;

            MineField = new MineField(selectedDifficulty.RowSize, selectedDifficulty.ColumnSize);

            MainGrid.Children.Add(InitializeField());

            stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            MainGrid.Children.Add(stackPanel);
            InitializeTimer();



            timerTextBlock = new TextBlock() { Text = "Time: 0" };
            stackPanel.Children.Add(timerTextBlock);
            stackPanel.Children.Add(new TextBlock() { Text = "Mines: " + MineField.RemainingMines });
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var elapsed = DateTime.Now - startTime;
            timerTextBlock.Text = $"Time: {elapsed.Seconds}";
        }

        protected virtual void OnGameOver()
        {
            timer.Stop();
            GameOver?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnGameWon()
        {
            timer.Stop();
            GameWon?.Invoke(this, EventArgs.Empty);
        }

        internal void OnEasyGameRequested(object sender, EventArgs e)
        {
            this.SelectedDifficulty = new Easy();
        }

        internal void OnMediumGameRequested(object sender, EventArgs e)
        {
            this.SelectedDifficulty = new Medium();
        }

        internal void OnHardGameRequested(object sender, EventArgs e)
        {
            this.SelectedDifficulty = new Hard();
        }

        public void TileButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        public void TileButton_Click(object sender, RoutedEventArgs e)
        {
            var tileButton = sender as TileButton;
            if (tileButton == null) return;

            if (firstClick)
            {
                MineField.PlaceMines(10); // Example: Place 10 mines
                firstClick = false;
                startTime = DateTime.Now;
                timer.Start();
            }

            int row = tileButton.Row;
            int col = tileButton.Column;

            if (MineField.HasMine(row, col))
            {
                MainGrid.Children.Remove(FieldGrid);
                OnGameOver();
                ReturnToMainMenu();
            }
            else
            {
                MineField.FloodFill(row, col);
                UpdateTileButtons();
                UpdateRemainingMines();
            }

            if (MineField.CheckWinCondition())
            {
                MainGrid.Children.Remove(FieldGrid);
                OnGameWon();
                ReturnToMainMenu();
            }
        }

        private void UpdateTileButtons()
        {
            foreach (TileButton tileButton in FieldGrid.Children)
            {
                tileButton.SetBackground();
            }
        }

        private FieldGrid InitializeField()
        {
            FieldGrid = new FieldGrid(SelectedDifficulty.RowSize, SelectedDifficulty.ColumnSize);
            FieldGrid.Width = FieldGrid.Columns * 45;
            FieldGrid.Height = FieldGrid.Rows * 45;
            FieldGrid.AddTiles(MineField);

            // subscribe to the Tilebuttons click events
            foreach (TileButton tileButton in FieldGrid.Children)
            {
                tileButton.Click += TileButton_Click;
                tileButton.MouseRightButtonDown += TileButton_MouseRightButtonDown;
            }

            return FieldGrid;
        }

        private void UpdateRemainingMines()
        {
            stackPanel.Children.Clear();
            stackPanel.Children.Add(timerTextBlock);
            stackPanel.Children.Add(new TextBlock() { Text = "Mines: " + MineField.RemainingMines });
        }

        private void ReturnToMainMenu()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new MainMenu();
            }
        }
    }

    public partial class FieldGrid : Grid
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public FieldGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            // Set the grid's row and column definitions
            for (int i = 0; i < Rows; i++)
            {
                RowDefinition rowDefinition = new RowDefinition()
                {
                    Height = new GridLength(1, GridUnitType.Star)
                };
                RowDefinitions.Add(rowDefinition);
            }
            for (int i = 0; i < Columns; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star)
                };
                ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        public void AddTiles(MineField mineField)
        {
            // Add the tiles to the grid, population goes in row-major order
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    var tileButton = new TileButton(mineField) { Row = i, Column = j };
                    Children.Add(tileButton);
                    SetRow(tileButton, i);
                    SetColumn(tileButton, j);
                }
            }
        }
    }
 }
