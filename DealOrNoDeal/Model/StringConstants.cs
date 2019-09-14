using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealOrNoDeal.Model
{

    /// <summary>
    ///     Holds constants that are used to replace magic numbers in the code. Any hard-coded string
    ///     that is more than a word or two should be placed here.
    /// </summary>
    public abstract class StringConstants
    {
        public const string RegularGameModeWelcome = "Welcome to Deal or No Deal!";

        public const string SyndicateGameModeWelcome = "Syndicated Deal or No Deal!";

        public const string MegaGameModeWelcome = "Mega Deal or No Deal!";

        public const string SetUpTheGame = "Please configure your game.";

        public const string SelectYourCase = "Please select your case.";

    }
}
