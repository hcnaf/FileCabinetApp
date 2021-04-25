using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Record validator interface.
    /// </summary>
    public interface IRecordValidator
    {
        /// <summary>
        /// Parameters validation.
        /// </summary>
        /// <param name="record">Parameters to validate.</param>
        public void ValidateParameters(FileCabinetRecord record);
    }
}
