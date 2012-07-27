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
			var combinations = generator.Generate(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 6);

			foreach (var combination in combinations)
			{
				WriteArray<int>(combination);
			}

			Console.WriteLine("CALL_ENUMPATTERNS_A:{0}", CombinationGenerator<int>.CALL_EDITPATTERNS_A);
			Console.WriteLine("CALL_ENUMPATTERNS_B:{0}", CombinationGenerator<int>.CALL_EDITPATTERNS_B);
		}

		private void WriteArray<T>(T[] array)
		{
			var result = new StringBuilder();
			result.Append("[ ");
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
