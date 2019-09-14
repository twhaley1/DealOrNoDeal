using System;
using DealOrNoDeal.Error;

namespace DealOrNoDeal.Model
{

    /// <summary>
    ///     Data class that stores a briefcase's id and dollar amount.
    /// </summary>
    public class Briefcase
    {

        /// <summary>
        ///     A briefcase identifier that is used to give a briefcase a type of identity.
        /// </summary>
        /// <value>
        ///     A unique integer.
        /// </value>
        public int Id { get; }

        /// <summary>
        ///     A dollar amount that is stored within the briefcase.
        /// </summary>
        /// <value>
        ///     A currency amount.
        /// </value>
        public int DollarAmount { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Briefcase"/> class.
        ///     Precondition: id greater than or equal to 0. id less than or equal to 25. dollarAmount greater than or equal to 0.
        ///     Post-condition: Id = id. DollarAmount = dollarAmount
        /// </summary>
        /// <param name="id">The briefcase's unique identifier.</param>
        /// <param name="dollarAmount">The dollar amount that is stored within the briefcase.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     id or dollarAmount - exception thrown by violated precondition.
        /// </exception>
        public Briefcase(int id, int dollarAmount)
        {
            if (!GameManager.IsValidBriefcaseId(id))
            {
                throw new ArgumentOutOfRangeException(nameof(id), ExceptionMessage.IdOutOfRange);
            }

            if (!GameManager.IsValidBriefcaseDollarAmount(dollarAmount))
            {
                throw new ArgumentOutOfRangeException(nameof(dollarAmount), ExceptionMessage.DollarAmountLessThanZero);
            }

            this.Id = id;
            this.DollarAmount = dollarAmount;
        }

    }
}
