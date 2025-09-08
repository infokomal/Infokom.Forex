using ForexPOS.Atomics;

namespace ForexPOS.Models
{
	public class PosModel
	{
		public PosModel()
		{
			this.Settings = SettingsModel.Load();


			this.Receipt = new ReceiptModel();
			this.Receipt.Width = this.Settings.PaperSize switch
			{
				PaperSize.C7 => 230,
				_ => 162,
			};
		}

		public string SelectedCurrency { get; set; }

		public double SelectedRate { get; set; }

		public SettingsModel Settings { get; }

		public ReceiptModel Receipt { get; }
	}
}
