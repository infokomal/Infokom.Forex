using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ForexPOS
{
	public class SettingsModel
	{
		public Dictionary<string, double> Rates { get; set; } = new();

		public string Printer { get; set; }

		public double EurAsk
		{
			get => this.Rates.ContainsKey("EurAsk") ? this.Rates["EurAsk"] : 0;
			set => this.Rates["EurAsk"] = value;
		}

		public double EurBid
		{
			get => this.Rates.ContainsKey("EurBid") ? this.Rates["EurBid"] : 0;
			set => this.Rates["EurBid"] = value;
		}

		public double UsdAsk
		{
			get => this.Rates.ContainsKey("UsdAsk") ? this.Rates["UsdAsk"] : 0;
			set => this.Rates["UsdAsk"] = value;
		}

		public double UsdBid
		{
			get => this.Rates.ContainsKey("UsdBid") ? this.Rates["UsdBid"] : 0;
			set => this.Rates["UsdBid"] = value;
		}

		public double ChfAsk
		{
			get => this.Rates.ContainsKey("ChfAsk") ? this.Rates["ChfAsk"] : 0;
			set => this.Rates["ChfAsk"] = value;
		}
		public double ChfBid
		{
			get => this.Rates.ContainsKey("ChfBid") ? this.Rates["ChfBid"] : 0;
			set => this.Rates["ChfBid"] = value;
		}
		public double GbpAsk
		{
			get => this.Rates.ContainsKey("GbpAsk") ? this.Rates["GbpAsk"] : 0;
			set => this.Rates["GbpAsk"] = value;
		}
		public double GbpBid
		{
			get => this.Rates.ContainsKey("GbpBid") ? this.Rates["GbpBid"] : 0;
			set => this.Rates["GbpBid"] = value;
		}


		private const string SettingsFile = "settings.json";

		public static SettingsModel Load()
		{
			if (File.Exists(SettingsFile))
			{
				var json = File.ReadAllText(SettingsFile);
				var loaded = JsonSerializer.Deserialize<SettingsModel>(json);
				if (loaded != null)
					return loaded;
			}
			// Default settings if file doesn't exist
			return new SettingsModel
			{
				Rates = new Dictionary<string, double>
			 {
				{ "EurBid", 97.13 }, { "EurAsk", 97.83 },
				{ "UsdBid", 89.50 }, { "UsdAsk", 90.10 },
				{ "ChfBid", 101.20 }, { "ChfAsk", 102.00 },
				{ "GbpBid", 113.40 }, { "GbpAsk", 114.25 }
			 },
				Printer = "POS-58"
			};
		}

		public static void Save(SettingsModel settings)
		{
			var json = JsonSerializer.Serialize(settings);
			File.WriteAllText(SettingsFile, json);
		}
	}
}
