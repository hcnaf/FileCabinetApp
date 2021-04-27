using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

#pragma warning disable CA1062 // Arguments must be already checked.

namespace FileCabinetApp
{
    /// <summary>
    /// Cabinet Service class.
    /// Creating, editing, searching by firstname, lastname, date of birth, returning array of records, number of records.
    /// </summary>
    public class FileCabinetService : IFileCabinetService
    {
        private static readonly CultureInfo Culture = new CultureInfo("ru-RU");

        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();

        private readonly IRecordValidator validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetService"/> class.
        /// </summary>
        /// <param name="validator">Default or custom validator.</param>
        public FileCabinetService(IRecordValidator validator)
        {
            this.validator = validator;
        }

        /// <summary>
        /// Creates record.
        /// </summary>
        /// <param name="record">Input parameters.</param>
        /// <returns>Id of created record.</returns>
        public int CreateRecord(FileCabinetRecord record)
        {
            this.validator.ValidateParameters(record);
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
            this.validator.ValidateParameters(record);
            this.list[id - 1] = record;
        }

        /// <summary>
        /// Finds records by first name.
        /// </summary>
        /// <param name="firstName">First name to find.</param>
        /// <returns>Array of found records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            return new ReadOnlyCollection<FileCabinetRecord>(this.firstNameDictionary[firstName]);
        }

        /// <summary>
        /// Finds records by last name.
        /// </summary>
        /// <param name="lastName">Last name to find.</param>
        /// <returns>Array of found records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            return new ReadOnlyCollection<FileCabinetRecord>(this.lastNameDictionary[lastName]);
        }

        /// <summary>
        /// Finds records by date of birth.
        /// </summary>
        /// <param name="dateOfBirth">Date of birth to find.</param>
        /// <returns>Array of found records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(DateTime dateOfBirth)
        {
            return new ReadOnlyCollection<FileCabinetRecord>(this.dateOfBirthDictionary[dateOfBirth].ToArray());
        }

        /// <summary>
        /// Returns all records.
        /// </summary>
        /// <returns>Array of all records.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            return new ReadOnlyCollection<FileCabinetRecord>(this.list.ToArray());
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
        /// Creates snapshot.
        /// </summary>
        /// <param name="fileExtention">File extention.</param>
        /// <param name="filePath">File path.</param>
        public void MakeSnapshot(string fileExtention, string filePath)
        {
            var snapshot = new FileCabinetServiceSnapshot(this.list.ToArray());
            if (fileExtention.Equals("csv", StringComparison.InvariantCultureIgnoreCase))
            {
                snapshot.SaveToCsv(filePath);
            }
            else if (fileExtention.Equals("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                snapshot.SaveToXml(filePath);
            }
            else
            {
                Console.WriteLine("Invalid file extention.");
            }
        }
    }
}
