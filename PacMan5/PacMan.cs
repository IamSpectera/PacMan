using System;
using System.Drawing;

public class Pacman
{
    public int GridX { get; set; }
    public int GridY { get; set; }
    public Image Sprite { get; set; }
    public string LastSuccessfulDirection { get; set; }
    
    public int Lives { get; set; } = 3; // Initialize with 3 lives

    public Pacman()
    {
        Sprite = Image.FromFile("..\\..\\images\\PM-pacman-right.png");
        LastSuccessfulDirection = "Right";
    }

    public void Move(string direction, int gridSize, Func<int, int, bool> isCollidingWithWall, int windowWidth, int windowHeight)
    {
        int newGridX = GridX;
        int newGridY = GridY;

        switch (direction)
        {
            case "Up":
                newGridY = Math.Max(0, GridY - 1);
                break;
            case "Down":
                newGridY = Math.Min(windowHeight / gridSize - 1, GridY + 1);
                break;
            case "Left":
                newGridX = Math.Max(0, GridX - 1);
                break;
            case "Right":
                newGridX = Math.Min(windowWidth / gridSize - 1, GridX + 1);
                break;
        }

        if (!isCollidingWithWall(newGridX, newGridY))
        {
            GridX = newGridX;
            GridY = newGridY;
            LastSuccessfulDirection = direction;

            // Change the sprite based on the direction
            switch (direction)
            {
                case "Up":
                    Sprite = Image.FromFile("..\\..\\images\\PM-pacman-up.png");
                    break;
                case "Down":
                    Sprite = Image.FromFile("..\\..\\images\\PM-pacman-down.png");
                    break;
                case "Left":
                    Sprite = Image.FromFile("..\\..\\images\\PM-pacman-left.png");
                    break;
                case "Right":
                    Sprite = Image.FromFile("..\\..\\images\\PM-pacman-right.png");
                    break;
            }
        }
        else
        {
            // If the new direction is blocked, try to move in the last successful direction
            if (direction != LastSuccessfulDirection)
            {
                Move(LastSuccessfulDirection, gridSize, isCollidingWithWall, windowWidth, windowHeight);
            }
        }
    }
    
    public class Food
    {
        public Rectangle Shape { get; set; }
        public bool IsEaten { get; set; }

        public Food(int x, int y, int size)
        {
            Shape = new Rectangle(x, y, size, size);
            IsEaten = false;
        }
    }
}