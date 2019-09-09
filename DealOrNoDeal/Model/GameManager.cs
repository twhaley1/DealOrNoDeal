using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the bank manager.
        /// </summary>
        /// <value>
        ///     Banker object.
        /// </value>
        public Banker Banker { get; }

        /// <summary>
        ///     Gets the round manager.
        /// </summary>
        /// <value>
        ///     RoundManager object.
        /// </value>
        public RoundManager RoundManager { get; }

        /// <summary>
        ///     Gets the case manager.
        /// </summary>
        /// <value>
        ///     CaseManager object.
        /// </value>
        public CaseManager CaseManager { get; }

        /// <summary>
        ///     Gets a value indicating whether the game is started - Player has selected a case.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this game is started; otherwise, <c>false</c>.
        /// </value>
        public bool IsGameStarted { get; private set; }

        /// <summary>
        ///     Gets the number of cases left to open in the current round.
        /// </summary>
        /// <value>
        ///     The cases left to open in the current round.
        /// </value>
        public int CasesLeftInCurrentRound { get; set; }

        /// <summary>
        ///     Gets a value indicating whether the current instance of the game is at the end of a round.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is at the end of a round; otherwise, <c>false</c>.
        /// </value>
        public bool NoRemainingCasesLeft => this.CasesLeftInCurrentRound == 0;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        /// </summary>
        public GameManager()
        {
            this.IsGameStarted = InitialGameStarted;
            this.CasesLeftInCurrentRound = InitialCasesLeft;

            this.Banker = new Banker(this);
            this.RoundManager = new RoundManager();
            this.CaseManager = new CaseManager();

            this.CaseManager.PopulateBriefCases(getNewListOfPossibleDollarAmounts());
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
        /// <returns>The dollar amount of the removed briefcase.</returns>
        /// <exception cref="ConstraintException">Occurs when the specified id corresponds with a briefcase that is no longer in play.</exception>
        public int ProcessBriefCaseRemoval(int id)
        {
            if (!this.CaseManager.IsIdStillInPlay(id))
            {
                throw new ConstraintException(ExceptionMessage.IdNotInPlay);
            }

            var briefcase = this.CaseManager.GetCaseWithId(id);
            if (this.IsGameStarted)
            {
                this.decrementCasesLeftInCurrentRound();
            }
            else
            {
                this.CaseManager.AllocateStartingBriefcase(briefcase);
                this.IsGameStarted = true;
            }

            return this.CaseManager.RemoveBriefcaseFromPlay(briefcase);
        }

        /// <summary>
        ///     Moves the game to the next round.
        ///     Post-condition: RoundIndex = RoundIndex@prev + 1 
        /// </summary>
        public void NextRound()
        {
            this.RoundManager.MoveToNextRound();
            this.CasesLeftInCurrentRound = this.RoundManager.GetNumberOfCasesToOpenThisRound();
        }

        private void decrementCasesLeftInCurrentRound()
        {
            this.CasesLeftInCurrentRound--;
        }

        private static IEnumerable<int> getNewListOfPossibleDollarAmounts()
        {
            return new List<int> {
                0, 1, 5, 10, 25, 50, 75, 100, 200, 300, 400, 500, 750, 1000, 5000, 10000, 25000, 50000, 75000, 100000,
                200000, 300000, 400000, 500000, 750000, 1000000
            };
        }

        #endregion

        #region Constants

        private const int MaximumBriefcaseId = CaseManager.TotalNumberOfCases - 1;
        private const bool InitialGameStarted = false;
        private const int InitialCasesLeft = 6;

        #endregion
    }
}