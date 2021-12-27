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
            monopoly.Play();            
            Console.ReadKey();
        }
    }
}
