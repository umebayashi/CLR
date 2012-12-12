using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Umebayashi.MathEx.Algebra;

namespace Umebayashi.MathEx
{
	public abstract class CombinatoricsGenerator<T> where T: IEquatable<T>, IComparable<T>
	{
		public CombinatoricsGenerator()
		{
			this.Result = new List<T[]>();
		}

		protected List<T[]> Result { get; set; }

		public abstract IEnumerable<T[]> Generate(IEnumerable<T> source, int length, bool allowDuplicate = false);

		protected void SortResult()
		{
			this.Result.Sort((x, y) =>
			{
				for (int i = 0; i < x.Length; i++)
				{
					if (x[i].Equals(y[i])) continue;
					return x[i].CompareTo(y[i]);
				}

				return 0;
			});
		}
	}

	public class CombinationGenerator<T> : CombinatoricsGenerator<T> where T : IEquatable<T>, IComparable<T>
	{
		public CombinationGenerator()
			: base()
		{
		}

		private int SourceLength { get; set; }
		private int TargetLength { get; set; }
		private List<bool[]> Patterns { get; set; }

		/// <summary>
		/// 要素の組み合わせの一覧を作成する
		/// </summary>
		/// <param name="source"></param>
		/// <param name="length"></param>
		/// <param name="allowDuplicate"></param>
		/// <returns></returns>
		public override IEnumerable<T[]> Generate(IEnumerable<T> source, int length, bool allowDuplicate = false)
		{
			this.SourceLength = source.Count();
			this.TargetLength = length;
			this.Patterns = new List<bool[]>();

			if (allowDuplicate)
			{
				throw new NotImplementedException();
			}
			else
			{
				bool[] flags = new bool[this.SourceLength];
				for (int i = 0; i < this.TargetLength; i++)
				{
					flags[i] = true;
				}
				this.EnumPatterns(flags);

				foreach (var pattern in this.Patterns)
				{
					this.Result.Add(source.Where((x, i) => pattern[i]).ToArray());
				}
			}

			this.SortResult();

			return this.Result;
		}

		private void EnumPatterns(bool[] flags)
		{
			this.Patterns.Add(flags);

			var stack = new Stack<bool[]>();
			stack.Push(flags);

			while (stack.Count() > 0)
			{
				var flags1 = stack.Pop();
				for (int i = 0; i < flags1.Length - 1; i++)
				{
					if (flags1[i] && !flags1[i + 1])
					{
						var flags2 = new bool[this.SourceLength];
						Array.Copy(flags1, flags2, flags1.Length);

						flags2[i] = flags1[i + 1];
						flags2[i + 1] = flags1[i];

						if (!this.Patterns.Exists(x => x.SequenceEqual(flags2)))
						{
							this.Patterns.Add(flags2);
							stack.Push(flags2);
						}
					}
				}
			}
		}
	}

	/// <summary>
	/// 要素の順列の一覧を作成する
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PermutationGenerator<T> : CombinatoricsGenerator<T> where T : IEquatable<T>, IComparable<T>
	{
		public PermutationGenerator(): base()
		{
		}

		public override IEnumerable<T[]> Generate(IEnumerable<T> source, int length, bool allowDuplicate = false)
		{
			// 組み合わせの一覧を取得する
			var genCombination = new CombinationGenerator<T>();
			var combinations = genCombination.Generate(source, length, allowDuplicate).ToArray();

			// 置換群の一覧を取得する(組み合わせの並べ替えに使用)
			var permutationGroups = PermutationGroup.Calculate(length).ToArray();

			if (allowDuplicate)
			{
				throw new NotImplementedException();
			}
			else
			{
				foreach (var combination in combinations)
				{
					foreach (var permutationGroup in permutationGroups)
					{
						var copy = new T[combination.Length];

						for (int i = 0; i < copy.Length; i++)
						{
							copy[i] = combination[permutationGroup.Vector[i]];
						}

						this.Result.Add(copy);
					}
				}
			}

			this.SortResult();

			return this.Result;
		}
	}

	/*
	public interface ICombinatoricsGenerator<T> where T : IEquatable<T>
	{
		void BeginGenerate(Action<T[]> getAction, Action<Exception> errorAction = null);
	}

	public static class Combinatorics<T> where T : IEquatable<T>
	{
		#region static method

		public static ICombinatoricsGenerator<T> GetCombinationGenerator(T[] source, int length, bool allowDuplicate)
		{
			if (allowDuplicate)
			{
				var generator = new AllowDuplicateCombinationGenerator
				{
					Length = length,
					Source = source
				};
				return generator;
			}
			else
			{
				var generator = new NotAllowDuplicateCombinationGenerator
				{
					Length = length,
					Source = source
				};
				return generator;
			}
		}

		public static ICombinatoricsGenerator<T> GetPermutationGenerator(T[] source, int length, bool allowDuplicate)
		{
			if (allowDuplicate)
			{
				var generator = new AllowDuplicatePermutationGenerator
				{
					Length = length,
					Source = source
				};
				return generator;
			}
			else
			{
				var generator = new NotAllowDuplicatePermutationGenerator
				{
					Length = length,
					Source = source
				};
				return generator;
			}
		}

		#endregion

		#region internal class

		/// <summary>
		/// 順列・組み合わせ生成クラスの基本クラス
		/// </summary>
		private abstract class CombinatoricsGenerator : ICombinatoricsGenerator<T>
		{
			#region constructor

			public CombinatoricsGenerator()
			{
				this.collection = new BlockingCollection<T[]>();
				this.checkList = new List<T[]>();
			}

			#endregion

			#region field / property

			protected BlockingCollection<T[]> collection;
			protected List<T[]> checkList;

			public T[] Source { get; internal set; }

			public int Length { get; internal set; }

			public bool Finished { get; internal set; }

			public bool NeedCheck { get; internal set; }

			#endregion

			#region 配列操作関連

			protected T[] CopyArray(T[] source)
			{
				T[] clone = new T[source.Length];
				for (int i = 0; i < source.Length; i++)
				{
					clone[i] = source[i];
				}

				return clone;
			}

			protected void FillArray(T[] target, T fillValue, int fillLength)
			{
				for (int i = 0; i < target.Length; i++)
				{
					if (i < fillLength)
					{
						target[i] = fillValue;
					}
				}
			}

			protected T[] SliceArray(T[] source, int startIndex)
			{
				if (startIndex == source.Length)
				{
					return new T[0];
				}
				else
				{
					T[] result = new T[source.Length - startIndex];
					for (int i = 0; i < result.Length; i++)
					{
						result[i] = source[startIndex + i];
					}
					return result;
				}
			}

			#endregion

			#region コレクション操作関連

			protected void AddToCollection(T[] item)
			{
				this.collection.Add(item);
			}

			protected void CompleteAddingToCollection()
			{
				this.collection.CompleteAdding();
			}

			protected void ClearCheckList()
			{
				this.checkList.Clear();
			}

			protected void AddToCheckList(T[] item)
			{
				this.checkList.Add(item);
			}

			protected bool ItemExists(T[] target)
			{
				return this.checkList.Exists(x => x.SequenceEqual(target));
			}

			protected T[] TakeFromCollection()
			{
				return this.collection.Take();
			}

			#endregion

			#region 実行関連

			protected virtual void Validate()
			{
				if (this.Length <= 0)
				{
					throw new InvalidOperationException(string.Format(
						"組み合わせのサイズには1以上の値を指定してください(組み合わせサイズ:{0})", this.Length));
				}
			}

			protected abstract Task CreateProducerTask();

			protected virtual Task CreateConsumerTask(Action<T[]> getAction)
			{
				Task consumerTask = Task.Factory.StartNew(() =>
				{
					this.Finished = false;
					try
					{
						while (true)
						{
							var item = this.TakeFromCollection();
							if (getAction != null)
							{
								getAction(item);
							}
						}
					}
					catch (InvalidOperationException)
					{
						this.Finished = true;
					}
				});

				return consumerTask;
			}

			public virtual void BeginGenerate(Action<T[]> getAction, Action<Exception> errorAction = null)
			{
				this.ClearCheckList();

				try
				{
					this.Validate();

					Task producerTask = this.CreateProducerTask();
					Task consumerTask = this.CreateConsumerTask(getAction);

					Task.WaitAll(producerTask, consumerTask);
				}
				catch (Exception ex)
				{
					if (errorAction != null)
					{
						errorAction(ex);
					}
					else
					{
						Console.WriteLine(ex.Message);
					}
				}
				finally
				{
					this.ClearCheckList();
				}
			}

			#endregion
		}

		/// <summary>
		/// 要素の重複ありの組み合わせを生成するクラス
		/// </summary>
		private class AllowDuplicateCombinationGenerator : CombinatoricsGenerator
		{
			protected override void Validate()
			{
				base.Validate();

				this.NeedCheck = false;
			}

			protected override Task CreateProducerTask()
			{
				Task producereTask = Task.Factory.StartNew(() =>
				{
					T[] wrkSource = this.CopyArray(this.Source).Distinct().OrderBy(x => x).ToArray();

					this.GenerateCombinations(wrkSource);

					this.CompleteAddingToCollection();
				});

				return producereTask;
			}

			private void GenerateCombinations(T[] source)
			{
				if (this.Length < 2)
				{
					for (int i = 0; i < source.Length; i++)
					{
						T[] target = new T[] { source[i] };
						this.AddToCollection(target);
					}
				}
				else
				{
					while (source.Length > 0)
					{
						T fillValue = source[0];
						source = this.SliceArray(source, 1);
						for (int j = 0; j < this.Length; j++)
						{
							int fillLength = this.Length - j;
							T[] target = new T[this.Length];
							this.FillArray(target, fillValue, fillLength);

							foreach (var item in this.GenerateCombinationInternal(target, fillLength, source))
							{
								this.AddToCollection(item);
							}
						}
					}
				}
			}

			private IEnumerable<T[]> GenerateCombinationInternal(T[] target, int startIndex, T[] source)
			{
				if (startIndex == target.Length)
				{
					yield return this.CopyArray(target);
				}
				else
				{
					for (int i = 0; i < source.Length; i++)
					{
						target[startIndex] = source[i];
						T[] newSource = this.SliceArray(source, i);
						foreach (var item in this.GenerateCombinationInternal(target, startIndex + 1, newSource))
						{
							yield return item;
						}
					}
				}
			}
		}

		/// <summary>
		/// 要素の重複なしの組み合わせを生成するクラス
		/// </summary>
		private class NotAllowDuplicateCombinationGenerator : CombinatoricsGenerator
		{
			protected override void Validate()
			{
				base.Validate();

				if (this.Source.Length < this.Length)
				{
					throw new InvalidOperationException(string.Format(
						"重複なしの場合、組み合わせのサイズは要素の個数以内である必要があります(組み合わせサイズ:{0} / 要素個数:{1})",
						this.Length,
						this.Source.Length));
				}

				int distinctCount = this.Source.Distinct().Count();

				if (this.Source.Length == distinctCount)
				{
					this.NeedCheck = false;
				}
				else
				{
					this.NeedCheck = true;
				}
			}

			protected override Task CreateProducerTask()
			{
				Task producerTask = Task.Factory.StartNew(() =>
				{
					var wrkSource = this.CopyArray(this.Source).OrderBy(x => x).ToArray();

					this.GenerateCombinations(wrkSource);

					this.CompleteAddingToCollection();
				});

				return producerTask;
			}

			private void GenerateCombinations(T[] source)
			{
				if (this.Length < 2)
				{
					for (int i = 0; i < source.Length; i++)
					{
						T[] target = new T[] { source[i] };
						if (this.NeedCheck)
						{
							var clone = this.CopyArray(target);
							this.AddToCheckList(clone);
							this.AddToCollection(clone);
						}
						else
						{
							this.AddToCollection(target);
						}
					}
				}
				else
				{
					while (source.Length >= this.Length)
					{
						T[] target = new T[this.Length];
						target[0] = source[0];

						source = this.SliceArray(source, 1);

						this.GenerateCombinationsInternal(source, target, 0);
					}
				}
			}

			private void GenerateCombinationsInternal(T[] source, T[] target, int index)
			{
				if (this.Length == index + 2)
				{
					for (int i = 0; i < source.Length; i++)
					{
						target[index + 1] = source[i];

						if (this.NeedCheck)
						{
							if (!this.ItemExists(target))
							{
								var clone = this.CopyArray(target);
								this.AddToCheckList(clone);
								this.AddToCollection(clone);
							}
						}
						else
						{
							this.AddToCollection(this.CopyArray(target));
						}
					}
				}
				else
				{
					for (int i = 0; i < source.Length - (this.Length - index - 2); i++)
					{
						target[index + 1] = source[i];

						T[] newSource = new T[source.Length - i - 1];
						for (int j = 0; j < newSource.Length; j++)
						{
							newSource[j] = source[i + j + 1];
						}

						this.GenerateCombinationsInternal(newSource, target, index + 1);
					}
				}
			}
		}

		/// <summary>
		/// 要素の重複ありの順列を生成するクラス
		/// </summary>
		private class AllowDuplicatePermutationGenerator : CombinatoricsGenerator
		{
			protected override Task CreateProducerTask()
			{
				Task producerTask = Task.Factory.StartNew(() =>
				{
					var wrkSource = this.CopyArray(this.Source).Distinct().OrderBy(x => x).ToArray();

					this.GeneratePermutations(wrkSource);

					this.CompleteAddingToCollection();
				});

				return producerTask;
			}

			private void GeneratePermutations(T[] source)
			{
				if (this.Length > 0)
				{
					for (int i = 0; i < source.Length; i++)
					{
						T[] target = new T[this.Length];
						target[0] = source[i];
						this.GeneratePermutationsInternal(source, target, 0);
					}
				}
			}

			private void GeneratePermutationsInternal(T[] source, T[] target, int index)
			{
				if (index == this.Length - 1)
				{
					var clone = this.CopyArray(target);
					if (this.NeedCheck)
					{
						this.AddToCheckList(clone);
						this.AddToCollection(clone);
					}
					else
					{
						this.AddToCollection(clone);
					}
				}
				else
				{
					for (int i = 0; i < source.Length; i++)
					{
						target[index + 1] = source[i];
						this.GeneratePermutationsInternal(source, target, index + 1);
					}
				}
			}
		}

		/// <summary>
		/// 要素の重複なしの順列を生成するクラス
		/// </summary>
		private class NotAllowDuplicatePermutationGenerator : CombinatoricsGenerator
		{
			protected override void Validate()
			{
				base.Validate();

				if (this.Source.Length < this.Length)
				{
					throw new InvalidOperationException(string.Format(
						"重複なしの場合、順列のサイズは要素の個数以内である必要があります(順列サイズ:{0} / 要素個数:{1})",
						this.Length,
						this.Source.Length));
				}

				int distinctCount = this.Source.Distinct().Count();

				if (this.Source.Length == distinctCount)
				{
					this.NeedCheck = false;
				}
				else
				{
					this.NeedCheck = true;
				}
			}

			protected override Task CreateProducerTask()
			{
				Task producerTask = Task.Factory.StartNew(() =>
				{
					var wrkSource = this.CopyArray(this.Source).OrderBy(x => x).ToArray();

					this.GeneratePermutations(wrkSource);

					this.CompleteAddingToCollection();
				});

				return producerTask;
			}

			private void GeneratePermutations(T[] source)
			{
				if (this.Length > 0)
				{
					for (int i = 0; i < source.Length; i++)
					{
						var target = new T[this.Length];
						target[0] = source[i];

						var newSource = new List<T>();
						for (int j = 0; j < source.Length; j++)
						{
							if (j != i)
							{
								newSource.Add(source[j]);
							}
						}
						this.GeneratePermutationsInternal(newSource.ToArray(), target, 0);
					}
				}
			}

			private void GeneratePermutationsInternal(T[] source, T[] target, int index)
			{
				if (index == this.Length - 1)
				{
					var clone = this.CopyArray(target);
					if (this.NeedCheck)
					{
						this.AddToCheckList(clone);
						this.AddToCollection(clone);
					}
					else
					{
						this.AddToCollection(clone);
					}
				}
				else
				{
					for (int i = 0; i < source.Length; i++)
					{
						target[index + 1] = source[i];

						var newSource = new List<T>();
						for (int j = 0; j < source.Length; j++)
						{
							if (j != i)
							{
								newSource.Add(source[j]);
							}
						}
						this.GeneratePermutationsInternal(newSource.ToArray(), target, index + 1);
					}
				}
			}
		}

		#endregion
	}
	*/	
}
