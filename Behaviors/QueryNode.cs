using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Behaviors
{
    abstract class QueryNode : DecisionNode
    {
        private DecisionNode _trueChild, _falseChild;
        public DecisionNode TrueChild
        {
            get { return _trueChild; }
            set { _trueChild = value; }
        }

        public DecisionNode FalseChild
        {
            get { return _falseChild; }
            set { _falseChild = value; }
        }

    }
}
