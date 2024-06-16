using guyproj.ViewModel;
using guyproj.View;
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

namespace guyproj.View
{
    public partial class MainWithFrame : Window
    {
        private SharedViewModel viewModel;
        public MainWithFrame()
        {
            InitializeComponent();
            mainFrame.Navigate(new Login(viewModel));
            Application.Current.Windows[0].Height = 530;
            Application.Current.Windows[0].Width = 500;
        }
        private void ClickClose(object sender, RoutedEventArgs e)
        {
            // Close the window
            System.Windows.Application.Current.Shutdown();
            this.Close();
        }

        // In MainWithFrame.xaml.cs
        private void ClickLogout(object sender, RoutedEventArgs e)
        {


            mainFrame.Navigate(new Login(viewModel));
            Application.Current.Windows[0].Height = 530;
            Application.Current.Windows[0].Width = 500;
        }

    }
}
