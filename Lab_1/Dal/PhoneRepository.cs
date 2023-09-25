using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Lab_1.Models;
using Microsoft.Extensions.Configuration;

namespace Lab_1.Dal
{
	public class PhoneRepository
    {
        private string dataFilePath;
        private readonly CsvConfiguration _csvConfiguration;

        public PhoneRepository(IConfiguration configuration)
        {
            dataFilePath = configuration.GetSection("LastFilePath").Value;
            _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
        }

        public List<PhoneModel> GetAll()
        {

            using StreamReader reader = new StreamReader(dataFilePath);
            using CsvReader csvReader = new CsvReader(reader, _csvConfiguration);
            csvReader.Read();
            return csvReader.GetRecords<PhoneModel>().ToList();
        }
    }
}
