using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx.Random
{
	/// <summary>
	/// MTRandomTest の概要の説明
	/// </summary>
	[TestClass]
	public class MTRandomTest
	{
		public MTRandomTest()
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
		public void TestNext()
		{
			var mtRandom = new MTRandom();

			for (int i = 0; i < 100; i++)
			{
				var value = mtRandom.Next(100);
				Console.WriteLine(value);
			}
		}

		[TestMethod]
		public void TestNextDouble()
		{
			var mtRandom = new MTRandom();

			for (int i = 0; i < 100; i++)
			{
				var value = mtRandom.NextDouble();
				Console.WriteLine(value);
			}
		}
	}
}
