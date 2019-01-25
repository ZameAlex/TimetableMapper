using System.Collections.Generic;
using System.Linq;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.Mappers.Interfaces;

namespace TimeTableLibrary.Mappers.FpmMappers
{
	public class FpmSubjectMapper : IMapper<string, FpmSubject>
	{
		FpmClient client;
		public FpmSubjectMapper(FpmClient client)
		{
			this.client = client;
		}

		public FpmSubject Map(string obj)
		{
			return client.Subjects.SingleOrDefault(s => s.Id == obj);
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
