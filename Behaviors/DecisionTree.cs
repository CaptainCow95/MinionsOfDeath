using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Behaviors
{
    class DecisionTree
    {
        private DecisionNode _root;
        public DecisionNode Root
        {
            get { return _root; }
            set { _root = value; }
        }
        public DoublePoint GetGoal()
        {
            throw new NotImplementedException();
        }
        public DoublePoint getGoal()
        {
            return _root.getGoal();
        }
    }
}
