using System;
using System.Collections.Generic;

namespace MONOPOLY_LEDUC_GABISON.models
{
    public class Player
    {
        // Constants
        public const int JAIL_POSITION = 10;
        public const int GO_TO_JAIL_CASE_POSITION = 30;
        public const int LIMIT_DOUBLES_IN_A_ROW = 3;
        public const int LIMIT_TURNS_IN_JAIL = 3;

        // Properties
        private string name;
        private int position;
        private bool isInJail;
        private int nbTurnInJail;
        private int nbCompletedLaps;
        private int nbDoublesInARow;
        private StrObserver lapObserver, inJailObserver, outJailObserver, doublesToJail, goToJailSquareObserver, doubleObserver, visitJailOnlyObserver;
            

        // Constructor
        public Player(string name)
        {
            this.name = name;
            position = 0;
            isInJail = false;
            nbTurnInJail = 0;
            nbCompletedLaps = 0;
            nbDoublesInARow = 0;
            lapObserver = new StrObserver($"New lap for {this.name}!");
            inJailObserver = new StrObserver($"Oh no! {this.name} is in jail (square 10)! Make a double or wait 3 turns to get out of jail.");
            outJailObserver = new StrObserver($"Congrats, {this.name}! You're finally out of jail!");
            doublesToJail = new StrObserver($"{this.name} made 3 doubles in a row.");
            goToJailSquareObserver = new StrObserver($"{this.name} arrived on square 30.");
            doubleObserver = new StrObserver($"{this.name} made a double.");
            visitJailOnlyObserver = new StrObserver($"{this.name} is visiting jail only (square 10).");
        }

        // Getters and Setters
        public string Name { get => name; set => name = value; }
        public int Position { get => position; set => position = value; }
        public bool IsInJail { get => isInJail; set => isInJail = value; }
        public int NbTurnInJail { get => nbTurnInJail; set => nbTurnInJail = value; }
        public int NbCompletedLaps { get => nbCompletedLaps; set => nbCompletedLaps = value; }
        public int NbDoublesInARow { get => nbDoublesInARow; set => nbDoublesInARow = value; }

        // Methods
        public void notifyLapObserver()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            lapObserver.Update();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
        public void notifyInJailObserver()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            inJailObserver.Update();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
        public void notifyOutJailObserver()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            outJailObserver.Update();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
        public void notifyDoubleObserver()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            doubleObserver.Update();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
        public void notifyDoubleToJailObserver()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            doublesToJail.Update();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
        public void notifyGoToJailSquareObserver()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            goToJailSquareObserver.Update();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
        public void notifyVisitJailOnlyObserver()
        {
            Console.ForegroundColor = ConsoleColor.White;
            visitJailOnlyObserver.Update();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }


        private void Move(int nbSteps)
        {
            position += nbSteps;
            if (position >= 40)
            {
                position %= 40;
                nbCompletedLaps++;
                notifyLapObserver();
            }
        }

        public bool Move(int nbSteps, bool isDouble)
        {
            if (isInJail)
            {
                nbTurnInJail++;
                if (isDouble || nbTurnInJail == LIMIT_TURNS_IN_JAIL)
                {
                    Move(nbSteps);
                    isInJail = false;
                    nbTurnInJail = 0;
                    notifyOutJailObserver();
                }
                return true;
            }

            if (isDouble)
            {
                notifyDoubleObserver();
                nbDoublesInARow++;
                if (nbDoublesInARow == LIMIT_DOUBLES_IN_A_ROW)
                {
                    notifyDoubleToJailObserver();   
                    GoToJail();
                    return true;
                }
            }

            Move(nbSteps);
            if (position == JAIL_POSITION && !isInJail)
                notifyVisitJailOnlyObserver();

            if (position == GO_TO_JAIL_CASE_POSITION)
            {
                notifyGoToJailSquareObserver();
                GoToJail();                
                return true;
            }

            if (!isDouble)
            {
                nbDoublesInARow = 0;
                return true;
            }

            return false;
        }

        public void GoToJail()
        {
            nbDoublesInARow = 0;
            position = JAIL_POSITION;
            isInJail = true;
            nbTurnInJail = 0;
            notifyInJailObserver();
        }

        public override string ToString()
        {
            return $"Name: {this.name}  \t|  Position: {this.position}  \t|  Completed Laps: {this.nbCompletedLaps}  \t|  Nb " +
                   $"Doubles in a row: {this.nbDoublesInARow}  \t|  Nb turns in jail: {this.nbTurnInJail}  \t|  Is in jail: {this.isInJail}";
        }
    }
}