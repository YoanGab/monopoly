using System;
using System.Collections;
using System.Collections.Generic;
using MONOPOLY_LEDUC_GABISON.models;

namespace MONOPOLY_LEDUC_GABISON.models
{
    public class Game
    {
        // Properties
        private static Game instance;
        private List<Player> players;
        private int indexCurrentPlayer;
        private Player currentPlayer;
        private int[] dice;

        public static Game GetInstance()
        {
            if (instance == null)
                instance = new Game();
            return instance;
        }

        // Constructor
        public Game()
        {
            players = new List<Player>();
            currentPlayer = null;
            dice = new int[2];            
        }

        // Getters & Setters
        public List<Player> Players { get => players; set => players = value; }

        public int IndexCurrentPlayer { get => indexCurrentPlayer; set => indexCurrentPlayer = value; }

        public Player CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

        public int[] Dice { get => dice; set => dice = value; }

        // Methods       
        private void AddPlayer(Player player)
        {
            players.Add(player);
        }

        private void RemovePlayer(Player player)
        {
            players.Remove(player);
        }

        private void RollDice(Random rnd)
        {            
            dice[0] = rnd.Next(1, 7);
            dice[1] = rnd.Next(1, 7);
            Console.WriteLine($"\nDie 1: {dice[0]}\nDie 2: {dice[1]}\nSum Dice: {dice[0]+dice[1]}");
        }

        private void GetFirstPlayer(Random rnd)
        {
            indexCurrentPlayer = rnd.Next(0, players.Count);
            currentPlayer = players[indexCurrentPlayer];
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n\n{currentPlayer.Name} starts the game!");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }

        private void GetNextPlayer()
        {
            indexCurrentPlayer++;
            if (indexCurrentPlayer >= players.Count)
                indexCurrentPlayer = 0;
            currentPlayer = players[indexCurrentPlayer];
        }

        public void DisplayTitle()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            const string space = "                                  ";
            Console.WriteLine("\n\n");
            Console.WriteLine(space + "$$\\      $$\\  $$$$$$\\  $$\\   $$\\  $$$$$$\\  $$$$$$$\\   $$$$$$\\  $$\\   $$\\     $$\\       ");
            Console.WriteLine(space + "$$$\\    $$$ |$$  __$$\\ $$$\\  $$ |$$  __$$\\ $$  __$$\\ $$  __$$\\ $$ |  \\$$\\   $$  |      ");
            Console.WriteLine(space + "$$$$\\  $$$$ |$$ /  $$ |$$$$\\ $$ |$$ /  $$ |$$ |  $$ |$$ /  $$ |$$ |   \\$$\\ $$  /       ");
            Console.WriteLine(space + "$$\\$$\\$$ $$ |$$ |  $$ |$$ $$\\$$ |$$ |  $$ |$$$$$$$  |$$ |  $$ |$$ |    \\$$$$  /        ");
            Console.WriteLine(space + "$$ \\$$$  $$ |$$ |  $$ |$$ \\$$$$ |$$ |  $$ |$$  ____/ $$ |  $$ |$$ |     \\$$  /         ");
            Console.WriteLine(space + "$$ |\\$  /$$ |$$ |  $$ |$$ |\\$$$ |$$ |  $$ |$$ |      $$ |  $$ |$$ |      $$ |          ");
            Console.WriteLine(space + "$$ | \\_/ $$ | $$$$$$  |$$ | \\$$ | $$$$$$  |$$ |       $$$$$$  |$$$$$$$$\\ $$ |          ");
            Console.WriteLine(space + "\\__|     \\__| \\______/ \\__|  \\__| \\______/ \\__|       \\______/ \\________|\\__|          \n\n");
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        public void Play()
        {
            DisplayTitle();
            Random rnd = new Random();            
            int nbPlayer;
            string p = "";            
            do
                if (players.Count <= 1)
                {
                    Console.Write("Select the number of players (2-4): ");
                    p = Console.ReadLine();
                } while (!int.TryParse(p, out nbPlayer) || nbPlayer <= 1 || nbPlayer > 4);

            for (int i = 1; i <= nbPlayer; i++)
            {
                Console.Write($"\nEnter the name of Player N°{i}: ");
                AddPlayer(new Player(Console.ReadLine()));
            }

            if (currentPlayer == null)
            {
                GetFirstPlayer(rnd);
            }

            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n\n{currentPlayer.Name}, Press 'Enter' to roll the dice.");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.ReadLine();
                RollDice(rnd);
                bool isTurnOver = currentPlayer.Move(dice[0] + dice[1], dice[0] == dice[1]);
                Console.WriteLine(currentPlayer);
                if (isTurnOver)
                {
                    GetNextPlayer();
                }
            } while (!IsGameOver());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n\nCongrats {GetWinner().Name} ! You're the winner !");
            Console.ForegroundColor = ConsoleColor.White;
            DisplayTitle();
        }

        private bool IsGameOver()
        {
            int indexLastPlayer = indexCurrentPlayer - 1;
            if (indexLastPlayer < 0)
            {
                indexLastPlayer = players.Count - 1;
            }
            return players[indexLastPlayer].NbCompletedLaps >= 3;
        }

        private Player GetWinner()
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