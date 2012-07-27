using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umebayashi.MathEx.Statistics
{
	public class ClusterAnalyzer<T> where T: IEquatable<T>
	{
		public ClusterAnalyzer(Func<IEnumerable<T>, IEnumerable<T>, double> distanceComparer)
		{
			this.DistanceComparer = distanceComparer;
		}

		public Func<IEnumerable<T>, IEnumerable<T>, double> DistanceComparer { get; private set; }

		public void Analyze(IEnumerable<T> data)
		{
			var clusters = data.Select(x => new Cluster<T>(x)).ToArray();
			var generator = new CombinationGenerator<Cluster<T>>();

			var list = new List<ClusterPair<T>>();
			var combinations = generator.Generate(clusters, 2);

			foreach (var combination in combinations)
			{
				var distance = this.DistanceComparer(combination[0].Items, combination[1].Items);
				var pair = new ClusterPair<T>(combination[0], combination[1], distance);
				list.Add(pair);
			}
		}
	}

	public class Cluster<T>: IEquatable<Cluster<T>>, IComparable<Cluster<T>> where T: IEquatable<T>
	{
		public Cluster(T item)
		{
			this.Items = new T[] { item };
		}

		public Cluster(IEnumerable<T> items)
		{
			this.Items = items;
		}

		public IEnumerable<T> Items { get; private set; }

		public bool Equals(Cluster<T> other)
		{
			if (this.Items.Count() != other.Items.Count())
			{
				return false;
			}
			return this.Items.SequenceEqual(other.Items);
		}

		public int CompareTo(Cluster<T> other)
		{
			return 0;
		}
	}

	public class ClusterPair<T> where T : IEquatable<T>
	{
		public ClusterPair(Cluster<T> item1, Cluster<T> item2, double distance)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Distance = distance;
		}

		public Cluster<T> Item1 { get; private set; }

		public Cluster<T> Item2 { get; private set; }

		public double Distance { get; private set; }
	}
}
