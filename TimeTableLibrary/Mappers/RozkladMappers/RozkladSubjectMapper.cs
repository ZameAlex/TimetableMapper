using System.Collections.Generic;
using System.Linq;
using TimeTableLibrary.Mappers.Interfaces;
using TimeTableLibrary.RozkladModels;
using TimeTableLibrary.RozkladRequests;

namespace TimeTableLibrary.Mappers.RozkladMappers
{
	public class RozkladSubjectMapper : IMapper<string, RozkladSubject>
	{
		RozkladClient client;
		public RozkladSubjectMapper(RozkladClient client)
		{
			this.client = client;
		}

		public RozkladSubject Map(string obj)
		{
			return client.Subjects.SingleOrDefault(s => s.Title == obj);
		}

		public IEnumerable<RozkladSubject> Map(IEnumerable<string> objs)
		{
			var result = new List<RozkladSubject>();
			foreach (var item in objs)
			{
				result.Add(Map(item));
			}
			return result;
		}
	}
}
