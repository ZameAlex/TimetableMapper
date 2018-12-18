namespace SimpleUI
{
	partial class SubjectsMapping
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
			this.fpmSubjects = new System.Windows.Forms.ListBox();
			this.rozkladSubject = new System.Windows.Forms.ComboBox();
			this.Map = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// fpmSubjects
			// 
			this.fpmSubjects.FormattingEnabled = true;
			this.fpmSubjects.Location = new System.Drawing.Point(329, 156);
			this.fpmSubjects.Name = "fpmSubjects";
			this.fpmSubjects.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.fpmSubjects.Size = new System.Drawing.Size(120, 95);
			this.fpmSubjects.TabIndex = 0;
			// 
			// rozkladSubject
			// 
			this.rozkladSubject.FormattingEnabled = true;
			this.rozkladSubject.Location = new System.Drawing.Point(202, 37);
			this.rozkladSubject.Name = "rozkladSubject";
			this.rozkladSubject.Size = new System.Drawing.Size(426, 21);
			this.rozkladSubject.TabIndex = 1;
			// 
			// Map
			// 
			this.Map.Location = new System.Drawing.Point(544, 295);
			this.Map.Name = "Map";
			this.Map.Size = new System.Drawing.Size(75, 23);
			this.Map.TabIndex = 2;
			this.Map.Text = "Map";
			this.Map.UseVisualStyleBackColor = true;
			this.Map.Click += new System.EventHandler(this.Map_Click);
			// 
			// SubjectsMapping
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.Map);
			this.Controls.Add(this.rozkladSubject);
			this.Controls.Add(this.fpmSubjects);
			this.Name = "SubjectsMapping";
			this.Text = "SubjectsMapping";
			this.Load += new System.EventHandler(this.SubjectsMapping_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox fpmSubjects;
		private System.Windows.Forms.ComboBox rozkladSubject;
		private System.Windows.Forms.Button Map;
	}
}