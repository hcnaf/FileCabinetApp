using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    public class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

        public int CreateRecord(string firstName, string lastName, DateTime dateOfBirth, decimal balance, char securityCharecter, short securityNumber)
        {
            if (firstName is null)
            {
                throw new ArgumentNullException(nameof(firstName));
            }

            if (firstName.Length < 2 || firstName.Length > 60 || !firstName.Contains(' '))
            {
                throw new ArgumentException("Invalid first name.");
            }

            if (lastName is null)
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            if (lastName.Length < 2 || lastName.Length > 60 || !lastName.Contains(' '))
            {
                throw new ArgumentException("Invalid last name.");
            }

            if (dateOfBirth > DateTime.Now || dateOfBirth < new DateTime(1950, 1, 1))
            {
                throw new ArgumentOutOfRangeException(nameof(dateOfBirth));
            }

            if (securityCharecter == '\0')
            {
                throw new ArgumentException("Security charecter is empty.");
            }

            var record = new FileCabinetRecord
            {
                Id = this.list.Count + 1,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Balance = balance,
                SecurityCharecter = securityCharecter,
                SecurityNumber = securityNumber,
            };

            this.list.Add(record);

            return record.Id;
        }

        public FileCabinetRecord[] GetRecords()
        {
            return this.list.ToArray();
        }

        public int GetStat()
        {
            return this.list.Count;
        }
    }
}
