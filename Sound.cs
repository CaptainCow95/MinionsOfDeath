using System.Media;

namespace MinionsOfDeath
{
    public class Sound
    {
        public static readonly SoundPlayer Combat = new SoundPlayer("Sounds/combat.wav");
        public static readonly SoundPlayer Strategize = new SoundPlayer("Sounds/strategize.wav");

        public static void Load()
        {
            Strategize.Load();
            Combat.Load();
        }

        public static void StopAll()
        {
            Strategize.Stop();
            Combat.Stop();
        }
    }
}