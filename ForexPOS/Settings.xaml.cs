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

namespace ForexPOS
{
	/// <summary>
	/// Interaction logic for SetRatesWindows.xaml
	/// </summary>
	public partial class Settings : Window
	{
		private SettingsModel _settings;

		public Settings(SettingsModel settings)
		{
			this.InitializeComponent();
			this._settings = settings;

			this.EurBidBox.Text = this._settings.Rates.GetValueOrDefault("EurBid", 0).ToString();
			this.EurAskBox.Text = this._settings.Rates.GetValueOrDefault("EurAsk", 0).ToString();
			this.UsdBidBox.Text = this._settings.Rates.GetValueOrDefault("UsdBid", 0).ToString();
			this.UsdAskBox.Text = this._settings.Rates.GetValueOrDefault("UsdAsk", 0).ToString();
			this.ChfBidBox.Text = this._settings.Rates.GetValueOrDefault("ChfBid", 0).ToString();
			this.ChfAskBox.Text = this._settings.Rates.GetValueOrDefault("ChfAsk", 0).ToString();
			this.GbpBidBox.Text = this._settings.Rates.GetValueOrDefault("GbpBid", 0).ToString();
			this.GbpAskBox.Text = this._settings.Rates.GetValueOrDefault("GbpAsk", 0).ToString();

			// Populate printers
			var printServer = new LocalPrintServer();
			foreach (var queue in printServer.GetPrintQueues())
				this.PrinterComboBox.Items.Add(queue.Name);

			// Select current printer if available
			if (!string.IsNullOrEmpty(this._settings.Printer) && this.PrinterComboBox.Items.Contains(this._settings.Printer))
				this.PrinterComboBox.SelectedItem = this._settings.Printer;
			else if (this.PrinterComboBox.Items.Count > 0)
				this.PrinterComboBox.SelectedIndex = 0;
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			double.TryParse(this.EurBidBox.Text, out double eurBid);
			double.TryParse(this.EurAskBox.Text, out double eurAsk);
			double.TryParse(this.UsdBidBox.Text, out double usdBid);
			double.TryParse(this.UsdAskBox.Text, out double usdAsk);
			double.TryParse(this.ChfBidBox.Text, out double chfBid);
			double.TryParse(this.ChfAskBox.Text, out double chfAsk);
			double.TryParse(this.GbpBidBox.Text, out double gbpBid);
			double.TryParse(this.GbpAskBox.Text, out double gbpAsk);

			this._settings.Rates["EurBid"] = eurBid;
			this._settings.Rates["EurAsk"] = eurAsk;
			this._settings.Rates["UsdBid"] = usdBid;
			this._settings.Rates["UsdAsk"] = usdAsk;
			this._settings.Rates["ChfBid"] = chfBid;
			this._settings.Rates["ChfAsk"] = chfAsk;
			this._settings.Rates["GbpBid"] = gbpBid;
			this._settings.Rates["GbpAsk"] = gbpAsk;
			this._settings.Printer = this.PrinterComboBox.SelectedItem?.ToString();

			SettingsModel.Save(this._settings);

			this.DialogResult = true;
			this.Close();
		}
	}
}
