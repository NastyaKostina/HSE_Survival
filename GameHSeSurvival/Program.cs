using System;
using ScoresForm;

namespace GameHSeSurvival
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
            {
                game.Run();
                int playerTime = (int)Math.Floor(game.finalTime);
                int playerScore = game.finalScore;
                EnterYourNameForm form = new EnterYourNameForm();
            }

        }
    }
#endif
}
