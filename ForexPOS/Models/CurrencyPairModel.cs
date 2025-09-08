namespace ForexPOS.Models
{
	public record CurrencyPairModel(CurrencyModel Source, CurrencyModel Target, double Bid, double Ask);
}
