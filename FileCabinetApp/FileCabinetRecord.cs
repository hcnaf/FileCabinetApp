using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    public class FileCabinetRecord
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal Balance { get; set; }

        public char SecurityCharecter { get; set; }

        public short SecurityNumber { get; set; }
    }
}
