using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Umebayashi.Games.Common.ViewModels;

// テンプレート コントロールのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234235 を参照してください

namespace Umebayashi.Games.Common.Controls
{
	public class GameBoardSquareControl : Control
	{
		#region constructor

		public GameBoardSquareControl()
		{
			this.DefaultStyleKey = typeof(GameBoardSquareControl);
		}

		#endregion

		#region field / property

		private GameBoardSquareViewModel _viewModel;

		protected GameBoardSquareViewModel ViewModel
		{
			get { return _viewModel; }
		}

		#endregion

		protected internal virtual void SetViewModel(GameBoardSquareViewModel viewModel)
		{
			_viewModel = viewModel;
			this.DataContext = viewModel;
		}
	}
}
