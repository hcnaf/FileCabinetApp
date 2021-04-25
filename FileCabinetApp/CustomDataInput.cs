using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Custom DataInput.
    /// </summary>
    internal class CustomDataInput : IDataInput
    {
        /// <summary>
        /// Data input method.
        /// </summary>
        /// <returns>Converted and validated parameter.</returns>
        public FileCabinetRecord DataInput()
        {
            var record = new FileCabinetRecord();

            Console.Write("First name: ");
            record.FirstName = ReadInput(
                new Func<string, Tuple<bool, string, string>>(StringConverter),
                new Func<string, Tuple<bool, string>>(FirstNameValidator));

            Console.Write("Last name: ");
            record.LastName = ReadInput(
                new Func<string, Tuple<bool, string, string>>(StringConverter),
                new Func<string, Tuple<bool, string>>(LastNameValidator));

            Console.Write("Date of birth: ");
            record.DateOfBirth = ReadInput(
                new Func<string, Tuple<bool, string, DateTime>>(DateConverter),
                new Func<DateTime, Tuple<bool, string>>(DateOfBirthValidator));

            Console.Write("Balance: ");
            record.Balance = ReadInput(
                new Func<string, Tuple<bool, string, decimal>>(DecimalConverter),
                new Func<decimal, Tuple<bool, string>>(BalanceValidator));

            Console.Write("Secure charecter: ");
            record.SecurityCharacter = ReadInput(
                new Func<string, Tuple<bool, string, char>>(CharConverter),
                new Func<char, Tuple<bool, string>>(SecurityCharecterValidator));

            Console.Write("Secure number: ");
            record.SecurityNumber = ReadInput(
                new Func<string, Tuple<bool, string, short>>(ShortConverter),
                new Func<short, Tuple<bool, string>>(SecurityNumberValidator));

            Console.Write("Payment system (Visa/MasterCard): ");
            record.PaymentSystem = ReadInput(
                new Func<string, Tuple<bool, string, string>>(StringConverter),
                new Func<string, Tuple<bool, string>>(PaymentSystemValidator));

            Console.Write("Residence (True/False): ");
            record.Residency = ReadInput(
                new Func<string, Tuple<bool, string, bool>>(BooleanConverter),
                new Func<bool, Tuple<bool, string>>(ResidenceValidator));

            Console.Write("Country code: ");
            record.CountryCode = ReadInput(
                new Func<string, Tuple<bool, string, int>>(IntConverter),
                new Func<int, Tuple<bool, string>>(CountryCodeValidator));

            return record;
        }

        /// <summary>
        /// Reads string, converts to T type and validates converted parameter.
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="converter">Converter delegate method.</param>
        /// <param name="validator">Validator delegate method.</param>
        /// <returns>Converted value.</returns>
        private static T ReadInput<T>(Func<string, Tuple<bool, string, T>> converter, Func<T, Tuple<bool, string>> validator)
        {
            do
            {
                T value;

                var input = Console.ReadLine();
                var conversionResult = converter(input);

                if (!conversionResult.Item1)
                {
                    Console.WriteLine($"Conversion failed: {conversionResult.Item2}. Please, correct your input.");
                    continue;
                }

                value = conversionResult.Item3;

                var validationResult = validator(value);
                if (!validationResult.Item1)
                {
                    Console.WriteLine($"Validation failed: {validationResult.Item2}. Please, correct your input.");
                    continue;
                }

                return value;
            }
            while (true);
        }

        private static Tuple<bool, string, string> StringConverter(string str)
        {
            if (str is null)
            {
                return new Tuple<bool, string, string>(false, "Value is null", string.Empty);
            }

            return new Tuple<bool, string, string>(true, string.Empty, str);
        }

        private static Tuple<bool, string> FirstNameValidator(string firstName)
        {
            if (firstName.Length < 2 || firstName.Length > 60 || firstName.Contains(' '))
            {
                return new Tuple<bool, string>(false, "Invalid first name");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string> LastNameValidator(string lastName)
        {
            if (lastName.Length < 2 || lastName.Length > 60 || lastName.Contains(' '))
            {
                return new Tuple<bool, string>(false, "Invalid last name.");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string> PaymentSystemValidator(string paymentSystem)
        {
            if (!paymentSystem.Equals("Visa", StringComparison.InvariantCultureIgnoreCase) &&
                !paymentSystem.Equals("MasterCard", StringComparison.InvariantCultureIgnoreCase))
            {
                return new Tuple<bool, string>(false, "Invalid payment system.");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string, DateTime> DateConverter(string date)
        {
            string[] dayMonthYear = date.Split('/');
            if (!int.TryParse(dayMonthYear[0], out int day))
            {
                return new Tuple<bool, string, DateTime>(
                    false,
                    "Invalid day.",
                    DateTime.MinValue);
            }

            if (!int.TryParse(dayMonthYear[1], out int month))
            {
                return new Tuple<bool, string, DateTime>(
                    false,
                    "Invalid month.",
                    DateTime.MinValue);
            }

            if (!int.TryParse(dayMonthYear[2], out int year))
            {
                return new Tuple<bool, string, DateTime>(
                    false,
                    "Invalid year.",
                    DateTime.MinValue);
            }

            return new Tuple<bool, string, DateTime>(
                true,
                string.Empty,
                new DateTime(year, month, day));
        }

        private static Tuple<bool, string> DateOfBirthValidator(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.Now || dateOfBirth < new DateTime(1950, 1, 1))
            {
                return new Tuple<bool, string>(false, "Date of birth is out of range.");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string, char> CharConverter(string ch)
        {
            if (ch.Length < 1)
            {
                return new Tuple<bool, string, char>(false, "Security character is empty.", '\0');
            }

            return new Tuple<bool, string, char>(true, string.Empty, ch[0]);
        }

        private static Tuple<bool, string> SecurityCharecterValidator(char securityCharecter)
        {
            if (!char.IsLetter(securityCharecter))
            {
                return new Tuple<bool, string>(false, "Securuty charecter must be a letter.");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string, int> IntConverter(string number)
        {
            if (int.TryParse(number, out int res))
            {
                return new Tuple<bool, string, int>(true, string.Empty, res);
            }

            return new Tuple<bool, string, int>(false, "Invalid number.", -1);
        }

        private static Tuple<bool, string> CountryCodeValidator(int number)
        {
            if (number < 1 || number > 999)
            {
                return new Tuple<bool, string>(false, "Invalid country code.");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string, short> ShortConverter(string number)
        {
            if (short.TryParse(number, out short res))
            {
                return new Tuple<bool, string, short>(true, string.Empty, res);
            }

            return new Tuple<bool, string, short>(false, "Invalid number.", -1);
        }

        private static Tuple<bool, string> SecurityNumberValidator(short number)
        {
            if (number < 0 || number > 9999)
            {
                return new Tuple<bool, string>(false, "Invalid security number.");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string, decimal> DecimalConverter(string dec)
        {
            if (decimal.TryParse(dec, out decimal res))
            {
                return new Tuple<bool, string, decimal>(true, string.Empty, res);
            }
            else
            {
                return new Tuple<bool, string, decimal>(false, "Invalid decimal value.", decimal.MinValue);
            }
        }

        private static Tuple<bool, string> BalanceValidator(decimal balance)
        {
            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string, bool> BooleanConverter(string b)
        {
            if (b.Equals("true", StringComparison.InvariantCultureIgnoreCase))
            {
                return new Tuple<bool, string, bool>(true, string.Empty, true);
            }
            else if (b.Equals("false", StringComparison.InvariantCultureIgnoreCase))
            {
                return new Tuple<bool, string, bool>(true, string.Empty, false);
            }
            else
            {
                return new Tuple<bool, string, bool>(false, "Invalid residence", false);
            }
        }

        private static Tuple<bool, string> ResidenceValidator(bool residence)
        {
            return new Tuple<bool, string>(true, string.Empty);
        }
    }
}
