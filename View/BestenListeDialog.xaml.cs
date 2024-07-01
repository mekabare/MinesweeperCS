using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Minesweeper.View
{
    /// <summary>
    /// Interaction logic for BestenlisteDialog.xaml
    /// </summary>
    public partial class BestenlisteDialog : Window
    {
        event EventHandler EnterNameRequested;
        event EventHandler NameEntered;
        event EventHandler ValidNameEntered;

        TextBox EnterNameBox;
        Label ResultDifficulty, ResultTime;
        string Name, Difficulty, Time;
        Spieler player = new Spieler();


        public BestenlisteDialog(bool wonGame)
        {
            InitializeComponent();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            LoadBestenliste();
  
            player = mainWindow.Spieler;

            if (wonGame)
            {
                TextBox EnterNameBox = new TextBox();
                this.ListGrid.Children.Add(EnterNameBox);
                OnEnterNameRequested();

            }

        }

        private void EnterNameBox_Changed(object sender, TextChangedEventArgs e)
        {
            KeyEventArgs keyEventArgs = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, Key.Return);
            if (EnterNameBox.Text.Length > 3)
            {
                EnterNameBox.Text = EnterNameBox.Text.ToUpper().Substring(0, 3);
                OnValidNameEntered(sender, keyEventArgs);
            }
        }

        protected virtual void OnEnterNameRequested()
        {
            EnterNameBox = new TextBox();
            ResultDifficulty = new Label();
            ResultTime = new Label();
            EnterNameBox.AcceptsReturn = false;
            ListGrid.Children.Add(EnterNameBox);
            ListGrid.Children.Add(ResultDifficulty);
            ListGrid.Children.Add(ResultTime);
            EnterNameBox.Focus();


        }


        private void OnValidNameEntered(object sender, KeyEventArgs e)
        {
          if (e.Key == Key.Return)
            {
                ReturnKey_Pressed(sender, e);
            }
        }

        private void ReturnKey_Pressed(object sender, RoutedEventArgs e)
        {

            OnNameEntered();

        }

        protected virtual void OnNameEntered()
        {
            this.Hide();
            player.Name = EnterNameBox.Text;
            player.Time = Int32.Parse(Time);

            SaveBestenliste();
            NameEntered?.Invoke(this, EventArgs.Empty);
        }
        public void LoadBestenliste()
        {
            int numberOfTextBoxes = 3; // For example, for name, difficulty, and time
            int numberOfRows = 9;
            for (int j = 2; j < numberOfRows; j++)
            {
                for (int i = 0; i < numberOfTextBoxes; i++)
                {
                    TextBox textBox = new TextBox();
                    // Assuming you want to add each TextBox to a new row
                    Grid.SetColumn(textBox, i);
                    Grid.SetRow(textBox, j);
                    ListGrid.Children.Add(textBox);
                    textBox.IsReadOnly = true;
                    textBox.Cursor = Cursors.Arrow;
                    textBox.IsHitTestVisible = false;





                }
            }
        }

        public void SaveBestenliste()
        {
            Bestenliste bestenliste = new Bestenliste();
            bestenliste.InsertSorted(player);
            bestenliste.SaveBest();
           

        }

        public void SortBestenliste()
        {
            // Sort the bestenliste difficulty devided by time, where Easy has a value of 1, Medium 2 and Hard 3

        }
    }
}
