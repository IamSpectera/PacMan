using System;
using System.Drawing;

public class Pacman
{
    public int X { get; set; }
    public int Y { get; set; }
    public Image Sprite { get; set; }

    public Pacman()
    {
        Sprite = Image.FromFile("..\\..\\images\\PM-pacman-right.png");
    }
    public void Move(string direction, int gridSize, Func<int, int, bool> isCollidingWithWall, int windowWidth, int windowHeight)
    {
        if (isCollidingWithWall(X, Y))
        {
            return;
        }

        int newX = X;
        int newY = Y;

        switch (direction)
        {
            case "Up":
                newY = (Y - gridSize < 0) ? 0 : Y - gridSize;
                break;
            case "Down":
                newY += gridSize;
                break;
            case "Left":
                newX = (X - gridSize < 0) ? 0 : X - gridSize;
                break;
            case "Right":
                newX += gridSize;
                break;
        }

        if (!isCollidingWithWall(newX, newY))
        {
            X = newX;
            Y = newY;
        }
        else
        {
            // Revised logic for adjusting position on collision detection
            switch (direction)
            {
                case "Up":
                    Y = newY - (newY % gridSize) + gridSize;
                    break;
                case "Down":
                    Y = Math.Min(newY - (newY % gridSize), windowHeight - gridSize);
                    break;
                case "Left":
                    X = newX - (newX % gridSize) + gridSize;
                    break;
                case "Right":
                    X = Math.Min(newX - (newX % gridSize), windowWidth - gridSize);
                    break;
            }
        }
    }

}