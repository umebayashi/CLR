using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx.Algebra
{
	/// <summary>
	/// 置換群
	/// </summary>
	public class PermutationGroup
	{
		#region constructor

		private PermutationGroup(VectorI vector, VectorI[] elements)
		{
			this.Vector = vector;
			this.Elements = elements;
		}

		#endregion

		#region field / property

		public VectorI Vector { get; private set; }

		public VectorI[] Elements { get; private set; }

		public int Sign
		{
			get
			{
				return (this.Elements.Length % 2 == 0) ? 1 : -1;
			}
		}

		#endregion

		#region method

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			var result = new StringBuilder();

			result.AppendFormat("[ Vector: {0}, ", this.Vector);
			result.Append("Elements: [");
			if (this.Elements != null)
			{
				for (int i = 0; i < this.Elements.Length; i++)
				{
					result.Append(this.Elements[i]);
					if (i < this.Elements.Length - 1)
					{
						result.Append(", ");
					}
				}
			}
			result.Append("] ]");

			return result.ToString();
		}

		#endregion

		#region static method

		/// <summary>
		/// 
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public static VectorI[] GetElements(int size)
		{
			var list = new List<VectorI>();
			
			for (int i = 0; i < size - 1; i++)
			{
				for (int j = i + 1; j < size; j++)
				{
					list.Add(new VectorI(i, j));
				}
			}

			return list.ToArray();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public static IEnumerable<PermutationGroup> Calculate(int size)
		{
			var source = GetElements(size);
			var listPermutation = new List<PermutationGroup>();

			listPermutation.Add(new PermutationGroup(new VectorI(Enumerable.Range(0, size).ToArray()), new VectorI[0]));

			int length = 0;
			var listElements = new List<VectorI[]>();
			while (listPermutation.Count() < size.Factorial())
			{
				length++;
				listElements.Clear();

				for (int i = 0; i < source.Length; i++)
				{
					var vector = new VectorI[length];
					vector[0] = source[i];
					CalculateElements(listElements, source, length, 0, vector);
				}

				foreach (var elements in listElements)
				{
					var vector = CalculateVector(size, elements);
					if (listPermutation.Where(x => x.Vector.Equals(vector)).Count() == 0)
					{
						listPermutation.Add(new PermutationGroup(vector, elements));
					}
				}
			}

			return listPermutation;
		}

		private static void CalculateElements(List<VectorI[]> list, VectorI[] source, int length, int index, VectorI[] vector)
		{
			if (index == length - 1)
			{
				var copy = new VectorI[length];
				Array.Copy(vector, copy, length);
				list.Add(copy);
			}
			else
			{
				index++;
				for (int i = 0; i < source.Length; i++)
				{
					vector[index] = source[i];
					CalculateElements(list, source, length, index, vector);
				}
			}
		}

		private static VectorI CalculateVector(int size, VectorI[] target)
		{
			var array = Enumerable.Range(0, size).ToArray();

			for (int i = 0; i < target.Length; i++)
			{
				var temp = array[target[i][0]];
				array[target[i][0]] = array[target[i][1]];
				array[target[i][1]] = temp;
			}

			return new VectorI(array);
		}

		#endregion
	}
}
