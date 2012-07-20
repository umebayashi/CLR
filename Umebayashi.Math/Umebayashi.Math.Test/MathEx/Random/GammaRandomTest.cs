using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx.Random
{
	[TestClass]
	public class GammaRandomTest
	{
		[TestMethod]
		public void TestNextDouble()
		{
			var random = new GammaRandom();

			for (int a = 1; a <= 10; a++)
			{
				Console.WriteLine("a = {0}", a);
				for (int i = 0; i < 20; i++)
				{
					Console.WriteLine(random.NextDouble(a));
				}
				Console.WriteLine();
			}
		}
	}
}
