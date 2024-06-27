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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Minesweeper;

namespace Minesweeper.View
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        // Events
        public event EventHandler NewGameRequested;
        public event EventHandler ExitRequested;
        public event EventHandler HighscoreRequested;
        public event EventHandler HowToRequested;


        public MainMenu()
        {
            InitializeComponent();
        }


        // Event raisers
        protected virtual void OnHighscoreRequested()
        {
            HighscoreRequested?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnNewGameRequested()
        {
            NewGameRequested?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnExitRequested()
        {
            ExitRequested?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnHowToRequested()
        {
            HowToRequested?.Invoke(this, EventArgs.Empty);
        }


        // Eventhandlers
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            OnNewGameRequested();
        }

        private void HighscoreDialogButton_Click(object sender, RoutedEventArgs e)
        {
            OnHighscoreRequested();
        }

        private void ExitGameButton_Click(object sender, RoutedEventArgs e)
        {
            OnExitRequested();
        }

        private void HowToDialogButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
