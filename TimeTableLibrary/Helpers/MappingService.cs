using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableLibrary.Helpers
{
	public class MappingService
	{
		private JsonHelper.JsonWriter _writer;
		private JsonHelper.JsonReader _reader;
		private string _teachersMappingFilePath;
		private string _subjectsMappingFilePath;

		public Dictionary<string, FpmModels.FpmSubject> Subjects { get; protected set; }
		public Dictionary<string, FpmModels.FpmTeacher> Teachers { get; protected set; }

		public MappingService(JsonHelper.JsonReader reader, JsonHelper.JsonWriter writer, string teachersMappingFilePath, string subjectsMappingFilePath)
		{
			_writer = writer;
			_reader = reader;
			_subjectsMappingFilePath = subjectsMappingFilePath;
			_teachersMappingFilePath = teachersMappingFilePath;
		}

		private void ReadFile(string filePath)
		{
			//FpmRequests.FpmClient.Instance.
		}

	}
}
