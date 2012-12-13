using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.Games.FifteenPuzzle.Core.Models
{
	public class FifteenPuzzlePieceModel
	{
		#region constructor

		public FifteenPuzzlePieceModel()
		{
		}

		#endregion

		#region field / property

		public int Number { get; set; }

		public int Row { get; set; }

		public int Column { get; set; }

		public bool IsEmpty { get; set; }

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

		#endregion
	}
}
