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
    
}
