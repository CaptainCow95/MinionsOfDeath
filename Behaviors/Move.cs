using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Behaviors
{
    class Move
    {
        private float _dV, _dTheta;

        public float DV
        {
            set { _dV = value; }
            get { return _dV;  }
        }

        public float _DTheta
        {
            set { _dTheta = value;  }
            get { return _dTheta; }
        }

    }
}
