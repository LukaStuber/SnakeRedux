using System;

namespace SnakeRedux
{
    class Snake
    {
        struct Board
        {
            public int width;
            public int height;
            public int[,] board;
            public Board(int x, int y)
            {
                width = x;
                height = y;
                board = new int[width, height];
            }
        }
        struct Player
        {
            public int x;
            public int y;
            public int length;
            public int[,] snake;
            public Player(Board board)
            {
                x = board.width / 2;
                y = board.height / 2;
                length = 3;
                snake = new int[board.width, board.height];
                for (int i = 0; i < 3; i++)
                {
                    snake[x - i, y] = i + 1;
                }
            }
        }
        struct Apple(Board board)
        {
            public bool[,] apples = new bool[board.width, board.height];
        }
        static void Main()
        {
            Console.Clear();
            Console.Title = "slayworldsnake";
            Console.CursorVisible = false;

            ConsoleKey lastInput;
            ConsoleKey extraInput;
            extraInput = ConsoleKey.None;

            Board board = new Board(10, 9);
            Player player = new Player(board);
            Apple apple = new Apple(board);

            Console.WriteLine("Enter to start...");
            lastInput = Console.ReadKey().Key;

            while (lastInput != ConsoleKey.Enter)
            {
                if (lastInput == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                lastInput = Console.ReadKey(true).Key;
            }
            lastInput = ConsoleKey.D;
            Thread inputThread = new Thread(delegate ()
            {
                InputLoop(ref lastInput, ref extraInput);
            });
            inputThread.Start();

            /*for (int i = 0; i < board.height; i++)
            {
                for (int j = 0; j < board.width; j++)
                {
                    Console.Write(player.snake[j, i]);
                }
                Console.WriteLine();
            }*/
            GenerateApple(player, board, ref apple);
            GameLoop(board, player, apple, ref lastInput, ref extraInput);
        }
        static void GameLoop(Board board, Player player, Apple apple, ref ConsoleKey lastInput, ref ConsoleKey extraInput)
        {
            bool gameOver = false;
            bool gameWon = false;
            while (true)
            {
                if (player.x > board.width || player.x < 0 || player.y > board.height || player.y < 0)
                {
                    gameOver = true;
                }
                if (player.length == board.width * board.height)
                {
                    gameWon = true;
                }               

                Render(board, apple, player);
                switch (extraInput)
                {
                    case ConsoleKey.Escape:
                        EndGame();
                        break;
                    case ConsoleKey.Enter:
                        RestartGame(board, ref player, ref lastInput);
                        gameOver = false;
                        break;
                }
                extraInput = ConsoleKey.None;

                if (OnApple(board, ref apple, ref player))
                {
                    CutTail(board, ref player);
                    MoveSnake(board, lastInput, ref player);
                    GenerateApple(player, board, ref apple);
                }
                else
                {
                    CutTail(board, ref player);
                    MoveSnake(board, lastInput, ref player);
                }

                if (gameOver)
                {
                    Console.Clear();
                    Console.WriteLine("You lose!");
                    Console.WriteLine("Press any key to restart...");
                    Console.WriteLine("Press Escape to exit...");
                    switch (lastInput)
                    {
                        case ConsoleKey.Escape:
                            EndGame();
                            break;
                        default:
                            RestartGame(board, ref player, ref lastInput);
                            gameOver = false;
                            break;
                    }
                }
                if (gameWon)
                {
                    Console.Clear();
                    Console.WriteLine("You Win!");
                    Console.WriteLine("Press any key to restart...");
                    Console.WriteLine("Press Escape to exit...");
                    switch (lastInput)
                    {
                        case ConsoleKey.Escape:
                            EndGame();
                            break;
                        default:
                            RestartGame(board, ref player, ref lastInput);
                            gameWon = false;
                            break;
                    }
                }
                Thread.Sleep(500);
            }
        }
        static void InputLoop(ref ConsoleKey lastInput, ref ConsoleKey extraInput)
        {
            ConsoleKey input;
            while (true)
            {
                input = Console.ReadKey(true).Key;
                if (input == ConsoleKey.W || input == ConsoleKey.A || input == ConsoleKey.S || input == ConsoleKey.D)
                {
                    lastInput = input;
                }
                else
                {
                    extraInput = input;
                }
            }
        }
        static void RestartGame(Board board, ref Player player, ref ConsoleKey lastInput)
        {
            player = new Player(board);
            lastInput = ConsoleKey.D;
        }
        static void EndGame()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Environment.Exit(0);
        }

        static void GenerateApple(Player player, Board board, ref Apple apple)
        {
            Random r = new();
            bool done = false;
            int x, y;

            while (!done)
            {
                x = r.Next(0, board.width);
                y = r.Next(0, board.height);

                if (player.snake[x, y] == 0)
                {
                    apple.apples[x, y] = true;
                    done = true;
                }
            }
        }

        static bool OnApple(Board board, ref Apple apple, ref Player player)
        {
            if (apple.apples[player.x, player.y] == true)
            {
                player.length++;
                apple.apples[player.x, player.y] = false;

                return true;
            }
            return false;
        }

        static void CutTail(Board board, ref Player player)
        {
            for (int y = 0; y < board.height; y++)
            {
                for (int x = 0; x < board.width; x++)
                {
                    if (player.snake[x, y] == player.length)
                    {
                        player.snake[x, y] = 0;
                    }
                }
            }
        }

        static void MoveSnake(Board board, ConsoleKey lastInput, ref Player player)
        {
            bool headMoved = false;

            for (int y = 0; y < board.height; y++)
            {
                for (int x = 0; x < board.width; x++)
                {
                    if (player.snake[x, y] != 0 && player.snake[x, y] != 1)
                    {
                        player.snake[x, y]++;
                    }
                    else if (player.snake[x, y] == 1 && !headMoved)
                    {
                        headMoved = true;
                        player.snake[x, y]++;
                        switch (lastInput)
                        {
                            case ConsoleKey.W:
                                player.y -= 1;
                                break;
                            case ConsoleKey.A:
                                player.x -= 1;
                                break;
                            case ConsoleKey.S:
                                player.y += 1;
                                break;
                            case ConsoleKey.D:
                                player.x += 1;
                                break;
                        }
                        player.snake[player.x, player.y] = 1;
                    }
                }
            }
        }

        static void Render(Board board, Apple apple, Player player)
        {
            RenderBoard(board);
            RenderApple(board, apple);
            RenderSnake(board, player);
        }

        static void RenderBoard(Board board)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            for (int y = 0; y < board.height; y++)
            {
                for (int x = 0; x < board.width; x++)
                {
                    if (x % 2 == 0 && y % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    else if (x % 2 != 0 && y % 2 != 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.Write("    ");
                }
                Console.WriteLine();
                for (int x = 0; x < board.width; x++)
                {
                    if (x % 2 == 0 && y % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    else if (x % 2 != 0 && y % 2 != 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.Write("    ");
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        static void RenderApple(Board board, Apple apple)
        {
            for (int y = 0; y < board.height; y++)
            {
                for (int x = 0; x < board.width; x++)
                {
                    if (apple.apples[x, y] == true)
                    {
                        Console.CursorLeft = x * 4;
                        Console.CursorTop = y * 2;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("    ");
                        Console.CursorLeft = x * 4;
                        Console.CursorTop = y * 2 + 1;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("    ");
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        static void RenderSnake(Board board, Player player)
        {
            for (int y = 0; y < board.height; y++)
            {
                for (int x = 0; x < board.width; x++)
                {
                    if (player.snake[x, y] != 0)
                    {
                        Console.CursorLeft = x * 4;
                        Console.CursorTop = y * 2;
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("    ");
                        Console.CursorLeft = x * 4;
                        Console.CursorTop = y * 2 + 1;
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("    ");
                    }
                }         
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}