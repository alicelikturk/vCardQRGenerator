using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace vCardQRGenerator.Infrastructure.Service
{
	public class CSVService : ICSVService
    {
        public IEnumerable<T> ReadCSV<T>(Stream file)
        {
            var reader = new StreamReader(file);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null
            };
            var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<T>();
            return records;
        }
    }
}

