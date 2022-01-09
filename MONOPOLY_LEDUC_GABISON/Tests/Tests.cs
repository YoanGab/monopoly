using System;
using NUnit.Framework;
using MONOPOLY_LEDUC_GABISON.models;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void SingletonGame()
        {
            Game monopoly = Game.GetInstance();
            Game monopoly2 = Game.GetInstance();
            Assert.AreEqual(monopoly, monopoly2);
        }

        [Test]
        public void TripleDouble()
        {
            Player player = new Player("Yoan");
            player.Move(2, true);
            player.Move(4, true);
            player.Move(6, true);
            Assert.AreEqual(10, player.Position);
            Assert.AreEqual(true, player.IsInJail);
        }

        [Test]
        public void DoubleDouble()
        {
            Player player = new Player("Yoan");
            player.Move(2, true);
            player.Move(4, true);
            Assert.AreEqual(6, player.Position);
            Assert.AreEqual(false, player.IsInJail);
            Assert.AreEqual(2, player.NbDoublesInARow);
            player.Move(3, false);
            Assert.AreEqual(false, player.IsInJail);
            Assert.AreEqual(0, player.NbDoublesInARow);
        }

        [Test]
        public void PrisonCase()
        {
            Player player = new Player("Yoan");
            player.Move(30, true);
            Assert.AreEqual(10, player.Position);
            Assert.AreEqual(true, player.IsInJail);
        }

        [Test]
        public void GetOutOfJailWithDouble()
        {
            Player player = new Player("Yoan");
            player.GoToJail();
            player.Move(4, true);
            Assert.AreEqual(14, player.Position);
            Assert.AreEqual(false, player.IsInJail);
        }

        [Test]
        public void StayInJailWithoutDouble()
        {
            Player player = new Player("Yoan");
            player.GoToJail();
            player.Move(3, false);
            Assert.AreEqual(10, player.Position);
            Assert.AreEqual(true, player.IsInJail);
        }

        [Test]
        public void GetOutOfJailAfter3Turns()
        {
            Player player = new Player("Yoan");
            player.GoToJail();
            player.Move(3, false);
            Assert.AreEqual(10, player.Position);
            Assert.AreEqual(true, player.IsInJail);

            player.Move(3, false);
            Assert.AreEqual(10, player.Position);
            Assert.AreEqual(true, player.IsInJail);

            player.Move(3, false);
            Assert.AreEqual(13, player.Position);
            Assert.AreEqual(false, player.IsInJail);
        }
    }
}