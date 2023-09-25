using System;
using System.Collections.Generic;
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
using Lab_1.Models;
using Microsoft.Extensions.Configuration;

namespace Lab_1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly IConfiguration _configuration;
		public MainWindow()
		{
			InitializeComponent();
			_configuration = new ConfigurationManager().AddJsonFile("")
		}


		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			var tmp = new List<PhoneModel>()
			{
				new PhoneModel()
				{
					PersonName = "Igor Vorobyev",
					PhoneNumber = "+79057461868"
				}
			};
			DataTable.ItemsSource = tmp;
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
