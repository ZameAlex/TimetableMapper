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
	public partial class SubjectsMapping : Form
	{
		FpmClient fpmClient;
		RozkladClient rozkladClient;
		Dictionary<string, string> mappedSubjects;
		public SubjectsMapping()
		{
			InitializeComponent();
		}

		public SubjectsMapping(FpmClient fpmClient, RozkladClient rozkladClient):this()
		{
			this.fpmClient = fpmClient;
			this.rozkladClient = rozkladClient;
			mappedSubjects = new Dictionary<string, string>();
		}

		private void Map_Click(object sender, EventArgs e)
		{
			mappedSubjects.Add(rozkladSubjects.Text, fpmClient.Subjects.Where(s => s.Name == fpmSubjects.Text).FirstOrDefault().Id);
		}

		private void SubjectsMapping_Load(object sender, EventArgs e)
		{
			fpmSubjects.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			fpmSubjects.AutoCompleteCustomSource = new AutoCompleteStringCollection();
			fpmSubjects.AutoCompleteCustomSource.AddRange(fpmClient.Subjects.Select(s => s.Name).ToArray());
			fpmSubjects.AutoCompleteSource = AutoCompleteSource.CustomSource;
			rozkladSubjects.Items.AddRange(rozkladClient.Subjects.ToArray());
		}

		private void SubjectsMapping_FormClosing(object sender, FormClosingEventArgs e)
		{
			TimeTableLibrary.CsvHelpers.CsvWriter writer = new TimeTableLibrary.CsvHelpers.CsvWriter("subjects.csv");
			writer.Write(mappedSubjects);
		}
	}
}
