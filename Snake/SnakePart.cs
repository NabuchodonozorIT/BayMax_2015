using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake
{
    public class SnakePart
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Rectangle Rect { get; private set; }

        public SnakePart(int x, int y)
        {
            X = x;
            Y = y;
            Rect = new Rectangle();
            Rect.Width = Rect.Height = 20;
            Rect.Fill = Brushes.Black;
        }
    }
}
