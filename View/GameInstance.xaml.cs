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
        private Spieler spieler = new Spieler();
        private MineField _mineField;
        private bool firstClick = true;
        private DispatcherTimer timer;
        private DateTime startTime;
        private MainWindow window;
        private bool isLost = false;

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

            window = Application.Current.MainWindow as MainWindow;

            SelectedDifficulty = selectedDifficulty;

            MineField = new MineField(selectedDifficulty.RowSize, selectedDifficulty.ColumnSize);

            MainGrid.Children.Add(InitializeField());

            stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            MainGrid.Children.Add(stackPanel);
            InitializeTimer();



            window.TimerDisplay.Content = "0";
            window.MineCounter.Content = selectedDifficulty.TotalMines;

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
            window.TimerDisplay.Content = $"{elapsed.Seconds}";
        }

        protected virtual void OnFirstClick(int row, int col)
        {
            MineField.PlaceMines(SelectedDifficulty.TotalMines, row, col);
            firstClick = false;
            startTime = DateTime.Now;
            timer.Start();
        }
        protected virtual void OnGameOver()
        {
            timer.Stop();
           
            MineField.RevealAllMines();
            UpdateTileButtons();

            isLost = true;

            MessageBox.Show("You died.");
            MainGrid.Children.Remove(FieldGrid);
            ReturnToMainMenu();
            GameOver?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnGameWon()
        {
            if (isLost = false) //Wurde das Spiel noch nicht verloren.
            {
                timer.Stop();
                BestenlisteDialog bestenlisteDialog = new BestenlisteDialog(true);
                bestenlisteDialog.ShowDialog();
                GameWon?.Invoke(this, EventArgs.Empty);
                if (bestenlisteDialog.DialogResult == true)
                {
                    ReturnToMainMenu();
                }
            }
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

            var tileButton = sender as TileButton;
            if (tileButton == null) return;

            int row = tileButton.Row;
            int col = tileButton.Column;

            MineField.ToggleFlag(row, col);

            UpdateTileButtons();
            UpdateRemainingMines();
        }

        public void TileButton_Click(object sender, RoutedEventArgs e)
        {
            var tileButton = sender as TileButton;
            if (tileButton == null) return;

            if (firstClick)
            {
               OnFirstClick(tileButton.Row, tileButton.Column);
            }

            int row = tileButton.Row;
            int col = tileButton.Column;

            if (MineField.HasMine(row, col))
            {
                OnGameOver();
            }
            else
            {
                MineField.FloodFill(row, col);
                UpdateTileButtons();
                UpdateRemainingMines();
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

            window.MineCounter.Content = MineField.RemainingMines;

        }

        private void ReturnToMainMenu()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.LoadMainMenu();
                mainWindow.Logo.Visibility = Visibility.Visible;
                mainWindow.TimerDisplay.Visibility = Visibility.Hidden;
                mainWindow.MineCounter.Visibility = Visibility.Hidden;
            }
            ResetGame();
        }
        public void ResetGame()
        {
           
            firstClick = true;

           
            if (timer != null)
            {
                timer.Stop();
                timer.Tick -= Timer_Tick; // Unsubscribe to avoid memory leaks
            }
            InitializeTimer(); 

            
            MineField = new MineField(SelectedDifficulty.RowSize, SelectedDifficulty.ColumnSize);

           
            MainGrid.Children.Remove(FieldGrid);
            MainGrid.Children.Add(InitializeField()); 

            // Reset UI elements
            window.TimerDisplay.Content = "0";
            window.MineCounter.Content = SelectedDifficulty.TotalMines;


        }

    }

    public partial class FieldGrid : Grid
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public RowDefinition RowDefinition { get; set; }
        public ColumnDefinition ColumnDefinition { get; set; }

        public FieldGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            // Set the grid's row and column definitions
            for (int i = 0; i < Rows; i++)
            {
                RowDefinition = new RowDefinition()
                {
                    Height = new GridLength(1, GridUnitType.Star)
                };
                RowDefinitions.Add(RowDefinition);
            }
            for (int j = 0; j < Columns; j++)
            {
                ColumnDefinition ColumnDefinition = new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star)
                };
                ColumnDefinitions.Add(ColumnDefinition);
            }
        }

        public void AddTiles(MineField mineField)
        {
            // Add the tiles to the grid, population goes in row-major order
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    var tileButton = new TileButton(mineField); // Assuming constructor modification to accept MineField
                    SetRow(tileButton, i);
                    SetColumn(tileButton, j);
                    tileButton.Row = i; // Assigning row
                    tileButton.Column = j; // Assigning column
                    Children.Add(tileButton);
                }
            }
        }
    }
 }
