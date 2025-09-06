namespace ForexPOS
{
	public record ReceiptModel
	{
		public string Header { get; set; } = "ForexPOS";

		public string Date { get; set; } = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
		public string Source { get; set; } = "100.00 EUR";
		public string Target { get; set; } = "9500.00 ALL";
		public string Change { get; set; } = "1.00";

		public int Width { get; set; } = 164;

		public string Footer { get; set; } = "Thank you!";
	}
}
