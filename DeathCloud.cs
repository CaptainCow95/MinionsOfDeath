using System;
using System.Collections.Generic;
using MinionsOfDeath.Graphics;

namespace MinionsOfDeath
{
	public class DeathCloud : GameObject
	{
		private double _timeAlive = 0;
		private const double TimeToLive = 0.5;

		public DeathCloud (DoublePoint pos) : base (new List<Sprite>() { new Sprite( new List<string>() {
				"Images/BattleCloud0.png", "Images/BattleCloud1.png",
				"Images/BattleCloud2.png", "Images/BattleCloud3.png",
		}, 0.1) })
		{
			_pos.X = pos.X;
			_pos.Y = pos.Y;
			State = 0;
		}

		public override void Update (double time)
		{
			_timeAlive += time;
			if (_timeAlive > TimeToLive)
			{
				Game.DeathClouds.Remove(this);
			}
			base.Update(time);
		}

		public override void Draw()
		{
			GetSprite().X = _pos.X + _left - _right;
			GetSprite().Y = _pos.Y + _top - _bottom;
			GetSprite().Draw();
		}
	}

	public class DeathCloudList
	{
		private static List<DeathCloud> _deathClouds = new List<DeathCloud>();
		//private static List<DeathCloud> _toRemoveDC = new List<DeathCloud>();

		public void Add(DeathCloud dc)
		{
			_deathClouds.Add(dc);
		}

		public void Remove(DeathCloud dc)
		{
			//_toRemoveDC.Add(dc);
			_deathClouds.Remove(dc);
		}

//		public void Flush()
//		{
//			_toRemoveDC.ForEach(dc => _deathClouds.Remove(dc));
//		}

		public void Draw()
		{
			_deathClouds.ForEach(dc => dc.Draw());
		}

		public void Update(double time)
		{
			_deathClouds.ForEach(dc => dc.Update(time));
		}
	}
}

