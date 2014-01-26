using System.Collections.Generic;

namespace MinionsOfDeath
{
    public class Player
    {
        private Dictionary<int, Minion> _minions;
        private int _pid;
        private Base _base;

        public Player(int ID)
        {
            _pid = ID;
            _minions = new Dictionary<int, Minion>();
        }

        public Dictionary<int, Minion> Minions
        {
            get { return _minions; }
        }

        public int PID
        {
            set { _pid = value; }
            get { return _pid; }
        }

        public Base Base
        {
            set { _base = value; }
            get { return _base; }
        }

        public void AddMinion(Minion minion)
        {
            _minions.Add(minion.ID, minion);
        }

        public void Draw()
        {
            foreach (KeyValuePair<int, Minion> minion in _minions)
            {
                minion.Value.Draw();
            }
        }

        public void SetSpecial(int MinionID)
        {
            foreach (KeyValuePair<int, Minion> minion in _minions)
            {
                minion.Value.IsSpecial = false;
            }
            _minions[MinionID].IsSpecial = true;
        }

        public void Update(double time)
        {
            foreach (KeyValuePair<int, Minion> minion in _minions)
            {
                minion.Value.Update(time);
            }
        }
    }
}