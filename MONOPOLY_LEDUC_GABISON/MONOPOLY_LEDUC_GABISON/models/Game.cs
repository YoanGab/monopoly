using System;
using System.Collections.Generic;

namespace MONOPOLY_LEDUC_GABISON.models
{
    public class Game
    {
        // Properties
        private List<Player> players;
        private int indexCurrentPlayer;
        private Player currentPlayer;
        private int[] dice;
        private List<Observer> lapObservers, jailObservers;

        // Constructor
        public Game()
        {
            players = new List<Player>();
            currentPlayer = null;
            dice = new int[2];
            lapObservers = new List<Observer>();
            jailObservers = new List<Observer>();
        }

        // Getters & Setters
        public List<Player> Players { get => players; set => players = value; }

        public int IndexCurrentPlayer { get => indexCurrentPlayer; set => indexCurrentPlayer = value; }

        public Player CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

        public int[] Dice { get => dice; set => dice = value; }

        // Methods       
        void registerLapObservers(Observer o) {
            lapObservers.Add(o);
        }

        void registerJailObservers(Observer o) {
            jailObservers.Add(o);
        }

        public void notifyLapObservers() {
            foreach (Observer o in lapObservers)
                o.update();
        }

        public void notifyJailObservers() {
            foreach (Observer o in jailObservers)
                o.update();
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            players.Remove(player);
        }

        public void RollDice()
        {
            Random rnd = new Random();
            dice[0] = rnd.Next(1, 7);
            dice[1] = rnd.Next(1, 7);
        }

        public void Play()
        {
            if (players.Count <= 1)
            {
                throw new Exception("Not enough player in the game");
            }

            if (currentPlayer == null)
            {
                Random rnd = new Random();
                indexCurrentPlayer = rnd.Next(0, players.Count);
                currentPlayer = players[indexCurrentPlayer];
            }

            RollDice();
            bool isTurnOver = currentPlayer.Move(dice[0] + dice[1], dice[0] == dice[1]);
            Console.WriteLine(currentPlayer);
            if (isTurnOver)
            {
                // Replace with Iterator pattern
                indexCurrentPlayer++;
                if (indexCurrentPlayer >= players.Count)
                {
                    indexCurrentPlayer = 0;
                }
                currentPlayer = players[indexCurrentPlayer];
            }
        }

        public bool IsGameOver()
        {
            int indexLastPlayer = indexCurrentPlayer - 1;
            if (indexLastPlayer < 0)
            {
                indexLastPlayer = players.Count - 1;
            }
            return players[indexLastPlayer].NbCompletedLaps >= 3;
        }

        public Player GetWinner()
        {
            int indexLastPlayer = indexCurrentPlayer - 1;
            if (indexLastPlayer < 0)
            {
                indexLastPlayer = players.Count - 1;
            }

            return players[indexLastPlayer];
        }
    }
}