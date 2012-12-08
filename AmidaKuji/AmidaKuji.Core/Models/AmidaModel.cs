using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmidaKuji.Core.Models
{
	public class AmidaModel
	{
		public AmidaModel()
		{
			this.Streams = new List<StreamInfo>();
			this.Bridges = new List<BridgeInfo>();
		}

		#region field / property

		public int StreamCount { get; set; }

		public int BlockCount { get; set; }

		public int MaxBranchCount { get; set; }

		public double CanvasHeight { get; set; }

		public double CanvasWidth { get; set; }

		public Margin InnerMargin { get; set; }

		public List<StreamInfo> Streams { get; private set; }

		public List<BridgeInfo> Bridges { get; private set; }

		private static Random _random = new Random(DateTime.Now.Millisecond);

		#endregion

		#region method

		public void Calculate()
		{
			this.Streams.Clear();
			this.Bridges.Clear();

			var x = this.InnerMargin.Left;
			var xDiff = (this.CanvasWidth - this.InnerMargin.Left - this.InnerMargin.Right) / (this.StreamCount - 1);
			var y1 = this.InnerMargin.Top;
			var y2 = this.CanvasHeight - this.InnerMargin.Bottom;
			var yLen = y2 - y1;
			var blockLen = yLen / this.BlockCount;
			var prevBridges = new List<int>();
			var curBridges = new List<int>();

			for (int i = 0; i < this.StreamCount; i++)
			{
				// 縦線情報の生成
				var infStream = new StreamInfo
				{
					Index = i,
					Line = new LineInfo
					{
						X1 = x,
						Y1 = y1,
						X2 = x,
						Y2 = y2
					}
				};
				this.Streams.Add(infStream);

				// 横線情報の生成
				if (i < this.StreamCount - 1)
				{
					var branchCount = _random.Next(this.MaxBranchCount - 1) + 1;
					curBridges.Clear();
					for (int i2 = 0; i2 < branchCount; i2++)
					{
						var bIndex = _random.Next(this.BlockCount - 1);
						while (curBridges.Contains(bIndex) || prevBridges.Contains(bIndex))
						{
							bIndex = _random.Next(this.BlockCount - 1);
						}
						curBridges.Add(bIndex);

						var yBridge = y1 + (bIndex + 1) * blockLen;
						var infBridge = new BridgeInfo
						{
							Target = infStream,
							Line = new LineInfo
							{
								X1 = x,
								Y1 = yBridge,
								X2 = x + xDiff,
								Y2 = yBridge
							}
						};

						this.Bridges.Add(infBridge);
					}

					prevBridges.Clear();
					prevBridges.AddRange(curBridges);
				}

				x += xDiff;
			}

			// 横線情報の生成
			//var dList = new List<double>();
			//for (int i1 = 0; i1 < 3; i1++)
			//{
			//	for (int i2 = 0; i2 < this.StreamCount - 1; i2++)
			//	{
			//		var d = _random.NextDouble();
			//		while (dList.Contains(d))
			//		{
			//			d = _random.NextDouble();
			//		}
			//		dList.Add(d);

			//		var position = yLen * d;
			//		var index1 = _random.Next(this.StreamCount - 1);
			//		var stream1 = this.Streams[index1];
			//		var stream2 = this.Streams[index1 + 1];
			//		this.Bridges.Add(new BridgeInfo
			//		{
			//			Stream1 = stream1,
			//			Stream2 = stream2,
			//			Position = position,
			//			Line = new LineInfo
			//			{
			//				X1 = stream1.Line.X1,
			//				Y1 = position + this.InnerMargin.Top,
			//				X2 = stream2.Line.X1,
			//				Y2 = position + this.InnerMargin.Top
			//			}
			//		});
			//	}
			//}
		}

		#endregion
	}
}
