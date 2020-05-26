using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NoughtsAndCrosses.UnitTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void MoveAndCheckWin_Player1WinsDiagonal_ReturnsTrue()
        {
            var game = new Game(new Player("Player1"), new Player("Player2"),'X');

            var result = game.MoveAndCheckWin(1);
            result = game.MoveAndCheckWin(2);
            result = game.MoveAndCheckWin(3);
            result = game.MoveAndCheckWin(4);
            result = game.MoveAndCheckWin(5);
            result = game.MoveAndCheckWin(6);
            result = game.MoveAndCheckWin(7);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MoveAndCheckWin_Player1DoesNotWin_ReturnsFalse()
        {
            var game = new Game(new Player("Player1"), new Player("Player2"), 'X');

            var result = game.MoveAndCheckWin(1);

            Assert.IsFalse(result);
        }

        [TestMethod]

        public void StartNewGame_SwapCurrentPlayer1_CurrentPlayerBecomesPlayer2()
        {
            var game = new Game(new Player("Player1"), new Player("Player2"), 'X');

            game.StartNewGame();

            Assert.AreEqual(game.GiveCurrentPlayer().GiveName(), "Player2");
        }

        [TestMethod]

        public void StartNewGame_InitializeBoardPositions_BoardIsEmpty()
        {
            var game = new Game(new Player("Player1"), new Player("Player2"), 'X');

            game.StartNewGame();

            Assert.AreEqual(9, game.PrintBoard().ToCharArray().Count(c => c == '-'));

        }

        [TestMethod]
        public void IsFinished_PlayersMoved9Times_ReturnsTrue()
        {
            var game = new Game(new Player("Player1"), new Player("Player2"), 'X');

            for (var i = 1; i <= 9; i++)
            {
                game.MoveAndCheckWin(i);
            }

            var result = game.IsFinished();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsFinished_PlayersMoved8Times_ReturnsFalse()
        {
            var game = new Game(new Player("Player1"), new Player("Player2"), 'X');

            for (var i = 1; i <= 8; i++)
            {
                game.MoveAndCheckWin(i);
            }

            var result = game.IsFinished();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsPositionEmptyAndValid_EnterUsedPosition_ReturnsFalse()
        {
            var game = new Game(new Player("Player1"), new Player("Player2"), 'X');

            for (var i = 1; i <= 8; i++)
            {
                game.MoveAndCheckWin(i);
            }

            var result = game.IsPositionEmptyAndValid(1);

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IsPositionEmptyAndValid_EnterFreePosition_ReturnsTrue()
        {
            var game = new Game(new Player("Player1"), new Player("Player2"), 'X');

            for (var i = 1; i <= 8; i++)
            {
                game.MoveAndCheckWin(i);
            }

            var result = game.IsPositionEmptyAndValid(9);

            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void IsSymbolValid_EnterValidSymbol_x_ReturnsTrue()
        {
            var result = Game.IsSymbolValid("x");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsSymbolValid_EnterUnvalidSymbol_k_ReturnsFalse()
        {
            var result = Game.IsSymbolValid("k");
            Assert.IsFalse(result);
        }

        [TestMethod]

        public void GiveNoughtValue_RetrieveTheNoughtValueO_ResultIsEqual()
        {
            var game = new Game(new Player("Player1"), new Player("Player2"), 'X');

            var result = Game.GiveNoughtValue();

            Assert.AreEqual(result,'O');
        }

        [TestMethod]

        public void GiveCrossValue_RetrieveTheCrossValueX_ResultIsEqual()
        {
            var game = new Game(new Player("Player1"), new Player("Player2"), 'X');

            var result = Game.GiveCrossValue();

            Assert.AreEqual(result, 'X');
        }

        [TestMethod]
        public void GiveCurrentPlayer_GameStartsWithPlayerPlayer1_ResultIsPlayer1()
        {
            var game = new Game(new Player("Player1"), new Player("Player2"), 'X');

            game.MoveAndCheckWin(1);
            game.MoveAndCheckWin(2);

            var result = game.GiveCurrentPlayer();

            Assert.AreEqual(result.GiveName(), "Player1");
        }

        [TestMethod]
        public void GiveNextOrPreviousPlayer_GameStartsWithPlayerPlayer1_ResultIsPlayer2()
        {
            var game = new Game(new Player("Player1"), new Player("Player2"), 'X');

            game.MoveAndCheckWin(1);
            game.MoveAndCheckWin(2);

            var result = game.GiveNextOrPreviousPlayer();

            Assert.AreEqual(result.GiveName(), "Player2");
        }

    }
}
