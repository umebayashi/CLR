using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Umebayashi.Enterprise.SharePoint
{
	[TestClass]
	public class ListsManagerTest
	{
		[TestMethod]
		public void NewTest()
		{
			var manager = new ListsManager();
		}

		[TestMethod]
		public void GetListCollectionTest()
		{
			var manager = new ListsManager();

			manager.GetListCollection();
		}
	}
}
