using CoreUI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.FpmRequests;

namespace CoreUI.Services
{
	internal class SetSubjectsService:SetService<FpmGroup,FpmSubject>
	{
		private FpmClient client;
		public SetSubjectsService()
		{

		}

		public void SetObjects(FpmGroup dependence, List<FpmSubject> objects)
		{
			throw new NotImplementedException();
		}
	}
}
