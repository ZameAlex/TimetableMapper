using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;

namespace TimeTableLibrary.Helpers.JsonHelper
{
	public class JsonReader
	{
		public Dictionary<string, string> ReadMappingFile(string fileName)
		{
			string fileContent;
			var result = new Dictionary<string, string>();
			using (var reader = new StreamReader(fileName))
			{
				fileContent = reader.ReadToEnd();
			}

			try
			{
				var jsonContent = JsonConvert.DeserializeObject<MappingTemplate>(fileContent);
				foreach (var pair in jsonContent.Pairs)
				{
					result.Add(pair.RozkladName, pair.FpmId);
				}
			}
			catch (Exception e)
			{
				//Console.WriteLine(e);
				//TODO:Add logger
				return null;
			}
			return result;
		}
	}
}
