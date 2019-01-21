using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeTableLibrary;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.RozkladRequests;
using TimeTableLibrary.Enums;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.RozkladModels;
using TimeTableLibrary.Mappers;

namespace SimpleUI
{
	public partial class Form1 : Form
	{
		FpmClient fpmClient;
		RozkladClient rozkladClient;
		TimeTableLibrary.CsvHelpers.CsvChecker checker;
		public Form1()
		{
			InitializeComponent();
			MapSubjects.Enabled = false;
			MapTeachers.Enabled = false;
			MapTimetable.Enabled = false;
			fpmClient = new FpmClient();
			
			
		}

		private async void Form1_Load(object sender, EventArgs e)
		{
			await fpmClient.InitRequest();
			fpmClient.User = new FpmUser("leo", "leoleo");
			await fpmClient.Login();
			await fpmClient.SelectSubjectsAndGroups();
			await fpmClient.SelectTeachers();
			textBox1.AutoCompleteCustomSource.AddRange(fpmClient.Groups.Select(g=>g.Name).ToArray());
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			
		}

		private async void GetTimeTable()
		{
			await rozkladClient.GetTimetable();
		}

		private void MapSubjects_Click(object sender, EventArgs e)
		{
			SubjectsMapping subjectsMapping = new SubjectsMapping(fpmClient, rozkladClient);
			subjectsMapping.ShowDialog();
			EnablingChanges();

		}

		private void MapTeachers_Click(object sender, EventArgs e)
		{
			TeachersMapping teachersMapping = new TeachersMapping(fpmClient, rozkladClient);
			teachersMapping.ShowDialog();
			EnablingChanges();
		}

		private void Select_Click(object sender, EventArgs e)
		{
			var group = fpmClient.Groups.Where(g => g.Name == textBox1.Text).FirstOrDefault();
			if (group != null && !String.IsNullOrWhiteSpace(group.Id))
			{
				fpmClient.CurrentGroup = group;
				rozkladClient = new RozkladClient(group.Name);
				GetTimeTable();
				checker = new TimeTableLibrary.CsvHelpers.CsvChecker(fpmClient, rozkladClient);
				MapSubjects.Enabled = true;
				MapTeachers.Enabled = true;
				EnablingChanges();
			}
		}

		private async void MapTimetable_Click(object sender, EventArgs e)
		{
			await fpmClient.ClearRequest();
			await fpmClient.SetRequest(new ResultLessonsMapper().Map(rozkladClient.rozkladTimeTable[0], rozkladClient.rozkladTimeTable[1]), checker);
		}

		private void EnablingChanges()
		{
			if (checker.IsSubjectMapped() && checker.IsTeacherMapped())
			{
				MapTimetable.Enabled = true;
				MapSubjects.Enabled = false;
				MapTeachers.Enabled = false;
			}
		}
	}
}
