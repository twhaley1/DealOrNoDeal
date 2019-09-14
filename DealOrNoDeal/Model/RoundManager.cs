
using System.Runtime.CompilerServices;

namespace DealOrNoDeal.Model
{
    /// <summary>
    ///     Manages round information and provides functionality to maneuver to the next round.
    /// </summary>
    public class RoundManager
    {
        private readonly int[] defaultTenRoundCasesPerRound = { 6, 5, 4, 3, 2, 1, 1, 1, 1, 1 };
        private readonly int[] shorterSevenRoundCasesPerRound = { 8, 6, 4, 3, 2, 1, 1 };
        private readonly int[] longerThirteenRoundCasesPerRound = { 7, 5, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
        private GameLength currentGameLength;
        private int[] currentCasesPerRound;

        #region Properties

        public GameLength CurrentGameLength
        {
            get => this.currentGameLength;
            set
            {
                this.currentGameLength = value;
                switch (value)
                {
                    case GameLength.Long:
                        this.CurrentCasesPerRound = this.longerThirteenRoundCasesPerRound;
                        break;
                    case GameLength.Short:
                        this.CurrentCasesPerRound = this.shorterSevenRoundCasesPerRound;
                        break;
                    case GameLength.Default:
                        this.CurrentCasesPerRound = this.defaultTenRoundCasesPerRound;
                        break;
                    default:
                        this.CurrentCasesPerRound = this.defaultTenRoundCasesPerRound;
                        break;
                }
            }
        }

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
        public bool IsFinalRound => this.CurrentRound == this.FinalRound;

        /// <summary>
        ///     Gets a value indicating whether the current instance of the game is at the semi final round.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the game is at the semi final round; otherwise, <c>false</c>.
        /// </value>
        public bool IsSemiFinalRound => this.CurrentRound == this.SemiFinalRound;

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

        private int FinalRound => this.CurrentCasesPerRound.Length;

        private int SemiFinalRound => this.FinalRound - 1;

        private int[] CurrentCasesPerRound
        {
            get => this.currentCasesPerRound;
            set
            {
                this.currentCasesPerRound = value;
                this.CasesLeftInCurrentRound = this.CurrentCasesPerRound[0];
            }
        }

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
            this.CurrentGameLength = GameLength.Default;
            this.CurrentCasesPerRound = this.defaultTenRoundCasesPerRound;
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
            return this.CurrentCasesPerRound[this.CurrentCasesPerRoundIndex];
        }

        /// <summary>
        ///     Gets the number of cases to open next round.
        /// </summary>
        /// <returns>Number of cases to be opened at the start of the next round.</returns>
        public int GetNumberOfCasesToOpenNextRound()
        {
            return this.CurrentCasesPerRound[this.CurrentCasesPerRoundIndex + 1];
        }

        #endregion

        #region Constants

        private const int InitialCurrentRound = 1;
        private const int InitialCurrentCasesPerRoundIndex = 0;
        private const int InitialCasesLeft = 6;

        #endregion
    }
}