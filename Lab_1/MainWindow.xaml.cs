using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab_1.Dal;
using Lab_1.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Path = System.IO.Path;

namespace Lab_1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly IConfiguration _configuration;
		private readonly IServiceProvider _serviceProvider;
		public MainWindow()
		{
			InitializeComponent();
			_configuration = new ConfigurationManager()
				.AddJsonFile("appsettings.json", false, true)
				.Build();

			IServiceCollection services = new ServiceCollection();

			services.AddSingleton(_configuration);
			services.AddSingleton<PhoneRepository>();

			_serviceProvider = services.BuildServiceProvider();
		}


		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			var tmpPath = _configuration.GetSection("LastFilePath").Value;
			if (!File.Exists(tmpPath))
			{
				var pathToFile = Path.Combine(Directory.GetCurrentDirectory(), "phoneData.csv");
				File.Create(pathToFile).Close();
				_configuration.GetRequiredSection("LastFilePath").Value = pathToFile;
				var tmpParams = _configuration.GetSection("LastFilePath");
				File.WriteAllText("appsettings.json", JsonConvert.SerializeObject(new { LastFilePath = tmpParams.Value}, Formatting.Indented));
			}

			DataTable.ItemsSource = _serviceProvider.GetRequiredService<PhoneRepository>().GetAll();
		}

		private void ChangeButton_OnClick(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void AddButton_OnClick(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void MenuItem_OnClick(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
