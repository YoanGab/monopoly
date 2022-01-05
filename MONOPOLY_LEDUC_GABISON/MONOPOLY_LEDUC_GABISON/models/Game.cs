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

        public static Game getInstance()
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
        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            players.Remove(player);
        }

        public void RollDice(Random rnd)
        {
            dice[0] = rnd.Next(1, 7);
            dice[1] = rnd.Next(1, 7);
            Console.WriteLine($"1st dice: {dice[0]}, 2nd dice: {dice[1]}\nTotal: {dice[0] + dice[1]}");
        }

        public void Play()
        {
            Random rnd = new Random();

            string space = "                                  ";
            Console.WriteLine("\n\n\n");
            Console.WriteLine(space + "$$\\      $$\\  $$$$$$\\  $$\\   $$\\  $$$$$$\\  $$$$$$$\\   $$$$$$\\  $$\\   $$\\     $$\\       ");
            Console.WriteLine(space + "$$$\\    $$$ |$$  __$$\\ $$$\\  $$ |$$  __$$\\ $$  __$$\\ $$  __$$\\ $$ |  \\$$\\   $$  |      ");
            Console.WriteLine(space + "$$$$\\  $$$$ |$$ /  $$ |$$$$\\ $$ |$$ /  $$ |$$ |  $$ |$$ /  $$ |$$ |   \\$$\\ $$  /       ");
            Console.WriteLine(space + "$$\\$$\\$$ $$ |$$ |  $$ |$$ $$\\$$ |$$ |  $$ |$$$$$$$  |$$ |  $$ |$$ |    \\$$$$  /        ");
            Console.WriteLine(space + "$$ \\$$$  $$ |$$ |  $$ |$$ \\$$$$ |$$ |  $$ |$$  ____/ $$ |  $$ |$$ |     \\$$  /         ");
            Console.WriteLine(space + "$$ |\\$  /$$ |$$ |  $$ |$$ |\\$$$ |$$ |  $$ |$$ |      $$ |  $$ |$$ |      $$ |          ");
            Console.WriteLine(space + "$$ | \\_/ $$ | $$$$$$  |$$ | \\$$ | $$$$$$  |$$ |       $$$$$$  |$$$$$$$$\\ $$ |          ");
            Console.WriteLine(space + "\\__|     \\__| \\______/ \\__|  \\__| \\______/ \\__|       \\______/ \\________|\\__|          \n\n");
            int nbPlayer;
            string p = "";
            do
                if (players.Count <= 1)
                {
                    Console.Write("Select the number of players: ");
                    p = Console.ReadLine();
                } while (!int.TryParse(p, out nbPlayer) || nbPlayer <= 1);

            for (int i = 1; i <= nbPlayer; i++)
            {
                Console.Write($"\nEnter the name of Player N°{i}: ");
                AddPlayer(new Player(Console.ReadLine()));
            }

            if (currentPlayer == null)
            {
                indexCurrentPlayer = rnd.Next(0, players.Count);
                currentPlayer = players[indexCurrentPlayer];
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"\n\n{currentPlayer.Name} starts the game!\n\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            

            do
            {
                Console.WriteLine($"\n\n{currentPlayer.Name}, Press 'Enter' to roll the dice");
                Console.ReadLine();
                RollDice(rnd);
                bool isTurnOver = currentPlayer.Move(dice[0] + dice[1], dice[0] == dice[1]);
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
            } while (!IsGameOver());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Congrats {GetWinner().Name} ! You're the winner !");
            Console.ForegroundColor = ConsoleColor.White;
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