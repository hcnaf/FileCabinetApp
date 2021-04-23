using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Default parameters validator.
    /// </summary>
    public class DefaultValidator : IRecordValidator
    {
        /// <summary>
        /// Validates default parameters.
        /// </summary>
        /// <param name="record">Parameters to validate.</param>
        public void ValidateParameters(FileCabinetRecord record)
        {
            if (record is null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            if (record.FirstName is null)
            {
                throw new ArgumentNullException(nameof(record.FirstName), "First name is null.");
            }

            if (record.FirstName.Length < 2 || record.FirstName.Length > 60 || record.FirstName.Contains(' '))
            {
                throw new ArgumentException("Invalid first name.");
            }

            if (record.LastName is null)
            {
                throw new ArgumentNullException(nameof(record.LastName), "Last name is null.");
            }

            if (record.LastName.Length < 2 || record.LastName.Length > 60 || record.LastName.Contains(' '))
            {
                throw new ArgumentException("Invalid last name.");
            }

            if (record.DateOfBirth > DateTime.Now || record.DateOfBirth < new DateTime(1950, 1, 1))
            {
                throw new ArgumentOutOfRangeException(nameof(record.DateOfBirth), "Date of birth is out of range.");
            }

            if (record.SecurityCharacter == '\0')
            {
                throw new ArgumentException("Security character is empty.");
            }

            if (!record.PaymentSystem.Equals("Visa", StringComparison.InvariantCultureIgnoreCase) &&
                !record.PaymentSystem.Equals("MasterCard", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException("Invalid payment system.");
            }

            if (record.CountryCode < 0 || record.CountryCode > 999)
            {
                throw new ArgumentOutOfRangeException(nameof(record.CountryCode), "Invalid country code.");
            }
        }
    }
}
