using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Data template.
    /// </summary>
    public class FileCabinetRecord
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        /// <value>
        /// Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets firstname.
        /// </summary>
        /// <value>
        /// Client's first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets client's last name.
        /// </summary>
        /// <value>
        /// Client's last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets client's date of birth.
        /// </summary>
        /// <value>
        /// Client's date of birth.
        /// </value>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets client's balance.
        /// </summary>
        /// <value>
        /// Client's balance.
        /// </value>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets client's security character.
        /// </summary>
        /// <value>
        /// Client's security character.
        /// </value>
        public char SecurityCharacter { get; set; }

        /// <summary>
        /// Gets or sets client's security number.
        /// </summary>
        /// <value>
        /// Client's security number.
        /// </value>
        public short SecurityNumber { get; set; }

        /// <summary>
        /// Gets or sets client's payment system.
        /// </summary>
        /// <value>
        /// Client's payment system(Visa/MasterCard).
        /// </value>
        public string PaymentSystem { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether client is resident or not.
        /// </summary>
        /// <value>
        /// Client's residency.
        /// </value>
        public bool Residency { get; set; }

        /// <summary>
        /// Gets or sets country code.
        /// </summary>
        /// <value>
        /// Country code.
        /// </value>
        public int CountryCode { get; set; }
    }
}
