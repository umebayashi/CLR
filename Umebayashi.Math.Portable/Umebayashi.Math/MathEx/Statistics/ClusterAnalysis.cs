using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx.Statistics
{
	public class ClusterAnalyzer<T> where T: ICoord, IEquatable<T>, IComparable<T>
	{
		public ClusterAnalyzer(Func<ClusterNode<T>, ClusterNode<T>, double> distanceComparer)
		{
			this.DistanceComparer = distanceComparer;
		}

		public Func<ClusterNode<T>, ClusterNode<T>, double> DistanceComparer { get; private set; }

		public void Analyze(IEnumerable<T> data)
		{
			//var clusters = data.Select(x => new Cluster<T>(x)).ToList();
			//var list = new List<ClusterPair<T>>();
			//var generator = new CombinationGenerator<Cluster<T>>();

			//var combinations = generator.Generate(clusters, 2);

			//foreach (var combination in combinations)
			//{
			//	var distance = this.DistanceComparer(combination[0].Items, combination[1].Items);
			//	var pair = new ClusterPair<T>(combination[0], combination[1], distance);
			//	list.Add(pair);
			//}

			//var minDistance = list.Where(x => x.Distance == list.Min(y => y.Distance)).First();
			//clusters.Remove(minDistance.Item1);
			//clusters.Remove(minDistance.Item2);
			//var parent = new Cluster<T>(null);
			var clusters = data.Select(x => new ClusterNode<T>{ Value = x }).ToList();
			var distances = new List<ClusterNodePair<T>>();
			var list = new List<ClusterNodePair<T>>();
			var generator = new CombinationGenerator<ClusterNode<T>>();

			while (clusters.Count > 1)
			{
				list.Clear();
				var combinations = generator.Generate(clusters, 2);
				foreach (var combination in combinations)
				{
					var distance = this.DistanceComparer(combination[0], combination[1]);
					var pair = new ClusterNodePair<T>(combination[0], combination[1], distance);
					list.Add(pair);
				}

				var minPair = list.Where(x => x.Distance == list.Min(y => y.Distance)).First();
				clusters.Remove(minPair.Item1);
				clusters.Remove(minPair.Item2);
				var parent = new ClusterNode<T>();
				minPair.Item1.Parent = parent;
				minPair.Item2.Parent = parent;
				parent.Nodes = new ClusterNode<T>[] { minPair.Item1, minPair.Item2 };
				clusters.Add(parent);
			}
		}
	}

	public interface ICoord
	{
		VectorD GetCoord();
	}

	public class ClusterNode<T> : ICoord, IEquatable<ClusterNode<T>>, IComparable<ClusterNode<T>> where T : ICoord, IEquatable<T>, IComparable<T>
	{
		public ClusterNode<T> Parent { get; set; }

		public IEnumerable<ClusterNode<T>> Nodes { get; set; }

		public T Value { get; set; }

		public bool Equals(ClusterNode<T> other)
		{
			if (this.Value == null)
			{
				if (other.Nodes == null)
				{
					return false;
				}
				else
				{
					return this.Nodes.SequenceEqual(other.Nodes);
				}
			}
			else
			{
				if (other.Value == null)
				{
					return false;
				}
				else
				{
					return this.Value.Equals(other.Value);
				}
			}
		}

		public int CompareTo(ClusterNode<T> other)
		{
			return 0;
		}

		public VectorD GetCoord()
		{
			if (this.Value == null)
			{
				var coords = this.Nodes.Select(x => x.GetCoord());
				return GetCenter(coords);
			}
			else
			{
				return this.Value.GetCoord();
			}
		}

		private VectorD GetCenter(IEnumerable<VectorD> vectors)
		{
			var center = new double[vectors.First().Length];
			for (int i = 0; i < center.Length; i++)
			{
				center[i] = vectors.Select(x => x[i]).Average();
			}

			return new VectorD(center);
		}
	}

	public class ClusterNodePair<T> where T : ICoord, IEquatable<T>, IComparable<T>
	{
		public ClusterNodePair(ClusterNode<T> item1, ClusterNode<T> item2, double distance)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Distance = distance;
		}

		public ClusterNode<T> Item1 { get; private set; }

		public ClusterNode<T> Item2 { get; private set; }

		public double Distance { get; private set; }
	}
}
