using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx
{
	public static class MathExtensions
	{
		#region static method

		#endregion

		#region extention method

		/// <summary>
		/// 階乗
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static int Factorial(this int value)
		{
			if (value < 0)
			{
				throw new ArgumentException("1未満の値は指定できません");
			}
			else if (value == 0 || value == 1)
			{
				return 1;
			}
			else
			{
				return value * Factorial(value - 1);
			}
		}

		/// <summary>
		/// 階乗
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static long Factorial(this long value)
		{
			if (value < 0L)
			{
				throw new ArgumentException("1未満の値は指定できません");
			}
			else if (value == 0L || value == 1L)
			{
				return 1L;
			}
			else
			{
				return value * Factorial(value - 1L);
			}
		}

		#endregion
	}
}
