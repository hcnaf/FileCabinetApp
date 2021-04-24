using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Extracted from FileCabinetService class interface.
    /// </summary>
    public interface IFileCabinetService
    {
        /// <summary>
        /// Creates record.
        /// </summary>
        /// <param name="record">Input parameters.</param>
        /// <returns>Id of created record.</returns>
        int CreateRecord(FileCabinetRecord record);

        /// <summary>
        /// Edits record #id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="record">Input parameters.</param>
        void EditRecord(int id, FileCabinetRecord record);

        /// <summary>
        /// Finds records by first name.
        /// </summary>
        /// <param name="firstName">First name to find.</param>
        /// <returns>Array of found records.</returns>
        ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName);

        /// <summary>
        /// Finds records by last name.
        /// </summary>
        /// <param name="lastName">Last name to find.</param>
        /// <returns>Array of found records.</returns>
        ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName);

        /// <summary>
        /// Finds records by date of birth.
        /// </summary>
        /// <param name="dateOfBirth">Date of birth to find.</param>
        /// <returns>Array of found records.</returns>
        ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(DateTime dateOfBirth);

        /// <summary>
        /// Returns all records.
        /// </summary>
        /// <returns>Array of all records.</returns>
        ReadOnlyCollection<FileCabinetRecord> GetRecords();

        /// <summary>
        /// Returns number of records.
        /// </summary>
        /// <returns>number of records.</returns>
        int GetStat();
    }
}
