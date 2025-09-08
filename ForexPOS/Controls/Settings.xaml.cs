using ForexPOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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

namespace ForexPOS.Controls
{
	/// <summary>
	/// Interaction logic for SetRatesWindows.xaml
	/// </summary>
	public partial class Settings : Window
	{
		public Settings()
		{
			this.InitializeComponent();

			// Populate printers
			var printServer = new LocalPrintServer();
			foreach (var queue in printServer.GetPrintQueues())
				this.PrinterComboBox.Items.Add(queue.Name);

			// Select current printer if available
			if (!string.IsNullOrEmpty(this.Source.PrinterName) && this.PrinterComboBox.Items.Contains(this.Source.PrinterName))
				this.PrinterComboBox.SelectedItem = this.Source.PrinterName;
			else if (this.PrinterComboBox.Items.Count > 0)
				this.PrinterComboBox.SelectedIndex = 0;
		}

		public SettingsModel Source { get => this.DataContext as SettingsModel; set => this.DataContext = value; }

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			SettingsModel.Save(this.Source);

			this.DialogResult = true;
			this.Close();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}
	}

	public class ComparisonConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value?.Equals(parameter);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value?.Equals(true) == true ? parameter : Binding.DoNothing;
		}
	}
}
