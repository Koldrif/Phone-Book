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
using System.Windows.Shapes;
using Lab_1.Dal;
using Lab_1.Models;

namespace Lab_1
{
	/// <summary>
	/// Логика взаимодействия для PhoneEntry.xaml
	/// </summary>
	public partial class PhoneEntry : Window
	{
		private PhoneRepository _phoneRepository;

		public PhoneEntry(PhoneRepository phoneRepository)
		{
			InitializeComponent();
			_phoneRepository = phoneRepository;
		}

		private void OnCancelClicked(object sender, RoutedEventArgs e)
		{

			this.Close();
        }

		private void OnSaveClicked(object sender, RoutedEventArgs e)
		{
			_phoneRepository.Add(new PhoneModel()
			{
				PersonName = personName.Text.Trim(),
				PhoneNumber = phoneText.Text.Trim()
			});
			this.Close();
		}
	}
}
