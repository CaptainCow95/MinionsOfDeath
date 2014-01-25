using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Behaviors
{
    class DecisionTree
    {
        private Minion _owner;
        private DecisionNode _root;

        public Minion Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public DecisionNode Root
        {
            get { return _root; }
            set { _root = value; }
        }
        public DoublePoint GetGoal()
        {
            throw new NotImplementedException();
        }

        /*
         * GetGoal should return a point that an owning minion will seek towards
         */
        public DoublePoint getGoal()
        {
            return _root.getGoal();
        }
    }
}
