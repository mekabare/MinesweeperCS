using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Minesweeper.View
{
    internal class BestenListeImMenu : BestenlisteDialog
    {
        public BestenListeImMenu() : base()
        {

            this.BottomDock.Visibility = System.Windows.Visibility.Visible;
            LoadBestenliste();
        }

        public override void LoadBestenliste()
        {
            Bestenliste = new Bestenliste();
            Bestenliste.SortListDescending();

            if (!Bestenliste.ReadList())
            { MessageBox.Show("Bestenlisten.txt konnte nicht geladen werden!"); this.Hide(); }

            else
            {
                foreach (var Spieler in Bestenliste.PlayerList)
                {
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
            }

        }

        public override void SaveBestenliste()
        {
            throw new NotImplementedException();
        }
    }
}
