internal class Program
{
    // Abstract class Player ensures that no direct object of Player can be created.
    // It also defines the structure for any type of player (real or AI).
    abstract class Player
    {
        public char Symbol { get; private set; }

        protected Player(char symbol)
        {
            Symbol = symbol;
        }

        // Abstract method that must be implemented by any subclass.
        public abstract int GetMove(char[,] board);
    }

    // RealPlayer represents a human player who manually inputs their move.
    class RealPlayer : Player
    {
        public RealPlayer(char symbol) : base(symbol) { }

        public override int GetMove(char[,] board)
        {
            int col;
            while (true)
            {
                Console.Write($"Player {Symbol}, choose a column (0-6): ");
                string input = Console.ReadLine();

                // Ensures input is a valid number within range.
                if (int.TryParse(input, out col) && col >= 0 && col <= 6)
                    return col;

                Console.WriteLine("Invalid input! Enter a number between 0-6.");
            }
        }
    }

    // AIPlayer represents a basic computer-controlled opponent.
    class AIPlayer : Player
    {
        private Random rand = new Random();

        public AIPlayer(char symbol) : base(symbol) { }

        public override int GetMove(char[,] board)
        {
            int col;

            // Randomly chooses a column that is not full.
            do
            {
                col = rand.Next(0, 7);
            } while (!IsColumnAvailable(board, col));

            Console.WriteLine($"AI Player {Symbol} chooses column {col}");
            return col;
        }

        // Checks if the top row of a column is empty, meaning the column is available.
        private bool IsColumnAvailable(char[,] board, int col)
        {
            return board[0, col] == '_';
        }
    }

    // This class handles the full game logic and flow.
    class NewGame
    {
        public void Start()
        {
            Board board = new Board();
            Player player1 = new RealPlayer('X');
            Player player2 = null;

            // Prompt user to choose game mode.
            while (true)
            {
                Console.WriteLine("Choose Game Mode:");
                Console.WriteLine("1. 2 Players");
                Console.WriteLine("2. 1 Player vs AI");
                Console.Write("Enter your Answer: ");
                string mode = Console.ReadLine();

                // Based on choice, assign appropriate player types.
                if (mode == "2")
                {
                    player2 = new AIPlayer('O');
                    break;
                }
                else if (mode == "1")
                {
                    player2 = new RealPlayer('O');
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please choose either 1 or 2.\n");
                }
            }

            Player currentPlayer = player1;

            // Initialize and display the game board.
            board.populateBoard();
            board.printBoard();

            // Main game loop
            while (true)
            {
                int column = currentPlayer.GetMove(board.gameBoard);
                bool success = board.userTurnOnTheBoard(column, currentPlayer.Symbol);

                if (success)
                {
                    board.printBoard();

                    // Check if current player has won.
                    if (board.CheckForWin(currentPlayer.Symbol))
                    {
                        Console.WriteLine($"Player {currentPlayer.Symbol} wins! Congratulations!");

                        // Ask if the user wants to play again.
                        while (true)
                        {
                            Console.Write("Do you want to play again? (1 = Yes, 2 = No): ");
                            string input = Console.ReadLine();

                            if (input == "1")
                            {
                                Console.Clear();
                                Start(); // Restart game
                                return;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("We will see each other again next time!");
                                Environment.Exit(0);
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter 1 or 2.");
                            }
                        }
                    }

                    // Switch to the next player.
                    if (currentPlayer == player1)
                    {
                        currentPlayer = player2;
                    }
                    else
                    {
                        currentPlayer = player1;
                    }
                }
            }
        }
    }

    // Handles the Connect 4 board grid, move placement, and win checking.
    class Board
    {
        public char[,] gameBoard = new char[7, 7];

        // Fills the board with empty placeholders ('_')
        public void populateBoard()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    gameBoard[i, j] = '_';
                }
            }
        }

        //Another method to print the current gameboard on the screen.
        public void printBoard()
        {
            Console.WriteLine();

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write(gameBoard[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("0 1 2 3 4 5 6"); // Shows the column indices.
            Console.WriteLine();
        }

        // Places a player’s symbol in the chosen column if possible.
        public bool userTurnOnTheBoard(int column, char userSymbol)
        {
            if (column < 0 || column > 6)
            {
                Console.WriteLine("Please choose between 0-6!");
                return false;
            }

            // Insert symbol in the lowest available row of the column.
            for (int row = 6; row >= 0; row--)
            {
                if (gameBoard[row, column] == '_')
                {
                    gameBoard[row, column] = userSymbol;
                    return true;
                }
            }

            Console.WriteLine("Column is already full! Please use other available columns.");
            return false;
        }

        // Checks all possible winning combinations: horizontal, vertical, and diagonal.
        public bool CheckForWin(char symbol)
        {
            // Horizontal check
            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col <= 3; col++)
                {
                    if (gameBoard[row, col] == symbol &&
                        gameBoard[row, col + 1] == symbol &&
                        gameBoard[row, col + 2] == symbol &&
                        gameBoard[row, col + 3] == symbol)
                    {
                        return true;
                    }
                }
            }

            // Vertical check
            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row <= 3; row++)
                {
                    if (gameBoard[row, col] == symbol &&
                        gameBoard[row + 1, col] == symbol &&
                        gameBoard[row + 2, col] == symbol &&
                        gameBoard[row + 3, col] == symbol)
                    {
                        return true;
                    }
                }
            }

            // Diagonal (top-left to bottom-right)
            for (int row = 0; row <= 3; row++)
            {
                for (int col = 0; col <= 3; col++)
                {
                    if (gameBoard[row, col] == symbol &&
                        gameBoard[row + 1, col + 1] == symbol &&
                        gameBoard[row + 2, col + 2] == symbol &&
                        gameBoard[row + 3, col + 3] == symbol)
                    {
                        return true;
                    }
                }
            }

            // Diagonal (bottom-left to top-right)
            for (int row = 3; row < 7; row++)
            {
                for (int col = 0; col <= 3; col++)
                {
                    if (gameBoard[row, col] == symbol &&
                        gameBoard[row - 1, col + 1] == symbol &&
                        gameBoard[row - 2, col + 2] == symbol &&
                        gameBoard[row - 3, col + 3] == symbol)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    // Static void that shows instructions
    static void Instructions()
    {
        Console.WriteLine("\n\nAbout Connect 4");
        Console.WriteLine("Connect Four (also known as Connect 4, Four Up, Plot Four, Find Four, Captain's Mistress, Four in a Row, Drop Four, and in the Soviet Union, Gravitrips) ");
        Console.WriteLine("is a game in which the players choose a color and then take turns dropping colored tokens into a six-row, seven-column vertically suspended grid. ");
        Console.WriteLine("The pieces fall straight down, occupying the lowest available space within the column. ");
        Console.WriteLine("The objective of the game is to be the first to form a horizontal, vertical, or diagonal line of four of one's own tokens. (Wikipedia).");
        Console.WriteLine("\nProgram was made by Arnold Jansen Agcaoili and Derek Carlos");
    }
    // Main menu display function
    static void Menu()
    {
        Console.WriteLine("WELCOME TO CONNECT THE 4 GAME!");
        Console.WriteLine("1. Start Game");
        Console.WriteLine("2. About");
        Console.WriteLine("3. Exit");
    }

    // Main entry point
    static void Main(string[] args)
    {
        Menu();
        while (true)
        {
            Console.Write("\nEnter your Answer: ");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                NewGame game = new NewGame();
                game.Start();
                return;
            }
            else if (choice == "2")
            {
                Instructions();
                Menu();
            }
            else if (choice == "3")
            {
                Console.WriteLine("We will see each other again next time!");
                break;
            }
            else
            {
                Console.WriteLine("Error, please enter a correct value to continue\n");
            }
        }
        // Player testPlayer = new RealPlayer('X');
        // Console.WriteLine($"Testing Player '{testPlayer.Symbol}' input:");
        // int chosenColumn = testPlayer.GetMove();
        // Console.WriteLine($"You selected column: {chosenColumn}");
    }
}