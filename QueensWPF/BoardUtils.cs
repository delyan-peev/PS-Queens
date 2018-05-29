using System;
using System.Windows;
using System.Windows.Controls;

namespace QueensWPF
{
    class BoardUtils
    {
        public static int[] FieldToCoordinates(string fieldName)
        {
            char[] coords = fieldName.ToCharArray();
            int row = (int)Char.GetNumericValue(coords[1]);
            int col = (int)Char.GetNumericValue(coords[2]);

            return new int[] { row, col };
        }

        public static void PlaceQueenInField(Button field)
        {
            Image image = new Image();
            image.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"pack://application:,,,/Resources/Queen.png"));

            StackPanel stackPnl = new StackPanel();
            stackPnl.Orientation = Orientation.Horizontal;
            stackPnl.Margin = new Thickness(10);
            stackPnl.Children.Add(image);

            field.Content = stackPnl;
        }

        public static bool hasQueenInField(Button field)
        {
            return field.Content != null && field.Content != "";
        }
    }
}
