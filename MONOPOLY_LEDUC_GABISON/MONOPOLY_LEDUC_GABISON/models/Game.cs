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
        private int[] dices;

        // Constructor
        public Game()
        {
            players = new List<Player>();
            currentPlayer = null;
            dices = new int[2];
        }

        // Getters & Setters
        public List<Player> Players { get => players; set => players = value; }

        public int IndexCurrentPlayer { get => indexCurrentPlayer; set => indexCurrentPlayer = value; }

        public Player CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

        public int[] Dices { get => dices; set => dices = value; }

        // Methods
        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            players.Remove(player);
        }

        public void RollDices()
        {
            Random rnd = new Random();
            dices[0] = rnd.Next(1, 7);
            dices[1] = rnd.Next(1, 7);
        }

        public void Play()
        {
            if (players.Count == 0)
            {
                throw new Exception("No player in the game");
            }

            if (currentPlayer == null)
            {
                Random rnd = new Random();
                indexCurrentPlayer = rnd.Next(0, players.Count);
                currentPlayer = players[indexCurrentPlayer];
            }

            RollDices();
            bool isTurnOver = currentPlayer.Move(dices[0] + dices[1], dices[0] == dices[1]);
            Console.WriteLine(currentPlayer);
            if (isTurnOver)
            {
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