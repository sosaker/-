using System;
using System.Collections.Generic;


class MazeGame
{
    
    private const int Width = 20;
    private const int Height = 10;

    
    private static readonly char WallChar = '#';
    private static readonly char PathChar = '.';
    private static readonly char PlayerChar = '@';
    private static readonly char ExitChar = 'E';

    
    struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    
    private static readonly List<Point> Directions = new List<Point>
    {
        new Point(-1, 0),
        new Point(1, 0),
        new Point(0, -1),
        new Point(0, 1)
    };

    //массив для хранения лабиринта
    private char[,] maze;

    //координаты игрока
    private Point playerPosition;

    //количество ходов до завершения игры
    private int maxMoves;

    private int currentMove;

    
    public MazeGame()
    {
        GenerateMaze();
        PlacePlayerAtStart();
        PlaceExitAtEnd();
        maxMoves = Width * Height / 2;
        currentMove = 0;
    }

    //генерация случайного лабиринта
    private void GenerateMaze()
    {
        maze = new char[Width, Height];
        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Height; ++j)
            {
                maze[i, j] = WallChar;
            }
        }

        var stack = new Stack<Point>();
        var startPoint = new Point(1, 1);
        stack.Push(startPoint);

        while (stack.Count > 0)
        {
            var currentCell = stack.Peek();
            maze[currentCell.X, currentCell.Y] = PathChar;

            var unvisitedNeighbors = GetUnvisitedNeighbors(currentCell);
            if (unvisitedNeighbors.Count == 0)
            {
                stack.Pop();
            }
            else
            {
                var nextCell = unvisitedNeighbors[new Random().Next(unvisitedNeighbors.Count)];
                CarvePassage(currentCell, nextCell);
                stack.Push(nextCell);
            }
        }
    }

    //список непосещённых соседей текущей клетки
    private List<Point> GetUnvisitedNeighbors(Point cell)
    {
        var neighbors = new List<Point>();
        foreach (var direction in Directions)
        {
            var neighbor = new Point(cell.X + direction.X * 2, cell.Y + direction.Y * 2);
            if (IsInBounds(neighbor) && maze[neighbor.X, neighbor.Y] == WallChar)
            {
                neighbors.Add(neighbor);
            }
        }
        return neighbors;
    }

    //проверка, находится ли точка внутри границ лабиринта
    private bool IsInBounds(Point point)
    {
        return point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;
    }

    //проход между двумя клетками
    private void CarvePassage(Point from, Point to)
    {
        var passage = new Point((from.X + to.X) / 2, (from.Y + to.Y) / 2);
        maze[passage.X, passage.Y] = PathChar;
    }

    
    private void PlacePlayerAtStart()
    {
        playerPosition = new Point(1, 1); 
    }

   
    private void PlaceExitAtEnd()
    {
        maze[Width - 2, Height - 2] = ExitChar; 
    }

    //текущее состояние лабиринта
    private void DrawMaze()
    {
        Console.Clear();
        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Height; ++j)
            {
                if (playerPosition.X == i && playerPosition.Y == j)
                {
                    Console.Write(PlayerChar);
                }
                else
                {
                    Console.Write(maze[i, j]);
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine($"Ходов осталось: {maxMoves - currentMove}");
    }

    
    private void ProcessInput()
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
        switch (key.Key)
        {
            case ConsoleKey.LeftArrow:
                MovePlayer(new Point(0, -1));
                break;
            case ConsoleKey.RightArrow:
                MovePlayer(new Point(0, 1));
                break;
            case ConsoleKey.UpArrow:
                MovePlayer(new Point(-1, 0));
                break;
            case ConsoleKey.DownArrow:
                MovePlayer(new Point(1, 0));
                break;
        }
    }

   
    private void MovePlayer(Point direction)
    {
        var newPosition = new Point(playerPosition.X + direction.X, playerPosition.Y + direction.Y);
        if (IsInBounds(newPosition) && maze[newPosition.X, newPosition.Y] != WallChar)
        {
            playerPosition = newPosition;
            currentMove++;
        }
    }

   
    private bool HasFoundExit()
    {
        return maze[playerPosition.X, playerPosition.Y] == ExitChar;
    }

    //запуск игры
    public void Run()
    {
        while (!HasFoundExit() && currentMove < maxMoves)
        {
            DrawMaze();
            ProcessInput();
           
        }

        DrawMaze();
        if (HasFoundExit())
        {
            Console.WriteLine("Вы нашли выход!");
        }
        else
        {
            Console.WriteLine("Время вышло. Вы проиграли.");
        }
    }

    
    static void Main(string[] args)
    {
        var game = new MazeGame();
        game.Run();
    }
}