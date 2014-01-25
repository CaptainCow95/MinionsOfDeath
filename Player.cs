using System.Collections.Generic;
namespace MinionsOfDeath
{
    internal class Player
    {
        private int _pid;
        private Dictionary<int,Minion> _minions;
        


        public int PID
        {
            set { _pid = value; }
            get { return _pid; }
        }

        public Dictionary<int, Minion> Minions
        {
            get { return _minions; }
        }

        public void SetSpecial(int MinionID)
        {
            foreach(KeyValuePair<int, Minion> minion in _minions){
                minion.Value.IsSpecial = false;

            }
            _minions[MinionID].IsSpecial = true;
        }

        public Player(int ID)
        {
            _pid = ID;
            _minions = new Dictionary<int, Minion>();
        }

        public void Update(double time)
        {
            foreach (KeyValuePair<int, Minion> minion in _minions)
            {
                minion.Value.Update(time);
            }
        }

        public void Draw()
        {
            foreach (KeyValuePair<int, Minion> minion in _minions)
            {
                minion.Value.Draw();
            }
        }

    }
}