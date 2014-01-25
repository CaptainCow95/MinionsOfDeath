namespace MinionsOfDeath
{
    public class Program
    {
        public static void Main()
        {
            using (Game game = new Game())
            {
                game.Run(60, 60);
            }
        }
    }
}