using guyproj.ViewModel;
using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace guyproj.View
{
    public partial class Trivia : Page
    {
        private string connectionString = @"Data Source=C:\Users\Guy\source\repos\guyproj\guyproj\table1.db;Version=3;";

        public Trivia(SharedViewModel _sharedViewModel)
        {
            SharedViewModel sharedViewModel = _sharedViewModel;
            InitializeComponent();
            LoadRandomQuestion();
        }

        private void LoadRandomQuestion()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Get a random question
                string query = "SELECT * FROM questions ORDER BY RANDOM() LIMIT 1";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set the question and options
                            QuestionText.Text = reader["question"].ToString();
                            OptionA.Content = reader["option1"].ToString();
                            OptionB.Content = reader["option2"].ToString();
                            OptionC.Content = reader["option3"].ToString();
                            OptionD.Content = reader["option4"].ToString();

                            // Store the correct option in a Tag or some other place to check later
                            OptionA.Tag = reader["correct_option"].ToString() == "option1";
                            OptionB.Tag = reader["correct_option"].ToString() == "option2";
                            OptionC.Tag = reader["correct_option"].ToString() == "option3";
                            OptionD.Tag = reader["correct_option"].ToString() == "option4";
                        }
                    }
                }
            }
        }

        private void Option_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            bool isCorrect = (bool)clickedButton.Tag;

            if (isCorrect)
            {
                FeedbackText.Text = "Correct!";
            }
            else
            {
                FeedbackText.Text = "Wrong!";
            }

            // Load another random question after a short delay
            Task.Delay(2000).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    FeedbackText.Text = "";
                    LoadRandomQuestion();
                });
            });
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // You can add submit logic here if needed
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow.WindowState == WindowState.Maximized)
            {
                parentWindow.WindowState = WindowState.Normal;
            }
            else
            {
                parentWindow.WindowState = WindowState.Maximized;
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Window parentWindow = Window.GetWindow(this);
                parentWindow.DragMove();
            }
        }

        private void OptionA_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OptionB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OptionC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OptionD_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
