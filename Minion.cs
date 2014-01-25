using MinionsOfDeath.Behaviors;

namespace MinionsOfDeath
{
    internal class Minion : GameObject
    {
		Behavior _behavior = null;

		public Behavior Behavior
		{
			get { return _behavior; }
			set { _behavior = value; }
		}

		public abstract void update(double time)
		{
			Move move = _behavior.getMove();
		}
    }
}