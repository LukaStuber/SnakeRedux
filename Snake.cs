namespace SnakeRedux {
    class Snake {        
<<<<<<< HEAD

        struct Board {
            public int width;
            public int height;
            public int[,] board;

            public Board(int x, int y) {
                width = x;
                height = y;

                board = new int[width, height];
            }
        }

        struct Player {
            public int x;
            public int y;
            public int length;
            public bool[,] snake;

            public Player(Board board) {
                x = board.width / 2;
                y = board.height / 2;
                length = 1;

                snake = new bool[board.width, board.height];
                snake[x, y] = true;
            }
        }

        struct Apple {
            public int count;
            public int[] x;
            public int[] y;
            public bool[,] apples;

            public Apple(int num, Board board) {
                Random r = new Random();

                count = num;
                x = new int[count];
                y = new int[count];

                apples = new bool[board.width, board.height];

                for (int i = 0; i < count; i++) {
                    x[i] = r.Next(0, board.width);
                    x[i] = r.Next(0, board.height);

                    apples[x[i], y[i]] = true;
                }
            }
        }

=======
>>>>>>> dbe4dc86c4d508a1107c9367ac9741db8125d6fe
        static void Main() {
            Console.Clear();
            Console.Title = "slayworldsnake";
            Console.CursorVisible = false;
            
            ConsoleKey lastInput;

<<<<<<< HEAD
            Board board = new Board(10, 9);
            Player player = new Player(board);

=======
>>>>>>> dbe4dc86c4d508a1107c9367ac9741db8125d6fe
            Console.WriteLine("Enter to start...");
            lastInput = Console.ReadKey().Key;
            
            while (lastInput != ConsoleKey.Enter) {
<<<<<<< HEAD
                if (lastInput == ConsoleKey.Escape) {
                    Environment.Exit(0);
                }
                lastInput = Console.ReadKey(true).Key;
            }

            lastInput = ConsoleKey.D;

=======
                lastInput = Console.ReadKey(true).Key;
            }

>>>>>>> dbe4dc86c4d508a1107c9367ac9741db8125d6fe
            Thread inputThread = new Thread(delegate ()
            {
                InputLoop(ref lastInput);
            });
            inputThread.Start();

<<<<<<< HEAD
            GameLoop(ref lastInput, board, player);
        }

        static void GameLoop(ref ConsoleKey lastInput, Board board, Player player) {
            bool gameOver = false;
            bool gameWon = false;

            while (true) {
                if (player.x > board.width || player.x < 0 || player.y > board.height || player.y < 0) {
                    gameOver = true;
                }
                if (player.length == board.width * board.height) {
                    gameWon = true;
                }

                Render(board, player);

                switch (lastInput) {
                    case ConsoleKey.Escape:
                        EndGame();
                        break;
                    case ConsoleKey.Enter:
                        RestartGame(board, ref player, ref lastInput);
                        gameOver = false;
                        break;
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

                if (gameOver) {
                    Console.Clear();
                    Console.WriteLine("You lose!");
                    Console.WriteLine("Press any key to restart...");
                    Console.WriteLine("Press Escape to exit...");

                    switch (lastInput) {
                        case ConsoleKey.Escape:
                            EndGame();
                            break;
                        case ConsoleKey.Enter:
                            RestartGame(board, ref player, ref lastInput);
                            gameOver = false;
                            break;
                    }
                }
                if (gameWon) {
                    Console.Clear();
                    Console.WriteLine("You Win!");
                    Console.WriteLine("Press any key to restart...");
                    Console.WriteLine("Press Escape to exit...");

                    switch (lastInput) {
                        case ConsoleKey.Escape:
                            EndGame();
                            break;
                        case ConsoleKey.Enter:
                            RestartGame(board, ref player, ref lastInput);
                            gameWon = false;
                            break;
                    }
                }

                Thread.Sleep(500);
            }
=======

            
>>>>>>> dbe4dc86c4d508a1107c9367ac9741db8125d6fe
        }

        static void InputLoop(ref ConsoleKey lastInput) {
            while (true) {
<<<<<<< HEAD
=======
                Console.WriteLine(lastInput);

                if (lastInput == ConsoleKey.Enter) {
                    StartGame();
                }
                if (lastInput == ConsoleKey.Escape) {
                    EndGame();
                }

>>>>>>> dbe4dc86c4d508a1107c9367ac9741db8125d6fe
                lastInput = Console.ReadKey(true).Key;
            }
        }

<<<<<<< HEAD
        static void RestartGame(Board board, ref Player player, ref ConsoleKey lastInput) {
            player = new Player(board);
            lastInput = ConsoleKey.D;
        }

        static void EndGame() {
            Console.Clear();
            Console.CursorVisible = true;
            Environment.Exit(0);
        }

        static void Render(Board board, Player player) {
            Console.Clear();

            for (int y = 0; y < board.height; y++) {
                for (int x = 0; x < board.width; x++) {
                    if (player.x == x && player.y == y) {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    else if (x % 2 == 0 && y % 2 == 0)
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

                for (int x = 0; x < board.width; x++) {
                    if (player.x == x && player.y == y) {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    else if (x % 2 == 0 && y % 2 == 0)
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
    }
}
=======
        static void StartGame() {

        }

        static void EndGame() {
            Console.CursorVisible = true;
            Environment.Exit(0);
        }
    }
}
>>>>>>> dbe4dc86c4d508a1107c9367ac9741db8125d6fe
