using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using PluginInterface;

namespace Snake
{

    public class SnakeGamePlayerLogic : IPlugin
    {
        private bool CheckCollisionKeyL = false;
        private bool CheckCollisionKeyR = false;
        private bool CheckCollisionKeyU = false;
        private bool CheckCollisionKeyD = false;
        private bool ProtectionMove = true;
        private int speed = 150;
        private MySnake _snake;
        private static readonly int SIZE = 25;
        private int _directionX = 1;
        private int _directionY = 0;
        private DispatcherTimer _timer;
        private SnakePart _food;
        private int _partsToAdd;
        private Grid _grid;
        private Grid _gridGameOver;
        private Grid _gridGridSnake;
        private EnumSnake enumSnake = new EnumSnake();

        public void Initialize(ref Grid gridGet, ref Grid gridGetGameOver, ref Grid gridGetGridSnake)
        {
            _grid = gridGet;
            _gridGameOver = gridGetGameOver;
            _gridGridSnake = gridGetGridSnake;
        }

        private void InitBoard()
        {
            for (int i = 0; i < _grid.Width / SIZE; i++)
            {
                ColumnDefinition columnDefinitions = new ColumnDefinition();
                columnDefinitions.Width = new GridLength(SIZE);
                _grid.ColumnDefinitions.Add(columnDefinitions);
            }
            for (int j = 0; j < _grid.Height - 160 / SIZE; j++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(SIZE);
                _grid.RowDefinitions.Add(rowDefinition);
            }
            _snake = new MySnake();
        }

        private void Cliner()
        {
            CheckCollisionKeyL = false;
            CheckCollisionKeyR = false;
            CheckCollisionKeyU = false;
            CheckCollisionKeyD = false;

            for (int i = 0; i < _grid.Width / SIZE; i++)
            {
                for (int j = 0; j < _grid.Height / SIZE; j++)
                {
                    _food = new SnakePart(i, j);
                    _food.Rect.Width = _food.Rect.Height = 23;
                    _food.Rect.Fill = Brushes.Orange;
                    _grid.Children.Add(_food.Rect);
                    Grid.SetColumn(_food.Rect, _food.X);
                    Grid.SetRow(_food.Rect, _food.Y);
                }
            }
        }

        private void InitSnake()
        {
            _grid.Children.Add(_snake.Head.Rect);
            foreach (SnakePart snakePart in _snake.Parts)
                _grid.Children.Add(snakePart.Rect);
            _snake.RedrawSnake();
        }

        private void InitFood()
        {
            _food = new SnakePart(10, 10);
            _food.Rect.Width = _food.Rect.Height = 25;
            _food.Rect.Fill = Brushes.Red;
            _grid.Children.Add(_food.Rect);
            Grid.SetColumn(_food.Rect, _food.X);
            Grid.SetRow(_food.Rect, _food.Y);
        }

        private void InitTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, speed);
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            MoveSnake();
        }

        private void MoveSnake()
        {
            int snakePartCount = _snake.Parts.Count;
            if (_partsToAdd > 0)
            {
                SnakePart newPart = new SnakePart(_snake.Parts[_snake.Parts.Count - 1].X,
                    _snake.Parts[_snake.Parts.Count - 1].Y);
                _grid.Children.Add(newPart.Rect);
                _snake.Parts.Add(newPart);
                _partsToAdd--;
            }

            for (int i = snakePartCount - 1; i >= 1; i--)
            {
                _snake.Parts[i].X = _snake.Parts[i - 1].X;
                _snake.Parts[i].Y = _snake.Parts[i - 1].Y;
            }
            _snake.Parts[0].X = _snake.Head.X;
            _snake.Parts[0].Y = _snake.Head.Y;
            _snake.Head.X += _directionX;
            _snake.Head.Y += _directionY;

            if (CheckCollision())
                EndGame();
            else
            {
                if (CheckFood())
                    RedrawFood();
                _snake.RedrawSnake();
            }
            ProtectionMove = true;
        }

        private void RedrawFood()
        {
            Grid.SetColumn(_food.Rect, _food.X);
            Grid.SetRow(_food.Rect, _food.Y);
        }

        private bool CheckFood()
        {
            Random rand = new Random();
            if (_snake.Head.X == _food.X && _snake.Head.Y == _food.Y)
            {
                _partsToAdd += 1;
                for (int i = 0; i < 20; i++)
                {
                    int x = rand.Next(0, (int)(_grid.Width / SIZE));
                    int y = rand.Next(0, (int)(_grid.Height / SIZE));
                    if (IsFieldFree(x, y))
                    {
                        _food.X = x;
                        _food.Y = y;
                        return true;
                    }
                }
                for (int i = 0; i < _grid.Width / SIZE; i++)
                    for (int j = 0; j < _grid.Height / SIZE; j++)
                    {
                        if (IsFieldFree(i, j))
                        {
                            _food.X = i;
                            _food.Y = j;
                            return true;
                        }
                    }
                EndGame();
            }
            return false;
        }

        private bool IsFieldFree(int x, int y)
        {
            if (_snake.Head.X == x && _snake.Head.Y == y)
                return false;
            foreach (SnakePart snakePart in _snake.Parts)
            {
                if (snakePart.X == x && snakePart.Y == y)
                    return false;
            }
            return true;
        }

        private void EndGame()
        {
            _timer.Stop();
            _directionX = 0;
            _directionY = 1;
            _gridGameOver.Visibility = Visibility.Visible;
        }

        private bool CheckCollision()
        {
            if (CheckBoardCollision())
                return true;
            if (CheckItselfCollision())
                return true;
            return false;
        }

        private bool CheckBoardCollision()
        {
            if (_snake.Head.X < 0 || _snake.Head.X > _grid.Width / SIZE)
                return true;
            if (_snake.Head.Y < 0 || _snake.Head.Y > _grid.Height / SIZE)
                return true;
            return false;
        }

        private bool CheckItselfCollision()
        {
            foreach (SnakePart snakePart in _snake.Parts)
            {
                if (_snake.Head.X == snakePart.X && _snake.Head.Y == snakePart.Y)
                    return true;
            }
            return false;
        }

        public void GameSnakePlay()
        {
            InitBoard();
            Cliner();
            InitSnake();
            InitTimer();
            InitFood();
        }

        private void SwitchWSAD(EnumSnake.WSAD wsad)
        {
            switch (wsad)
            {
                case EnumSnake.WSAD.W:
                    CheckCollisionKeyL = false;
                    CheckCollisionKeyR = false;
                    CheckCollisionKeyU = false;
                    CheckCollisionKeyD = true;                   
                    break;
                case EnumSnake.WSAD.S:
                    CheckCollisionKeyL = false;
                    CheckCollisionKeyR = true;
                    CheckCollisionKeyU = false;
                    CheckCollisionKeyD = false;
                    break;
                case EnumSnake.WSAD.A:
                    CheckCollisionKeyL = true;
                    CheckCollisionKeyR = false;
                    CheckCollisionKeyU = false;
                    CheckCollisionKeyD = false;
                    break;
                case EnumSnake.WSAD.D:
                    CheckCollisionKeyL = false;
                    CheckCollisionKeyR = false;
                    CheckCollisionKeyU = true;
                    CheckCollisionKeyD = false;
                    break;
            }
            ProtectionMove = false;
        }

        public void UpGesturePassive()
        {
            if (CheckCollisionKeyU == false && ProtectionMove == true)
            {
                _directionX = 0;
                _directionY = -1;
                SwitchWSAD(EnumSnake.WSAD.W);
            }
        }

        public void DownGesturePassive()
        {
            if (CheckCollisionKeyD == false && ProtectionMove == true)
            {
                _directionX = 0;
                _directionY = 1;
                SwitchWSAD(EnumSnake.WSAD.D);
            }
        }

        public void RightGesturePassive()
        {
            if (CheckCollisionKeyR == false && ProtectionMove == true)
            {
                _directionX = 1;
                _directionY = 0;
                SwitchWSAD(EnumSnake.WSAD.A);
            }
        }

        public void LeftGesturePassive()
        {
            if (CheckCollisionKeyL == false && ProtectionMove == true)
            {
                _directionX = -1;
                _directionY = 0;
                SwitchWSAD(EnumSnake.WSAD.S);
            }
        }
    }
}