using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MONOPOLY_LEDUC_GABISON.models;

namespace MONOPOLY_LEDUC_GABISON
{
    class Program
    {
        static void Main(string[] args)
        {
            Game monopoly = new Game();
            monopoly.AddPlayer(new Player("Yoan"));
            monopoly.AddPlayer(new Player("Bastien"));

            do
            {
                monopoly.Play();
            } while(!monopoly.IsGameOver());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Congrats {monopoly.GetWinner().Name} ! You're the winner !");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
    }
}
