using System.Collections.Generic;
using System.Windows.Controls;

namespace Snake
{
    public class MySnake
    {
        public Sna


        public MySnake()
        {
            Head = new SnakePart(20, 10);
            Head.Rect.Width = Head.Rect.Height = 25;
            Head.Rect.Fill = System.Windows.Media.Brushes.Red;
            Parts = new List<SnakePart>();
            Parts.Add(new SnakePart(19, 10));
            Parts.Add(new SnakePart(18, 10));
            Parts.Add(new SnakePart(17, 10));
            Parts.Add(new SnakePart(16, 10));
            Parts.Add(new SnakePart(15, 10));
        }

        public void RedrawSnake()
        {
            Grid.SetColumn(Head.Rect, Head.X);
            Grid.SetRow(Head.Rect, Head.Y);
            foreach (SnakePart snakePart in Parts)
            {
                Grid.SetColumn(snakePart.Rect, snakePart.X);
                Grid.SetRow(snakePart.Rect, snakePart.Y);
            }
        }
    }
}