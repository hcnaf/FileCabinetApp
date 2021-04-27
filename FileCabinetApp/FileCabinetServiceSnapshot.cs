using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;

namespace FileCabinetApp
{
    /// <summary>
    /// Memento for FileCabinetService.
    /// </summary>
    public class FileCabinetServiceSnapshot
    {
        private readonly CultureInfo culture = new CultureInfo("ru-RU");

        private readonly FileCabinetRecord[] records;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetServiceSnapshot"/> class.
        /// Constuctor.
        /// </summary>
        /// <param name="records">Records to save.</param>
        public FileCabinetServiceSnapshot(FileCabinetRecord[] records)
        {
            this.records = records;
        }

        /// <summary>
        /// Save to csv file method.
        /// </summary>
        /// <param name="filePath">File path.</param>
        public void SaveToCsv(string filePath)
        {
            using StreamWriter streamWriter = new StreamWriter(filePath);
            using CsvWriter csvWriter = new CsvWriter(streamWriter, this.culture, true);
            csvWriter.WriteRecords(this.records);
            Console.WriteLine($"Snapshot was made and saved like csv-file.");
        }

        /// <summary>
        /// Save to xml file method.
        /// </summary>
        /// <param name="filePath">File path.</param>
        public void SaveToXml(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
