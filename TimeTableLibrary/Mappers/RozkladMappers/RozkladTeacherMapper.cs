using System.Collections.Generic;
using System.Linq;
using TimeTableLibrary.Mappers.Interfaces;
using TimeTableLibrary.RozkladModels;
using TimeTableLibrary.RozkladRequests;

namespace TimeTableLibrary.Mappers.RozkladMappers
{
	public class RozkladTeacherMapper : IMapper<string, RozkladTeacher>
	{
		RozkladClient client;
		public RozkladTeacherMapper(RozkladClient client)
		{
			this.client = client;
		}

		public RozkladTeacher Map(string obj)
		{
			return client.Teachers.SingleOrDefault(t=>t.Name==obj);
		}

		public IEnumerable<RozkladTeacher> Map(IEnumerable<string> objs)
		{
			var result = new List<RozkladTeacher>();
			foreach (var item in objs)
			{
				result.Add(Map(item));
			}
			return result;
		}
	}
}
