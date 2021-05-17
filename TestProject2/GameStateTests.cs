using System.Drawing;
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
            var expectedLivesNumber = 4;

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
        public void TakeCorn()
        {
            var gameController = new GameController();
            
            gameController.AddObject(new Food(new Point(3, 2), TypeName.Corn));
            gameController.GetFood();
            
            Assert.AreEqual(10, gameController.GetScore());
        }
        
        [Test]
        public void TakeCorn2()
        {
            var gameController = new GameController();
            
            gameController.AddObject(new Food(new Point(4, 2), TypeName.Corn));
            gameController.GetFood();
            
            Assert.AreEqual(10, gameController.GetScore());
        }
        [Test]
        public void TakeCornWhenJump()
        {
            var gameController = new GameController();
            
            gameController.AddObject(new Food(new Point(4, 2), TypeName.Corn));
            gameController.Jump();
            gameController.GetFood();
            
            Assert.AreEqual(0, gameController.GetScore());
        }

    }
}
