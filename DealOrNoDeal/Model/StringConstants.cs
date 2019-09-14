namespace DealOrNoDeal.Model
{

    /// <summary>
    ///     Holds constants that are used to replace magic numbers in the code. Single character strings or
    ///     one word strings are not made constants.
    /// </summary>
    public abstract class StringConstants
    {

        /// <summary>
        ///     A whitespace character.
        /// </summary>
        public const string Space = " ";

        /// <summary>
        ///     The regular game mode welcome.
        /// </summary>
        public const string RegularGameModeWelcome = "Welcome to Deal or No Deal!";

        /// <summary>
        ///     The syndicated game mode welcome.
        /// </summary>
        public const string SyndicateGameModeWelcome = "Welcome to Syndicated Deal or No Deal!";

        /// <summary>
        ///     The mega game mode welcome.
        /// </summary>
        public const string MegaGameModeWelcome = "Welcome to Mega Deal or No Deal!";

        /// <summary>
        ///     Game set up.
        /// </summary>
        public const string SetUpTheGame = "Please configure your game.";

        /// <summary>
        ///     Select your case.
        /// </summary>
        public const string SelectYourCase = "Please select your case.";

        /// <summary>
        ///     A congratulations for winning.
        /// </summary>
        public const string YouWin = "Congrats you win";

        /// <summary>
        ///     States that the game is over.
        /// </summary>
        public const string GameOver = "GAME OVER";

        /// <summary>
        ///     Used as a prefix for the value of briefcase's dollar amount.
        /// </summary>
        public const string YourCaseContainedPrefix = "Your case contained:";

        /// <summary>
        ///     Used as a prefix for the value of accepted offer.
        /// </summary>
        public const string AcceptedOfferPrefix = "Accepted offer:";

        /// <summary>
        ///     Used as a prefix for the value of the last offer.
        /// </summary>
        public const string LastOfferPrefix = "Last offer:";

        /// <summary>
        ///     Used as a prefix for the value of the current offer.
        /// </summary>
        public const string CurrentOfferPrefix = "Current offer:";

        /// <summary>
        ///     States that it is now the final round.
        /// </summary>
        public const string IsFinalRound = "This is the final round";

        /// <summary>
        ///     States to select a case above.
        /// </summary>
        public const string SelectCaseAbove = "Select final case above";

        /// <summary>
        ///     The deal or no deal question.
        /// </summary>
        public const string DealOrNoDeal = "Deal or No Deal?";
    }
}
