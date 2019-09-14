using System;
using System.Collections.Generic;
using System.Linq;
using DealOrNoDeal.Error;

namespace DealOrNoDeal.Model
{
    /// <summary>
    ///     Banker class that maintains the state of all offers throughout the game.
    ///     Offers functionality and information statistics regarding the offers.
    /// </summary>
    public class Banker
    {
        #region Data members

        #region Constants

        private const int RoundingFactor = 100;

        #endregion

        private readonly IList<int> offers;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the current bank offer.
        /// </summary>
        /// <value>
        ///     The current bank offer.
        /// </value>
        public int CurrentOffer => this.offers.Last();

        /// <summary>
        ///     Gets the minimum bank offer.
        /// </summary>
        /// <value>
        ///     The minimum bank offer.
        /// </value>
        public int MinOffer => this.offers.Min();

        /// <summary>
        ///     Gets the maximum bank offer.
        /// </summary>
        /// <value>
        ///     The maximum bank offer.
        /// </value>
        public int MaxOffer => this.offers.Max();

        /// <summary>
        ///     Gets the average offer rounded to the nearest 100.
        /// </summary>
        /// <value>
        ///     The average offer.
        /// </value>
        public int AvgOffer =>
            (int) Math.Round(this.offers.Average() / RoundingFactor, 0, MidpointRounding.AwayFromZero) * RoundingFactor;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Banker" /> class.
        /// </summary>
        public Banker()
        {
            this.offers = new List<int>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Calculates the current offer. The offer given depends on the current state of the game and is
        ///     determined by the following formula:
        ///     offer = amountOfMoneyInAllRemainingBriefCases / casesToOpenNextRound / totalNumberOfCasesRemaining
        ///     The offer is then rounded to the nearest hundred.
        ///     Precondition: dollarAmountsInPlay does not equal null AND dollarAmountsInPlay.Count does not equal 0 AND
        ///     casesToOpenNextRound does not equal 0.
        /// </summary>
        /// <param name="dollarAmountsInPlay">A list of all the remaining dollar amounts still contained in briefcases in play.</param>
        /// <param name="casesToOpenNextRound">The number of cases to open next round.</param>
        /// <returns>The banker's offer for the player.</returns>
        public int CalculateOffer(IList<int> dollarAmountsInPlay, int casesToOpenNextRound)
        {
            if (dollarAmountsInPlay == null)
            {
                throw new ArgumentNullException(nameof(dollarAmountsInPlay),
                    ExceptionMessage.DollarAmountsInPlayMustNotBeNull);
            }

            if (dollarAmountsInPlay.Count == 0)
            {
                throw new ArgumentException(ExceptionMessage.DollarAmountsInPlayMustNotBeEmpty);
            }

            if (casesToOpenNextRound == 0)
            {
                throw new ArgumentException(ExceptionMessage.CasesToOpenNextRoundMustNotBeZero);
            }

            var numberOfCasesRemaining = dollarAmountsInPlay.Count;
            var totalDollarAmountRemaining = this.sumDollarAmounts(dollarAmountsInPlay);
            var unRoundedOffer = totalDollarAmountRemaining / (decimal) casesToOpenNextRound / numberOfCasesRemaining;

            var roundedOffer = (int) Math.Round(unRoundedOffer / RoundingFactor, 0, MidpointRounding.AwayFromZero) *
                               RoundingFactor;

            return roundedOffer;
        }

        /// <summary>
        ///     Adds the formal offer to the list of offers.
        ///     Post-condition: CurrentOffer = offer. If offer less than MinOffer, MinOffer = offer.
        ///     If offer greater than MaxOffer, MaxOffer = offer.
        /// </summary>
        /// <param name="offer">The offer.</param>
        public void AddFormalOffer(int offer)
        {
            this.offers.Add(offer);
        }

        private int sumDollarAmounts(IEnumerable<int> dollarAmounts)
        {
            return dollarAmounts.Sum();
        }

        #endregion
    }
}