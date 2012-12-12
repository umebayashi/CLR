using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SysMath = System.Math;

namespace Umebayashi.MathEx
{
	public static class MathUtil
	{
		public static int Factorial(int n)
		{
			if (n == 1)
			{
				return 1;
			}
			else
			{
				return n * Factorial(n - 1);
			}
		}
	}
}
