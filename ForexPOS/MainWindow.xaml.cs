using ForexPOS.Controls;
using ForexPOS.Models;
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



		public MainWindow()
		{
			this.InitializeComponent();
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


			var spread = _selectedCurrency.ToUpper() switch
			{
				"EUR" => (this.DataContext as PosModel)?.Settings.EUR,
				"USD" => (this.DataContext as PosModel)?.Settings.USD,
				"GBP" => (this.DataContext as PosModel)?.Settings.GBP,
				"CHF" => (this.DataContext as PosModel)?.Settings.CHF,
				_ => null,
			};

			if(spread != null)
			{
				this._selectedRate = this._selectedRateType switch
				{
					"Bid" => spread.Bid,
					"Ask" => spread.Ask,
					_ => 0
				};
			}

			(this.DataContext as PosModel).Receipt.ExchangeRate = this._selectedRate;

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
			if(this.DataContext is not PosModel posModel || posModel.Receipt is not ReceiptModel receiptModel)
			{
				MessageBox.Show("E pamundur te printohet fatura!", "", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}



			receiptModel.Date = DateTime.Now.ToString("dd.MM.yyyy HH:mm");


			if (this._selectedRateType == "Bid")
			{
				receiptModel.SourceCurrency = this._selectedCurrency.ToUpper();
				receiptModel.TargetCurrency = "ALL";				
				double.TryParse(this._inputValue, out var sourceAmmount);
				receiptModel.SourceAmount = sourceAmmount;
				receiptModel.TargetAmount = sourceAmmount * this._selectedRate;
			}
			else
			{
				receiptModel.SourceCurrency = "ALL";
				receiptModel.TargetCurrency = this._selectedCurrency.ToUpper();
				double.TryParse(this._inputValue, out var sourceAmmount);
				receiptModel.SourceAmount = sourceAmmount;
				receiptModel.TargetAmount = sourceAmmount / this._selectedRate;
			}

			receiptModel.CurrencyPair = $"{this._selectedCurrency.ToUpper()}/ALL";
			receiptModel.ExchangeRate = this._selectedRate;


			var receipt = new Receipt();
			receipt.DataContext = receiptModel;
			var dialog = new Window
			{
				Title = "Paraqit Faturen",
				Content = receipt,
				SizeToContent = SizeToContent.WidthAndHeight,
				WindowStartupLocation = WindowStartupLocation.CenterOwner,
				Owner = this
			};
			

			receipt.Measure(new Size(receiptModel.Width, double.PositiveInfinity));
			receipt.Arrange(new Rect(0, 0, receiptModel.Width, receipt.DesiredSize.Height));


			if (this.DataContext is PosModel model)
			{
				// Print directly to the configured printer
				LocalPrintServer printServer = new LocalPrintServer();
				PrintQueue printQueue = printServer.GetPrintQueue(model.Settings.PrinterName);
				PrintTicket printTicket = printQueue.DefaultPrintTicket;
				PrintDialog pd = new PrintDialog();
				pd.PrintQueue = printQueue;
				pd.PrintTicket = printTicket;
				pd.PrintVisual(receipt, "Forex Receipt");

				this.ClearButton_Click(sender, e);
			}
			dialog.ShowDialog();
		}

		private void SettingsButton_Click(object sender, RoutedEventArgs e)
		{
			if(this.DataContext is PosModel model)
			{
				var dlg = new Settings();
				dlg.Source = model.Settings;
				if (dlg.ShowDialog() == true)
				{
					this.UpdateDisplays();
				}
			}
			
		}
	}
}