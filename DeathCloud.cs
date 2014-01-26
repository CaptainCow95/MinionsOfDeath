using System;
using System.Collections.Generic;
using MinionsOfDeath.Graphics;

namespace MinionsOfDeath
{
	public class DeathCloud : GameObject
	{
		private double _timeAlive = 0;
		private const double TimeToLive = 0.5;

		public DeathCloud (List<Sprite> sprites) : base (sprites)
		{

		}

		public override void Update (double time)
		{
			_timeAlive += time;
			if (_timeAlive > TimeToLive) {

			}
			base.Update (time);
		}
	}
}

