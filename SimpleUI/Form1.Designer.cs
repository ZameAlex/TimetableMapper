namespace SimpleUI
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.MapSubjects = new System.Windows.Forms.Button();
			this.MapTeachers = new System.Windows.Forms.Button();
			this.MapTimetable = new System.Windows.Forms.Button();
			this.Select = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.textBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.textBox1.Location = new System.Drawing.Point(322, 57);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "Select group";
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// MapSubjects
			// 
			this.MapSubjects.Location = new System.Drawing.Point(113, 118);
			this.MapSubjects.Name = "MapSubjects";
			this.MapSubjects.Size = new System.Drawing.Size(93, 23);
			this.MapSubjects.TabIndex = 1;
			this.MapSubjects.Text = "Map subjects";
			this.MapSubjects.UseVisualStyleBackColor = true;
			this.MapSubjects.Click += new System.EventHandler(this.MapSubjects_Click);
			// 
			// MapTeachers
			// 
			this.MapTeachers.Location = new System.Drawing.Point(322, 118);
			this.MapTeachers.Name = "MapTeachers";
			this.MapTeachers.Size = new System.Drawing.Size(100, 23);
			this.MapTeachers.TabIndex = 2;
			this.MapTeachers.Text = "Map teachers";
			this.MapTeachers.UseVisualStyleBackColor = true;
			// 
			// MapTimetable
			// 
			this.MapTimetable.Location = new System.Drawing.Point(508, 118);
			this.MapTimetable.Name = "MapTimetable";
			this.MapTimetable.Size = new System.Drawing.Size(99, 23);
			this.MapTimetable.TabIndex = 3;
			this.MapTimetable.Text = "Map timetable";
			this.MapTimetable.UseVisualStyleBackColor = true;
			// 
			// Select
			// 
			this.Select.Location = new System.Drawing.Point(442, 55);
			this.Select.Name = "Select";
			this.Select.Size = new System.Drawing.Size(75, 23);
			this.Select.TabIndex = 4;
			this.Select.Text = "Select";
			this.Select.UseVisualStyleBackColor = true;
			this.Select.Click += new System.EventHandler(this.Select_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.Select);
			this.Controls.Add(this.MapTimetable);
			this.Controls.Add(this.MapTeachers);
			this.Controls.Add(this.MapSubjects);
			this.Controls.Add(this.textBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button MapSubjects;
		private System.Windows.Forms.Button MapTeachers;
		private System.Windows.Forms.Button MapTimetable;
		private System.Windows.Forms.Button Select;
	}
}

