using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableLibrary.FpmRequests;

namespace CoreUI.Services.Interfaces
{
	internal abstract class SetService<T,V>
	{
		private FpmClient client;
		public SetService(FpmClient client)
		{
			this.client = client;
		}
		public virtual void SetObjects (T dependence, List<V> objects)
		{
			client
		}
	}
}
