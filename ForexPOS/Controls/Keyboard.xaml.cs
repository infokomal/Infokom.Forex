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
	/// Interaction logic for Keyboard.xaml
	/// </summary>
	public partial class Keyboard : UserControl
	{
		public Keyboard()
		{
			this.InitializeComponent();
		}
	}

	public class KeyboardModel
	{
 		public string Input { get; set; } = string.Empty;

		public void Append(string value)
		{
			this.Input += value;
		}

		public void Backspace()
		{
			if (this.Input.Length > 0)
			{
				this.Input = this.Input[..^1];
			}
		}

		public void Clear()
		{
			this.Input = string.Empty;
		}

		private void OnButtonClick(object sender, RoutedEventArgs e)
		{
			if (sender is Button button && button.Content is string value)
			{
				switch (value)
				{
					case "C":
						this.Clear();
						break;
					case "⌫":
						this.Backspace();
						break;
					default:
						this.Append(value);
						break;
				}
			}
		}
	}
}
