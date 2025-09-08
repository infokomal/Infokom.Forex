using ForexPOS.Atomics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;

namespace ForexPOS.Models
{
	public class SpreadModel
	{
		public double Bid { get; set; }
		public double Ask { get; set; }
	}

	public class SettingsModel
	{
		public string PrinterName { get; set; }

		public PaperSize PaperSize { get; set; }

		public SpreadModel EUR { get; set; } = new SpreadModel { Bid = 97.13, Ask = 97.83 };
		public SpreadModel USD { get; set; } = new SpreadModel { Bid = 89.50, Ask = 90.10 };
		public SpreadModel CHF { get; set; } = new SpreadModel { Bid = 101.20, Ask = 102.00 };
		public SpreadModel GBP { get; set; } = new SpreadModel { Bid = 113.40, Ask = 114.25 };


		private const string SETTINGS_FILE = "settings.json";

		public static SettingsModel Load()
		{
			var defaultSettings = new SettingsModel();

			if (!File.Exists(SETTINGS_FILE))
			{
				Save(defaultSettings);
			}

			var json = File.ReadAllText(SETTINGS_FILE);
			var loadedSettings = JsonSerializer.Deserialize<SettingsModel>(json);



			return loadedSettings ?? defaultSettings;
		}

		public static void Save(SettingsModel settings)
		{
			var json = JsonSerializer.Serialize(settings);
			File.WriteAllText(SETTINGS_FILE, json);
		}
	}
}
