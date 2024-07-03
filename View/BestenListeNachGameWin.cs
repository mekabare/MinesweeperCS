using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

// NOCH NICHT GETESTET WEIL ICH ZU BLOED BIN ZU GEWINNEN

namespace Minesweeper.View
{
    internal class BestenListeNachGameWin : BestenlisteDialog
    {
        public Spieler CurrentSpieler { get; set; }
        public BestenListeNachGameWin() : base()
        {
            this.BottomDock.Visibility = System.Windows.Visibility.Hidden;
        }

        public override void LoadBestenliste()
        {
            Bestenliste.GetRankInList(CurrentSpieler);

            foreach (var Spieler in Bestenliste.PlayerList)
            {
                if (Spieler == CurrentSpieler)
                {
                    break;
                }
                TextBox tbName = new TextBox();
                tbName.Text = Spieler.Name;

                TextBox tbMode = new TextBox();
                tbMode.Text = Spieler.Difficulty.ToString();
                TextBox tbTime = new TextBox();
                tbTime.Text = Spieler.Time.ToString();

                ListGrid.Children.Add(tbName);
                ListGrid.Children.Add(tbMode);
                ListGrid.Children.Add(tbTime);

            }
            
            TextBox tbNewName = new TextBox();
            tbNewName.Text = "";
            tbNewName.IsReadOnly = false;
            TextBox tbNewMode = new TextBox();
            tbNewName.Text = CurrentSpieler.Difficulty.ToString();
            TextBox tbNewTime = new TextBox();
            tbNewTime.Text = CurrentSpieler.Time.ToString();

            tbNewName.Focus(); // wait for user input
            ListGrid.Children.Add(tbNewName);
            ListGrid.Children.Add(tbNewMode);
            ListGrid.Children.Add(tbNewTime);

        }

        public override void SaveBestenliste()
        {
            throw new NotImplementedException();
        }
    }

}
