using System;
using System.Collections.Generic;
using System.IO;
using System.Printing;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ForexPOS
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Button _selectedExchangeButton;
		private string _selectedCurrency;
		private string _selectedRateType;
		private double _selectedRate;
		private string _inputValue = "0"; // Default to "0" for initial display

		private readonly Dictionary<string, double> _customRates = new()
	   {
		  { "EurBid", 97.13 }, { "EurAsk", 97.83 },
		  { "UsdBid", 89.50 }, { "UsdAsk", 90.10 },
		  { "ChfBid", 101.20 }, { "ChfAsk", 102.00 },
		  { "GbpBid", 113.40 }, { "GbpAsk", 114.25 }
	   };


		private SettingsModel _settings = new SettingsModel();

		public MainWindow()
		{
			this.InitializeComponent();
			this.LoadSettings();
		}

		private void NumberButton_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			if (button == null) return;

			string value = button.Content.ToString();

			if (value == ".")
			{
				if (!this._inputValue.Contains("."))
					this._inputValue += ".";
			}
			else
			{
				if (this._inputValue == "0")
					this._inputValue = value;
				else
					this._inputValue += value;
			}

			this.UpdateDisplays();
		}

		private void ClearButton_Click(object sender, RoutedEventArgs e)
		{
			this._inputValue = "0";
			this.UpdateDisplays();
		}

		private void ExchangeButton_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			if (button == null) return;

			// Highlight selected button
			if (this._selectedExchangeButton != null)
				this._selectedExchangeButton.ClearValue(Button.BackgroundProperty);

			button.Background = Brushes.Orange;
			this._selectedExchangeButton = button;

			// Parse tag: e.g. "EUR_Bid"
			string tag = button.Tag as string;
			if (string.IsNullOrEmpty(tag)) return;

			var parts = tag.Split('_');
			if (parts.Length != 2) return;

			// Resource keys in XAML are PascalCase: EurBid, UsdAsk, etc.
			this._selectedCurrency = parts[0].Substring(0, 1).ToUpper() + parts[0].Substring(1).ToLower();
			this._selectedRateType = parts[1].Substring(0, 1).ToUpper() + parts[1].Substring(1).ToLower();

			string rateKey = $"{this._selectedCurrency}{this._selectedRateType}";
			if (!this._customRates.TryGetValue(rateKey, out this._selectedRate))
			{
				MessageBox.Show($"Custom rate not found: {rateKey}");
				this._selectedRate = 0;
				this.UpdateDisplays();
				return;
			}

			// Custom rate found
			this.UpdateDisplays();
		}

		private void UpdateDisplays()
		{
			if (string.IsNullOrEmpty(this._selectedCurrency) || string.IsNullOrEmpty(this._selectedRateType) || this._selectedRate == 0)
			{
				this.TextBoxSource.Text = "";
				this.TextBoxTarget.Text = "";
				return;
			}

			if (this._selectedRateType == "Bid")
			{
				this.TextBoxSource.Text = $"{this._inputValue} {this._selectedCurrency}";
				if (double.TryParse(this._inputValue, out double amount))
					this.TextBoxTarget.Text = $"{(amount * this._selectedRate):F2} ALL";
				else
					this.TextBoxTarget.Text = $"0.00 ALL";
			}
			else // Ask
			{
				this.TextBoxSource.Text = $"{this._inputValue} ALL";
				if (double.TryParse(this._inputValue, out double amount))
					this.TextBoxTarget.Text = $"{(amount / this._selectedRate):F4} {this._selectedCurrency}";
				else
					this.TextBoxTarget.Text = $"0.0000 {this._selectedCurrency}";
			}
		}

		private void PrintButton_Click(object sender, RoutedEventArgs e)
		{

			


			const int receiptWidth = 203; // or 384 for most 58mm printers

			var header = new TextBlock
			{
				Text =
					$"\nForexPOS\n" +
					$"\n{DateTime.Now:dd.MM.yyyy HH:mm}\n" +
					$"\n----------------\n",
				FontFamily = new FontFamily("Consolas"),
				FontSize = 10,
			};




			// Parse and format values
			double fromValue = 0, toValue = 0;
			string fromCurrency = "", toCurrency = "";
			if (this._selectedRateType == "Bid")
			{
				double.TryParse(this._inputValue, out fromValue);
				toValue = Math.Ceiling(fromValue * this._selectedRate * 100) / 100;
				fromCurrency = this._selectedCurrency;
				toCurrency = "ALL";
			}
			else
			{
				double.TryParse(this._inputValue, out fromValue);
				toValue = Math.Ceiling((fromValue / this._selectedRate) * 100) / 100;
				fromCurrency = "ALL";
				toCurrency = this._selectedCurrency;
			}

			double rateValue = (this._selectedRateType == "Bid") ? this._selectedRate : 1.0 / this._selectedRate;
			rateValue = Math.Ceiling(rateValue * 100) / 100;




			var receiptControl = new Receipt();
			var receiptData = receiptControl.Source;
			receiptData.Date = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
			receiptData.Source = $"{fromValue:F2} {fromCurrency}";
			receiptData.Target = $"{toValue:F2} {toCurrency}";
			receiptData.Change = rateValue.ToString("F2");

			receiptControl.Measure(new Size(receiptData.Width, double.PositiveInfinity));
			receiptControl.Arrange(new Rect(0, 0, receiptData.Width, receiptControl.DesiredSize.Height));

			// Print directly to the configured printer
			LocalPrintServer printServer = new LocalPrintServer();
			PrintQueue printQueue = printServer.GetPrintQueue(_settings.Printer);
			PrintTicket printTicket = printQueue.DefaultPrintTicket;
			PrintDialog pd = new PrintDialog();
			pd.PrintQueue = printQueue;
			pd.PrintTicket = printTicket;
			pd.PrintVisual(receiptControl, "Forex Receipt");

			this.ClearButton_Click(sender, e);
		}

		private void SetRatesButton_Click(object sender, RoutedEventArgs e)
		{
			var dlg = new Settings(_settings);
			if (dlg.ShowDialog() == true)
			{
				this.LoadSettings();
				this.UpdateDisplays();
			}
		}


		private void LoadSettings()
		{
			this._settings = SettingsModel.Load();

			if (this.TxtEurBidRate != null) this.TxtEurBidRate.Text = $"{this._settings.EurBid:F2} ALL";
			if (this.TxtEurAskRate != null) this.TxtEurAskRate.Text = $"{this._settings.EurAsk:F2} ALL";
			if (this.TxtUsdBidRate != null) this.TxtUsdBidRate.Text = $"{this._settings.UsdBid:F2} ALL";
			if (this.TxtUsdAskRate != null) this.TxtUsdAskRate.Text = $"{this._settings.UsdAsk:F2} ALL";
			if (this.TxtChfBidRate != null) this.TxtChfBidRate.Text = $"{this._settings.ChfBid:F2} ALL";
			if (this.TxtChfAskRate != null) this.TxtChfAskRate.Text = $"{this._settings.ChfAsk:F2} ALL";
			if (this.TxtGbpBidRate != null) this.TxtGbpBidRate.Text = $"{this._settings.GbpBid:F2} ALL";
			if (this.TxtGbpAskRate != null) this.TxtGbpAskRate.Text = $"{this._settings.GbpAsk:F2} ALL";
		}
	}
}