using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx
{
	/// <summary>
	/// ComplexTest の概要の説明
	/// </summary>
	[TestClass]
	public class ComplexTest
	{
		public ComplexTest()
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
		public void TestAddI()
		{
			var x = new ComplexI(1, 2);
			var y = new ComplexI(3, 7);
			var z = x + y;

			Assert.AreEqual<ComplexI>(new ComplexI(4, 9), z);
		}

		[TestMethod]
		public void TestAddD()
		{
			var x = new ComplexD(1.1, 2.5);
			var y = new ComplexD(3.4, 7.8);
			var z = x + y;

			Assert.AreEqual<ComplexD>(new ComplexD(4.5, 10.3), z);
		}

		[TestMethod]
		public void TestSubtractI()
		{
			var x = new ComplexI(3, 5);
			var y = new ComplexI(2, 9);
			var z = x - y;

			Assert.AreEqual<ComplexI>(new ComplexI(1, -4), z);
		}
	}
}
