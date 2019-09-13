
namespace DealOrNoDeal.Model
{
    /// <summary>
    ///     Manages round information and provides functionality to maneuver to the next round.
    /// </summary>
    public class RoundManager
    {
        private readonly int[] casesPerRound = { 6, 5, 4, 3, 2, 1, 1, 1, 1, 1 };

        #region Properties

        /// <summary>
        ///     Gets the current round of the game.
        /// </summary>
        /// <value>
        ///     The current round.
        /// </value>
        public int CurrentRound { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether the game is at the final round.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the game is at the final round; otherwise, <c>false</c>.
        /// </value>
        public bool IsFinalRound => this.CurrentRound == FinalRound;

        /// <summary>
        ///     Gets a value indicating whether the current instance of the game is at the semi final round.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the game is at the semi final round; otherwise, <c>false</c>.
        /// </value>
        public bool IsSemiFinalRound => this.CurrentRound == SemiFinalRound;

        /// <summary>
        ///     Gets a value indicating whether the current instance of the game is at the first round.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the game is at the first round; otherwise, <c>false</c>.
        /// </value>
        public bool IsFirstRound => this.CurrentRound == InitialCurrentRound;

        /// <summary>
        ///     Gets a value indicating whether the current instance of the game is at the end of a round.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is at the end of a round; otherwise, <c>false</c>.
        /// </value>
        public bool NoRemainingCasesLeft => this.CasesLeftInCurrentRound == 0;

        /// <summary>
        ///     Gets the number of cases left to open in the current round.
        /// </summary>
        /// <value>
        ///     The cases left to open in the current round.
        /// </value>
        public int CasesLeftInCurrentRound { get; set; }

        private int CurrentCasesPerRoundIndex { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoundManager" /> class.
        /// </summary>
        public RoundManager()
        {
            this.CurrentRound = InitialCurrentRound;
            this.CurrentCasesPerRoundIndex = InitialCurrentCasesPerRoundIndex;
            this.CasesLeftInCurrentRound = InitialCasesLeft;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves to next round by incrementing Round property and setting
        ///     initial number of cases for that round
        ///     Post-condition: Round == Round@prev + 1 and CasesLeftInCurrentRound is updated
        /// </summary>
        public void MoveToNextRound()
        {
            this.CurrentRound++;
            this.CurrentCasesPerRoundIndex++;
            this.CasesLeftInCurrentRound = this.GetNumberOfCasesToOpenThisRound();
        }

        /// <summary>
        ///     Gets the number of cases to open this round.
        /// </summary>
        /// <returns>Number of cases to be opened at the start of the current round.</returns>
        public int GetNumberOfCasesToOpenThisRound()
        {
            return this.casesPerRound[this.CurrentCasesPerRoundIndex];
        }

        /// <summary>
        ///     Gets the number of cases to open next round.
        /// </summary>
        /// <returns>Number of cases to be opened at the start of the next round.</returns>
        public int GetNumberOfCasesToOpenNextRound()
        {
            return this.casesPerRound[this.CurrentCasesPerRoundIndex + 1];
        }

        #endregion

        #region Constants

        private const int InitialCurrentRound = 1;
        private const int InitialCurrentCasesPerRoundIndex = 0;
        private const int FinalRound = 10;
        private const int SemiFinalRound = FinalRound - 1;
        private const int InitialCasesLeft = 6;

        #endregion
    }
}