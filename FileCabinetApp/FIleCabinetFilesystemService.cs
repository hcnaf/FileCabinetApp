using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// File system mode class.
    /// </summary>
    public class FileCabinetFilesystemService : IFileCabinetService
    {
        private readonly IRecordValidator validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetFilesystemService"/> class.
        /// </summary>
        /// <param name="validator">Default or custom validator.</param>
        public FileCabinetFilesystemService(IRecordValidator validator)
        {
            this.validator = validator;
        }

        /// <summary>
        /// Adds record to file.
        /// </summary>
        /// <param name="record">Record to add.</param>
        /// <returns>Record identificator.</returns>
        public int CreateRecord(FileCabinetRecord record)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Edits record in a file.
        /// </summary>
        /// <param name="id">File identificator.</param>
        /// <param name="record">New record.</param>
        public void EditRecord(int id, FileCabinetRecord record)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find record in a file by date of birth.
        /// </summary>
        /// <param name="dateOfBirth">Date of birth.</param>
        /// <returns>Founded record.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByDateOfBirth(DateTime dateOfBirth)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds record in a file by first name.
        /// </summary>
        /// <param name="firstName">First name to find.</param>
        /// <returns>Founded record.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByFirstName(string firstName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds record in a file by last name.
        /// </summary>
        /// <param name="lastName">Last name to find.</param>
        /// <returns>Founded record.</returns>
        public ReadOnlyCollection<FileCabinetRecord> FindByLastName(string lastName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all record in a file.
        /// </summary>
        /// <returns>All records in a file.</returns>
        public ReadOnlyCollection<FileCabinetRecord> GetRecords()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns number of records in a file.
        /// </summary>
        /// <returns>Number of records.</returns>
        public int GetStat()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Может даже и не нужно. Удали как-нибудь потом из интерфейса.
        /// </summary>
        /// <param name="fileExtention">File extention.</param>
        /// <param name="filePath">File path.</param>
        public void MakeSnapshot(string fileExtention, string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
