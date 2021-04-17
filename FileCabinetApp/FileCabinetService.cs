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

        public void EditRecord(int id, string firstName, string lastName, DateTime dateOfBirth, decimal balance, char securityCharecter, short securityNumber)
        {
            if (id < 1 || id > this.list.Count)
            {
                throw new ArgumentException("No such id.");
            }

            this.list[id - 1].FirstName = firstName;
            this.list[id - 1].LastName = lastName;
            this.list[id - 1].DateOfBirth = dateOfBirth;
            this.list[id - 1].Balance = balance;
            this.list[id - 1].SecurityCharecter = securityCharecter;
            this.list[id - 1].SecurityNumber = securityNumber;
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
