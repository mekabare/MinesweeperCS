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
            player = mainWindow.Spieler;

            if (wonGame)
            {
                OnEnterNameRequested();
            }

        }
        protected virtual void OnEnterNameRequested()
        {
            EnterNameBox = new TextBox();
            ResultDifficulty = new Label();
            ResultTime = new Label();
            EnterNameBox.TextChanged += EnterNameBox_Changed;
            EnterNameBox.AcceptsReturn = true;
            ListGrid.Children.Add(EnterNameBox);
            ListGrid.Children.Add(ResultDifficulty);
            ListGrid.Children.Add(ResultTime);
            EnterNameBox.Focus();


        }


        private void EnterNameBox_Changed(object sender, RoutedEventArgs e)
        {
                if (EnterNameBox.Text.Length > 3)
                {
                    EnterNameBox.Text = EnterNameBox.Text.ToUpper().Substring(0, 3);
                    OnValidNameEntered();
                }
        }

        private void OnValidNameEntered()
        {
            EnterNameBox.AcceptsReturn = true;
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
        public void LoadBestenliste(string name, string difficulty, string time)
        {
            Bestenliste bestenliste = new Bestenliste();
            bestenliste.ReadLists();
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
