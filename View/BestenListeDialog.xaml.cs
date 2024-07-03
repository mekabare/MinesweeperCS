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
using System.Windows.Shapes;

namespace Minesweeper.View
{
    /// <summary>
    /// Interaction logic for BestenlisteDialog.xaml
    /// </summary>
    abstract partial class BestenlisteDialog : Window
    {
        private Bestenliste bestenliste;

        public Bestenliste Bestenliste { get; set; }
        public BestenlisteDialog()
        {
            InitializeComponent();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        }

       public abstract void LoadBestenliste();

       public abstract void SaveBestenliste();


        // TODO event handler for radio buttons
    }
}
