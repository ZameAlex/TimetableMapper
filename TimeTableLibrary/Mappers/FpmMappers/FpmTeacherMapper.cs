using System.Collections.Generic;
using System.Linq;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.Mappers.Interfaces;

namespace TimeTableLibrary.Mappers.FpmMappers
{
	public class FpmTeacherMapper : IMapper<string, FpmTeacher>
	{
		FpmClient client;
		public FpmTeacherMapper(FpmClient client)
		{
			this.client = client;
		}

		public FpmTeacher Map(string obj)
		{
			return client.Teachers.SingleOrDefault(t=>t.Name==obj);
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
