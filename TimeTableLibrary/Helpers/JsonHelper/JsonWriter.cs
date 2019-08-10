using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;

namespace TimeTableLibrary.Helpers.JsonHelper
{
	public class JsonWriter
	{
		public void WriteMappingToFile(Dictionary<string, string> pairs, string fileName, string mappingName)
		{
			try
			{
				using (var writer = new StreamWriter(fileName, false))
				{
					var mappingObject = new MappingTemplate()
					{
						Mapping = mappingName,
						Pairs = ConvertDictionaryToPairs(pairs)
					};
					var fileContent = JsonConvert.SerializeObject(mappingObject);
					writer.Write(fileContent);
				}
			}
			catch (Exception e)
			{
				//TODO:Add logger
				return;
			}
		}

		private Pair[] ConvertDictionaryToPairs(Dictionary<string, string> dictionary)
		{
			var result = new List<Pair>();
			foreach (var pair in dictionary)
			{
				result.Add(new Pair
				{
					RozkladName = pair.Key,
					FpmId = pair.Value
				});
			}

			return result.ToArray();
		}
	}
}
