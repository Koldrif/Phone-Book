using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
	public class AppSettings
	{
		public string LastFilePath { get; set; }


		public void UpdateConfigurationFile()
		{
			File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), JsonConvert.SerializeObject(this));
		}
	}
}
