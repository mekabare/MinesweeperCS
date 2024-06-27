using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for DifficultyDialog.xaml
    /// </summary>
    public partial class DifficultyDialog : Window
    {
        private GameDifficulty gameDifficulty;

        public GameDifficulty GameDifficulty
        {
            get => gameDifficulty;
            set { gameDifficulty = value; }
        }
        public DifficultyDialog()
        {
            InitializeComponent();
        }

        private void EasyButton_Click(object sender, RoutedEventArgs e)
        {
            gameDifficulty = new Easy();
            GameInstancePage gameInstancePage = new GameInstancePage(GameDifficulty);
            this.DialogResult = true;
            this.Close();
        }

        private void MediumButton_Click(object sender, RoutedEventArgs e)
        {
            gameDifficulty = new Medium();
            GameInstancePage gameInstancePage = new GameInstancePage(GameDifficulty);
            this.DialogResult = true;
            this.Close();
        }

        private void HardButton_Click(object sender, RoutedEventArgs e)
        {
            gameDifficulty = new Hard();
            GameInstancePage gameInstancePage = new GameInstancePage(GameDifficulty);
            this.DialogResult = true;
            this.Close();
        }
    }
}
