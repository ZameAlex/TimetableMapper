using CoreUI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTableLibrary.FpmModels;

namespace CoreUI.Services.Implementation
{
	internal class SetTeacherService : SetService<FpmTeacher, FpmSubject>
	{
		public void SetObjects(FpmTeacher dependence, List<FpmSubject> objects)
		{
			throw new NotImplementedException();
		}
	}
}
