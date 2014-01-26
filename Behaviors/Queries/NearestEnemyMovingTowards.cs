using MinionsOfDeath.Navigation;
using System.Linq;

namespace MinionsOfDeath.Behaviors.Queries
{
    public class NearestEnemyMovingTowards : QueryNode
    {
        public NearestEnemyMovingTowards(Minion owner)
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
                succeed = _owner.MoveDirectionUp && !minion.MoveDirectionUp && _owner.Pos.Y < minion.Pos.Y
                       || !_owner.MoveDirectionUp && minion.MoveDirectionUp && _owner.Pos.Y > minion.Pos.Y;
            }
            else
            {
                Minion minion = Game.Player1.Minions.Values.OrderBy(e => WaypointGraph.getDistance((int)_owner.Pos.X, (int)e.Pos.X, (int)_owner.Pos.Y, (int)e.Pos.Y)).First();
                succeed = _owner.MoveDirectionUp && !minion.MoveDirectionUp && _owner.Pos.Y < minion.Pos.Y
                       || !_owner.MoveDirectionUp && minion.MoveDirectionUp && _owner.Pos.Y > minion.Pos.Y;
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