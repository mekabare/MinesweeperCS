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

namespace Minesweeper.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameDifficulty selectedDifficulty;
        private Spieler spieler;
        private GameInstance gameInstance;
        private MainMenu mainMenu;
        private Bestenliste bestenlisten;
        private BestenlisteDialog bestenListeDialog;
        private DifficultyDialog difficultyDialog;

        public Spieler Spieler
        {
            get => spieler;
            set { spieler = value; }
        }

        public GameDifficulty SelectedDifficulty 
        { 
            get => selectedDifficulty; 
            set { selectedDifficulty = value; }
        }

        public GameInstance GameInstance
        {
            get => gameInstance;
            set { gameInstance = value; }
        }
        public MainMenu MainMenu { get; set; }

        public Bestenliste Bestenlisten { get; set; }

        public BestenlisteDialog BestenlisteDialog { get; set; }

        public DifficultyDialog DifficultyDialog { get; set; }



        public MainWindow()
        {
            InitializeComponent();

            DifficultyDialog = new DifficultyDialog();
            BestenlisteDialog = new BestenlisteDialog();
            MainMenu = new MainMenu();

            LoadMainMenu(DifficultyDialog);
            MainMenu.NewGameRequested += MainMenu_NewGameButton_Click;
            MainMenu.HighscoreRequested += MainMenu_HighscoreButton_Click;
            MainMenu.ExitRequested += MainMenu_ExitButton_Click;
            DifficultyDialog.EasyGameRequested += MainMenu_EasyButtonClick;
            DifficultyDialog.MediumGameRequested += MainMenu_MediumButtonClick;
            DifficultyDialog.HardGameRequested += MainMenu_HardGameRequested;
            DifficultyDialog.BackToMenuRequested += DifficultyDialog_BackToMenuRequested;

        }

        private void DifficultyDialog_BackToMenuRequested(object sender, EventArgs e)
        {
            DifficultyDialog.Close();
        }

        private void MainMenu_HardGameRequested(object sender, EventArgs e)
        {
            DifficultyDialog.Close();
            LoadGameInstancePage(new Hard());
        }

        private void MainMenu_MediumButtonClick(object sender, EventArgs e)
        {
            DifficultyDialog.Close();
            LoadGameInstancePage(new Medium());
        }

        // Routing Events
        private void MainMenu_EasyButtonClick(object sender, EventArgs e)
        {
            DifficultyDialog.Close();
            LoadGameInstancePage(new Easy());
        }

        private void MainMenu_ExitButton_Click(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MainMenu_HighscoreButton_Click(object sender, EventArgs e)
        {
            LoadBestenListeDialog();
        }

        private void LoadBestenListeDialog()
        {
            bestenListeDialog = new BestenlisteDialog();
            bestenListeDialog.ShowDialog();

        }

        /// <summary>
        /// On Startup: Laedt das Hauptmenue in den Frame "MainContent"
        /// </summary>
        public void LoadMainMenu(DifficultyDialog difficultyDialog)
        {
    
            MainMenu = new MainMenu();
            MainContent.Content = MainMenu;
        }

        public void MainMenu_NewGameButton_Click(object sender, EventArgs e)
        {
            LoadDifficultyDialog();
            
        }

        private void LoadDifficultyDialog()
        {
            DifficultyDialog.ShowDialog();
        }



        /// <summary>
        /// Laedt die GameInstance mit dem gewaehlten Schwierigkeitsgrad in den Frame "MainContent"
        /// </summary>
        /// <param name="difficulty"></param>
        public void LoadGameInstancePage(GameDifficulty difficulty)
        {
            Spieler = new Spieler();
            Spieler.Difficulty = difficulty;
            gameInstance = new GameInstance(difficulty);
            MainContent.Content = gameInstance;

        }

    }
}
