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
        event EventHandler EditableDialogOpened;
        event EventHandler EnterNameRequested;
        event EventHandler NameEntered;

        TextBox EnterNameBox;
        Label ResultDifficulty, ResultTime;
        string Name, Difficulty, Time;

        public BestenlisteDialog()
        {
            InitializeComponent();
        }
        public BestenlisteDialog(bool wonGame)
        {
            InitializeComponent();

            if (wonGame)
            {
                EnterNameBox = new TextBox();
                ResultDifficulty = new Label();
                ResultTime = new Label();
                EnterNameBox.TextChanged += EnterNameBox_Changed;
                EnterNameBox.AcceptsReturn = true;
                ListGrid.Children.Add(EnterNameBox);
                ListGrid.Children.Add(ResultDifficulty);
                ListGrid.Children.Add(ResultTime);
            }

        }

        protected virtual void OnEnterNameRequested()
        {
            EnterNameRequested?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnNameEntered()
        {
            NameEntered?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnEditableDialogOpened()
        {
            EditableDialogOpened?.Invoke(this, EventArgs.Empty);
        }

        private void EnterNameBox_Changed(object sender, RoutedEventArgs e)
        {
            OnEnterNameRequested();
        }

        private void ReturnKey_Pressed(object sender, RoutedEventArgs e)
        {
            OnNameEntered();
        }

        public void LoadBestenliste(string name, string difficulty, string time)
        {
            Name = name;
            Difficulty = difficulty;
            Time = time;
            ResultDifficulty.Content = Difficulty;
            ResultTime.Content = Time;
        }

        public void SaveBestenliste()
        {
            // Save the name, difficulty and time to the database
        }

        public void SortBestenliste()
        {
            // Sort the bestenliste difficulty devided by time, where Easy has a value of 1, Medium 2 and Hard 3

        }
    }
}
