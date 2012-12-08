using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmidaKuji.StoreApp.Models
{
	public class AmidaBridge
	{
		public int StreamIndex { get; set; }

		public double Position { get; set; }

		private static readonly Random _random = new Random(DateTime.Now.Millisecond);

		public static IEnumerable<AmidaBridge> GenerateBridges(int streamCount, double streamLength)
		{
			return null;
		}
	}
}
