using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx.Random
{
	[TestClass]
	public class Chi2RandomTest
	{
		[TestMethod]
		public void TestNextDouble()
		{
			var random = new Chi2Random();

			for (int fd = 1; fd < 10; fd++)
			{
				Console.WriteLine("自由度:{0}", fd);
				for (int i = 0; i < 20; i++)
				{
					Console.WriteLine(random.NextDouble(fd));
				}
				Console.WriteLine();
			}
		}
	}
}
