using Minesweeper.View;
using System;
using System.Collections;
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
using System.Xml.Schema;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameDifficulty selectedDifficulty;
        private Spieler spieler;
        private MineFieldPage mineFieldPage;
        private MainMenu mainMenu;

        public Spieler Spieler
        {
            get => spieler;
            set { spieler = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            mainMenu = new MainMenu();
            MainContent.Content = mainMenu;
            Spieler = new Spieler();
            Spieler.Name = "AAA";

            //  MainContent.Content = new MainMenu();
            //  mainMenu.GetSelectedDifficulty();
            // Spieler.Difficulty = selectedDifficulty;


            switch (Spieler.Difficulty.ToString())
            {
                case "Easy":
                    GameDifficulty easy = new Easy();
                    mineFieldPage = new MineFieldPage(easy);
                    break;
                case "Medium":
                    GameDifficulty medium = new Medium();
                    mineFieldPage = new MineFieldPage(medium);
                    break;
                case "Hard":
                    GameDifficulty hard = new Hard();
                    mineFieldPage = new MineFieldPage(hard);
                    break;
            }

            MainContent.Content= mineFieldPage;
  

        }
    }
}
