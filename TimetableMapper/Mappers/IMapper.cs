using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableMapper.Mappers
{
	interface IMapper<T, R> 
	where T:class
	where R:class
	{
		R Map(T model);
		List<R> Map(List<T> models);
	}
}
