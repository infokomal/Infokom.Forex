using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ForexPOS.Controls
{
	/// <summary>
	/// Interaction logic for Currency.xaml
	/// </summary>
	public partial class Currency : UserControl
	{
		public Currency()
		{	
			this.CurrencyCode = "USD";
			this.CurrencyName = "United States Dollar";
			this.CurrencyFlag = new BitmapImage(new Uri("pack://application:,,,/Images/Flags/64x64/USD.png"));
			this.InitializeComponent();
		}


		public string CurrencyCode { get; set; }
		public string CurrencyName { get; set; }
		public ImageSource CurrencyFlag { get; set; }
	}
}
