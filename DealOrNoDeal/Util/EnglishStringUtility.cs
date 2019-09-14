using System;
using DealOrNoDeal.Error;

namespace DealOrNoDeal.Util
{
    /// <summary>
    ///     Utility class that contains methods designed to manipulate strings that contain letters of the
    ///     english alphabet.
    ///     NOTICE: All methods that deal with manipulating plural strings do not handle the case of english
    ///     words that have a two character plural signature such as 'classes'. The target for these methods are
    ///     words with the one character plural signature such as 'cases' or 'shoes'
    /// </summary>
    public static class EnglishStringUtility
    {
        #region Methods

        /// <summary>
        ///     Appends an s depending on number specified. If the number is 0 or 1 then an s is appended if one does
        ///     not already exist. If the number is anything else, then an s is removed if one exists. The values passed
        ///     in are not changed. Negative numbers are accepted.
        ///     Precondition: stringThatFollowsNumber is not null.
        /// </summary>
        /// <param name="number">The number that the string follows</param>
        /// <param name="stringThatFollowsNumber">The string that follows the number.</param>
        /// <returns>a string combination of the number and a correctly plural string</returns>
        public static string AppendSDependingOnNumber(int number, string stringThatFollowsNumber)
        {
            stringThatFollowsNumber = stringThatFollowsNumber ?? throw new ArgumentNullException(
                                          nameof(stringThatFollowsNumber),
                                          ExceptionMessage.NullStringsNotAllowed);

            var copyOfString = string.Copy(stringThatFollowsNumber).Trim();
            var numberStringPhrase = number + " " + copyOfString;
            var endsWithS = copyOfString.EndsWith('s');
            var absoluteNumber = Math.Abs(number);

            if (absoluteNumber == 1)
            {
                return endsWithS ? numberStringPhrase.Substring(0, numberStringPhrase.Length - 1) : numberStringPhrase;
            }

            return endsWithS ? numberStringPhrase : numberStringPhrase + "s";
        }


        /// <summary>
        ///     When the two parameters are on separate lines, aligns the target string with
        ///     the first character after the alignmentDeclaration.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="alignmentDeclaration">The alignment declaration.</param>
        /// <returns></returns>
        public static String alignTargetAfter(string target, string alignmentDeclaration)
        {
            target = target ?? throw new ArgumentNullException(nameof(target), ExceptionMessage.NullStringsNotAllowed);
            alignmentDeclaration = alignmentDeclaration ??
                                   throw new ArgumentNullException(nameof(alignmentDeclaration),
                                       ExceptionMessage.NullStringsNotAllowed);

            var spaces = "";
            for (var i = 0; i < alignmentDeclaration.Length; i++)
            {
                spaces += " ";
            }

            return spaces + target;
        }

        #endregion
    }
}