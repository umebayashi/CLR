using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx
{
	/// <summary>
	/// CombinatoricsTest の概要の説明
	/// </summary>
	[TestClass]
	public class CombinatoricsTest
	{
		public CombinatoricsTest()
		{
			//
			// TODO: コンストラクター ロジックをここに追加します
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///現在のテストの実行についての情報および機能を
		///提供するテスト コンテキストを取得または設定します。
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region 追加のテスト属性
		//
		// テストを作成する際には、次の追加属性を使用できます:
		//
		// クラス内で最初のテストを実行する前に、ClassInitialize を使用してコードを実行してください
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// クラス内のテストをすべて実行したら、ClassCleanup を使用してコードを実行してください
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// 各テストを実行する前に、TestInitialize を使用してコードを実行してください
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// 各テストを実行した後に、TestCleanup を使用してコードを実行してください
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void TestPermutationCount()
		{
			var result = Combinatorics.PermutationCount(10, 5);
			Assert.AreEqual<long>(30240, result);
		}

		[TestMethod]
		public void TestCombinationCount()
		{
			var result = Combinatorics.CombinationCount(10, 5);
			Assert.AreEqual<long>(252, result);
		}

		[TestMethod]
		public void TestGenerateCombinationNotAllowDuplicate()
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

		[TestMethod]
		public void TestGeneratePermutationNotAllowDuplicate()
		{
			var generator = new PermutationGenerator<int>();
			var permutations = generator.Generate(new int[] { 0, 1, 2, 3, 4 }, 3);

			int count = 0;
			foreach (var permutation in permutations)
			{
				WriteArray<int>(permutation, ++count);
			}

			Console.WriteLine();
			Console.Write("順列数:{0}", count);
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
		/*
		[TestMethod]
		public void TestGetPermutationGenerator()
		{
			var generator = Combinatorics<int>.GetPermutationGenerator(new int[] { 0, 1, 2, 3, 4 }, 5, false);

			int index = 0;
			generator.BeginGenerate(x =>
			{
				index++;
				Console.Write("({0}): [", index.ToString("0000"));
				for (int i = 0; i < x.Length; i++)
				{

					Console.Write(x[i]);
					if (i < x.Length - 1)
					{
						Console.Write(", ");
					}
				}
				Console.WriteLine("]");
			});
		}
		*/
	}
}
