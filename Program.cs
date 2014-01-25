using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath
{
    public class Program
    {
        public static void Main()
        {
            using (Game game = new Game())
            {
                game.Run();
            }
        }
    }
}
