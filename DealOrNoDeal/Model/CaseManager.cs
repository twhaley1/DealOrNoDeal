using System;
using System.Collections.Generic;
using System.Data;
using DealOrNoDeal.Error;

namespace DealOrNoDeal.Model
{
    /// <summary>
    ///     Manages the state of all cases in the game, and provides functionality to
    ///     interact with them.
    /// </summary>
    public class CaseManager
    {
        #region Types and Delegates

        public delegate void Action(Briefcase briefcase);

        #endregion

        #region Data members

        /// <summary>
        ///     The total number of cases at the start of the game.
        /// </summary>
        public const int TotalNumberOfCases = 26;

        private const int InitialStartingBriefcaseId = -1;
        private const Briefcase InitialBriefcase = null;
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

        public Briefcase GetCaseWithId(int id)
        {
            foreach (var briefcase in this.briefcases)
            {
                if (briefcase.Id == id)
                {
                    return briefcase;
                }
            }

            return null;
        }

        public int GetDollarAmountIn(int id)
        {
            if (!this.IsIdStillInPlay(id))
            {
                throw new ConstraintException(ExceptionMessage.IdNotInPlay);
            }

            return this.GetCaseWithId(id).DollarAmount;
        }

        public int RemoveBriefcaseFromPlay(Briefcase briefcase)
        {
            var targetBriefcaseAmount = briefcase.DollarAmount;
            this.briefcases.Remove(briefcase);

            return targetBriefcaseAmount;
        }

        public bool IsIdStillInPlay(int id)
        {
            foreach (var briefcase in this.briefcases)
            {
                if (briefcase.Id == id)
                {
                    return true;
                }
            }

            return false;
        }

        public void AllocateStartingBriefcase(Briefcase targetBriefcase)
        {
            this.StartingCase = targetBriefcase;
            this.StartingCaseId = targetBriefcase.Id;
        }

        public void PopulateBriefCases(IEnumerable<int> dollarAmountsToDistribute)
        {
            var possibleDollarAmounts = new List<int>(dollarAmountsToDistribute);
            for (var i = 0; i < TotalNumberOfCases; i++)
            {
                this.briefcases.Add(new Briefcase(i, this.getUniqueRandomDollarAmountFrom(possibleDollarAmounts)));
            }
        }

        public void ForEachCopyOfACase(Action performAction)
        {
            IEnumerable<Briefcase> copyCases = new List<Briefcase>(this.briefcases);
            foreach (var briefcase in copyCases)
            {
                performAction(briefcase);
            }
        }

        public IList<int> GetListOfDollarAmountsLeftInPlay()
        {
            var dollarAmountsLeftInPlay = new List<int> { this.StartingCase.DollarAmount };
            this.ForEachCopyOfACase(briefcase => dollarAmountsLeftInPlay.Add(briefcase.DollarAmount));

            return dollarAmountsLeftInPlay;
        }

        private int getUniqueRandomDollarAmountFrom(IList<int> dollarAmounts)
        {
            var randomIndex = new Random().Next(0, dollarAmounts.Count);
            var chosenDollarAmount = dollarAmounts[randomIndex];
            dollarAmounts.RemoveAt(randomIndex);

            return chosenDollarAmount;
        }

        #endregion
    }
}