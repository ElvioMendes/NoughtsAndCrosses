using System;

namespace NoughtsAndCrosses
{
    internal class Program
    {
        private static void Main()
        {
            var startedTime = DateTime.Now;
            Console.WriteLine("Choose a name for Player 1");
            var player1Name = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(player1Name))
            {
                Console.WriteLine("Please choose a non-empty name");
                player1Name = Console.ReadLine();
            }

            Console.WriteLine("Choose a name for Player 2");
            var player2Name = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(player2Name))
            {
                Console.WriteLine("Please choose a non-empty name");
                player2Name = Console.ReadLine();
            }
            var player1 = new Player(player1Name);
            var player2 = new Player(player2Name);

            Player chosenFirstPlayer;
            Player secondPlayer;

            Console.WriteLine("Please choose who plays first, {0} or {1}: ", player1.GiveName(), player2.GiveName());
            var firstPlayer = Console.ReadLine();
            while (true)
            {
                if (firstPlayer == player1.GiveName())
                {
                    chosenFirstPlayer = player1;
                    secondPlayer = player2;
                    break;
                }

                if (firstPlayer == player2.GiveName())
                {
                    chosenFirstPlayer = player2;
                    secondPlayer = player1;
                    break;
                }

                Console.WriteLine("Invalid Name");
                Console.WriteLine("Please choose who plays first, {0} or {1}: ", player1.GiveName(), player2.GiveName());
                firstPlayer = Console.ReadLine();
            }

            Console.WriteLine("\n{0} you start first, please choose your symbol to start, {1} or {2}", firstPlayer, Game.GiveNoughtValue(), Game.GiveCrossValue());

            string symbol;
            while (true)
            {
                symbol = Console.ReadLine();
                if (Game.IsSymbolValid(symbol))
                    break;

                Console.WriteLine("Invalid Symbol\nPlease chose your symbol {0} or {1}", Game.GiveNoughtValue(), Game.GiveCrossValue());
            }

            var game = new Game(chosenFirstPlayer, secondPlayer, Convert.ToChar(symbol ?? string.Empty));
            Console.Clear();
            Console.WriteLine(Game.Presentation());

          
            while (true)
            {
                while (true)
                {
                    Console.WriteLine(game.PrintBoard());
                    if (game.IsFinished())
                    {
                        Console.WriteLine("Game finished with a Draw\n");
                        Console.WriteLine("Player {0}: {1} victories\nPlayer {2}: {3} victories\n", game.GiveCurrentPlayer().GiveName(), game.GiveCurrentPlayer().Victories, game.GiveNextOrPreviousPlayer().GiveName(), game.GiveNextOrPreviousPlayer().Victories);
                        break;
                    }

                    Console.WriteLine("{0}, what is your move?", game.GiveCurrentPlayer().GiveName());
                    var chosenPosition = Console.ReadLine();
                    if (int.TryParse(chosenPosition, out var position))
                    {
                        if (game.IsPositionEmptyAndValid(position))
                        {
                            if (!game.MoveAndCheckWin(position)) continue;
                            game.GiveCurrentPlayer().Victories++;
                            Console.WriteLine(game.PrintBoard());
                            Console.WriteLine("Player {0} won!!!\n", game.GiveCurrentPlayer().GiveName());
                            Console.WriteLine("Player {0}: {1} victories\nPlayer {2}: {3} victories\n", game.GiveCurrentPlayer().GiveName(), game.GiveCurrentPlayer().Victories, game.GiveNextOrPreviousPlayer().GiveName(), game.GiveNextOrPreviousPlayer().Victories);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Position not empty or out of range");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a numerical value to choose a position");
                    }
                }


                Console.WriteLine("Play Again Y/N ?");
                var playAgain = Console.ReadLine()?.ToUpper();
                while (playAgain != "Y" && playAgain != "N")
                {
                    Console.WriteLine("Please press Y to play again, or N to exit");
                    playAgain = Console.ReadLine()?.ToUpper();
                }

                if (playAgain == "Y")
                {
                    Console.Clear();
                    Console.WriteLine("Ok, let's play again");
                    game.StartNewGame();
                    continue;
                }

                Console.Clear();
                Console.WriteLine("Game Finished\n");

                Console.WriteLine("Player {0}: {1} victories\nPlayer {2}: {3} victories\n", game.GiveCurrentPlayer().GiveName(), game.GiveCurrentPlayer().Victories, game.GiveNextOrPreviousPlayer().GiveName(), game.GiveNextOrPreviousPlayer().Victories);

                TimeSpan totalTime = DateTime.Now - startedTime;
                Console.WriteLine("Total time played: {0:hh\\:mm\\:ss} hours", totalTime);

                break;

            }



        }

    }
}
