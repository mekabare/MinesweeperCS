using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Minesweeper.View
{
    /// <summary>
    /// Interaction logic for DifficultyDialog.xaml
    /// </summary>
    public partial class DifficultyDialog : Window
    {
        public DifficultyDialog()
        {
            InitializeComponent();
        }

        // Events
        public event EventHandler EasyGameRequested;
        public event EventHandler MediumGameRequested;
        public event EventHandler HardGameRequested;
        public event EventHandler BackToMenuRequested;

        // Event raisers
        protected virtual void OnEasyGameRequested()
        {
          EasyGameRequested?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnMediumGameRequested()
        {
            MediumGameRequested?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnHardGameRequested() {

            HardGameRequested?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnBackToMenuRequested()
        {
            BackToMenuRequested?.Invoke(this, EventArgs.Empty);
        }


        // Eventhandlers
        private void EasyButton_Click(object sender, RoutedEventArgs e)
        {
            OnEasyGameRequested();
        }
       
        private void MediumButton_Click(object sender, RoutedEventArgs e)
        {
            OnMediumGameRequested();
        }
      
        private void HardButton_Click(object sender, RoutedEventArgs e)
        {
            OnHardGameRequested();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            OnBackToMenuRequested();
        }
    }
}
