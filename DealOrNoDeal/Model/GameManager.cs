using System.Data;
using DealOrNoDeal.Error;

namespace DealOrNoDeal.Model
{
    /// <summary>
    ///     Handles the management of the actual game play.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private readonly Banker banker;
        private readonly RoundManager roundManager;
        private readonly CaseManager caseManager;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether the game is started - Player has selected a case.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this game is started; otherwise, <c>false</c>.
        /// </value>
        public bool IsGameStarted { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this game is over.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the game is over; otherwise, <c>false</c>.
        /// </value>
        public bool IsGameOver { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether the current instance of the game is at the end of a round.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is at the end of a round; otherwise, <c>false</c>.
        /// </value>
        public bool GetNoRemainingCasesLeft => this.roundManager.NoRemainingCasesLeft;

        /// <summary>
        ///     Gets a value indicating whether the current round is the final round.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it is the final round; otherwise, <c>false</c>.
        /// </value>
        public bool GetIsFinalRound => this.roundManager.IsFinalRound;

        /// <summary>
        ///     Gets a value indicating whether the current round is the semi final round.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it is the semi final round; otherwise, <c>false</c>.
        /// </value>
        public bool GetIsSemiFinalRound => this.roundManager.IsSemiFinalRound;

        /// <summary>
        ///     Gets a value indicating whether the current round is the first round.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it is the first round; otherwise, <c>false</c>.
        /// </value>
        public bool GetIsFirstRound => this.roundManager.IsFirstRound;

        /// <summary>
        ///     Gets the get current round number.
        /// </summary>
        /// <value>
        ///     The current round.
        /// </value>
        public int GetCurrentRound => this.roundManager.CurrentRound;

        /// <summary>
        ///     Gets the get number of cases to open in current round.
        /// </summary>
        /// <value>
        ///     The get number cases to open in current round.
        /// </value>
        public int GetNumberCasesToOpenInCurrentRound => this.roundManager.GetNumberOfCasesToOpenThisRound();

        /// <summary>
        ///     Gets the number of cases left in current round.
        /// </summary>
        /// <value>
        ///     The number of cases left in current round.
        /// </value>
        public int GetNumberCasesLeftInCurrentRound => this.roundManager.CasesLeftInCurrentRound;

        /// <summary>
        ///     Gets the minimum offer.
        /// </summary>
        /// <value>
        ///     The minimum offer.
        /// </value>
        public int GetMinOffer => this.banker.MinOffer;

        /// <summary>
        ///     Gets the maximum offer.
        /// </summary>
        /// <value>
        ///     The maximum offer.
        /// </value>
        public int GetMaxOffer => this.banker.MaxOffer;

        /// <summary>
        ///     Gets the current offer.
        /// </summary>
        /// <value>
        ///     The current offer.
        /// </value>
        public int GetCurrentOffer => this.calculateOffer();

        /// <summary>
        ///     Gets the starting briefcase's identifier.
        /// </summary>
        /// <value>
        ///     The starting briefcase's identifier.
        /// </value>
        public int GetStartingBriefcaseId => this.caseManager.StartingCaseId;

        /// <summary>
        ///     Gets the starting briefcase's dollar amount.
        /// </summary>
        /// <value>
        ///     The starting briefcase's dollar amount.
        /// </value>
        public int GetStartingBriefcaseDollarAmount => this.caseManager.StartingCase.DollarAmount;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        /// </summary>
        public GameManager()
        {
            this.IsGameStarted = InitialGameStarted;
            this.IsGameOver = InitialGameOver;

            this.banker = new Banker();
            this.roundManager = new RoundManager();
            this.caseManager = new CaseManager();

            this.caseManager.PopulateBriefCases();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Determines whether id is valid.
        /// </summary>
        /// <param name="id">A briefcase identifier. This corresponds to a Briefcase's id attribute.</param>
        /// <returns>
        ///     <c>true</c> if id is greater than or equal to 0 and less than or equal to 25; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidBriefcaseId(int id)
        {
            return id >= 0 && id <= MaximumBriefcaseId;
        }

        /// <summary>
        ///     Determines whether or not the dollar amount is valid.
        /// </summary>
        /// <param name="dollarAmount">A briefcase's dollar amount. This corresponds to a Briefcase's dollar amount attribute.</param>
        /// <returns>
        ///     <c>true</c> if dollarAmount is greater than or equal to zero; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidBriefcaseDollarAmount(int dollarAmount)
        {
            return dollarAmount >= 0;
        }

        /// <summary>
        ///     Processes the removal of briefcase with a specific id attribute.
        ///     Precondition: The id must be related to a briefcase that is currently in play.
        /// </summary>
        /// <param name="id">A briefcase identifier.</param>
        /// <returns>The dollar amount in the removed briefcase.</returns>
        /// <exception cref="ConstraintException">Occurs when the specified id corresponds with a briefcase that is no longer in play.</exception>
        public int ProcessBriefCaseRemoval(int id)
        {
            if (!this.caseManager.IsIdStillInPlay(id))
            {
                throw new ConstraintException(ExceptionMessage.IdNotInPlay);
            }

            var briefcase = this.caseManager.GetCaseWithId(id);
            if (this.IsGameStarted)
            {
                this.decrementCasesLeftInCurrentRound();
            }
            else
            {
                this.caseManager.AllocateStartingBriefcase(briefcase);
                this.IsGameStarted = true;
            }

            return this.caseManager.RemoveBriefcaseFromPlay(briefcase);
        }

        /// <summary>
        ///     Moves the game to the next round.
        /// </summary>
        public void NextRound()
        {
            this.roundManager.MoveToNextRound();
        }

        /// <summary>
        ///     Gets the dollar amount in the briefcase with the specified id.
        /// </summary>
        /// <param name="briefcaseId">The briefcase identifier.</param>
        /// <returns>The dollar amount in the briefcase with the specified id.</returns>
        public int GetIdsDollarAmount(int briefcaseId)
        {
            return this.caseManager.GetDollarAmountIn(briefcaseId);
        }

        /// <summary>
        ///     Gets the last briefcases dollar amount.
        ///     Precondition: There must only be one briefcase left in play, apart from the starting briefcase.
        /// </summary>
        /// <returns>The dollar amount contained in the last briefcase, not the player's starting briefcase.</returns>
        public int GetLastBriefcasesDollarAmount()
        {
            return this.caseManager.GetLastBriefcase().DollarAmount;
        }

        /// <summary>
        ///     Gets the last briefcases identifier.
        ///     Precondition: There must only be one briefcase left in play, apart from the starting briefcase.
        /// </summary>
        /// <returns>The id of the last briefcase, not the player's starting briefcase.</returns>
        public int GetLastBriefcasesId()
        {
            return this.caseManager.GetLastBriefcase().Id;
        }


        /// <summary>
        ///     Ends the game.
        ///     Post-condition: IsGameOver = true
        /// </summary>
        public void EndGame()
        {
            this.IsGameOver = true;
        }

        private void decrementCasesLeftInCurrentRound()
        {
            this.roundManager.CasesLeftInCurrentRound--;
        }

        private int calculateOffer()
        {
            return this.banker.CalculateOffer(this.caseManager.GetDollarAmountsInPlay(),
                this.roundManager.GetNumberOfCasesToOpenNextRound());
        }

        #endregion

        #region Constants

        private const int MaximumBriefcaseId = CaseManager.TotalNumberOfCases - 1;
        private const bool InitialGameStarted = false;
        private const bool InitialGameOver = false;

        #endregion
    }
}