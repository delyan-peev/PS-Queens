using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace QueensWPF
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        private int BoardSize;
        private bool[,] Board;

        public Game(int boardSize)
        {
            this.BoardSize = boardSize;
            InitializeComponent();
            GenerateBoard();
        }

        void OnFieldClicked(object sender, RoutedEventArgs e)
        {
            UpdateField((Button)sender);
            UpdateFieldColors();

            if (CountSafeQueens() == BoardSize)
                GameWonRestart();
        }

        private void UpdateField(Button clickedField)
        {
            int[] coords = BoardUtils.FieldToCoordinates(clickedField.Name);

            if (BoardUtils.hasQueenInField(clickedField))
            {
                // Clear field
                clickedField.Content = "";
                Board[coords[0], coords[1]] = false;
            }
            else
            {
                // Add Queen
                BoardUtils.PlaceQueenInField(clickedField);
                Board[coords[0], coords[1]] = true;
            }
        }

        private bool IsSafe(int row, int col)
        {
            int i, j;

            /* Check row */
            for (i = 0; i < BoardSize; i++)
                if (Board[row, i] && i != col) return false;
            /* Check col */
            for (i = 0; i < BoardSize; i++)
                if (Board[i, col] && i != row) return false;
            /* Check upper diagonal on left side */
            for (i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--)
                if (Board[i, j]) return false;
            /* Check lower diagonal on left side */
            for (i = row + 1, j = col - 1; j >= 0 && i < BoardSize; i++, j--)
                if (Board[i, j]) return false;
            /* Check upper diagonal on right side */
            for (i = row - 1, j = col + 1; i >= 0 && j < BoardSize; i--, j++)
                if (Board[i, j]) return false;
            /* Check lower diagonal on right side */
            for (i = row + 1, j = col + 1; i < BoardSize && j < BoardSize; i++, j++)
                if (Board[i, j]) return false;

            return true;
        }

        private void UpdateFieldColors()
        {
            IEnumerator iterator = CheckerBoard.Children.GetEnumerator();

            while (iterator.MoveNext())
            {
                Button field = (Button)iterator.Current;

                if (field.Name != "" && field.Content != null)
                {
                    int[] coords = BoardUtils.FieldToCoordinates(field.Name);

                    if (!IsSafe(coords[0], coords[1]) && BoardUtils.hasQueenInField(field))
                    {
                        field.Background = Brushes.Red;
                    }
                    else
                    {
                        ColorField(coords[0], coords[1], field);
                    }
                }
            }
        }

        private int CountSafeQueens()
        {
            int count = 0;

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if (Board[row, col] && IsSafe(row, col))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private void ResetClicked(object sender, RoutedEventArgs e)
        {
            CheckerBoard.Children.Clear();
            GenerateBoard();
        }

        private void GenerateBoard()
        {
            Board = new bool[BoardSize, BoardSize];
            GenerateFields();
           // GenerateResetButton();
        }

        private void GenerateFields()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                //CheckerBoard.SetValue(Grid.RowProperty, row % 8);
                for (int col = 0; col < BoardSize; col++)
                {
                    GenerateField(row, col);
                }
            }
        }

        private void GenerateField(int row, int col)
        {
            Button field = new Button();

            ColorField(row, col, field);

            field.Name = String.Format("A{0}{1}", row, col);
            field.Click += new RoutedEventHandler(OnFieldClicked);

            //CheckerBoard.SetValue(Grid.ColumnProperty, col);
            CheckerBoard.Children.Add(field);
        }

        private void ColorField(int row, int col, Button field)
        {
            if ((row + col) % 2 == 1)
            {
                field.Background = Brushes.Black;
            }
            else
            {
                field.Background = Brushes.White;
            }
        }

        private void GenerateResetButton()
        {
            CheckerBoard.SetValue(Grid.RowProperty, BoardSize);
            CheckerBoard.SetValue(Grid.ColumnProperty, 0);
            Button resetButton = new Button();
            resetButton.Content = "Reset";
            resetButton.FontWeight = FontWeights.Bold;
            resetButton.Click += new RoutedEventHandler(ResetClicked);
            CheckerBoard.Children.Add(resetButton);
        }

        private void GameWonRestart()
        {
            string messageBoxText = String.Format("Congratulations! You have successfully placed {0} queens without either of them attacking another.", BoardSize);
            string caption = "You succeeded!";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);

            CheckerBoard.Children.Clear();
            GenerateBoard();
        }
    }
}
