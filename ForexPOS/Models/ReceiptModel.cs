namespace ForexPOS.Models
{
	public record ReceiptModel
	{
		public string Header { get; set; } = "ForexPOS";

		public string Date { get; set; }

		public string SourceCurrency { get; set; } = "EUR";
		public double SourceAmount { get; set; } = 0;
		public string TargetCurrency { get; set; } = "ALL";
		public double TargetAmount { get; set; } = 0;

		public string CurrencyPair { get; set; } = "EUR/ALL";
		public double ExchangeRate { get; set; } = 95;

		public string Source => $"{this.SourceAmount:0.00} {this.SourceCurrency}";
		public string Target => $"{this.TargetAmount:0.00} {this.TargetCurrency}";
		public string Change => $"{this.ExchangeRate:0.00}";

		public int Width { get; set; } = 164;

		public string Footer { get; set; } = "Thank you!";
	}
}
