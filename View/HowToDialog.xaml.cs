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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace Minesweeper.View
{
    /// <summary>
    /// Interaction logic for HowToDialog.xaml
    /// </summary>
    public partial class HowToDialog : Window
    {
        public event EventHandler OpenDialog;
        public event EventHandler CloseDialog;

        public virtual void OnOpenDialog()
        {
            OpenDialog?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnCloseDialog()
        {
            CloseDialog?.Invoke(this, EventArgs.Empty);
        }

        public void OpenHowToDialog()
        {

        }

        public HowToDialog()
        {

            InitializeComponent();

            OpenHowToDialog();

            Hilfstextbox.Text = ReadHelpTextFromFile();


        }//HowToDialog


        public string ReadHelpTextFromFile()
        {
            bool ok = false;
            string text = "None";

            try {
                // add new path to the help file at /Assets/HowTo.txt
                string path = @"Hilfstext.txt";
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    if (fs.CanRead)
                    {

                        using (StreamReader sr = new StreamReader(fs))
                        {
                            text = sr.ReadToEnd();       //Text einlesen
                        }

                    }//if fs.CanRead
                    else { text = "File cannot be read."; }

                }//using FileStream
            }//try
            catch (FileNotFoundException ex) { text = ex.ToString(); }  //Wenn die Dati nicht gelesen werden konnte

            return text;
        }//ReadHelpTextFromFile

        private void OKButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
  
        }
    }//partial class
}//Namespace
