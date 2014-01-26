using System;
using System.Collections.Generic;
using MinionsOfDeath.Graphics;

namespace MinionsOfDeath
{
	public class DeathCloud : GameObject
	{
		private double _timeAlive = 0;
		private const double TimeToLive = 0.5;

		public DeathCloud () : base (new List<Sprite>() { new Sprite(new List<string>() {
				"Images/BattleCloud0.png", "Images/BattleCloud1.png",
				"Images/BattleCloud2.png", "Images/BattleCloud3.png",
		}, 0.1) })
		{

		}

		public override void Update (double time)
		{
			_timeAlive += time;
			if (_timeAlive > TimeToLive)
			{

			}
			base.Update (time);
		}
	}

	public class DeathCloudList
	{
		private static List<DeathCloud> _deathClouds;
		private static List<DeathCloud> _toRemoveDC;

		public void Add(DeathCloud dc)
		{
			_deathClouds.Add(dc);
		}

		public void Remove(DeathCloud dc)
		{
			_toRemoveDC.Add(dc);
		}

		public void Flush()
		{
			foreach (DeathCloud dc in _toRemoveDC)
			{
				_deathClouds.Remove(dc);
			}
		}

		public void Draw()
		{
			foreach (DeathCloud dc in _deathClouds)
			{
				dc.Draw();
			}
		}

		public void Update(double time)
		{
			foreach (DeathCloud dc in _deathClouds)
			{
				dc.Update(time);
			}
		}
	}
}

