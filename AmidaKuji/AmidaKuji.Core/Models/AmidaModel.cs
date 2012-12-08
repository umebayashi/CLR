using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmidaKuji.Core.Models
{
	public class AmidaModel
	{
		public AmidaModel(int streamCount, int blockCount, double canvasHeight, double canvasWidth)
		{
			this.StreamCount = streamCount;
			this.BlockCount = blockCount;
			this.CanvasHeight = canvasHeight;
			this.CanvasWidth = canvasWidth;

			this.Streams = new List<StreamInfo>();
			this.Bridges = new List<BridgeInfo>();
		}

		#region field / property

		public int StreamCount { get; set; }

		public int BlockCount { get; set; }

		public double CanvasHeight { get; set; }

		public double CanvasWidth { get; set; }

		public List<StreamInfo> Streams { get; private set; }

		public List<BridgeInfo> Bridges { get; private set; }

		private static Random _random = new Random(DateTime.Now.Millisecond);

		#endregion

		#region method

		public void Calculate()
		{
			this.Streams.Clear();
			this.Bridges.Clear();

			var x = 100.0;
			var xDiff = (this.CanvasWidth - 200.0) / (this.StreamCount - 1);
			var y1 = 100.0;
			var y2 = this.CanvasHeight - 100.0;

			// 縦線情報の生成
			for (int i = 0; i < this.StreamCount; i++)
			{
				this.Streams.Add(new StreamInfo
				{
					Index = i,
					Line = new LineInfo
					{
						X1 = x,
						Y1 = y1,
						X2 = x,
						Y2 = y2
					}
				});

				x += xDiff;
			}

			// 横線情報の生成
			var yLen = y2 - y1;
			var dList = new List<double>();
			for (int i1 = 0; i1 < 3; i1++)
			{
				for (int i2 = 0; i2 < this.StreamCount - 1; i2++)
				{
					var d = _random.NextDouble();
					while (dList.Contains(d))
					{
						d = _random.NextDouble();
					}
					dList.Add(d);

					var position = yLen * d;
					var index1 = _random.Next(this.StreamCount - 1);
					//var index1 = i2;
					var stream1 = this.Streams[index1];
					var stream2 = this.Streams[index1 + 1];
					this.Bridges.Add(new BridgeInfo
					{
						Stream1 = stream1,
						Stream2 = stream2,
						Position = position,
						Line = new LineInfo
						{
							X1 = stream1.Line.X1,
							Y1 = position + 100.0,
							X2 = stream2.Line.X1,
							Y2 = position + 100.0
						}
					});
				}
			}
		}

		#endregion
	}
}
