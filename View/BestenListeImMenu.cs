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

            if (!Bestenliste.ReadList())
            { MessageBox.Show("Bestenlisten.txt konnte nicht geladen werden!" + Bestenliste.PlayerList.Length); }

            else
            {
                int startIndex = 1;
                for (int i = 0; i < Bestenliste.PlayerList.Length; i++)
                {
                    int rowIndex = startIndex + i;
                    Spieler Spieler = Bestenliste.PlayerList[i];
                    RowDefinition rowDefinition = new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Auto)
                    };
                    // insert after first row definition
                    ListGrid.RowDefinitions.Insert(rowIndex, rowDefinition);


                    TextBox tbName = new TextBox
                    {
                        Text = Spieler.Name.ToUpper()
                    };

                    TextBox tbMode = new TextBox 
                    {
                        Text = Spieler.Difficulty.ToString().ToUpper()
                    };
                    
                    TextBox tbTime = new TextBox 
                    { 
                        Text = Spieler.Time.ToString()
                    };

                    TextBox tbScore = new TextBox
                    {
                        Text = Spieler.Score.ToString(),
                        FontSize = 16
                    };
 

                    Grid.SetColumn(tbName, 0);
                    Grid.SetColumn(tbMode, 1);
                    Grid.SetColumn(tbTime, 2);
                    Grid.SetColumn(tbScore, 3);
                    Grid.SetRow(tbName, rowIndex);
                    Grid.SetRow(tbMode, rowIndex);
                    Grid.SetRow(tbTime, rowIndex);
                    Grid.SetRow(tbScore, rowIndex);


                    ListGrid.Children.Add(tbName);
                    ListGrid.Children.Add(tbMode);
                    ListGrid.Children.Add(tbTime);
                    ListGrid.Children.Add(tbScore);


                }
            }

        }

        public override void RadioButton_Checked(object sender, EventArgs e)
        {
            if (Bestenliste != null)
            {
                RadioButton rb = (RadioButton)sender;
                switch (rb.Name)
                {
                    case "Descending":
                        Bestenliste.SortListDescending();
                        break;
                    case "Ascending":
                        Bestenliste.SortListAscending();
                        break;
                }
                LoadBestenliste();
            }
         }

        public override void SaveBestenliste()
        {
            throw new NotImplementedException();
        }

    }
}
