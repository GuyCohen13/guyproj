using guyproj.ViewModel;
using guyproj.View;
using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace guyproj.View
{
    public partial class Register : Page
    {
        private SharedViewModel _sharedViewModel;

        public Register(SharedViewModel sharedViewModel)
        {
            InitializeComponent();
            _sharedViewModel = sharedViewModel;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (!AreAllValuesFilled())
            {
                MessageBox.Show("Please fill all the fields & make sure passwords are equal");
                return;
            }

            string email = txtUser.Text;
            string firstName = txtName.Text;
            string lastName = txtFamily.Text;
            string password = txtPassword.Password; // Use Password property to retrieve the password

            string connectionString = @"Data Source=C:\Users\Guy\source\repos\guyproj\guyproj\table1.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO users (Email, FirstName, LastName, Password) VALUES (@Email, @FirstName, @LastName, @Password)";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Password", password);
                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("User registered successfully!");
            NavigationService.Navigate(new Login(_sharedViewModel));
        }

        private bool AreAllValuesFilled()
        {
            return !string.IsNullOrEmpty(txtUser.Text) &&
                   !string.IsNullOrEmpty(txtName.Text) &&
                   !string.IsNullOrEmpty(txtFamily.Text) &&
                   !string.IsNullOrEmpty(txtPassword.Password); // Check PasswordBox value
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login(_sharedViewModel));
        }

        private void txtAge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnRegister_Click(sender, e);
            }
        }
    }
}
