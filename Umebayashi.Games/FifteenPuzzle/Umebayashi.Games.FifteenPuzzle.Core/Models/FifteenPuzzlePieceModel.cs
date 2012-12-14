using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Umebayashi.Games.Models;

namespace Umebayashi.Games.FifteenPuzzle.Core.Models
{
	public class FifteenPuzzlePieceModel : ObservableModelBase
	{
		#region constructor

		public FifteenPuzzlePieceModel(FifteenPuzzleModel puzzle)
		{
			this.Puzzle = puzzle;
		}

		#endregion

		#region field / property

		public FifteenPuzzleModel Puzzle { get; private set; }

		private int _number;

		public int Number 
		{ 
			get { return _number; }
			set
			{
				if (_number != value)
				{
					this._number = value;
					this.OnPropertyChanged("Number");
				}
			}
		}

		private int _row;

		public int Row 
		{
			get { return _row; }
			set
			{
				if (_row != value)
				{
					_row = value;
					this.OnPropertyChanged("Row");
				}
			}
		}

		private int _column;

		public int Column 
		{
			get { return _column; }
			set
			{
				if (_column != value)
				{
					_column = value;
					this.OnPropertyChanged("Column");
				}
			}
		}

		private bool _isEmpty;

		public bool IsEmpty 
		{
			get { return _isEmpty; }
			set
			{
				if (_isEmpty != value)
				{
					_isEmpty = value;
					this.OnPropertyChanged("IsEmpty");
				}
			}
		}

		#endregion

		#region method

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("(Number: {0} / Row: {1} / Column: {2} / IsEmpty: {3})",
				this.Number,
				this.Row,
				this.Column,
				this.IsEmpty);
		}

		public void MovePiece()
		{
			this.Puzzle.MovePiece(this);
		}

		#endregion
	}
}
