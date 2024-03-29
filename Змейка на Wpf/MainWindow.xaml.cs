﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Змейка_на_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Fields
        private List<int> ListResult { get; set; }
        
        private readonly Dictionary<GridValue, ImageSource> gridValToImage = new()
        {
            { GridValue.Empty, Images.Empty },
            { GridValue.Snake, Images.Body },
            { GridValue.Food, Images.Food },     
        };

        private readonly Dictionary<Direction, int> dirToRotation = new()
        {
            { Direction.Up, 0 },
            { Direction.Right, 90 },
            { Direction.Down, 180 },
            { Direction.Left, 270 }
        };
        private int rows = new Random().Next(10, 40);
        private int cols = new Random().Next(10, 40);
        private readonly Image[,] gridImages;
        private GameState gameState;
        private bool gameRunning;
        private bool gamePaused = false;
        private bool isFullScreen = false;
        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetupGrid();
            gameState = new GameState(rows, cols);
            Pause.Visibility = Visibility.Hidden;
            ListResult = new() { 0 };
        }
        #endregion

        #region Methods
        private async Task RunGame()
        {
            Draw();
            await ShowCountDown();
            Overlay.Visibility = Visibility.Hidden;
            await GameLoop();
            await ShowGameOver();
            gameState = new GameState(rows, cols);

        }
        private async Task GameLoop()
        {
            while (!gameState.GameOver)
            {
                await Task.Delay(120);
                if (!gamePaused)
                {
                    Pause.Visibility = Visibility.Hidden;
                    gameState.Move();
                    Draw();
                }
                else
                {
                    Pause.Visibility = Visibility.Visible;
                    PauseText.Text = "PRESS ESCAPE TO CONTINUE OR PRESS ENTER TO EXIT";
                }
            }
        }
        private Image[,] SetupGrid()
        {
            Image[,] images = new Image[rows, cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;
            GameGrid.Width = GameGrid.Height * (cols / (double)rows);
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Image image = new()
                    {
                        Source = Images.Empty,
                        RenderTransformOrigin = new Point(0.5, 0.5)
                    };
                    images[r, c] = image;
                    GameGrid.Children.Add(image);
                }
            }
            return images;
        }
        private void Draw()
        {
            DrawGrid();
            DrawSnakeHead();
            ScoreText.Text = $"Score {gameState.Score}";
            BestText.Text = $"Best {ListResult[ListResult.LastIndexOf(ListResult.Max())]}";
        }
        private void DrawGrid()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    GridValue gridVal = gameState.Grid[r, c];
                    gridImages[r, c].Source = gridValToImage[gridVal];
                    gridImages[r, c].RenderTransform = Transform.Identity;
                }
            }
        }
        private void DrawSnakeHead()
        {
            Position headPos = gameState.HeadPosition();
            Image image = gridImages[headPos.Row, headPos.Col];
            image.Source = Images.Head;
            int rotation = dirToRotation[gameState.Dir];
            image.RenderTransform = new RotateTransform(rotation);
        }
        private async Task DrawDeadSnake()
        {
            List<Position> positions = new List<Position>(gameState.SnakePositions());
            for (int i = 0; i < positions.Count; i++)
            {
                Position pos = positions[i];
                ImageSource source = (i == 0) ? Images.DeadHead : Images.DeadBody;
                gridImages[pos.Row, pos.Col].Source = source;
                if (i <= 10)
                    await Task.Delay(50);
                else
                    await Task.Delay(20);
            }
        }
        private async Task ShowCountDown()
        {
            for (int i = 3; i >= 1; i--)
            {
                OverlayText.Text = i.ToString();
                await Task.Delay(500);
            }
        }
        private async Task ShowGameOver()
        {
            await DrawDeadSnake();
            ListResult.Add(gameState.Score);
            int i = ListResult.LastIndexOf(ListResult.Max());
            await Task.Delay(500);
            Overlay.Visibility = Visibility.Visible;
            OverlayText.Text = "PRESS ANY KEY TO START";
        }
        #endregion

        #region EventsHandlers
        private async void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }
            switch (e.Key)
            {
                case Key.Left or Key.A:
                    if (gameState.Dir != Direction.Right)
                        gameState.ChangeDirection(Direction.Left);
                    break;
                case Key.Right or Key.D:
                    if (gameState.Dir != Direction.Left)
                        gameState.ChangeDirection(Direction.Right);
                    break;
                case Key.Up or Key.W:
                    if (gameState.Dir != Direction.Down)
                        gameState.ChangeDirection(Direction.Up);
                    break;
                case Key.Down or Key.S:
                    if (gameState.Dir != Direction.Up)
                        gameState.ChangeDirection(Direction.Down);
                    break;
                case Key.Escape:
                    if (gamePaused) gamePaused = false; else gamePaused = true;
                    break;
                case Key.R:
                    MainWindow mw = new();
                    mw.Show();
                    this.Close();
                    Application.Current.MainWindow = mw;
                    break;
                case Key.Enter:
                    if (gamePaused)
                    {
                        Application.Current.Shutdown();
                    }
                    break;
                case Key.F11:
                    if (!isFullScreen)
                    {
                        WindowState = WindowState.Maximized;
                        isFullScreen = true;
                    }
                    else
                    {
                        WindowState = WindowState.Normal;
                        isFullScreen = false;
                    }
                    break;

                default:
                    {
                        if (Overlay.Visibility == Visibility.Visible)
                            e.Handled = true;
                        if (!gameRunning)
                        {
                            gameRunning = true;
                            await RunGame();
                            gameRunning = false;
                        }
                        break;
                    }
            }
        }
        #endregion
        
    }
}
