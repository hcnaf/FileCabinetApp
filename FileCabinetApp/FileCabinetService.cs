using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CA1304

namespace FileCabinetApp
{
    public class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();
        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();
        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();

        public int CreateRecord(string firstName, string lastName, DateTime dateOfBirth, decimal balance, char securityCharecter, short securityNumber)
        {
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
            try
            {
                this.firstNameDictionary[firstName.ToLower()].Add(record);
            }
            catch (KeyNotFoundException)
            {
                this.firstNameDictionary.Add(firstName.ToLower(), new List<FileCabinetRecord> { record });
            }

            try
            {
                this.lastNameDictionary[lastName.ToLower()].Add(record);
            }
            catch (KeyNotFoundException)
            {
                this.lastNameDictionary.Add(lastName.ToLower(), new List<FileCabinetRecord> { record });
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

        public void EditRecord(int id, string firstName, string lastName, DateTime dateOfBirth, decimal balance, char securityCharecter, short securityNumber)
        {
            this.list[id - 1].FirstName = firstName;
            this.list[id - 1].LastName = lastName;
            this.list[id - 1].DateOfBirth = dateOfBirth;
            this.list[id - 1].Balance = balance;
            this.list[id - 1].SecurityCharecter = securityCharecter;
            this.list[id - 1].SecurityNumber = securityNumber;
        }

        public FileCabinetRecord[] FindByFirstName(string firstName)
        {
            return this.firstNameDictionary[firstName].ToArray();
        }

        public FileCabinetRecord[] FindByLastName(string lastName)
        {
            return this.lastNameDictionary[lastName].ToArray();
        }

        public FileCabinetRecord[] FindByDateOfBirth(DateTime dateOfBirth)
        {
            return this.dateOfBirthDictionary[dateOfBirth].ToArray();
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
