﻿using System;
using System.Collections.Generic;
using System.Globalization;

#pragma warning disable CA1062 // Arguments must be already checked.

namespace FileCabinetApp
{
    /// <summary>
    /// Cabinet Service class.
    /// Creating, editing, searching by firstname, lastname, date of birth, returning array of records, number of records.
    /// </summary>
    public abstract class FileCabinetService
    {
        private static readonly CultureInfo Culture = new CultureInfo("ru-RU");

        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();

        /// <summary>
        /// Creates record.
        /// </summary>
        /// <param name="record">Input parameters.</param>
        /// <returns>Id of created record.</returns>
        public int CreateRecord(FileCabinetRecord record)
        {
            this.ValidateParameters(record);
            record.Id = this.list.Count + 1;
            this.list.Add(record);
            try
            {
                this.firstNameDictionary[record.FirstName.ToLower(Culture)].Add(record);
            }
            catch (KeyNotFoundException)
            {
                this.firstNameDictionary.Add(record.FirstName.ToLower(Culture), new List<FileCabinetRecord> { record });
            }

            try
            {
                this.lastNameDictionary[record.LastName.ToLower(Culture)].Add(record);
            }
            catch (KeyNotFoundException)
            {
                this.lastNameDictionary.Add(record.LastName.ToLower(Culture), new List<FileCabinetRecord> { record });
            }

            try
            {
                this.dateOfBirthDictionary[record.DateOfBirth].Add(record);
            }
            catch (KeyNotFoundException)
            {
                this.dateOfBirthDictionary.Add(record.DateOfBirth, new List<FileCabinetRecord> { record });
            }

            return record.Id;
        }

        /// <summary>
        /// Edits record #id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="record">Input parameters.</param>
        public void EditRecord(int id, FileCabinetRecord record)
        {
            this.ValidateParameters(record);
            this.list[id - 1] = record;
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

        /// <summary>
        /// Validates parameters.
        /// </summary>
        /// <param name="record">Parameters to validate.</param>
        protected abstract void ValidateParameters(FileCabinetRecord record);
    }
}
