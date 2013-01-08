using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx.Random
{
	/// <summary>
	/// NormalRandomTest の概要の説明
	/// </summary>
	[TestClass]
	public class NormalRandomTest
	{
		public NormalRandomTest()
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
		public void TestNextDouble()
		{
			var random = new NormalRandom();
			var list = new List<double>();
			for (int i = 0; i < 10000000; i++)
			{
				var value = random.NextDouble();
				//Console.WriteLine(value);
				list.Add(value);
			}

			Console.WriteLine();
			Console.WriteLine("平均: {0}", list.Average());
			Console.WriteLine("分散: {0}", VarP(list.ToArray())); 
		}

		private static double VarP(params double[] values)
		{
			var average = values.Average();
			var diffSqrSum = values.Select(x => (x - average) * (x - average)).Sum();
			return diffSqrSum / values.Length;
		}
	}
}
