using System.Linq;

namespace NoughtsAndCrosses
{
    public class Game
    {
        private char[,] _boardPositions = new char[3, 3];

        private const char NoughtValue = 'O';
        private const char CrossValue = 'X';
        private const char EmptyValue = '-';


        private Player _currentPlayer;
        private Player _nextOrPreviousPlayer;


        public Game(Player chosenFirstPlayer, Player secondPlayer, char startingSymbol)
        {
            this._currentPlayer = chosenFirstPlayer;
            this._nextOrPreviousPlayer = secondPlayer;

            if (startingSymbol == NoughtValue)
            {
                chosenFirstPlayer.Symbol = NoughtValue;
                secondPlayer.Symbol = CrossValue;
            }
            else
            {
                chosenFirstPlayer.Symbol = CrossValue;
                secondPlayer.Symbol = NoughtValue;
            }

            InitializeBoardPositions(this);
        }


        public string PrintBoard()
        {
            var boardLines = "";

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    boardLines += this._boardPositions[i, j];
                }

                boardLines += "\n";
            }

            return "\nBoard:\n" + boardLines + "\n";
        }


        public static string Presentation()
        {
            return @"
The Board is composed of 9 positions in the format below:

1|2|3
4|5|6
7|8|9

In order to chose your move, please adopt this numbering convention and choose a free position.
";
        }

        public bool MoveAndCheckWin(int position)
        {

            this._boardPositions[BoardLineIndex(position), BoardColumnIndex(position)] = _currentPlayer.Symbol;

            var completesALine = (
                new[] { this._boardPositions[1, 0], this._boardPositions[1, 1], this._boardPositions[1, 2] }.All(s => s == this._currentPlayer.Symbol) ||
                new[] { this._boardPositions[0, 0], this._boardPositions[0, 1], this._boardPositions[0, 2] }.All(s => s == this._currentPlayer.Symbol) ||
                new[] { this._boardPositions[2, 0], this._boardPositions[2, 1], this._boardPositions[2, 2] }.All(s => s == this._currentPlayer.Symbol) ||
                new[] { this._boardPositions[0, 0], this._boardPositions[1, 0], this._boardPositions[2, 0] }.All(s => s == this._currentPlayer.Symbol) ||
                new[] { this._boardPositions[0, 1], this._boardPositions[1, 1], this._boardPositions[2, 1] }.All(s => s == this._currentPlayer.Symbol) ||
                new[] { this._boardPositions[0, 2], this._boardPositions[1, 2], this._boardPositions[2, 2] }.All(s => s == this._currentPlayer.Symbol) ||
                new[] { this._boardPositions[0, 0], this._boardPositions[1, 1], this._boardPositions[2, 2] }.All(s => s == this._currentPlayer.Symbol) ||
                new[] { this._boardPositions[2, 0], this._boardPositions[1, 1], this._boardPositions[0, 2] }.All(s => s == this._currentPlayer.Symbol));

            if (completesALine)
                return true;


            NextPlayer();

            return false;
        }

        public void StartNewGame()
        {
            NextPlayer();
            InitializeBoardPositions(this);
        }

        private static void InitializeBoardPositions(Game thisGame)
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    thisGame._boardPositions[i, j] = EmptyValue;
                }
            }
        }

        public bool IsFinished()
        {
            return this._boardPositions.Cast<char>().All(position => position != EmptyValue);
        }

        private void NextPlayer()
        {
            var aux = _currentPlayer;
            _currentPlayer = _nextOrPreviousPlayer;
            _nextOrPreviousPlayer = aux;

        }

        public bool IsPositionEmptyAndValid(int boardPosition)
        {
            if (boardPosition < 1 || boardPosition > 9)
            {
                return false;
            }
            var line = BoardLineIndex(boardPosition);
            var column = BoardColumnIndex(boardPosition);

            return _boardPositions[line, column] == EmptyValue;
        }

        private static int BoardLineIndex(int boardPosition)
        {
            return (boardPosition - 1) / 3;
        }
        private static int BoardColumnIndex(int boardPosition)
        {
            return boardPosition - (BoardLineIndex(boardPosition) * 3) - 1;
        }

        public static bool IsSymbolValid(string symbol)
        {
            return symbol.ToUpper() == NoughtValue.ToString() || symbol.ToUpper() == CrossValue.ToString();
        }

        public static char GiveNoughtValue()
        {
            return NoughtValue;
        }

        public static char GiveCrossValue()
        {
            return CrossValue;
        }

        public Player GiveCurrentPlayer()
        {
            return this._currentPlayer;
        }

        public Player GiveNextOrPreviousPlayer()
        {
            return this._nextOrPreviousPlayer;
        }

    }
}