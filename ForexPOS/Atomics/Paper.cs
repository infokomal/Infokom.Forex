using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForexPOS.Atomics
{
	public enum PaperSize
	{
		/// <summary>
		/// A Series 841 × 1189 mm (33.1 × 46.8 in)
		/// </summary>
		A0 = 100,
		A1,
		A2,
		A3,
		A4,
		A5,
		A6,
		A7,
		A8,
		A9,


		/// <summary>
		/// B Series 1000 × 1414 mm (39.4 × 55.7 in)
		/// </summary>
		B0 = 200,
		B1,
		B2,
		B3,
		B4,
		B5,
		B6,
		B7,
		B8,
		B9,

		/// <summary>
		/// C Series 917 × 1297 mm (36.1 × 51.1 in)
		/// </summary>
		C0 = 300,

		/// <summary>
		/// C Series 648 × 917 mm (25.5 × 36.1 in)
		/// </summary>
		C1,

		/// <summary>
		/// C Series 458 × 648 mm (18.0 × 25.5 in)
		/// </summary>
		C2,

		C3,

		C4,

		C5,

		C6,

		/// <summary>
		/// C Series 81x114 mm (3.2 × 4.5 in)
		/// </summary>
		C7,

		/// <summary>
		/// C Series 57 × 81 mm (2.2 × 3.2 in)
		/// </summary>
		C8,
		C9,
	}
	public static class Paper
	{
		public enum Size
		{
			/// <summary>
			/// A Series 841 × 1189 mm (33.1 × 46.8 in)
			/// </summary>
			A0 = 100,
			A1,
			A2,
			A3,
			A4,
			A5,
			A6,
			A7,
			A8,
			A9,


			/// <summary>
			/// B Series 1000 × 1414 mm (39.4 × 55.7 in)
			/// </summary>
			B0 = 200,
			B1,
			B2,
			B3,
			B4,
			B5,
			B6,
			B7,
			B8,
			B9,

			/// <summary>
			/// C Series 917 × 1297 mm (36.1 × 51.1 in)
			/// </summary>
			C0 = 300,

			/// <summary>
			/// C Series 648 × 917 mm (25.5 × 36.1 in)
			/// </summary>
			C1,

			/// <summary>
			/// C Series 458 × 648 mm (18.0 × 25.5 in)
			/// </summary>
			C2,

			C3,

			C4,

			C5,

			C6,

			/// <summary>
			/// C Series 81x114 mm (3.2 × 4.5 in)
			/// </summary>
			C7,

			/// <summary>
			/// C Series 57 × 81 mm (2.2 × 3.2 in)
			/// </summary>
			C8,
			C9,
		}


		public static decimal GetWidth(this Size size) => size switch
		{
			Size.A0 => 841,
			Size.A1 => 594,
			Size.A2 => 420,
			Size.A3 => 297,
			Size.A4 => 210,
			Size.A5 => 148,
			Size.A6 => 105,
			Size.A7 => 74,
			Size.A8 => 52,
			Size.A9 => 37,

			Size.B0 => 1000,
			Size.B1 => 707,
			Size.B2 => 500,
			Size.B3 => 353,
			Size.B4 => 250,
			Size.B5 => 176,
			Size.B6 => 125,
			Size.B7 => 88,
			Size.B8 => 62,
			Size.B9 => 44,
			
			Size.C0 => 917,
			Size.C1 => 648,
			Size.C2 => 458,
			Size.C3 => 324,
			Size.C4 => 229,
			Size.C5 => 162,
			Size.C6 => 114,
			Size.C7 => 81,
			Size.C8 => 57,
			Size.C9 => 40,
			_ => throw new ArgumentOutOfRangeException(nameof(size), $"Not expected size value: {size}"),
		};
		public static decimal GetHeight(this Size size)
		{
			size.Deconstruct(out int width, out int height);
			return height;
		}

		public static void Deconstruct(this Size size, out int width, out int height)
		{
			(width, height) = size switch
			{
				Size.A0 => (841, 1189),
				Size.A1 => (594, 841),
				Size.A2 => (420, 594),
				Size.A3 => (297, 420),
				Size.A4 => (210, 297),
				Size.A5 => (148, 210),
				Size.A6 => (105, 148),
				Size.A7 => (74, 105),
				Size.A8 => (52, 74),
				Size.A9 => (37, 52),
				Size.B0 => (1000, 1414),
				Size.B1 => (707, 1000),
				Size.B2 => (500, 707),
				Size.B3 => (353, 500),
				Size.B4 => (250, 353),
				Size.B5 => (176, 250),
				Size.B6 => (125, 176),
				Size.B7 => (88, 125),
				Size.B8 => (62, 88),
				Size.B9 => (44, 62),
				Size.C0 => (917, 1297),
				Size.C1 => (648, 917),
				Size.C2 => (458, 648),
				Size.C3 => (324, 458),
				Size.C4 => (229, 324),
				Size.C5 => (162, 229),
				Size.C6 => (114, 162),
				Size.C7 => (81, 114),
				Size.C8 => (57, 81),
				Size.C9 => (40, 57),
				_ => throw new ArgumentOutOfRangeException(nameof(size), $"Not expected size value: {size}"),
			};
		}
	}
}
