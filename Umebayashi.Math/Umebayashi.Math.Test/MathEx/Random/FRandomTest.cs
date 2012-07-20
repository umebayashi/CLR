using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx.Random
{
	[TestClass]
	public class FRandomTest
	{
		[TestMethod]
		public void TestNextDouble()
		{
			var random = new FRandom();

			var fdPairs = new Tuple<int, int>[] {
				new Tuple<int, int>(4, 6),
				new Tuple<int, int>(10, 10)
			};

			var list = new List<double>();
			foreach (var fdPair in fdPairs)
			{
				list.Clear();

				for (int i = 0; i < 100; i++)
				{
					list.Add(random.NextDouble(fdPair.Item1, fdPair.Item2));
				}
				list.Sort();

				Console.WriteLine("自由度({0}, {1})", fdPair.Item1, fdPair.Item2);
				list.ForEach(x => Console.WriteLine(x));
				Console.WriteLine();
			}
		}
	}
}
