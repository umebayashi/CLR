using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// テンプレート コントロールのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234235 を参照してください

namespace Umebayashi.Games.FifteenPuzzle.StoreApp.Controls
{
	public sealed class PuzzlePiece : Control
	{
		#region constructor

		public PuzzlePiece()
		{
			this.DefaultStyleKey = typeof(PuzzlePiece);
		}

		#endregion

		#region field / property

		#region Number

		public static readonly DependencyProperty NumberProperty = DependencyProperty.Register(
			"Number",
			typeof(int),
			typeof(PuzzlePiece),
			new PropertyMetadata(0, OnNumberChanged));

		private static void OnNumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
		{
			var target = d as PuzzlePiece;
			if (target != null)
			{
			}
		}

		public int Number
		{
			get { return (int)GetValue(NumberProperty); }
			set { SetValue(NumberProperty, value); }
		}

		#endregion

		#endregion

		#region method

		protected override void OnPointerPressed(PointerRoutedEventArgs e)
		{
			base.OnPointerPressed(e);

			var row = (int)this.GetValue(Grid.RowProperty);
			var column = (int)this.GetValue(Grid.ColumnProperty);

		}

		protected override void OnPointerMoved(PointerRoutedEventArgs e)
		{
			base.OnPointerMoved(e);
			if (e.Pointer.IsInContact)
			{
			}
		}

		protected override void OnPointerReleased(PointerRoutedEventArgs e)
		{
			base.OnPointerReleased(e);
		}

		#endregion
	}
}
