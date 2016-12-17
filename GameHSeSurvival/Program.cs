using System;
using ScoresForm;
using System.Windows.Forms;

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
            int playerTime, playerScore;
            using (var game = new Game1())
            {
                game.Run();
                playerTime = (int)Math.Floor(game.finalTime);
                playerScore = game.finalScore;
            }
            if (playerScore != 0)
            {
                Application.EnableVisualStyles();
                Application.Run(new EnterYourNameForm(playerTime, playerScore));
            }
        }
    }
#endif
}
