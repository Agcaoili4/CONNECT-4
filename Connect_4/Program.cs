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

        }

        // Game Board output
        // Needs to have 7 columns and 7 rows.
        class Board
        {

        }

        // Static void that shows instructions
        static void Instructions()
        {
            Console.WriteLine("About Connect 4");
            Console.WriteLine("Connect Four (also known as Connect 4, Four Up, Plot Four, Find Four, Captain's Mistress, Four in a Row, Drop Four, and in the Soviet Union, Gravitrips) ");
            Console.WriteLine("is a game in which the players choose a color and then take turns dropping colored tokens into a six-row, seven-column vertically suspended grid. ");
            Console.WriteLine("The pieces fall straight down, occupying the lowest available space within the column. ");
            Console.WriteLine("The pieces fall straight down, occupying the lowest available space within the column. The objective of the game is to be the first to form a horizontal, vertical,\n or diagonal line of four of one's own tokens. (Wikipedia).");
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