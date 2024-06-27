using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for HowToDialog.xaml
    /// </summary>
    public partial class HowToDialog : Window
    {
        public HowToDialog()
        {
            string hilfstext;

            hilfstext = ReadHelpTextFromFile();

            InitializeComponent();

            Hilfstextbox.Content = hilfstext;

        }//HowToDialog


        public string ReadHelpTextFromFile()
        {
            const string path = @"Hilfstext.txt";
            bool ok = false;
            string text = "None";

            try {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                    if (fs.CanRead) {

                        using (StreamReader sr = new StreamReader(fs))
                        {
                            text = sr.ReadLine();       //Text einlesen
                        }

                    }//if fs.CanRead
                    else { text = "File cannot be read."; }

                }//using FileStream
            }//try
            catch (FileNotFoundException ex) { text = ex.ToString(); }  //Wenn die Dati nicht gelesen werden konnte

            return text;
        }//ReadHelpTextFromFile


    }//partial class
}//Namespace
