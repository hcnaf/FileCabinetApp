using System;
using System.Collections.Generic;
using System.Globalization;

#pragma warning disable CA1062 // Arguments must be already checked.

namespace FileCabinetApp
{
    /// <summary>
    /// Cabinet Service class.
    /// Creating, editing, searching by firstname, lastname, date of birth, returning array of records, number of records.
    /// </summary>
    public class FileCabinetService
    {
        private static readonly CultureInfo Culture = new CultureInfo("ru-RU");

        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();

        /// <summary>
        /// Creates record.
        /// </summary>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="dateOfBirth">Date of birth.</param>
        /// <param name="balance">Balance.</param>
        /// <param name="securityCharecter">Security character.</param>
        /// <param name="securityNumber">Security number.</param>
        /// <returns>Id of created record.</returns>
        public int CreateRecord(string firstName, string lastName, DateTime dateOfBirth, decimal balance, char securityCharecter, short securityNumber)
        {
            var record = new FileCabinetRecord
            {
                Id = this.list.Count + 1,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Balance = balance,
                SecurityCharacter = securityCharecter,
                SecurityNumber = securityNumber,
            };

            this.list.Add(record);
            try
            {
                this.firstNameDictionary[firstName.ToLower(Culture)].Add(record);
            }
            catch (KeyNotFoundException)
            {
                this.firstNameDictionary.Add(firstName.ToLower(Culture), new List<FileCabinetRecord> { record });
            }

            try
            {
                this.lastNameDictionary[lastName.ToLower(Culture)].Add(record);
            }
            catch (KeyNotFoundException)
            {
                this.lastNameDictionary.Add(lastName.ToLower(Culture), new List<FileCabinetRecord> { record });
            }

            try
            {
                this.dateOfBirthDictionary[dateOfBirth].Add(record);
            }
            catch (KeyNotFoundException)
            {
                this.dateOfBirthDictionary.Add(dateOfBirth, new List<FileCabinetRecord> { record });
            }

            return record.Id;
        }

        /// <summary>
        /// Edits record #id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="firstName">New first name.</param>
        /// <param name="lastName">New last name.</param>
        /// <param name="dateOfBirth">New date of birth.</param>
        /// <param name="balance">New balance.</param>
        /// <param name="securityCharecter">New security character.</param>
        /// <param name="securityNumber">New security number.</param>
        public void EditRecord(int id, string firstName, string lastName, DateTime dateOfBirth, decimal balance, char securityCharecter, short securityNumber)
        {
            this.list[id - 1].FirstName = firstName;
            this.list[id - 1].LastName = lastName;
            this.list[id - 1].DateOfBirth = dateOfBirth;
            this.list[id - 1].Balance = balance;
            this.list[id - 1].SecurityCharacter = securityCharecter;
            this.list[id - 1].SecurityNumber = securityNumber;
        }

        /// <summary>
        /// Finds records by first name.
        /// </summary>
        /// <param name="firstName">First name to find.</param>
        /// <returns>Array of found records.</returns>
        public FileCabinetRecord[] FindByFirstName(string firstName)
        {
            return this.firstNameDictionary[firstName].ToArray();
        }

        /// <summary>
        /// Finds records by last name.
        /// </summary>
        /// <param name="lastName">Last name to find.</param>
        /// <returns>Array of found records.</returns>
        public FileCabinetRecord[] FindByLastName(string lastName)
        {
            return this.lastNameDictionary[lastName].ToArray();
        }

        /// <summary>
        /// Finds records by date of birth.
        /// </summary>
        /// <param name="dateOfBirth">Date of birth to find.</param>
        /// <returns>Array of found records.</returns>
        public FileCabinetRecord[] FindByDateOfBirth(DateTime dateOfBirth)
        {
            return this.dateOfBirthDictionary[dateOfBirth].ToArray();
        }

        /// <summary>
        /// Returns all records.
        /// </summary>
        /// <returns>Array of all records.</returns>
        public FileCabinetRecord[] GetRecords()
        {
            return this.list.ToArray();
        }

        /// <summary>
        /// Returns number of records.
        /// </summary>
        /// <returns>number of records.</returns>
        public int GetStat()
        {
            return this.list.Count;
        }
    }
}
