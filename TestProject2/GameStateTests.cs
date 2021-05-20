﻿using System.Drawing;
using Logic;
using NUnit.Framework;

namespace TestProject2
{
    [TestFixture]
    public class GameStateTests
    {
        [Test]
        public void MapMovement()
        {
            var gameController = new GameController();
            var obj = gameController.GetGameObjectList()[0];
            gameController.ChangeState();

            var objPosition = obj.PositionAndSize;
            var expectedObj = new PositionAndSize(new Point(10, 2), new Size(1, 1));

            Assert.AreEqual(expectedObj.Position.X, objPosition.Position.X);
        }

        [Test]
        public void ScoreChange()
        {
            var gameController = new GameController();
            for (var i = 0; i < 7; i++)
            {
                gameController.ChangeState();
            }

            gameController.GetFood();

            var score = gameController.GetScore();
            var expectedScore = 10;

            Assert.AreEqual(expectedScore, score);
        }

        [Test]
        public void LifeChange_WhenCollideWithObstacle()
        {
            var gameController = new GameController();
            for (var i = 0; i < 33; i++)
            {
                gameController.ChangeState();
            }

            var livesNumber = gameController.GetLife();
            var expectedLivesNumber = 3;

            Assert.AreEqual(expectedLivesNumber, livesNumber);
        }

        [Test]
        public void LifeChange_WhenTakeBadFood()
        {
            var gameController = new GameController();
            for (var i = 0; i < 17; i++)
            {
                gameController.ChangeState();
            }

            gameController.GetFood();

            var livesNumber = gameController.GetLife();
            var expectedLivesNumber = 4;

            Assert.AreEqual(expectedLivesNumber, livesNumber);
        }

        [Test]
        public void Score_WhenTakeFoodWhichBeforePlayer()
        {
            var gameController = new GameController();

            gameController.AddObject(new Food(new Point(3, 2), TypeName.Corn));
            gameController.GetFood();

            Assert.AreEqual(10, gameController.GetScore());
        }

        [Test]
        public void Score_WhenTakeFoodOnPlayerPositionX()
        {
            var gameController = new GameController();

            gameController.AddObject(new Food(new Point(4, 2), TypeName.Corn));
            gameController.GetFood();

            Assert.AreEqual(10, gameController.GetScore());
        }

        [Test]
        public void Score_WhenTakeFoodInJump()
        {
            var gameController = new GameController();

            gameController.AddObject(new Food(new Point(4, 2), TypeName.Corn));
            gameController.Jump();
            gameController.GetFood();

            Assert.AreEqual(0, gameController.GetScore());
        }

        [Test]
        public void LifeChange_WhenCollideWithBird()
        {
            var gameController = new GameController();

            gameController.AddObject(new Bird(new Point(5, 1)));
            gameController.ChangeState();
            
            Assert.AreEqual(4, gameController.GetLife());
        }
        
        [Test]
        public void LifeChange_WhenCollideWithBirdAndCrouching()
        {
            var gameController = new GameController();

            gameController.AddObject(new Bird(new Point(5, 1)));
            gameController.SitDown();
            gameController.ChangeState();
            
            Assert.AreEqual(5, gameController.GetLife());
        }
    }
}