using System;
using System.Drawing;

namespace PacMan5
{
    public class Ghost
{
    public int GridX { get; set; }
    public int GridY { get; set; }
    public Image Sprite { get; set; }
    
    public Ghost()
    {
        Sprite = Image.FromFile("..\\..\\images\\PM-ghost.png");
    }

    public void MoveTowards(Pacman pacman, int gridSize, Func<int, int, bool> isCollidingWithWall, int formWidth, int formHeight)
    {
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };
        double minDistance = double.MaxValue;
        int bestDir = -1;

        for (int dir = 0; dir < 4; dir++)
        {
            int newGridX = GridX + dx[dir];
            int newGridY = GridY + dy[dir];

            if (newGridX < 0 || newGridX >= formWidth / gridSize || newGridY < 0 || newGridY >= formHeight / gridSize || isCollidingWithWall(newGridX, newGridY))
                continue;

            double distance = Math.Sqrt(Math.Pow(newGridX - pacman.GridX, 2) + Math.Pow(newGridY - pacman.GridY, 2));

            if (distance < minDistance)
            {
                minDistance = distance;
                bestDir = dir;
            }
        }

        if (bestDir != -1)
        {
            GridX += dx[bestDir];
            GridY += dy[bestDir];
        }
    }
}
}