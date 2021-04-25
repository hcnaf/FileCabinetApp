using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Data input.
    /// </summary>
    public interface IDataInput
    {
        /// <summary>
        /// Data input method.
        /// </summary>
        /// <returns>Converted and validated parameter.</returns>
        FileCabinetRecord DataInput();
    }
}
