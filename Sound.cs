using System.Media;

namespace MinionsOfDeath
{
    public class Sound
    {
		public static readonly SoundPlayer Combat = new SoundPlayer("Sounds/Combat.wav");
		public static readonly SoundPlayer Strategize = new SoundPlayer("Sounds/Strategize.wav");
		//public static readonly SoundPlayer Death = new SoundPlayer("Sounds/Death.wav");
		public static readonly SoundPlayer Victory = new SoundPlayer("Sounds/VictoryLong.wav");

        public static void Load()
        {
            Strategize.Load();
            Combat.Load();
			//Death.Load();
			Victory.Load();
        }

        public static void StopAll()
        {
            Strategize.Stop();
            Combat.Stop();
			//Death.Stop();
			Victory.Stop();
        }
    }
}