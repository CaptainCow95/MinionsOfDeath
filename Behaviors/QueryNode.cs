﻿namespace MinionsOfDeath.Behaviors
{
    public abstract class QueryNode : DecisionNode
    {
        private DecisionNode _trueChild, _falseChild;

        public QueryNode(Minion owner)
            : base(owner)
        {
        }

        public DecisionNode FalseChild
        {
            get { return _falseChild; }
            set { _falseChild = value; }
        }

        public DecisionNode TrueChild
        {
            get { return _trueChild; }
            set { _trueChild = value; }
        }
    }
}