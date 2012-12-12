using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx.Random
{
	public class GammaRandom
	{
		private static readonly object lockObject = new object();
		private static MTRandom mtRandom = new MTRandom();

		public double NextDouble(double a)
		{
			lock (lockObject)
			{
				double x;
				if (a > 1)
				{
					double t = Math.Sqrt(2 * a - 1);
					double u, y;

					do
					{
						do
						{
							do
							{
								x = 1 - mtRandom.NextDouble();
								y = 2 * mtRandom.NextDouble() - 1;
							} while (x * x + y * y > 1);

							y /= x;
							x = t * y + a - 1;
						} while (x <= 0);

						u = (a - 1) * Math.Log(x / (a - 1)) - t * y;
					} while (u < -50 || mtRandom.NextDouble() > (1 + y * y) * Math.Exp(u));
				}
				else
				{
					double t = Math.E / (a + Math.E);
					double y;

					do
					{
						if (mtRandom.NextDouble() < t)
						{
							x = Math.Pow(mtRandom.NextDouble(), 1 / a);
							y = Math.Exp(-x);
						}
						else
						{
							x = 1 - Math.Log(mtRandom.NextDouble());
							y = Math.Pow(x, a - 1);
						}
					} while (mtRandom.NextDouble() >= y);
				}

				return x;
			}
		}
	}
}
