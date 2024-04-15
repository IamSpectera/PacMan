using System.Collections.Generic;
using System.Drawing;

public class GameBoard
{
    public int[,] Layout { get; set; }
    public List<Rectangle> Walls { get; set; }
    public int GridSize { get; set; }
    public int WallThickness { get; set; }
    public int WallSize { get; set; }
    
    public List<Pacman.Food> Foods { get; set; }


    public GameBoard(int gridSize, int wallThickness, int wallSize)
    {
        GridSize = gridSize;
        WallThickness = wallThickness;
        WallSize = wallSize;

        // Initialize the Walls list
        Walls = new List<Rectangle>();
        // Initialize the Foods list
        Foods = new List<Pacman.Food>();
        

        // Layout of the game arena
        Layout = new int[,]
        {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1 },
            { 1, 3, 1, 1, 3, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 3, 1, 1, 3, 1 },
            { 1, 3, 1, 1, 3, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 3, 1, 1, 3, 1 },
            { 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1 },
            { 1, 3, 1, 1, 3, 1, 3, 1, 1, 1, 1, 1, 1, 3, 1, 3, 1, 1, 3, 1 },
            { 1, 3, 3, 3, 3, 1, 3, 1, 1, 1, 1, 1, 1, 3, 1, 3, 3, 3, 3, 1 },
            { 1, 1, 1, 1, 3, 1, 3, 3, 3, 3, 3, 3, 3, 3, 1, 3, 1, 1, 1, 1 },
            { 1, 0, 0, 1, 3, 1, 3, 1, 0, 0, 0, 0, 1, 3, 1, 3, 1, 0, 0, 1 },
            { 1, 1, 1, 1, 3, 1, 3, 1, 0, 0, 0, 0, 1, 3, 1, 3, 1, 1, 1, 1 },
            { 1, 3, 3, 3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3, 3, 1 },
            { 1, 1, 1, 1, 3, 1, 3, 3, 3, 3, 3, 3, 3, 3, 1, 3, 1, 1, 1, 1 },
            { 1, 0, 0, 1, 3, 1, 3, 3, 3, 1, 1, 3, 3, 3, 1, 3, 1, 0, 0, 1 },
            { 1, 1, 1, 1, 3, 1, 3, 1, 1, 1, 1, 1, 1, 3, 1, 3, 1, 1, 1, 1 },
            { 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1 },
            { 1, 3, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 3, 1 },
            { 1, 3, 1, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 1, 3, 1 },
            { 1, 3, 1, 3, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 3, 1, 3, 1 },
            { 1, 3, 1, 3, 1, 1, 1, 1, 3, 3, 3, 3, 1, 1, 1, 1, 3, 1, 3, 1 },
            { 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        };
        
        // Create the walls based on the layout
        for (int i = 0; i < Layout.GetLength(0); i++)
        {
            for (int j = 0; j < Layout.GetLength(1); j++)
            {
                if (Layout[i, j] == 1)
                {
                    // Add a wall of normal height
                    Walls.Add(new Rectangle(j * GridSize, i * GridSize, WallThickness, WallThickness));
                }
                else if (Layout[i, j] == 2)
                {
                    // Gate for the ghosts
                    Walls.Add(new Rectangle(j * GridSize, i * GridSize, WallThickness, WallThickness * 2));
                }
                else if (Layout[i, j] == 3)
                {
                    // Add a food item
                    Foods.Add(new Pacman.Food(j * GridSize, i * GridSize, GridSize / 4));
                }
            }
        }
    }
}