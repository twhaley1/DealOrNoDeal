
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
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves to next round by incrementing Round property and setting
        ///     initial number of cases for that round
        ///     Post-condition: Round == Round@prev + 1
        /// </summary>
        public void MoveToNextRound()
        {
            this.CurrentRound++;
            this.CurrentCasesPerRoundIndex++;
        }

        /// <summary>
        ///     Gets the number of cases to open this round.
        /// </summary>
        /// <returns>number of cases to be opened at the start of the current round.</returns>
        public int GetNumberOfCasesToOpenThisRound()
        {
            return this.casesPerRound[this.CurrentCasesPerRoundIndex];
        }

        public int getNumberOfCasesToOpenNextRound()
        {
            return this.casesPerRound[this.CurrentCasesPerRoundIndex + 1];
        }

        #endregion

        #region Constants

        private const int InitialCurrentRound = 1;
        private const int InitialCurrentCasesPerRoundIndex = 0;
        private const int FinalRound = 10;
        private const int SemiFinalRound = FinalRound - 1;

        #endregion
    }
}