//TEST Message - Derek
//testing

namespace Connect_4
{

    internal class Program
    {
        // Created an abstract class in order for additional security purpose.
        // Cannot be instantiated on the main class.
        abstract class Player
        {
            public char Symbol { get; private set; }

            protected Player(char symbol)
            {
                Symbol = symbol;
            }

            public abstract int GetMove();
        }

        class RealPlayer : Player
        {
            public RealPlayer(char symbol) : base(symbol) { }

            public override int GetMove()
            {
                int col;
                while (true)
                {
                    Console.Write($"Player {Symbol}, choose a column (0-6): ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out col) && col >= 0 && col <= 6)
                        return col;
                    Console.WriteLine("Invalid input! Enter a number between 0-6.");
                }
            }
        }
        // Here it will be implemented the whole game.
        class NewGame
        {
            public void Start()
            {
                Board board = new Board();
                //Specifically put '' for char instead of ""
                Player player1 = new RealPlayer('X');
                Player player2 = new RealPlayer('O');
                Player currentPlayer = player1;

                // Initialize and show empty board
                board.populateBoard();
                board.printBoard();

                while (true)
                {
                    // Get the move from current player
                    int column = currentPlayer.GetMove();

                    // Try placing the piece
                    bool success = board.userTurnOnTheBoard(column, currentPlayer.Symbol);

                    if (success)
                    {
                        board.printBoard();
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


        // Game Board output
        // Needs to have 7 columns and 7 rows.
        class Board
        {
            // I added these but feel free to change them - Derek
            public char[,] gameBoard = new char[7, 7];

            // Don't know if we need this, but will be used to initialize each element of the array with _ to show that they are empty.
            public void populateBoard()
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        gameBoard[i, j] = '_'; // '_' represents an empty cell
                    }
                }
            }

            //Another method to print the current gameboard on the screen
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
                Console.WriteLine("0 1 2 3 4 5 6");
                Console.WriteLine();
            }

            public bool userTurnOnTheBoard(int column, char userSymbol)
            {
                if (column < 0 || column > 6)
                {
                    Console.WriteLine("Please choose between 0-6!");
                    return false;
                }

                for (int row = 6; row >= 0; row--)
                {
                    if (gameBoard[row, column] == '_')
                    {
                        gameBoard[row, column] = userSymbol;
                        return true;
                    }
                }

                // If the loop completes, the column is full
                Console.WriteLine("Column is already full! Please use other available columns.");
                return false; // Unsuccessful placement
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

        static void Menu()
        {
            Console.WriteLine("WELCOME TO CONNECT THE 4 GAME!");
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. About");
            Console.WriteLine("3. Exit");
        }

        //Intro to the game
        static void Main(string[] args)
        {
            Menu();
            // Choices using while loop
            while (true)
            {
                // Using string create an if else statement to choose an action
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
                    Console.WriteLine("Error, please enter a correct value to continue");
                    Console.WriteLine();
                }
            }

            // Implemented some testing for player to see if column choice would be correct

            // Player testPlayer = new RealPlayer('X');
            // Console.WriteLine($"Testing Player '{testPlayer.Symbol}' input:");
            // int chosenColumn = testPlayer.GetMove();
            // Console.WriteLine($"You selected column: {chosenColumn}");
        }
    }
}