using Minesweeper.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace Minesweeper
{
    /// <summary>
    /// Interaktionslogik für BestenlisteTest.xaml
    /// </summary>
    public partial class BestenlisteTest : Window
    {
        Spieler player = new Spieler();
        Bestenliste bestenliste = new Bestenliste();
        GameDifficulty schwer = null;
        string difficulty;
        int time;
        string name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wonGame">True, falls das Spiel gewonnen wurde</param>
        public BestenlisteTest(bool wonGame)
        {
            InitializeComponent();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow; //Verbindung zu MainWindow

            bestenliste = new Bestenliste();

            player = mainWindow.Spieler;

            if (player != null) //Um Fehler beim Start zu verhindern
            {
                difficulty = player.Difficulty.ToString();
                schwer = player.Difficulty;

                ListeBestücken();

                if (wonGame)
                {

                }

                Show();
            }
        }//Konstruktor


        private void ListeBestücken()
        {
            switch (schwer.ToString()) //Schwierigkeitsgrad
            {
                case "Easy": //Easy
                    {
                        bestenliste.PlatzhalterList = bestenliste.EasyList;
                        break;
                    }
                case "Medium": //Medium
                    {
                        bestenliste.PlatzhalterList = bestenliste.MediumList;
                        break;
                    }
                case "Hard": //Hard
                    {
                        bestenliste.PlatzhalterList = bestenliste.HardList;
                        break;
                    }
            }//switch

            SpielerOneName.Text = bestenliste.PlatzhalterList[0].Name;
            SpielerOneModeTime.Content = string.Format("{0}\t{1}",
                bestenliste.PlatzhalterList[0].Difficulty.ToString(),
                bestenliste.PlatzhalterList[0].Time.ToString());

            SpielerTwoName.Text = bestenliste.PlatzhalterList[1].Name;
            SpielerTwoModeTime.Content = string.Format("{0}\t{1}",
                bestenliste.PlatzhalterList[1].Difficulty.ToString(),
                bestenliste.PlatzhalterList[1].Time.ToString());

            SpielerThreeName.Text = bestenliste.PlatzhalterList[2].Name;
            SpielerThreeModeTime.Content = string.Format("{0}\t{1}",
                bestenliste.PlatzhalterList[2].Difficulty.ToString(),
                bestenliste.PlatzhalterList[2].Time.ToString());

            SpielerFourName.Text = bestenliste.PlatzhalterList[3].Name;
            SpielerFourModeTime.Content = string.Format("{0}\t{1}",
                bestenliste.PlatzhalterList[3].Difficulty.ToString(),
                bestenliste.PlatzhalterList[3].Time.ToString());

            SpielerFiveName.Text = bestenliste.PlatzhalterList[4].Name;
            SpielerFiveModeTime.Content = string.Format("{0}\t{1}",
                bestenliste.PlatzhalterList[4].Difficulty.ToString(),
                bestenliste.PlatzhalterList[4].Time.ToString());

            SpielerSixName.Text = bestenliste.PlatzhalterList[5].Name;
            SpielerSixModeTime.Content = string.Format("{0}\t{1}",
                bestenliste.PlatzhalterList[5].Difficulty.ToString(),
                bestenliste.PlatzhalterList[5].Time.ToString());

            SpielerSevenName.Text = bestenliste.PlatzhalterList[6].Name;
            SpielerSevenModeTime.Content = string.Format("{0}\t{1}",
                bestenliste.PlatzhalterList[6].Difficulty.ToString(),
                bestenliste.PlatzhalterList[6].Time.ToString());

            SpielerEightName.Text = bestenliste.PlatzhalterList[7].Name;
            SpielerEightModeTime.Content = string.Format("{0}\t{1}",
                bestenliste.PlatzhalterList[7].Difficulty.ToString(),
                bestenliste.PlatzhalterList[7].Time.ToString());

            SpielerNineName.Text = bestenliste.PlatzhalterList[8].Name;
            SpielerNineModeTime.Content = string.Format("{0}\t{1}",
                bestenliste.PlatzhalterList[8].Difficulty.ToString(),
                bestenliste.PlatzhalterList[8].Time.ToString());

            SpielerTenName.Text = bestenliste.PlatzhalterList[9].Name;
            SpielerTenModeTime.Content = string.Format("{0}\t{1}",
                bestenliste.PlatzhalterList[9].Difficulty.ToString(),
                bestenliste.PlatzhalterList[9].Time.ToString());

        }//ListeBestücken

        private void EasyRadio_Checked(object sender, RoutedEventArgs e)
        {
            schwer = new Easy();
            ListeBestücken();
        }

        private void MediumRadio_Checked(object sender, RoutedEventArgs e)
        {
            schwer = new Medium();
            ListeBestücken();
        }

        private void HardRadio_Checked(object sender, RoutedEventArgs e)
        {
            schwer = new Hard();
            ListeBestücken();
        }

    }//partial class
}//Namespace
