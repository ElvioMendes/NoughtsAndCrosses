namespace NoughtsAndCrosses
{
    public class Player
    {
        private string _name;

        public char Symbol { get; set; }

        public int Victories { get; set; }

        public Player(string name)
        {
            this._name = name;
        }

        public string GiveName()
        {
            return this._name;
        }

    }
}