using System.Collections.Generic;
using System.Linq;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.Helpers;
using TimeTableLibrary.Mappers.Interfaces;

namespace TimeTableLibrary.Mappers.FpmMappers
{
	public class FpmSubjectMapper : IMapper<string, FpmSubject>
	{
		MappingService service;
		public FpmSubjectMapper(MappingService service)
		{
			this.service = service;
		}

		public FpmSubject Map(string obj)
		{
			return service.Subjects[obj];
		}

		public IEnumerable<FpmSubject> Map(IEnumerable<string> objs)
		{
			var result = new List<FpmSubject>();
			foreach (var item in objs)
			{
				result.Add(Map(item));
			}
			return result;
		}
	}
}
