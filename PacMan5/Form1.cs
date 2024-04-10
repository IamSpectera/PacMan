using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace PacMan5
{
    public partial class Form1 : Form
    {
        private GameBoard gameBoard;
        private Pacman pacman;
        private Ghost ghost;
        private Timer gameTimer;
        private int gridSize = 20; // Define the size of the grid cells

        public Form1()
        {
            InitializeComponent();

            gameBoard = new GameBoard();
            pacman = new Pacman { X = 50, Y = 50 }; // Initialize Pacman's position
            ghost = new Ghost { X = 100, Y = 100 }; // Initialize Ghost's position

            gameTimer = new Timer();
            gameTimer.Interval = 50;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            this.KeyDown += Form1_KeyDown;
            this.KeyPreview = true;
        }

        private string direction = "Right";

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    direction = "Up";
                    break;
                case Keys.Down:
                case Keys.S:
                    direction = "Down";
                    break;
                case Keys.Left:
                case Keys.A:
                    direction = "Left";
                    break;
                case Keys.Right:
                case Keys.D:
                    direction = "Right";
                    break;
            }
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawImage(gameBoard.Sprite, new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height));

            e.Graphics.DrawImage(pacman.Sprite,
                new Rectangle(pacman.X, pacman.Y, this.ClientSize.Width / 20, this.ClientSize.Height / 20));

            e.Graphics.DrawImage(ghost.Sprite,
                new Rectangle(ghost.X, ghost.Y, this.ClientSize.Width / 20, this.ClientSize.Height / 20));

            foreach (var pellet in gameBoard.Dots)
            {
                e.Graphics.FillRectangle(Brushes.White, new Rectangle(pellet.X, pellet.Y, 5, 5));
            }

            foreach (var wall in gameBoard.Walls)
            {
                e.Graphics.FillRectangle(Brushes.Blue, wall);
            }
        }


        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // Store old position
            var oldX = pacman.X;
            var oldY = pacman.Y;

            // Move Pacman based on current direction, now also passing window's width and height
            pacman.Move(direction, gridSize, IsCollidingWithWall, this.ClientSize.Width, this.ClientSize.Height);

            // Invalidate old position
            this.Invalidate(new Rectangle(oldX, oldY, this.ClientSize.Width / gridSize, this.ClientSize.Height / gridSize));

            // Invalidate new position
            this.Invalidate(new Rectangle(pacman.X, pacman.Y, this.ClientSize.Width / gridSize, this.ClientSize.Height / gridSize));
        }

        private bool IsCollidingWithWall(int x, int y)
        {
            var size = Math.Min(this.ClientSize.Width, this.ClientSize.Height) / 20;
            var offset = size / 2; // Adjust the offset value

            var pacmanRect = new Rectangle(x + offset, y + offset, size - offset, size - offset);

            return pacmanRect.Left < 0 || pacmanRect.Top < 0 || pacmanRect.Right > this.ClientSize.Width - size || pacmanRect.Bottom > this.ClientSize.Height - size || gameBoard.Walls.Any(wall => wall.IntersectsWith(pacmanRect));
        }
    }
}