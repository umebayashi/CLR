using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmidaKuji.Core.Models
{
	public class BridgeInfo
	{
		public StreamInfo Stream1 { get; set; }

		public StreamInfo Stream2 { get; set; }

		public double Position { get; set; }

		public LineInfo Line { get; set; }
	}
}
