using System;
using System.Drawing;

namespace PacMan5
{
    public class Ghost
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Image Sprite { get; set; }

        public Ghost()
        {
            Sprite = Image.FromFile("..\\..\\images\\PM-ghost.png");
        }
    }
}