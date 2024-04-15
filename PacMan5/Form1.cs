using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace PacMan5
{
    public partial class Form1 : Form
    {
        private GameBoard gameBoard;
        private Pacman pacman;
        private Ghost ghost;
        private Timer gameTimer;
        private int gridSize = 20; // The size of pacman

        public Form1()
        {
            InitializeComponent();

            // Wall Dimensions
            gameBoard = new GameBoard(20, 10, 20);
            
            // Pacman Location on the board
            pacman = new Pacman { GridX = 185 / gridSize, GridY = 280 / gridSize };
            
            // Initialize Ghost's position
            ghost = new Ghost
            {
                GridX = 10, GridY = 10
            }; 

            gameTimer = new Timer();
            gameTimer.Interval = 225;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            this.KeyDown += Form1_KeyDown;
            this.KeyPreview = true;
        }
        
        // Starting Direction of Pacman 
        private string direction = "Right";

        // Key presses for the form
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Change direction based on key press & PNG
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    direction = "Up";
                    pacman.Sprite = Image.FromFile("..\\..\\images\\PM-pacman-up.png");
                    break;
                case Keys.Down:
                case Keys.S:
                    direction = "Down";
                    pacman.Sprite = Image.FromFile("..\\..\\images\\PM-pacman-down.png");
                    break;
                case Keys.Left:
                case Keys.A:
                    direction = "Left";
                    pacman.Sprite = Image.FromFile("..\\..\\images\\PM-pacman-left.png");
                    break;
                case Keys.Right:
                case Keys.D:
                    direction = "Right";
                    pacman.Sprite = Image.FromFile("..\\..\\images\\PM-pacman-right.png");
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw the walls
            foreach (var wall in gameBoard.Walls)
            {
                e.Graphics.FillRectangle(Brushes.Blue, wall.X, wall.Y, gameBoard.WallThickness, gameBoard.WallThickness);
            }

            e.Graphics.DrawImage(ghost.Sprite, new Rectangle(ghost.GridX * gridSize, ghost.GridY * gridSize, gridSize, gridSize));
            // Draw the Pacman
            e.Graphics.DrawImage(pacman.Sprite,
                new Rectangle((int)(pacman.GridX * gridSize), (int)(pacman.GridY * gridSize), gridSize, gridSize));

            // Draw the food
            foreach (Pacman.Food food in gameBoard.Foods)
            {
                if (!food.IsEaten)
                {
                    e.Graphics.FillEllipse(Brushes.Yellow, food.Shape);
                }
            }
            
        }
        
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // Store old position
            var oldGridX = pacman.GridX;
            var oldGridY = pacman.GridY;
            var oldGhostGridX = ghost.GridX;
            var oldGhostGridY = ghost.GridY;

            // Move Ghost towards Pacman
            ghost.MoveTowards(pacman, gridSize, IsCollidingWithWall, this.Width, this.Height);

            // Check if Ghost has caught Pacman
            if (ghost.GridX == pacman.GridX && ghost.GridY == pacman.GridY)
            {
                gameTimer.Stop();
                MessageBox.Show("Game Over, the ghost caught Pacman!");
                return;
            }

            // Redraws where ghost was so he isn't there no more
            this.Invalidate(new Rectangle(oldGhostGridX * gridSize, oldGhostGridY * gridSize, gridSize, gridSize));

            // Draws ghost in new location
            this.Invalidate(new Rectangle(ghost.GridX * gridSize, ghost.GridY * gridSize, gridSize, gridSize));
            
            
            // Move Pacman based on current direction
            pacman.Move(direction, gridSize, IsCollidingWithWall, this.Width, this.Height);

            foreach (Pacman.Food food in gameBoard.Foods)
            {
                if (!food.IsEaten && pacman.GridX == food.Shape.X / gridSize && pacman.GridY == food.Shape.Y / gridSize)
                {
                    food.IsEaten = true;
                    break;
                }
            }
            
            if (!gameBoard.Foods.Any(food => !food.IsEaten))
            {
                gameTimer.Stop();
                MessageBox.Show("Congratulations, you won!");
            }
            
            // Redraws where pacman was so he isnt there no more
            this.Invalidate(new Rectangle(oldGridX * gridSize, oldGridY * gridSize, gridSize, gridSize));

            // Draws pacman in new location
            this.Invalidate(new Rectangle(pacman.GridX * gridSize, pacman.GridY * gridSize, gridSize, gridSize));
        }

        private bool IsCollidingWithWall(int gridX, int gridY)
        {
            int x = gridX * gridSize;
            int y = gridY * gridSize;

            var pacmanRect = new Rectangle(x, y, gridSize, gridSize);
            return gameBoard.Walls.Any(wall => wall.IntersectsWith(pacmanRect));
        }
    }
}