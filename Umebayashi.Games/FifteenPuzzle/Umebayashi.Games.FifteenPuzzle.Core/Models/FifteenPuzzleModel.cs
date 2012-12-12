using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umebayashi.MathEx.Random;

namespace Umebayashi.Games.FifteenPuzzle.Core.Models
{
	public class FifteenPuzzleModel
	{
		#region constructor

		public FifteenPuzzleModel(int size)
		{
			this.Size = size;
		}

		#endregion

		#region field / property

		public int Size
		{
			get;
			private set;
		}

		private NormalRandom _random = new NormalRandom();

		private List<FifteenPuzzlePieceModel> _pieces = new List<FifteenPuzzlePieceModel>();

		public List<FifteenPuzzlePieceModel> Pieces
		{
			get { return _pieces; }
		}

		#endregion

		#region method

		public void Initialize()
		{
			this.Pieces.Clear();

			int number = 1;
			for (int r = 0; r < this.Size; r++)
			{
				for (int c = 0; c < this.Size; c++)
				{
					var piece = new FifteenPuzzlePieceModel
					{
						Number = number,
						Row = r,
						Column = c
					};
					this.Pieces.Add(piece);
				}
			}
		}

		public void Shuffle()
		{
		}

		#endregion
	}
}
