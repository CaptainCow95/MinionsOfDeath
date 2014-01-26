using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath
{
    public class Base : GameObject
    {
        private int _pid;


        public int PID
        {
            get { return _pid; }
            set { _pid = value; }
        }
    }
}
