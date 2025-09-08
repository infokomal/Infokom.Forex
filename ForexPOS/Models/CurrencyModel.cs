using ForexPOS.Atomics;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ForexPOS.Models
{
	public record CurrencyModel
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public ImageSource Flag { get; set; }







		public static CurrencyModel From(Currency code)
		{
			return code switch
			{
				Currency.ALL => new CurrencyModel { Code = "ALL", Name = "Albanian Lek", Flag = new BitmapImage(new Uri("pack://application:,,,/Images/Flags/64x64/ALL.png")) },
				Currency.USD => new CurrencyModel { Code = "USD", Name = "United States Dollar", Flag = new BitmapImage(new Uri("pack://application:,,,/Images/Flags/64x64/USD.png")) },
				Currency.EUR => new CurrencyModel { Code = "EUR", Name = "Euro", Flag = new BitmapImage(new Uri("pack://application:,,,/Images/Flags/64x64/EUR.png")) },
				Currency.GBP => new CurrencyModel { Code = "GBP", Name = "British Pound", Flag = new BitmapImage(new Uri("pack://application:,,,/Images/Flags/64x64//GBP.png")) },
				Currency.CHF => new CurrencyModel { Code = "CHF", Name = "Swiss Franc", Flag = new BitmapImage(new Uri("pack://application:,,,/Images/Flags/64x64//CHF.png")) },
				_ => throw new ArgumentOutOfRangeException(nameof(code), code, null)
			};
		}


		public static IEnumerable<CurrencyModel> GetAllCurrencies()
		{
			return new List<CurrencyModel>
			{
				new CurrencyModel { Code = "ALL", Name = "Albanian Lek", Flag = new BitmapImage(new Uri("pack://application:,,,/Images/Flags/64x64/ALL.png")) },
				new CurrencyModel { Code = "USD", Name = "United States Dollar", Flag = new BitmapImage(new Uri("pack://application:,,,/Images/Flags/64x64/USD.png")) },
				new CurrencyModel { Code = "EUR", Name = "Euro", Flag = new BitmapImage(new Uri("pack://application:,,,/Images/Flags/64x64/EUR.png")) },
				new CurrencyModel { Code = "GBP", Name = "British Pound", Flag = new BitmapImage(new Uri("pack://application:,,,/Images/Flags/64x64//GBP.png")) },
				new CurrencyModel { Code = "CHF", Name = "Swiss Franc", Flag = new BitmapImage(new Uri("pack://application:,,,/Images/Flags/64x64//CHF.png")) },
			};
		}
	}
}
