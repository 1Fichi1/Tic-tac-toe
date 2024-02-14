using System;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private bool isPlayerXTurn = true;
        private bool isGameActive = true;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            isGameActive = true;
            StatusText.Text = "Ход крестиков";
            foreach (UIElement element in GameGrid.Children)
            {
                if (element is Button button && button.Name != "RestartButton")
                {
                    button.Content = string.Empty;
                    button.IsEnabled = true;
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!isGameActive) return;

            var button = (Button)sender;
            if (!string.IsNullOrEmpty(button.Content as string)) return;

            button.Content = isPlayerXTurn ? "X" : "O";
            isPlayerXTurn = !isPlayerXTurn;
            StatusText.Text = isPlayerXTurn ? "Ход крестиков" : "Ход ноликов";
            CheckForWinner();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
        }

        private void CheckForWinner()
        {
            var buttons = new Button[9];
            for (int i = 0; i < 9; i++)
            {
                buttons[i] = (Button)FindName($"Button{i}");
            }


            int[,] winningCombinations = new int[,]
            {
                {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, 
                {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, 
                {0, 4, 8}, {2, 4, 6}
            };

            for (int i = 0; i < 8; i++)
            {
                int a = winningCombinations[i, 0], b = winningCombinations[i, 1], c = winningCombinations[i, 2];
                string btnA = buttons[a].Content as string, btnB = buttons[b].Content as string, btnC = buttons[c].Content as string;

                if (!string.IsNullOrEmpty(btnA) && btnA == btnB && btnB == btnC)
                {
                    isGameActive = false;
                    StatusText.Text = $"{btnA} победили!";
                    return;
                }
            }

   
            bool isDraw = true;
            foreach (var button in buttons)
            {
                if (string.IsNullOrEmpty(button.Content as string))
                {
                    isDraw = false;
                    break;
                }
            }

            if (isDraw)
            {
                isGameActive = false;
                StatusText.Text = "Ничья!";
            }
        }
    }
}
