using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Umebayashi.MathEx.Statistics
{
	[TestClass]
	public class ClusterAnalysisTest
	{
		public class Data : ICoord, IEquatable<Data>, IComparable<Data>
		{
			/// <summary>
			/// 国名
			/// </summary>
			public string CountryName { get; set; }

			/// <summary>
			/// AIDS患者
			/// </summary>
			public double AIDSPatients { get; set; }

			/// <summary>
			/// 新聞発行部数
			/// </summary>
			public double NewspaperCirculations { get; set; }

			public static IEnumerable<Data> GetAll()
			{
				var result = new Data[] {
					new Data { CountryName = "A", AIDSPatients = 6.6, NewspaperCirculations = 35.8 },
					new Data { CountryName = "B", AIDSPatients = 8.4, NewspaperCirculations = 22.1 },
					new Data { CountryName = "C", AIDSPatients = 24.2, NewspaperCirculations = 19.1 },
					new Data { CountryName = "D", AIDSPatients = 10.0, NewspaperCirculations = 34.4 },
					new Data { CountryName = "E", AIDSPatients = 14.5, NewspaperCirculations = 9.9 },
					new Data { CountryName = "F", AIDSPatients = 12.2, NewspaperCirculations = 31.1 },
					new Data { CountryName = "G", AIDSPatients = 4.8, NewspaperCirculations = 53.0 },
					new Data { CountryName = "H", AIDSPatients = 19.8, NewspaperCirculations = 7.5 },
					new Data { CountryName = "I", AIDSPatients = 6.1, NewspaperCirculations = 53.4 },
					new Data { CountryName = "J", AIDSPatients = 26.8, NewspaperCirculations = 50.0 },
					new Data { CountryName = "K", AIDSPatients = 7.4, NewspaperCirculations = 42.1 }
				};

				return result;
			}

			public override bool Equals(object obj)
			{
				if (obj is Data)
				{
					return this.Equals((Data)obj);
				}
				return base.Equals(obj);
			}

			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			public override string ToString()
			{
				return string.Format("[ CounryName: {0}, AIDSPatients: {1}, NewspaperCiculations; {2} ]",
					this.CountryName, this.AIDSPatients, this.NewspaperCirculations);
			}

			public bool Equals(Data other)
			{
				return this.CountryName.Equals(other.CountryName);
			}

			public int CompareTo(Data other)
			{
				throw new NotImplementedException();
			}

			public VectorD GetCoord()
			{
				return new VectorD(this.AIDSPatients, this.NewspaperCirculations);
			}
		}

		[TestMethod]
		public void TestAnalyze()
		{
			var analyzer = new ClusterAnalyzer<Data>(this.TestDistanceComparer);

			analyzer.Analyze(Data.GetAll());
		}

		private double TestDistanceComparer(ClusterNode<Data> item1, ClusterNode<Data> item2)
		{
			var v1 = item1.GetCoord();
			var v2 = item2.GetCoord();
			var vd = v1 - v2;
			var distance = vd.Norm();

			return distance;
		}

		private VectorD CalculateCoord(IEnumerable<Data> items)
		{
			return new VectorD(items.Select(x => x.AIDSPatients).Average(), items.Select(x => x.NewspaperCirculations).Average());
		}
	}
}
