using System;
using System.Globalization;

namespace BuildingBlocks.Domain
{
    public abstract class ValidationException : Exception
    {
    }

    /// <summary>
    /// Exception thrown by <see cref="ValueObject"/> when the input was exceed maximum expected length
    /// </summary>
    public class MaxLengthValidationException : ValidationException
    {
        private readonly string _name;
        private readonly int _maxLength;

        public MaxLengthValidationException(string name, int maxLength)
        {
            _name = name;
            _maxLength = maxLength;
        }
        private static string DefaultErrorMessageString => "The {0} was exceed maximum expected length {1}";

        public override string Message => string.Format(CultureInfo.CurrentCulture, DefaultErrorMessageString, _name, _maxLength);
    }
}