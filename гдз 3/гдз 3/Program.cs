using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class GameObject
{
    public char Symbol { get; protected set; }
    public int X { get; set; }
    public int Y { get; set; }
    public ConsoleColor Color { get; protected set; }
}

public class Player : GameObject
{
    public Player(int x, int y)
    {
        X = x;
        Y = y;
        Symbol = 'C';
        Color = ConsoleColor.White;
    }

    // Метод для изменения символа игрока при активации плитки
    public void Activate()
    {

        Symbol = 'Ⓒ';
        Color = ConsoleColor.Blue;
    }
    public void Deactivate()
    {
        Symbol = 'C';
        Color = ConsoleColor.White;


    }

    public override string ToString() => $"Player at ({X}, {Y})";
}

public class Stone : GameObject
{
    public Stone(int x, int y)
    {
        X = x;
        Y = y;
        Symbol = 'R';
        Color = ConsoleColor.Magenta;
    }

    // Метод для изменения символа камня при активации
    public void Activate()
    {
        Symbol = 'Ⓡ';
        Color = ConsoleColor.Blue;


    }
    public void Deactivate()
    {
        Symbol = 'R';
        Color = ConsoleColor.Magenta;


    }

    public override string ToString() => $"Stone at ({X}, {Y})";
}

public class Tree : GameObject
{
    public Tree(int x, int y)
    {
        X = x;
        Y = y;
        Symbol = 'T';
        Color = ConsoleColor.Gray;


    }

    public override string ToString() => $"Tree at ({X}, {Y})";
}

public class Tile : GameObject
{
    public bool IsActive { get; private set; }

    public Tile(int x, int y)
    {
        X = x;
        Y = y;
        Symbol = 'O';
        Color = ConsoleColor.Yellow;


    }

    // Активируем плитку
    public void Activate()
    {
        if (!IsActive)
        {
            IsActive = true;
            Color = ConsoleColor.Blue;
            Console.Write(Symbol);
            
        }
    }

    // Деактивируем плитку
    public void Deactivate()
    {
        if (IsActive)
        {
            IsActive = false;
            Symbol = 'O';
            Color = ConsoleColor.Yellow;
            Console.Write(Symbol);
            Console.ResetColor();
        }
    }

    public override string ToString() => $"Tile at ({X}, {Y})";
}

public class Level
{
    private readonly List<GameObject> _objects;
    public Player Player { get; private set; }
    public bool IsCompleted { get; private set; }

    public Level(List<GameObject> objects, Player player)
    {
        _objects = objects;
        Player = player;
    }

    public List<GameObject> GetObjects() => _objects.ToList();

    // Проверка завершения уровня
    public void CheckCompletion()
    {
        var tiles = _objects.Where(obj => obj is Tile).Cast<Tile>().ToList();
        var stones = _objects.Where(obj => obj is Stone).Cast<Stone>().ToList();

        foreach (var tile in tiles)
        {
            if (!tile.IsActive && !stones.Any(stone => stone.X == tile.X && stone.Y == tile.Y))
                return;
        }

        IsCompleted = true;
    }

    // Обновление состояния плиток после хода
    public void UpdateTiles()
    {
        foreach (var tile in _objects.OfType<Tile>())
        {
            var onTile = _objects.FirstOrDefault(obj => obj.X == tile.X && obj.Y == tile.Y);
            if (onTile != null && (onTile is Player || onTile is Stone))
            {
                tile.Activate();
            }
            else
            {
                tile.Deactivate();
            }
        }
    }
}

public class GameEngine
{
    private const int Width = 10;
    private const int Height = 10;
    private readonly List<Level> _levels;
    private int _currentLevelIndex;

    public GameEngine(List<Level> levels)
    {
        _levels = levels;
        _currentLevelIndex = 0;
    }

    //запуск игры
    public void Start()
    {
        while (_currentLevelIndex < _levels.Count)
        {
            PlayCurrentLevel();
            _currentLevelIndex++;
        }

        Console.WriteLine("Поздравляем! Вы прошли все уровни!");
    }

    //игрок играет текущий уровень
    private void PlayCurrentLevel()
    {
        var level = _levels[_currentLevelIndex];
        DrawLevel(level.GetObjects());

        while (!level.IsCompleted)
        {
            Console.WriteLine($"Уровень {_currentLevelIndex + 1}");

            MovePlayer(level);
            level.CheckCompletion();
            level.UpdateTiles();
            DrawLevel(level.GetObjects());
            

        }




    }

    // Отображение текущего уровня
    private static void DrawLevel(IEnumerable<GameObject> objects)
    {
        Console.Clear();

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                var obj = objects.FirstOrDefault(o => o.X == x && o.Y == y);
                if (obj != null)
                {
                    Console.ForegroundColor = obj.Color;

                    Console.Write(obj.Symbol);
                    Console.ResetColor(); 
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write('#');
                    Console.ForegroundColor = ConsoleColor.DarkGreen;

                }
            }
            Console.WriteLine();
        }
    }

    //перемещение игрока
    private void MovePlayer(Level level)
    {
        ConsoleKey key;
        do
        {
            key = Console.ReadKey(true).Key;
        } while (key != ConsoleKey.UpArrow &&
                 key != ConsoleKey.DownArrow &&
                 key != ConsoleKey.LeftArrow &&
                 key != ConsoleKey.RightArrow);

        var player = level.Player;
        var newX = player.X;
        var newY = player.Y;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                newY--;
                break;
            case ConsoleKey.DownArrow:
                newY++;
                break;
            case ConsoleKey.LeftArrow:
                newX--;
                break;
            case ConsoleKey.RightArrow:
                newX++;
                break;
        }

        if (newX >= 0 && newX < Width && newY >= 0 && newY < Height)
        {
            var targetObj = level.GetObjects().FirstOrDefault(obj => obj.X == newX && obj.Y == newY);

            if (targetObj is Stone stone)
            {
                var nextX = newX;
                var nextY = newY;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        nextY--;
                        break;
                    case ConsoleKey.DownArrow:
                        nextY++;
                        break;
                    case ConsoleKey.LeftArrow:
                        nextX--;
                        break;
                    case ConsoleKey.RightArrow:
                        nextX++;
                        break;
                }

                var nextTarget = level.GetObjects().FirstOrDefault(obj => obj.X == nextX && obj.Y == nextY);
                if (nextTarget is Tile)
                {
                    stone.X = nextX;
                    stone.Y = nextY;
                    stone.Activate();


                }
                else if (nextTarget is Tile == false)
                
                {
                    stone.X = nextX;
                    stone.Y = nextY;
                    stone.Deactivate();
                }
            }
            else if (targetObj is Tile tile)
            {
                player.X = newX;
                player.Y = newY;
                player.Activate();

            }
            else if (targetObj == null)
            {
                player.X = newX;
                player.Y = newY;
                player.Deactivate();
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        //создание уровней
        var levels = CreateLevels();

        //запуск игры
        new GameEngine(levels).Start();
    }

    private static List<Level> CreateLevels()
    {
        var player1 = new Player(0, 0);
        var stone11 = new Stone(8, 6);
        var stone12 = new Stone(3, 5);
        var tree11 = new Tree(5, 6);
        var tree12 = new Tree(7, 4);
        var tile11 = new Tile(2, 3);
        var tile12 = new Tile(7, 7);
        var tile13 = new Tile(8, 9);
        var objects1 = new List<GameObject>
        {
            player1,
            stone11,
            stone12,
            tree11,
            tree12,
            tile11,
            tile12,
            tile13

        };
        var level1 = new Level(objects1, player1);
       

        
        var player2 = new Player(4, 4);
        var stone21 = new Stone(6, 7);
        var stone22 = new Stone(8, 7);
        var stone23 = new Stone(3, 6);
        var tree21 = new Tree(1, 5);
        var tree22 = new Tree(3, 7);
        var tree23 = new Tree(6, 2);
        var tile21 = new Tile(2, 2);
        var tile22 = new Tile(0, 5);
        var tile23 = new Tile(8, 5);
        var tile24 = new Tile(1, 8);
        var objects2 = new List<GameObject>
        {
        player2,
        stone21,
        stone22,
        stone23,
        tree21,
        tree22,
        tree23,
        tile21,
        tile22,
        tile23,
        tile24,
        
        };
        var level2 = new Level(objects2, player2);

        return new List<Level>
        {
            level1,
            level2
        }; 
    }
}