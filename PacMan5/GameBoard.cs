using System;
using System.Collections.Generic;
using System.Drawing;
using PacMan5;

public class GameBoard
{
    public Image Sprite { get; set; }
    public List<Dots> Dots { get; set; }
    public List<Rectangle> Walls { get; set; }

    public GameBoard()
    {
        Sprite = Image.FromFile("..\\..\\images\\PM-pacmanarena.png");
        Dots = new List<Dots>();
        Walls = new List<Rectangle>();

        // Wall Creations
        Walls.Add(new Rectangle(100, 100, 50, 10));
    }
}