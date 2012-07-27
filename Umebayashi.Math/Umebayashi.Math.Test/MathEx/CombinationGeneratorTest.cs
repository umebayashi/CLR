using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx
{
	[TestClass]
	public class CombinationGeneratorTest
	{
		[TestMethod]
		public void TestGenerateNotAllowDuplicate()
		{
			var generator = new CombinationGenerator<int>();
			var combinations = generator.Generate(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 5);

			int count = 0;
			foreach (var combination in combinations)
			{
				WriteArray<int>(combination, ++count);
			}

			Console.WriteLine();
			Console.Write("組合せ数:{0}", count);
		}

		private void WriteArray<T>(T[] array, int count)
		{
			var result = new StringBuilder();
			result.AppendFormat("({0})[ ", count.ToString("000"));
			for (int i = 0; i < array.Length; i++)
			{
				result.Append(array[i]);
				if (i < array.Length - 1)
				{
					result.Append(", ");
				}
			}
			result.Append(" ]");

			Console.WriteLine(result.ToString());
		}
	}
}
