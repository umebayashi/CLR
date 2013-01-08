using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx
{
	/// <summary>
	/// MatrixTest の概要の説明
	/// </summary>
	[TestClass]
	public class MatrixTest
	{
		public MatrixTest()
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
		public void TestToString()
		{
			var m1 = new MatrixD(new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 }, 2, 3);
			Console.Write(m1);
		}

		[TestMethod]
		public void TestToVector()
		{
			var m1 = new MatrixD(new double[] { 1, 2, 3, 4 }, 2, 2);
			var v1 = m1.ToVector();

			Assert.AreEqual<VectorD>(new VectorD(1, 2, 3, 4), v1);
		}

		[TestMethod]
		public void TestNorm()
		{
			var m1 = new VectorD(1.0, 2.0, 3.0, 4.0).ToMatrix(2, 2);

			var normO = m1.Norm(MatrixNormType.O);
			Assert.AreEqual<double>(7.0, normO);

			var normI = m1.Norm(MatrixNormType.I);
			Assert.AreEqual<double>(6.0, normI);

			var normF = Math.Round(m1.Norm(MatrixNormType.F), 6);
			Assert.AreEqual<double>(5.477226, normF);

			var normM = m1.Norm(MatrixNormType.M);
			Assert.AreEqual<double>(4.0, normM);
		}

		[TestMethod]
		public void TestCrossProd()
		{
			var a = new VectorD(2, -3, 5);
			var b = new VectorD(-1, 3, 1);

			var x1 = MatrixD.CrossProd((MatrixD)a, (MatrixD)b);
			Assert.AreEqual<MatrixD>(
				new MatrixD(new double[] { -6 }, 1, 1),
				x1);
		}

		[TestMethod]
		public void TestDeterminant()
		{
			var A = new MatrixD(new double[] { 5 }, 1, 1);
			var detA = A.Determinant();
			Assert.AreEqual<double>(5.0, detA);

			var B = new MatrixD(new double[] { -2 }, 1, 1);
			var detB = B.Determinant();
			Assert.AreEqual<double>(-2.0, detB);

			var C = new MatrixD(new double[] { 1, -1, 3, 2 }, 2, 2);
			var detC = C.Determinant();
			Assert.AreEqual<double>(5.0, detC);

			var D = new MatrixD(new double[] { -2, 1, -1, 4 }, 2, 2);
			var detD = D.Determinant();
			Assert.AreEqual<double>(-7.0, detD);

			var X = new MatrixD(new double[] { 1, -1, 2, 1, 3, 3, -2, 0, 1 }, 3, 3);
			var detX = X.Determinant();
			Assert.AreEqual<double>(22.0, detX);

			var Y = new MatrixD(new double[] { 2, 2, 1, -1, 4, 3, 5, -1, 2 }, 3, 3);
			var detY = Y.Determinant();
			Assert.AreEqual<double>(37.0, detY);
		}

		[TestMethod]
		public void TestCofactor()
		{
			var m1 = new MatrixD(new double[] { 1.0, 4.0, 7.0, 2.0, 5.0, 8.0, 3.0, 6.0, 9.0 }, 3, 3, false);
			var cf1 = m1.Cofactor(1, 1);
			Assert.AreEqual<double>(-12, Math.Round(cf1));
			//var m1 = new MatrixD(new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0 }, 3, 3, false);
			//var cf1 = m1.Cofactor(0, 0);

			//Assert.AreEqual<MatrixD>(
			//	new MatrixD(new double[] { 5.0, 6.0, 8.0, 9.0 }, 2, 2),
			//	cf1);

			//var m2 = new MatrixD(new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0 }, 3, 3, true);
			//var cf2 = m2.Cofactor(0, 0);

			//Assert.AreEqual<MatrixD>(
			//	new MatrixD(new double[] { 5.0, 6.0, 8.0, 9.0 }, 2, 2, true),
			//	cf2);
		}

		[TestMethod]
		public void TestInverse()
		{
			var m1 = new MatrixD(new double[] { 3.0, 3.0, -1.0, -3.0, 2.0, -5.0, 1.0, 0, 1.0 }, 3, 3, false);
			var r1 = m1.Inverse();
		}

		[TestMethod]
		public void TestTridiagonalize()
		{
			var m1 = new MatrixD(new double[] { 1.0, 1.0, 1.0, 1.0, 1.0, 2.0, 2.0, 2.0, 1.0, 2.0, 3.0, 3.0, 1.0, 2.0, 3.0, 4.0 }, 4, 4, false);
			var td1 = m1.Tridiagonalize();

			Console.WriteLine(td1);
		}

		[TestMethod]
		public void TestQRDecompose()
		{
			var m1 = new MatrixD(new double[] { 5, 2, 1, -1, 3, 3, 0, 1, -1 }, 3, 3 );
			//var m1 = new MatrixD(new double[] { 16, 2, 1, 4, -1, 12, 3, -2, 1, 1, -24, 1, 2, -1, 2, 20 }, 4, 4);
			Console.WriteLine(m1);

			var result = m1.QRDecompose();
			Console.WriteLine(result.Q);
			Console.WriteLine(result.Q * result.Q.Transpose());
			Console.WriteLine(result.R);
			Console.WriteLine(result.Q * result.R);
		}
	}
}
