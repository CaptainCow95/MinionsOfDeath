namespace MinionsOfDeath.Behaviors
{
    internal abstract class QueryNode : DecisionNode
    {
        private DecisionNode _trueChild, _falseChild;

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