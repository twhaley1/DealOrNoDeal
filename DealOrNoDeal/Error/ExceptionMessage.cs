namespace DealOrNoDeal.Error
{

    /// <summary>
    ///     Data class that is meant to solely hold constant strings for explanatory
    ///     exception messages.
    /// </summary>
    public abstract class ExceptionMessage
    {

        /// <summary>
        ///     Message for when a Briefcase ID is out of range.
        /// </summary>
        public const string IdOutOfRange = "Id must be greater than or equal to 1 and less than or equal to 26.";

        /// <summary>
        ///     Message for when a Briefcase DollarAmount is less than zero.
        /// </summary>
        public const string DollarAmountLessThanZero = "Dollar amount must be greater than or equal to 0.";

        /// <summary>
        ///     Message for when the GetLastBriefcase method is called with more than one briefcase remaining.
        /// </summary>
        public const string IncorrectNumberOfBriefcasesRemaining = "There must only be one briefcase left in play.";

        /// <summary>
        ///     Message for when a method tries to access a briefcase with an id that is no longer in play.
        /// </summary>
        public const string IdNotInPlay = "The Id must be in a briefcase that has not already been removed.";

        /// <summary>
        ///     Message for when the dollarAmountsInPlay is passed in as null.
        /// </summary>
        public const string DollarAmountsInPlayMustNotBeNull = "dollarAmountsInPlay cannot be null.";

        /// <summary>
        ///     Message for when the dollarAmountsInPlay is passed in without any elements in the list.
        /// </summary>
        public const string DollarAmountsInPlayMustNotBeEmpty = "dollarAmountsInPlay cannot be empty.";

        /// <summary>
        ///     Message for when casesToOpenNextRound is zero.
        /// </summary>
        public const string CasesToOpenNextRoundMustNotBeZero = "casesToOpenNextRound cannot be zero.";

        /// <summary>
        ///     Message for when a method has a null string passed in to it.
        /// </summary>
        public const string NullStringsNotAllowed = "A null string is not accepted.";

        /// <summary>
        ///     Message for when a method has a null manager passed in to it.
        /// </summary>
        public const string NullManagerNotAllowed = "Manager can not be null.";
    }
}
