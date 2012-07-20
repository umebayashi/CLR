using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx
{
	/// <summary>
	/// VectorTest の概要の説明
	/// </summary>
	[TestClass]
	public class VectorTest
	{
		public VectorTest()
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
		public void TestVectorDProd()
		{
			var vector = new VectorD(2.5, 3.0, 4.1);
			var prod = Math.Round(vector.Prod(), 2);

			Assert.AreEqual<double>(30.75, prod);
		}

		[TestMethod]
		public void TestUnion()
		{
			var x = new VectorD(2, 4, 6, 8);
			var y = new VectorD(4, 8, 12, 14, 16);
			var union = x.Union(y);

			Assert.AreEqual<VectorD>(
				new VectorD(2, 4, 6, 8, 12, 14, 16),
				union);
		}

		[TestMethod]
		public void TestIntersect()
		{
			var x = new VectorD(2, 4, 6, 8);
			var y = new VectorD(4, 8, 12, 14, 16);
			var intersect = x.Intersect(y);

			Assert.AreEqual<VectorD>(
				new VectorD(4, 8),
				intersect);
		}

		[TestMethod]
		public void TestSort()
		{
			var x = new VectorM(3.2m, 1.3m, 9.0m, 7.6m, 2.6m);
			
			x.Sort();
			Assert.AreEqual<VectorM>(
				new VectorM(1.3m, 2.6m, 3.2m, 7.6m, 9.0m),
				x);

			x.Sort(true);
			Assert.AreEqual<VectorM>(
				new VectorM(9.0m, 7.6m, 3.2m, 2.6m, 1.3m),
				x);

			var y = new VectorC(
				new ComplexD(2, 3),
				new ComplexD(-1, -4),
				new ComplexD(6, 7),
				new ComplexD(1, 2));

			y.Sort();
			Assert.AreEqual<VectorC>(
				new VectorC(
					new ComplexD(-1, -4),
					new ComplexD(1, 2),
					new ComplexD(2, 3),
					new ComplexD(6, 7)),
				y);

			y.Sort(true);
			Assert.AreEqual<VectorC>(
				new VectorC(
					new ComplexD(6, 7),
					new ComplexD(2, 3),
					new ComplexD(1, 2),
					new ComplexD(-1, -4)),
				y);
		}

		[TestMethod]
		public void TestSum()
		{
			var x = new VectorM(3.2m, 1.3m, 9.0m, 7.6m, 2.6m);

			var sumX = x.Sum();
			Assert.AreEqual<decimal>(23.7m, sumX);

			var y = new VectorC(
				new ComplexD(2, 3),
				new ComplexD(-1, -4),
				new ComplexD(6, 7),
				new ComplexD(1, 2));

			//var sumY = y.Sum();
		}
	}
}
