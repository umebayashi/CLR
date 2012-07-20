using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Umebayashi.MathEx.Random
{
	/// <summary>
	/// メルセンヌ・ツイスタ乱数クラス
	/// </summary>
	public class MTRandom
	{
		#region const

		private static object lockObj = new object();

		private const int N = 624;

		private const int M = 397;

		private const uint MATRIX_A = 0x9908b0dfU;

		private const uint UPPER_MASK = 0x80000000U;

		private const uint LOWER_MASK = 0x7fffffffU;

		#endregion

		#region constructor

		public MTRandom() : this(new int[] { 
			DateTime.Now.Year,
			DateTime.Now.Month,
			DateTime.Now.Day,
			DateTime.Now.Hour, 
			DateTime.Now.Minute, 
			DateTime.Now.Second, 
			DateTime.Now.Millisecond })
		{
		}

		public MTRandom(int[] init_key)
		{
			uint[] init = init_key.Select(x => (uint)x).ToArray();
			InitByArray(init, init.Length);
		}

		#endregion

		#region field

		private uint[] mt = new uint[N];

		private uint mti = N + 1;

		#endregion

		#region method

		private void InitGenRand(uint s)
		{
			mt[0] = s & 0xffffffff;
			for (mti = 1; mti < N; mti++)
			{
				mt[mti] = (1812433253U * (mt[mti - 1] ^ (mt[mti - 1] >> 30)) + mti);
				mt[mti] &= 0xffffffffU;
			}
		}

		private void InitByArray(uint[] init_key, int key_length)
		{
			InitGenRand(19650218U);

			int i = 1;
			int j = 0;
			for (int k = (N > key_length ? N : key_length); k > 0; k--)
			{
				mt[i] =
					(mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1664525U)) +
					init_key[j] +
					(uint)j;
				mt[i] &= 0xffffffffU;
				i++;
				j++;
				if (i >= N)
				{
					mt[0] = mt[N - 1];
					i = 1;
				}
				if (j >= key_length)
				{
					j = 0;
				}
			}
			for (int k = N - 1; k > 0; k--)
			{
				mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1566083941U)) - 1;
				mt[i] &= 0xffffffffU;
				i++;
				if (i >= N)
				{
					mt[0] = mt[N - 1];
					i = 1;
				}
			}

			mt[0] = 0x80000000U;
		}

		private uint GenRandInt32()
		{
			uint y;
			uint[] mag01 = new uint[] { 0x0U, MATRIX_A };

			if (mti >= N)
			{
				if (mti == N + 1)
				{
					InitGenRand(5489U);
				}
				for (int kk = 0; kk < N - M; kk++)
				{
					y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
					mt[kk] = mt[kk + M] ^ (y >> 1) ^ mag01[y & 0x1U];
				}
				for (int kk = N - M; kk < N - 1; kk++)
				{
					y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
					mt[kk] = mt[kk + (M - N)] ^ (y >> 1) ^ mag01[y & 0x1U];
				}
				y = (mt[N - 1] & UPPER_MASK) | (mt[0] & LOWER_MASK);
				mt[N - 1] = mt[M - 1] ^ (y >> 1) ^ mag01[y & 0x1U];

				mti = 0;
			}

			y = mt[mti++];

			y ^= (y >> 11);
			y ^= (y << 7) & 0x9d2c5680U;
			y ^= (y << 15) & 0xefc60000U;
			y ^= (y >> 18);

			return y;
		}

		private int GenRandInt31()
		{
			return (int)(GenRandInt32() >> 1);
		}

		private double GenRandReal1()
		{
			return GenRandInt32() * (1.0 / 4294967295.0);
		}

		private double GenRandReal2()
		{
			return GenRandInt32() * (1.0 / 4294967296.0);
		}

		private double GenRandReal3()
		{
			return ((double)GenRandInt32() + 0.5) * (1.0 / 4294967296); 
		}

		#endregion

		#region public method

		/// <summary>
		/// 0以上の乱数を返す
		/// </summary>
		/// <returns></returns>
		public int Next()
		{
			return GenRandInt31();
		}

		/// <summary>
		/// 指定した最大値より小さい0以上の乱数を返す
		/// </summary>
		/// <param name="maxValue"></param>
		/// <returns></returns>
		public int Next(int maxValue)
		{
			lock (lockObj)
			{
				var value = GenRandInt31() % maxValue;
				return value;
			}
		}

		/// <summary>
		/// 指定した範囲内の乱数を返す
		/// </summary>
		/// <param name="minValue"></param>
		/// <param name="maxValue"></param>
		/// <returns></returns>
		public int Next(int minValue, int maxValue)
		{
			lock (lockObj)
			{
				var value = (GenRandInt31() % (maxValue - minValue + 1)) + minValue;
				return value;
			}
		}

		/// <summary>
		/// 0.0と1.0の間の乱数を返す
		/// </summary>
		/// <returns></returns>
		public double NextDouble()
		{
			lock (lockObj)
			{
				return GenRandReal1();
			}
		}

		#endregion
	}
}
