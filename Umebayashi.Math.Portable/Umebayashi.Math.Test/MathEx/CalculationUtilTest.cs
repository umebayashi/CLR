using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx
{
	[TestClass]
	public class CalculationUtilTest
	{
		[TestMethod]
		public void TestFactorial()
		{
			for (long i = 0L; i <= 15L; i++)
			{
				Console.WriteLine("Factorial({0}): {1}", i, i.Factorial());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestFactorialException()
		{
			var fac = (-1).Factorial();
		}
	}
}
