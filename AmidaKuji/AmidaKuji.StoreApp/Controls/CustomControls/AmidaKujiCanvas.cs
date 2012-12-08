using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using AmidaKuji.Core.Models;

namespace AmidaKuji.StoreApp.Controls.CustomControls
{
	public sealed class AmidaKujiCanvas : Canvas
	{
		#region constructor

		public AmidaKujiCanvas()
		{
		}

		#endregion

		#region field / property

		private List<Shape> _lines = new List<Shape>();

		private const int STREAM_COUNT_DEFAULT = 3;

		public static DependencyProperty StreamCountProperty = DependencyProperty.Register(
			"StreamCount",
			typeof(int),
			typeof(AmidaKujiCanvas),
			new PropertyMetadata(STREAM_COUNT_DEFAULT, OnStreamCountChanged));

		private static void OnStreamCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
		{
			var target = d as AmidaKujiCanvas;
			if (d != null)
			{
				target.Refresh();
			}
		}

		public int StreamCount
		{
			get { return (int)GetValue(StreamCountProperty); }
			set { SetValue(StreamCountProperty, value); }
		}

		private const int BLOCK_COUNT_DEFAULT = 10;

		public static DependencyProperty BlockCountProperty = DependencyProperty.Register(
			"BlockCount",
			typeof(int),
			typeof(AmidaKujiCanvas),
			new PropertyMetadata(BLOCK_COUNT_DEFAULT, OnBlockCountChanged));

		private static void OnBlockCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
		{
			var target = d as AmidaKujiCanvas;
			if (d != null)
			{
				target.Refresh();
			}
		}

		public int BlockCount
		{
			get { return (int)GetValue(BlockCountProperty); }
			set { SetValue(BlockCountProperty, value); }
		}

		#endregion

		#region method

		protected override Size ArrangeOverride(Size finalSize)
		{
			return base.ArrangeOverride(finalSize);
		}

		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			var model = new AmidaModel(this.StreamCount, this.BlockCount, availableSize.Height, availableSize.Width);

			model.Calculate();

			// 既存のLineオブジェクトをクリア
			foreach (var line in _lines)
			{
				if (this.Children.Contains(line))
				{
					this.Children.Remove(line);
				}
			}
			_lines.Clear();

			// あみだくじの縦線を生成
			foreach (var infStream in model.Streams)
			{
				var line = new Line
				{
					X1 = infStream.Line.X1,
					X2 = infStream.Line.X2,
					Y1 = infStream.Line.Y1,
					Y2 = infStream.Line.Y2,
					Stroke = new SolidColorBrush(Colors.White),
					StrokeThickness = 5.0
				};

				_lines.Add(line);
			}

			// あみだくじの横線を生成
			foreach (var infBridge in model.Bridges)
			{
				var line = new Line
				{
					X1 = infBridge.Line.X1,
					X2 = infBridge.Line.X2,
					Y1 = infBridge.Line.Y1,
					Y2 = infBridge.Line.Y2,
					Stroke = new SolidColorBrush(Colors.White),
					StrokeThickness = 5.0
				};

				_lines.Add(line);
			}

			// 生成したLineオブジェクトをCanvasに追加
			foreach (var line in _lines)
			{
				this.Children.Add(line);
			}

			// 子要素のサイズを調整
			foreach (var child in this.Children)
			{
				child.Measure(availableSize);
			}

			return base.MeasureOverride(availableSize);
		}

		private void Refresh()
		{
			this.InvalidateArrange();
			this.InvalidateMeasure();
		}

		#endregion
	}
}
