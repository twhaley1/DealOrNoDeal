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

        private int currentOffer;

        #region Properties

        /// <summary>
        ///     Gets the current bank offer.
        /// </summary>
        /// <value>
        ///     The current bank offer.
        /// </value>
        public int CurrentOffer
        {
            get => this.currentOffer;
            private set
            {
                this.currentOffer = value;
                this.updateOfferStatistics(value);
            }
        }

        /// <summary>
        ///     Gets the minimum bank offer.
        /// </summary>
        /// <value>
        ///     The minimum bank offer.
        /// </value>
        public int MinOffer { get; private set; }

        /// <summary>
        ///     Gets the maximum bank offer.
        /// </summary>
        /// <value>
        ///     The maximum bank offer.
        /// </value>
        public int MaxOffer { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Banker" /> class.
        /// </summary>
        public Banker()
        {
            this.CurrentOffer = InitialCurrentOffer;
            this.MinOffer = InitialMinOffer;
            this.MaxOffer = InitialMaxOffer;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Calculates the current offer. The offer given depends on the current state of the game and is
        ///     determined by the following formula:
        ///     offer = amountOfMoneyInAllRemainingBriefCases / casesToOpenNextRound / totalNumberOfCasesRemaining
        ///     The offer is then rounded to the nearest dollar.
        ///     Precondition: dollarAmountsInPlay does not equal null AND dollarAmountsInPlay.Count does not equal 0 AND
        ///     casesToOpenNextRound does not equal 0.
        ///     Post-condition: CurrentOffer = the banker's new offer. If CurrentOffer greater than MaxOffer, then MaxOffer = CurrentOffer.
        ///     If CurrentOffer less than MinOffer, then MinOffer = CurrentOffer
        /// </summary>
        /// <param name="dollarAmountsInPlay">A list of all the remaining dollar amounts still contained in briefcases in play.</param>
        /// <param name="casesToOpenNextRound">The number of cases to open next round.</param>
        /// <returns>The banker's current offer for the player.</returns>
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

            var roundedOffer = (int) Math.Round(unRoundedOffer, MidpointRounding.AwayFromZero);
            this.CurrentOffer = roundedOffer;

            return this.CurrentOffer;
        }

        private int sumDollarAmounts(IEnumerable<int> dollarAmounts)
        {
            return dollarAmounts.Sum();
        }

        private void updateOfferStatistics(int newOffer)
        {
            if (newOffer < this.MinOffer)
            {
                this.MinOffer = newOffer;
            }

            if (newOffer > this.MaxOffer)
            {
                this.MaxOffer = newOffer;
            }
        }

        #endregion

        #region Constants

        private const int InitialMinOffer = int.MaxValue;
        private const int InitialMaxOffer = int.MinValue;
        private const int InitialCurrentOffer = 0;

        #endregion
    }
}