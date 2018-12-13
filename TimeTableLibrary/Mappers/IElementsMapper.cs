using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableLibrary.Mappers
{
	public interface IElementsMapper<T,R>
	{
		Dictionary<T, List<R>> Map(List<R> fpmElements, List<T> rozkladElements);
	}
}
