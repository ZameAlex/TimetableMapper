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
		RozkladSubject[] rzkSubjects;
		Dictionary<RozkladSubject, List<FpmSubject>> mappedSubj;
		int number = 0;
		public SubjectsMapping()
		{
			InitializeComponent();
		}

		public SubjectsMapping(FpmClient fpmClient, RozkladClient rozkladClient):this()
		{
			this.fpmClient = fpmClient;
			this.rozkladClient = rozkladClient;
		}

		private void SelectCurrent()
		{
			//fpmSubjects.Items.Clear();
			var currentObj = mappedSubj.Keys.ElementAt(number);
			rozkladSubject.Items.Add(currentObj);
			rozkladSubject.SelectedItem = rozkladSubject.Items[number];
			fpmSubjects.Items.AddRange(mappedSubj[currentObj].ToArray());
		}

		private void Map_Click(object sender, EventArgs e)
		{
			number++;
			if (number < mappedSubj.Count())
				SelectCurrent();
		}

		private void SubjectsMapping_Load(object sender, EventArgs e)
		{
			rozkladSubject.Enabled = false;
			rzkSubjects = rozkladClient.rozkladTimeTable[0].Select(r => r.Subject).Union(rozkladClient.rozkladTimeTable[1].Select(r => r.Subject)).Distinct().ToArray();
			//rozkladSubject.Items.AddRange(rzkSubjects);

			SubjectsMapper mapper = new SubjectsMapper();
			mappedSubj = mapper.Map(fpmClient.Subjects, rzkSubjects.ToList());
			mapper.WriteNewMapping(mappedSubj);
			SelectCurrent();
		}
	}
}
