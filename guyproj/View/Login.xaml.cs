using guyproj.ViewModel;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Data.SQLite;

namespace guyproj.View
{
    public partial class Login : Page
    {
        private SharedViewModel _sharedViewModel;
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private double storedPosition;
        private bool max = false;

        public Login(SharedViewModel sharedViewModel)
        {
            InitializeComponent();
            _sharedViewModel = sharedViewModel;
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (max == false)
            {
                parentWindow.WindowState = WindowState.Maximized;
                max = true;
            }
            else
            {
                parentWindow.WindowState = WindowState.Normal;
                max = false;
            }
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string enteredEmail = txtUser.Text;
                string enteredPassword = txtPassword.Password; // Use Password property for PasswordBox

                if (string.IsNullOrEmpty(enteredEmail) || string.IsNullOrEmpty(enteredPassword))
                {
                    MessageBox.Show("Please enter both email and password.");
                    return;
                }

                bool userFound = AuthenticateUser(enteredEmail, enteredPassword);

                if (userFound)
                {
                    NavigationService.Navigate(new Trivia(_sharedViewModel));
                }
                else
                {
                    MessageBox.Show("Invalid email or password. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private bool AuthenticateUser(string email, string password)
        {
            string connectionString = @"Data Source=C:\Users\Guy\source\repos\guyproj\guyproj\table1.db;Version=3;";

            try
            {
                using (var connection = new System.Data.SQLite.SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT UserId, FirstName, LastName, Email, Password, IsAdmin FROM users WHERE Email = @Email AND Password = @Password LIMIT 1";

                    using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Access columns by name to avoid index issues
                                string dbEmail = reader["Email"].ToString();
                                string dbPassword = reader["Password"].ToString();

                                return dbEmail == email && dbPassword == password;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred in AuthenticateUser: {ex.Message}");
                return false;
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Register(_sharedViewModel));
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

    }
}
