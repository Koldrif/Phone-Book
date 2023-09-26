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
using Microsoft.Win32;
using Newtonsoft.Json;
using Path = System.IO.Path;

namespace Lab_1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//private readonly IConfiguration _configuration;
		private readonly IServiceProvider _serviceProvider;
		private PhoneRepository _phoneRepository;
		private readonly AppSettings _appSettings;
		public MainWindow()
		{
			InitializeComponent();
			//_configuration = new ConfigurationManager()
			//	.AddJsonFile("appsettings.json", false, true)
			//	.Build();

			IServiceCollection services = new ServiceCollection();

			var currentDirectory = Directory.GetCurrentDirectory();
			var filePath = Path.Combine(currentDirectory, "appsettings.json");
			_appSettings = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(filePath));
			if (_appSettings == null)
			{
				throw new FileLoadException("Unable to read appsetting.json");
			}
			services.AddSingleton(_appSettings);

			services.AddSingleton<PhoneRepository>();
			services.AddTransient<PhoneEntry>();


			_serviceProvider = services.BuildServiceProvider();

		}


		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(_appSettings.LastFilePath))
			{
				_appSettings.LastFilePath = Path.Combine(Directory.GetCurrentDirectory(), "phoneData.csv");
				_appSettings.UpdateConfigurationFile();
			}
			if (!File.Exists(_appSettings.LastFilePath))
			{
				File.Create(_appSettings.LastFilePath).Close();
			}
			_phoneRepository = _serviceProvider.GetRequiredService<PhoneRepository>();

			DataTable.ItemsSource = _phoneRepository.GetAll();


		}

		private void ChangeButton_OnClick(object sender, RoutedEventArgs e)
		{
			int columnIndex = DataTable.SelectedCells[0].Column.DisplayIndex;
			int rowIndex = DataTable.Items.IndexOf(DataTable.SelectedCells[0].Item);

			
		}

		private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void AddButton_OnClick(object sender, RoutedEventArgs e)
		{
			_serviceProvider.GetRequiredService<PhoneEntry>().Show();
		}

		private void MenuItem_OnClick(object sender, RoutedEventArgs e)
		{
			/*
			 * 1. При нажатии на кнопку открывается окно выбора директории
			 * 2. Если нажимаем Cancel, то выходим из функции
			 * 3. Если выбрали файл, то
			 *		3.1 обновляем appsettings,
			 *		3.2 Меняем директорию в phoneRepos
			 *		3.3 Обновляем таблицу
			 */

			var fileDialog = new OpenFileDialog();
			var result = fileDialog.ShowDialog();
			if (result == true)
			{
				var text = fileDialog.FileName;
				_appSettings.LastFilePath = text;
				_appSettings.UpdateConfigurationFile();
			}
		}

		public void UpdateFunction()
		{
			DataTable.ItemsSource = _phoneRepository.GetAll();
		}

		private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
		{
			UpdateFunction();
		}


	}
}
