using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace QueensWPF
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (BoardSize.Text.Length > 0)
            {
                int boardSize = int.Parse(BoardSize.Text);

                if (boardSize >= 3 && boardSize <= 8)
                {
                    Game gamePage = new Game(boardSize);
                    this.Content = gamePage;
                }
                else
                {
                    string messageBoxText = "Value should be between 5 and 8";
                    string caption = "Error";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);
                }
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
