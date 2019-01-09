using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.Mappers;
using TimeTableLibrary.RozkladModels;
using TimeTableLibrary.RozkladRequests;

namespace SimpleUI
{
	public partial class TeachersMapping : Form
	{
		FpmClient fpmClient;
		RozkladClient rozkladClient;
		Dictionary<string, string> mappedTeachers;
		public TeachersMapping()
		{
			InitializeComponent();
		}

		public TeachersMapping(FpmClient fpmClient, RozkladClient rozkladClient):this()
		{
			this.fpmClient = fpmClient;
			this.rozkladClient = rozkladClient;
			mappedTeachers = new Dictionary<string, string>();
		}

		private void Map_Click(object sender, EventArgs e)
		{
			mappedTeachers.Add(rozkladTeachers.Text, fpmClient.Teachers.Where(s => s.Name == fpmTeachers.Text).FirstOrDefault().Id);
		}

		private void SubjectsMapping_Load(object sender, EventArgs e)
		{
			fpmTeachers.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			fpmTeachers.AutoCompleteCustomSource = new AutoCompleteStringCollection();
			fpmTeachers.AutoCompleteCustomSource.AddRange(fpmClient.Teachers.Select(s => s.Name).ToArray());
			fpmTeachers.AutoCompleteSource = AutoCompleteSource.CustomSource;
			rozkladTeachers.Items.AddRange(rozkladClient.Teachers.ToArray());
		}

		private void SubjectsMapping_FormClosing(object sender, FormClosingEventArgs e)
		{
			TimeTableLibrary.CsvHelpers.CsvWriter writer = new TimeTableLibrary.CsvHelpers.CsvWriter("teachers.csv");
			writer.Write(mappedTeachers);
		}
	}
}
