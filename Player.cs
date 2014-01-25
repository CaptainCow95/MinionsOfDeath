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
            
        }

    }
}