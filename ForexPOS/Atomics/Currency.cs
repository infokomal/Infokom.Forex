namespace ForexPOS.Atomics
{
	public enum Currency
	{
		None = 0,
		ALL, // Albanian Lek
		USD, // United States Dollar
		EUR, // Euro
		GBP, // British Pound
		CHF, // Swiss Franc
	}

	public static class CurrencyCodeExtensions
	{
		public static string GetDisplayName(this Currency code)
		{
			return code switch
			{
				Currency.ALL => "Albanian Lek",
				Currency.USD => "United States Dollar",
				Currency.EUR => "Euro",
				Currency.GBP => "British Pound",
				Currency.CHF => "Swiss Franc",
				_ => "Unknown Currency"
			};
		}

		public static string GetFlagUri(this Currency code)
		{
			return code switch
			{
				Currency.ALL => "pack://application:,,,/Images/Flags/64x64/ALL.png",
				Currency.USD => "pack://application:,,,/Images/Flags/64x64/USD.png",
				Currency.EUR => "pack://application:,,,/Images/Flags/64x64/EUR.png",
				Currency.GBP => "pack://application:,,,/Images/Flags/64x64/GBP.png",
				Currency.CHF => "pack://application:,,,/Images/Flags/64x64/CHF.png",
				_ => "pack://application:,,,/Images/Flags/64x64/Unknown.png"
			};
		}
	}
}
