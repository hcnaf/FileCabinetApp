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
                this.firstNameDictionary[firstName].Add(record);
            }
            catch (KeyNotFoundException)
            {
                this.firstNameDictionary.Add(firstName.ToLower(), new List<FileCabinetRecord> { record });
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
            List<FileCabinetRecord> res = new List<FileCabinetRecord>();
            foreach (FileCabinetRecord record in this.list)
            {
                if (record.LastName.Equals(lastName, StringComparison.InvariantCultureIgnoreCase))
                {
                    res.Add(record);
                }
            }

            return res.ToArray();
        }

        public FileCabinetRecord[] FindByDateOfBirth(DateTime dateOfBirth)
        {
            List<FileCabinetRecord> res = new List<FileCabinetRecord>();
            foreach (FileCabinetRecord record in this.list)
            {
                if (record.DateOfBirth == dateOfBirth)
                {
                    res.Add(record);
                }
            }

            return res.ToArray();
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
