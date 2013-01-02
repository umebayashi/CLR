using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Umebayashi.Enterprise.SharePoint.Services.Lists;

namespace Umebayashi.Enterprise.SharePoint
{
	public class ListsManager
	{
		#region constructor

		public ListsManager()
		{
			this.Service = new ListsSoapClient();
			//this.Service.ClientCredentials = CredentialCache.DefaultCredentials;
		}

		#endregion

		#region field / property

		private ListsSoapClient Service { get; set; }

		#endregion

		#region method

		public async void GetListCollection()
		{
			var lists = await this.Service.GetListCollectionAsync();
			var result = lists.Body.GetListCollectionResult;
		}

		#endregion
	}
}
