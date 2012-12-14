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

		public FifteenPuzzleModel()
		{
		}

		#endregion

		#region field / property

		public int Size
		{
			get;
			set;
		}

		private MTRandom _randomM = new MTRandom();
		private NormalRandom _randomN = new NormalRandom();

		private List<FifteenPuzzlePieceModel> _pieces = new List<FifteenPuzzlePieceModel>();

		public List<FifteenPuzzlePieceModel> Pieces
		{
			get { return _pieces; }
		}

		private Stack<FifteenPuzzlePieceModel> _targetHistory = new Stack<FifteenPuzzlePieceModel>();

		#endregion

		#region method

		public void Initialize()
		{
			this.Pieces.Clear();

			int number = 0;
			int max = this.Size * this.Size;
			for (int r = 0; r < this.Size; r++)
			{
				for (int c = 0; c < this.Size; c++)
				{
					number++;
					var piece = new FifteenPuzzlePieceModel(this);
					piece.Number = number;
					piece.Row = r;
					piece.Column = c;
					piece.IsEmpty = (number == max);
					//{
					//	Number = number,
					//	Row = r,
					//	Column = c,
					//	IsEmpty = (number == max)
					//};
					this.Pieces.Add(piece);
				}
			}
		}

		public void Shuffle()
		{
			var neighbor = new List<FifteenPuzzlePieceModel>();
			for (int i = 0; i < 10 * (this.Size - 2); i++)
			{
				neighbor.Clear();

				var pEmpty = this.Pieces.Where(x => x.IsEmpty).First();

				// 上のコマが存在するか
				if (pEmpty.Row > 0)
				{
					neighbor.Add(this.Pieces.Where(x => (x.Row == pEmpty.Row - 1) && (x.Column == pEmpty.Column)).First());
				}
				// 右のコマが存在するか
				if (pEmpty.Column < this.Size - 1)
				{
					neighbor.Add(this.Pieces.Where(x => (x.Row == pEmpty.Row) && (x.Column == pEmpty.Column + 1)).First());
				}
				// 下のコマが存在するか
				if (pEmpty.Row < this.Size - 1)
				{
					neighbor.Add(this.Pieces.Where(x => (x.Row == pEmpty.Row + 1) && (x.Column == pEmpty.Column)).First());
				}
				// 左のコマが存在するか
				if (pEmpty.Column > 0)
				{
					neighbor.Add(this.Pieces.Where(x => (x.Row == pEmpty.Row) && (x.Column == pEmpty.Column - 1)).First());
				}

				// 直前に動かしたコマは移動対象から外す
				FifteenPuzzlePieceModel lastTarget = null;
				if (_targetHistory.Count > 0)
				{
					lastTarget = _targetHistory.Peek();
				}

				if (lastTarget != null)
				{
					neighbor.Remove(lastTarget);
				}

				// コマを移動する
				var index = _randomM.Next(neighbor.Count);
				var target = neighbor[index];
				this.Swap(pEmpty, target);

				// 移動履歴を追加する
				_targetHistory.Push(target);
			}
		}

		private void Swap(FifteenPuzzlePieceModel piece1, FifteenPuzzlePieceModel piece2)
		{
			int r1 = piece1.Row;
			int c1 = piece1.Column;
			int r2 = piece2.Row;
			int c2 = piece2.Column;
			piece1.Row = r2;
			piece1.Column = c2;
			piece2.Row = r1;
			piece2.Column = c1;
		}

		public void MovePiece(FifteenPuzzlePieceModel piece)
		{
			var pEmpty = this.Pieces.Where(x => x.IsEmpty).First();

			// 下が空白コマか
			if (piece.Row == pEmpty.Row - 1 && piece.Column == pEmpty.Column)
			{
				this.Swap(pEmpty, piece);
				return;
			}

			// 左が空白コマか
			if (piece.Row == pEmpty.Row && piece.Column == pEmpty.Column + 1)
			{
				this.Swap(pEmpty, piece);
				return;
			}

			// 上が空白コマか
			if (piece.Row == pEmpty.Row + 1 && piece.Column == pEmpty.Column)
			{
				this.Swap(pEmpty, piece);
				return;
			}

			// 右が空白コマか
			if (piece.Row == pEmpty.Row && piece.Column == pEmpty.Column - 1)
			{
				this.Swap(pEmpty, piece);
				return;
			}
		}

		#endregion
	}
}
