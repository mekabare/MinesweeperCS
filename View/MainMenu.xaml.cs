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

namespace Minesweeper.View
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        internal GameDifficulty GetSelectedDifficulty()
        {
            GameDifficulty gameDifficulty = new Easy();
            return gameDifficulty;
        }

        private void HighscoreDialogButton_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void ExitGameButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        public void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            // opens diaglog to choose difficulty
            DifficultyDialog difficultyDialog = new DifficultyDialog();
            difficultyDialog.ShowDialog();
            if (difficultyDialog.DialogResult == true)
            {
                GameInstancePage gameInstancePage = new GameInstancePage(difficultyDialog.GameDifficulty);
            }

        }
    }
}
