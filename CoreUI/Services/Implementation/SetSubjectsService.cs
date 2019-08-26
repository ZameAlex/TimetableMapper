using CoreUI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.FpmRequests;

namespace CoreUI.Services.Implementation
{
	public class SetSubjectsService:SetService<FpmGroup>
	{
		public SetSubjectsService(FpmClient client) : base(client)
		{
		}
	}
}
