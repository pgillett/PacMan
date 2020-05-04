﻿using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace NPacMan.Game.Tests.GameTests
{
    public class CoinTests
    {
        private readonly TestGameSettings _gameSettings;
        private readonly TestGameClock _gameClock;

        public CoinTests()
        {
            _gameSettings = new TestGameSettings();
            _gameClock = new TestGameClock();
        }

        [Fact]
        public void ScoreDoesNotChangeWhenNoCoinIsCollected()
        {
            var game = new Game(_gameClock, _gameSettings);
            var x = game.PacMan.X;
            var y = game.PacMan.Y;
            var score = game.Score;

            game.ChangeDirection(Direction.Down);

            _gameClock.Tick();

            game.Score.Should().Be(score);
        }

        [Fact]
        public void IncrementsScoreBy10WhenCoinCollected()
        {
            var game = new Game(_gameClock, _gameSettings);
            var x = game.PacMan.X;
            var y = game.PacMan.Y;

            game.ChangeDirection(Direction.Down);

            _gameSettings.Coins.Add((x, y + 1));
            _gameClock.Tick();

            game.Score.Should().Be(10);
        }

        [Fact]
        public void IncrementsScoreBy20WhenTwoCoinsAreCollected()
        {
            var game = new Game(_gameClock, _gameSettings);
            var x = game.PacMan.X;
            var y = game.PacMan.Y;

            game.ChangeDirection(Direction.Down);

            _gameSettings.Coins.Add((x, y + 1));
            _gameSettings.Coins.Add((x, y + 2));

            _gameClock.Tick();
            _gameClock.Tick();

            game.Score.Should().Be(20);
        }

        [Fact]
        public void CoinShouldBeCollected()
        {
            var game = new Game(_gameClock, _gameSettings);
            var x = game.PacMan.X;
            var y = game.PacMan.Y;

            game.ChangeDirection(Direction.Down);

            _gameSettings.Coins.Add((x, y + 1));

            _gameClock.Tick();

            game.Coins.Should().NotContain((x, y + 1));
        }

        [Fact]
        public void JustTheCollectedCoinShouldBeCollected()
        {
            var game = new Game(_gameClock, _gameSettings);
            var x = game.PacMan.X;
            var y = game.PacMan.Y;

            game.ChangeDirection(Direction.Down);

            _gameSettings.Coins.Add((x, y + 1));
            _gameSettings.Coins.Add((x, y + 2));

            _gameClock.Tick();

            game.Coins.Should().NotContain((x, y + 1));
            game.Coins.Should().Contain((x, y + 2));
        }

        [Fact]
        public void GameContainsAllCoins()
        {
            var gameBoard = new TestGameSettings();
            gameBoard.Coins.Add((1, 1));
            gameBoard.Coins.Add((1, 2));
            gameBoard.Coins.Add((2, 2));

            var gameClock = new TestGameClock();
            var game = new Game(gameClock, gameBoard);

            gameClock.Tick();

            game.Coins.Should().BeEquivalentTo(
                (1, 1),
                (1, 2),
                (2, 2)
            );
        }

        [Fact]
        public void PacManDoesNotCollectCoinAndScoreStaysTheSameWhenCollidesWithGhost()
        {
            var x = _gameSettings.PacMan.X + 1;
            var y = _gameSettings.PacMan.Y;

            _gameSettings.Ghosts.Add(new Ghost("Ghost1", x, y, CellLocation.TopLeft, new StandingStillGhostStrategy(), new StandingStillGhostStrategy()));
            _gameSettings.Coins.Add((x, y));

            var game = new Game(_gameClock, _gameSettings);
            var score = game.Score;

            game.ChangeDirection(Direction.Right);
            _gameClock.Tick();

            using var _ = new AssertionScope();
            game.Coins.Should().Contain((x, y));
            game.Score.Should().Be(score);
        }
    }
}