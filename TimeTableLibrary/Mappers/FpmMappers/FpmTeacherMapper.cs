using System.Collections.Generic;
using System.Linq;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.Helpers;
using TimeTableLibrary.Mappers.Interfaces;

namespace TimeTableLibrary.Mappers.FpmMappers
{
	public class FpmTeacherMapper : IMapper<string, FpmTeacher>
	{
		MappingService service;
		public FpmTeacherMapper(MappingService service)
		{
			this.service = service;
		}

		public FpmTeacher Map(string obj)
		{
			return service.Teachers[obj];
		}

		public IEnumerable<FpmTeacher> Map(IEnumerable<string> objs)
		{
			var result = new List<FpmTeacher>();
			foreach (var item in objs)
			{
				result.Add(Map(item));
			}
			return result;
		}
	}
}
