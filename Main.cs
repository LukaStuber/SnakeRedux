namespace SnakeRedux {
    class Snake {        
        static void Main() {
            Console.Clear();
            Console.Title = "slayworldsnake";
            Console.CursorVisible = false;
            
            ConsoleKey lastInput;

            Console.WriteLine("Enter to start...");
            lastInput = Console.ReadKey().Key;
            
            while (lastInput != ConsoleKey.Enter) {
                lastInput = Console.ReadKey(true).Key;
            }

            Thread inputThread = new Thread(delegate ()
            {
                InputLoop(ref lastInput);
            });
            inputThread.Start();


            
        }

        static void InputLoop(ref ConsoleKey lastInput) {
            while (true) {
                Console.WriteLine(lastInput);

                if (lastInput == ConsoleKey.Enter) {
                    StartGame();
                }
                if (lastInput == ConsoleKey.Escape) {
                    EndGame();
                }

                lastInput = Console.ReadKey(true).Key;
            }
        }

        static void StartGame() {

        }

        static void EndGame() {
            Console.CursorVisible = true;
            Environment.Exit(0);
        }
    }
}