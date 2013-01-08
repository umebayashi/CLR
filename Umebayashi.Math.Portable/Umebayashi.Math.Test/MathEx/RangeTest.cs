using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx
{
	[TestClass]
	public class RangeTest
	{
		[TestMethod]
		public void TestRangeD()
		{
			var range = new RangeD(-5.0, 5.0, 0.1);
			range.ToList().ForEach(x => Console.WriteLine(x));
			//foreach (var value in range)
			//{
			//	Console.WriteLine(value);
			//}
		}
	}
}
