namespace MONOPOLY_LEDUC_GABISON.models
{
    public class Player
    {
        // Constants
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

        // Constructor
        public Player(string name)
        {
            this.name = name;
            this.position = 0;
            this.isInJail = false;
            this.nbTurnInJail = 0;
            this.nbCompletedLaps = 0;
            this.nbDoublesInARow = 0;
        }

        // Getters and Setters
        public string Name { get => name; set => name = value; }
        public int Position { get => position; set => position = value; }
        public bool IsInJail { get => isInJail; set => isInJail = value; }
        public int NbTurnInJail { get => nbTurnInJail; set => nbTurnInJail = value; }
        public int NbCompletedLaps { get => nbCompletedLaps; set => nbCompletedLaps = value; }
        public int NbDoublesInARow { get => nbDoublesInARow; set => nbDoublesInARow = value; }

        // Methods
        public int Move(int nbSteps)
        {
            this.position += nbSteps;
            if (this.position >= 40)
            {
                this.position %= 40;
                this.nbCompletedLaps++;
            }
            return this.position;
        }

        public bool Move(int nbSteps, bool isDouble)
        {
            if (this.isInJail)
            {
                this.nbTurnInJail++;
                if (isDouble || this.nbTurnInJail == LIMIT_TURNS_IN_JAIL)
                {
                    this.Move(nbSteps);
                    this.isInJail = false;
                    this.nbTurnInJail = 0;
                }
                return true;
            }

            if (isDouble)
            {
                this.nbDoublesInARow++;
                if (this.nbDoublesInARow == LIMIT_DOUBLES_IN_A_ROW)
                {
                    this.GoToJail();
                    return true;
                }
            }

            this.Move(nbSteps);
            if (this.position == GO_TO_JAIL_CASE_POSITION)
            {
                this.GoToJail();
                return true;
            }

            if (!isDouble)
            {
                this.nbDoublesInARow = 0;
                return true;
            }

            return false;
        }

        public void GoToJail()
        {
            this.nbDoublesInARow = 0;
            this.position = 10;
            this.isInJail = true;
            this.nbTurnInJail = 0;
        }

        public override string ToString()
        {
            return this.name + " : " + this.position + " : " + this.nbCompletedLaps + " : " + this.nbDoublesInARow + " : " + this.nbTurnInJail + " : " + this.isInJail;
        }
    }
}