﻿namespace SimpleUI
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
			this.rozkladSubjects = new System.Windows.Forms.ComboBox();
			this.Map = new System.Windows.Forms.Button();
			this.fpmSubjects = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// rozkladSubject
			// 
			this.rozkladSubjects.FormattingEnabled = true;
			this.rozkladSubjects.Location = new System.Drawing.Point(49, 12);
			this.rozkladSubjects.Name = "rozkladSubject";
			this.rozkladSubjects.Size = new System.Drawing.Size(266, 21);
			this.rozkladSubjects.TabIndex = 1;
			// 
			// Map
			// 
			this.Map.Location = new System.Drawing.Point(240, 123);
			this.Map.Name = "Map";
			this.Map.Size = new System.Drawing.Size(75, 23);
			this.Map.TabIndex = 2;
			this.Map.Text = "Map";
			this.Map.UseVisualStyleBackColor = true;
			this.Map.Click += new System.EventHandler(this.Map_Click);
			// 
			// fpmSubject
			// 
			this.fpmSubjects.Location = new System.Drawing.Point(49, 57);
			this.fpmSubjects.Name = "fpmSubject";
			this.fpmSubjects.Size = new System.Drawing.Size(266, 20);
			this.fpmSubjects.TabIndex = 3;
			// 
			// SubjectsMapping
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(327, 167);
			this.Controls.Add(this.fpmSubjects);
			this.Controls.Add(this.Map);
			this.Controls.Add(this.rozkladSubjects);
			this.Name = "SubjectsMapping";
			this.Text = "SubjectsMapping";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubjectsMapping_FormClosing);
			this.Load += new System.EventHandler(this.SubjectsMapping_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ComboBox rozkladSubjects;
		private System.Windows.Forms.Button Map;
		private System.Windows.Forms.TextBox fpmSubjects;
	}
}