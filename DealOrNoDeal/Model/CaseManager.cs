using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DealOrNoDeal.Error;

namespace DealOrNoDeal.Model
{
    /// <summary>
    ///     Manages the state of all cases in the game, and provides functionality to
    ///     interact with them.
    /// </summary>
    public class CaseManager
    {

        #region Data members

        private readonly IList<Briefcase> briefcases;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the player's starting briefcase identifier.
        /// </summary>
        /// <value>
        ///     The player's starting briefcase identifier.
        /// </value>
        public int StartingCaseId { get; private set; }

        /// <summary>
        ///     Gets the player's starting briefcase.
        /// </summary>
        /// <value>
        ///     The player's starting briefcase.
        /// </value>
        public Briefcase StartingCase { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CaseManager" /> class.
        /// </summary>
        public CaseManager()
        {
            this.StartingCaseId = InitialStartingBriefcaseId;
            this.StartingCase = InitialBriefcase;

            this.briefcases = new List<Briefcase>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the last briefcase to be opened by the player. This is the briefcase in which the player
        ///     will choose their starting briefcase or this briefcase.
        ///     Precondition: there must only be one briefcase left in play.
        /// </summary>
        /// <returns>the last briefcase in play.</returns>
        /// <exception cref="ConstraintException">Occurs when there is more than one briefcase still in play.</exception>
        public Briefcase GetLastBriefcase()
        {
            if (this.briefcases.Count != 1)
            {
                throw new ConstraintException(ExceptionMessage.IncorrectNumberOfBriefcasesRemaining);
            }

            return this.briefcases[0];
        }

        /// <summary>
        ///     Gets the case with chosen identifier.
        ///     Precondition: The chosen id must belong to a briefcase that is still in play.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The briefcase with the chosen identifier or null if a briefcase was not found.</returns>
        /// <exception cref="ConstraintException">Occurs when the id chosen belongs to a briefcase that is not in play.</exception>
        public Briefcase GetCaseWithId(int id)
        {
            if (!this.IsIdStillInPlay(id))
            {
                throw new ConstraintException(ExceptionMessage.IdNotInPlay);
            }

            return this.briefcases.Single(briefcase => briefcase.Id == id);
        }


        /// <summary>
        ///     Gets the dollar amount in the briefcase with the chosen id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The dollar amount in the briefcase with the specified id.</returns>
        public int GetDollarAmountIn(int id)
        {
            return this.GetCaseWithId(id).DollarAmount;
        }

        /// <summary>
        ///     Removes the specified briefcase from play.
        /// </summary>
        /// <param name="briefcase">The briefcase.</param>
        /// <returns>The dollar amount in the removed briefcase.</returns>
        public int RemoveBriefcaseFromPlay(Briefcase briefcase)
        {
            var targetBriefcaseAmount = briefcase.DollarAmount;
            this.briefcases.Remove(briefcase);

            return targetBriefcaseAmount;
        }

        /// <summary>
        ///     Determines whether a briefcase with the specified id is still in play.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <c>true</c> if a briefcase with the specified id is in play; otherwise, <c>false</c>.
        /// </returns>
        public bool IsIdStillInPlay(int id)
        {
            return this.briefcases.Any(briefcase => briefcase.Id == id);
        }

        /// <summary>
        ///     Allocates the starting briefcase and its id.
        ///     Post-condition: StartingCase = targetBriefcase and StartingCaseId = targetBriefcase.Id
        /// </summary>
        /// <param name="targetBriefcase">The target briefcase.</param>
        public void AllocateStartingBriefcase(Briefcase targetBriefcase)
        {
            this.StartingCase = targetBriefcase;
            this.StartingCaseId = targetBriefcase.Id;
        }

        /// <summary>
        ///     Populates the game's briefcases with briefcase objects with unique identifiers and dollar amounts.
        /// </summary>
        public void PopulateBriefCases(IEnumerable<int> dollarAmounts)
        {
            this.briefcases.Clear();
            var possibleDollarAmounts = new List<int>(dollarAmounts);
            for (var i = 0; i < TotalNumberOfCases; i++)
            {
                this.briefcases.Add(new Briefcase(i, this.getUniqueRandomDollarAmountFrom(possibleDollarAmounts)));
            }
        }

        /// <summary>
        ///     Gets the new list of possible dollar amounts depending on the game mode.
        ///     The default is GameMode.Regular
        /// </summary>
        /// <param name="mode">The game mode.</param>
        /// <returns></returns>
        public IList<int> GetNewListOfPossibleDollarAmounts(GameMode mode)
        {
            switch (mode)
            {
                case GameMode.Regular:
                    return new List<int> {
                        0, 1, 5, 10, 25, 50, 75, 100, 200, 300, 400, 500, 750, 1000, 5000, 10000, 25000, 50000, 75000, 100000,
                        200000, 300000, 400000, 500000, 750000, 1000000
                    };
                case GameMode.Mega:
                    return new List<int> {
                        0, 100, 500, 1000, 2500, 5000, 7500, 10000, 20000, 30000, 40000, 50000, 75000, 100000,
                        225000, 400000, 500000, 750000, 1000000, 2000000, 3000000, 4000000, 5000000, 6000000, 8500000, 10000000
                    };
                case GameMode.Syndicated:
                    return new List<int> {
                        0, 1, 5, 10, 25, 50, 75, 100, 200, 300, 400, 500, 750, 1000, 2500, 5000, 10000, 25000, 50000, 75000,
                        100000, 150000, 200000, 250000, 350000, 500000
                    };
                default:
                    return new List<int> {
                        0, 1, 5, 10, 25, 50, 75, 100, 200, 300, 400, 500, 750, 1000, 5000, 10000, 25000, 50000, 75000, 100000,
                        200000, 300000, 400000, 500000, 750000, 1000000
                    };
            }

        }

        /// <summary>
        ///     Gets a list of dollar amounts in play.
        /// </summary>
        /// <returns>a list of dollar amounts left in play.</returns>
        public IList<int> GetDollarAmountsInPlay()
        {
            var dollarAmounts = this.briefcases.Select(briefcase => briefcase.DollarAmount).ToList();
            dollarAmounts.Add(this.StartingCase.DollarAmount);
            return dollarAmounts;
        }

        /// <summary>
        ///     Puts the starting case in play.
        /// </summary>
        public void PutStartingCaseInPlay()
        {
            this.briefcases.Add(this.StartingCase);
        }

        private int getUniqueRandomDollarAmountFrom(IList<int> dollarAmounts)
        {
            var randomIndex = new Random().Next(0, dollarAmounts.Count);
            var chosenDollarAmount = dollarAmounts[randomIndex];
            dollarAmounts.RemoveAt(randomIndex);

            return chosenDollarAmount;
        }

        #endregion

        #region Constants


        /// <summary>
        ///     The total number of cases at the start of the game.
        /// </summary>
        public const int TotalNumberOfCases = 26;

        private const int InitialStartingBriefcaseId = -1;
        private const Briefcase InitialBriefcase = null;

        #endregion
    }
}