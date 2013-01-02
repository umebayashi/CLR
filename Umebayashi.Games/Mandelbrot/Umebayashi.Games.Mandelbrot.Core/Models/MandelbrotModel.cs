using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umebayashi.MathEx;
using Umebayashi.Games.Models;
namespace Umebayashi.Games.Mandelbrot.Core.Models
{
	public class MandelbrotModel : ObservableModelBase
	{
		#region constructor

		public MandelbrotModel(int calculationLimit)
		{
			this.CalculationLimit = calculationLimit;
		}

		#endregion

		#region field / property

		public int CalculationLimit { get; private set; }

		#endregion

		#region method

		public void Calculate()
		{
		}

		#endregion
	}
}
