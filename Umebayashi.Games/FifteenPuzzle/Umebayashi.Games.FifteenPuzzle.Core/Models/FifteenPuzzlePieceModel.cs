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

		#endregion
	}
}
