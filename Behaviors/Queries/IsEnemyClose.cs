using MinionsOfDeath.Navigation;
using System.Linq;

namespace MinionsOfDeath.Behaviors.Queries
{
    public class IsEnemyClose : QueryNode
    {
        private double closeDist = 200;

        public IsEnemyClose(Minion owner)
            : base(owner)
        {
        }

        public override DoublePoint GetGoal()
        {
            bool player1 = Game.Player1.Minions.ContainsValue(_owner);
            bool succeed;
            if (player1)
            {
                Minion minion = Game.Player2.Minions.Values.OrderBy(e => WaypointGraph.getDistance((int)_owner.Pos.X, (int)e.Pos.X, (int)_owner.Pos.Y, (int)e.Pos.Y)).First();
                succeed = WaypointGraph.getDistance((int)_owner.Pos.X, (int)minion.Pos.X, (int)_owner.Pos.Y, (int)minion.Pos.Y) < closeDist;
            }
            else
            {
                Minion minion = Game.Player1.Minions.Values.OrderBy(e => WaypointGraph.getDistance((int)_owner.Pos.X, (int)e.Pos.X, (int)_owner.Pos.Y, (int)e.Pos.Y)).First();
                succeed = WaypointGraph.getDistance((int)_owner.Pos.X, (int)minion.Pos.X, (int)_owner.Pos.Y, (int)minion.Pos.Y) < closeDist;
            }
            if (succeed)
            {
                return TrueChild.GetGoal();
            }
            else
            {
                return FalseChild.GetGoal();
            }
        }
    }
}