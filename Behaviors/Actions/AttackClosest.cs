using MinionsOfDeath.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MinionsOfDeath.Behaviors.Actions
{
    internal class AttackClosest : Action
    {
        public override DoublePoint GetGoal()
        {
            bool player1 = Game.Player1.Minions.ContainsValue(_owner);
            Minion closest;
            if (player1)
            {
                closest = Game.Player2.Minions.Values.OrderBy(e => WaypointGraph.getDistance((int)_owner.Pos.X, (int)e.Pos.X, (int)_owner.Pos.Y, (int)e.Pos.Y)).First();
            }
            else
            {
                closest = Game.Player1.Minions.Values.OrderBy(e => WaypointGraph.getDistance((int)_owner.Pos.X, (int)e.Pos.X, (int)_owner.Pos.Y, (int)e.Pos.Y)).First();
            }

            return closest.Pos;
        }
    }
}
