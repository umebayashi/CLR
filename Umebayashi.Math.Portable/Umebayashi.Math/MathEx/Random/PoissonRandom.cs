using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx.Random
{
	public class PoissonRandom
	{
		#region constructor

		public PoissonRandom(double lambda)
		{
			this.random = new MTRandom();
			this.lambda = lambda;
		}
		
		public PoissonRandom(int[] init_key, double lambda)
		{
			this.random = new MTRandom(init_key);
			this.lambda = lambda;
		}

		#endregion

		#region field

		private MTRandom random;

		private double lambda;

		#endregion

		#region method

		public int Next()
		{
			var tmpLambda = Math.Exp(this.lambda) * random.NextDouble();
			int k = 0;
			while (tmpLambda > 1)
			{
				tmpLambda *= random.NextDouble();
				k++;
			}

			return k;
		}

		#endregion
	}
}
