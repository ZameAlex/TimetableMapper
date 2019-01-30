using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.FpmRequests;

namespace CoreUI.Services.Interfaces
{
	internal abstract class SetService<T>
	{
		private FpmClient client;
		public SetService(FpmClient client)
		{
			this.client = client;
		}
		public async virtual void SetObjects (T dependency, List<FpmSubject> objects)
		{
			await client.SetDependencies<T>(dependency, objects);
		}
	}
}
