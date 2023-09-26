using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Formats.Asn1;
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
        private AppSettings _appSettings;
        private readonly CsvConfiguration _csvConfiguration;

        private ObservableCollection<PhoneModel> _phones;

        public PhoneRepository(AppSettings configuration)
        {
			_appSettings = configuration;
            _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };
            UpdateRepository();
            _phones.CollectionChanged += (sender, args) => Trace.WriteLine("Collection changed");

		}

		public ObservableCollection<PhoneModel> GetAll()
        {
	        return _phones;
        }

        public void Add(PhoneModel phoneModel)
        {
            _phones.Add(phoneModel);
			WriteDataToFile();

        }

        public void WriteDataToFile()
        {
	        using var writer = getCsvWriter();
	        writer.WriteRecords(_phones);
		}

		private CsvWriter getCsvWriter()
        {
	        StreamWriter reader = new StreamWriter(_appSettings.LastFilePath, new FileStreamOptions()
	        {
                Access = FileAccess.Write,
                Mode = FileMode.Open
	        });
	        CsvWriter csvWriter = new CsvWriter(reader, _csvConfiguration);
	        return csvWriter;
        }

		private CsvReader getCsvReader()
        {
	        StreamReader reader = new StreamReader(_appSettings.LastFilePath);
	        CsvReader csvReader = new CsvReader(reader, _csvConfiguration);
	        return csvReader;
        }

		public void ChangeDataList(List<PhoneModel> newModel)
		{
			_phones.Clear();
			foreach (var record in newModel)
			{
				_phones.Add(record);
			}
			WriteDataToFile();
		}

		public void UpdateRepository()
		{
			using var csvReader = getCsvReader();
			if (_phones is null)
			{
				_phones = new ObservableCollection<PhoneModel>();
			}
			else
			{
				_phones.Clear();
			}

			foreach (var record in csvReader.GetRecords<PhoneModel>())
			{
				_phones.Add(record);	
			}

		}


    }
}
