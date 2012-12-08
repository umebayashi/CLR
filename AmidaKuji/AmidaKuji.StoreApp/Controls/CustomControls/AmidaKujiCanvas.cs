using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.ApplicationModel;
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

		#region StreamCount

		private const int STREAM_COUNT_DEFAULT = 3;

		public static DependencyProperty StreamCountProperty = DependencyProperty.Register(
			"StreamCount",
			typeof(int),
			typeof(AmidaKujiCanvas),
			new PropertyMetadata(STREAM_COUNT_DEFAULT, OnStreamCountChanged));

		private static void OnStreamCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
		{
			var target = d as AmidaKujiCanvas;
			if (target != null)
			{
				target.Refresh();
			}
		}

		public int StreamCount
		{
			get { return (int)GetValue(StreamCountProperty); }
			set { SetValue(StreamCountProperty, value); }
		}

		#endregion

		#region BlockCount

		private const int BLOCK_COUNT_DEFAULT = 10;

		public static DependencyProperty BlockCountProperty = DependencyProperty.Register(
			"BlockCount",
			typeof(int),
			typeof(AmidaKujiCanvas),
			new PropertyMetadata(BLOCK_COUNT_DEFAULT, OnBlockCountChanged));

		private static void OnBlockCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
		{
			var target = d as AmidaKujiCanvas;
			if (target != null)
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

		#region MaxBranchCount

		private const int MAX_BRANCH_COUNT_DEFAULT = 5;

		public static readonly DependencyProperty MaxBranchCountProperty = DependencyProperty.Register(
			"MaxBranchCount",
			typeof(int),
			typeof(AmidaKujiCanvas),
			new PropertyMetadata(MAX_BRANCH_COUNT_DEFAULT, OnMaxBranchCountChanged));

		private static void OnMaxBranchCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
		{
			var target = d as AmidaKujiCanvas;
			if (target != null)
			{
				target.Refresh();
			}
		}

		public int MaxBranchCount
		{
			get { return (int)GetValue(MaxBranchCountProperty); }
			set { SetValue(MaxBranchCountProperty, value); }
		}

		#endregion

		#region InnerMargin

		private static readonly Thickness DefaultInnerMargin = new Thickness(100.0);

		public static readonly DependencyProperty InnerMarginProperty = DependencyProperty.Register(
			"InnerMargin",
			typeof(Thickness),
			typeof(AmidaKujiCanvas),
			new PropertyMetadata(DefaultInnerMargin, OnInnerMarginChanged));

		private static void OnInnerMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
		{
			var target = d as AmidaKujiCanvas;
			if (target != null)
			{
				target.Refresh();
			}
		}

		public Thickness InnerMargin
		{
			get { return (Thickness)GetValue(InnerMarginProperty); }
			set { SetValue(InnerMarginProperty, value); }
		}

		#endregion

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
			if (!DesignMode.DesignModeEnabled)
			{
				var model = new AmidaModel
				{
					StreamCount = this.StreamCount,
					BlockCount = this.BlockCount,
					MaxBranchCount = this.MaxBranchCount,
					CanvasHeight = availableSize.Height,
					CanvasWidth = availableSize.Width,
					InnerMargin = new Margin
					{
						Left = this.InnerMargin.Left,
						Top = this.InnerMargin.Top,
						Right = this.InnerMargin.Right,
						Bottom = this.InnerMargin.Bottom
					}
				};

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
			}

			return base.MeasureOverride(availableSize);
		}

		public void Refresh()
		{
			this.InvalidateArrange();
			this.InvalidateMeasure();
		}

		#endregion
	}
}
