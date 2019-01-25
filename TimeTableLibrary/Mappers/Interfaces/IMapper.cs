using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableLibrary.Mappers.Interfaces
{
	public interface IMapper<TObject,TResult>
	{
		TResult Map(TObject obj);

		IEnumerable<TResult> Map(IEnumerable<TObject> objs);
	}
}
