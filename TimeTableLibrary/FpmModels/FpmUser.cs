using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableLibrary.FpmModels
{
	public class FpmUser
	{
		public string Login { get; set; }
		public string Password { get; set; }

		public FpmUser()
		{

		}

		public FpmUser(string login, string password)
		{
			Login = login;
			Password = password;
		}
	}
}
