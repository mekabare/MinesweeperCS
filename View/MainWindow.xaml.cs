﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        private HowToDialog howToDialog;
        
        public DependencyPropertyChangedEventHandler PropertyChanged { get; private set; }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new DependencyPropertyChangedEventArgs());
        }

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

        public HowToDialog HowToDialog { get; set; }



        public MainWindow()
        {
            InitializeComponent();

            DifficultyDialog = new DifficultyDialog(); // Subscriber
            BestenlisteDialog = new BestenlisteDialog();
            HowToDialog = new HowToDialog();
            MainMenu = new MainMenu(); // Publisher

            LoadMainMenu();
            MainMenu.NewGameRequested += MainMenu_NewGameButton_Click;
            MainMenu.HighscoreRequested += MainMenu_HighscoreButton_Click;
            MainMenu.ExitRequested += MainMenu_ExitButton_Click;
            MainMenu.HowToDialogButton.Click += MainMenu_HowToDialogButton_Click;
            DifficultyDialog.EasyGameRequested += GameInstance_OnEasyGameRequested; // OnEasyGameRequested
            DifficultyDialog.MediumGameRequested += GameInstance_OnMediumGameRequested; // OnMediumGameRequested
            DifficultyDialog.HardGameRequested += GameInstance_OnHardGameRequested; // OnHardGameRequested
            DifficultyDialog.BackToMenuRequested += DifficultyDialog_BackToMenuRequested;

        }

        private void GameInstance_OnEasyGameRequested(object sender, EventArgs e)
        {
            DifficultyDialog.Close();
            var gameInstance = new GameInstance(new Easy());

            MainContent.Content= gameInstance;
            
        }

        private void GameInstance_OnMediumGameRequested(object sender, EventArgs e)
        {
            DifficultyDialog.Close();
            gameInstance = new GameInstance(new Medium());
            MainContent.Content = gameInstance;
        }

        private void GameInstance_OnHardGameRequested(object sender, EventArgs e)
        {
            DifficultyDialog.Close();
            gameInstance = new GameInstance(new Hard());
            MainContent.Content = gameInstance;
        }

        private void MainMenu_HowToDialogButton_Click(object sender, EventArgs e)
        {
            HowToDialog = new HowToDialog();
            if (HowToDialog != null)
                HowToDialog.ShowDialog();
        }

        private void DifficultyDialog_BackToMenuRequested(object sender, EventArgs e)
        {
            DifficultyDialog = new DifficultyDialog();
            if (DifficultyDialog != null)
                DifficultyDialog.Close();
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
        public void LoadMainMenu()
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
