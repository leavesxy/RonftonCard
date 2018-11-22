namespace RonftonCard.Tester
{
	partial class TestMainFrm
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
			this.BtnGetUTCTime = new System.Windows.Forms.Button();
			this.BtnExit = new System.Windows.Forms.Button();
			this.TxtTrace = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtnGetUTCTime
			// 
			this.BtnGetUTCTime.Location = new System.Drawing.Point(9, 18);
			this.BtnGetUTCTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnGetUTCTime.Name = "BtnGetUTCTime";
			this.BtnGetUTCTime.Size = new System.Drawing.Size(119, 27);
			this.BtnGetUTCTime.TabIndex = 0;
			this.BtnGetUTCTime.Text = "GetUtcTime";
			this.BtnGetUTCTime.UseVisualStyleBackColor = true;
			this.BtnGetUTCTime.Click += new System.EventHandler(this.BtnGetUTCTime_Click);
			// 
			// BtnExit
			// 
			this.BtnExit.Location = new System.Drawing.Point(654, 66);
			this.BtnExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnExit.Name = "BtnExit";
			this.BtnExit.Size = new System.Drawing.Size(119, 27);
			this.BtnExit.TabIndex = 0;
			this.BtnExit.Text = "Exit";
			this.BtnExit.UseVisualStyleBackColor = true;
			this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
			// 
			// TxtTrace
			// 
			this.TxtTrace.Location = new System.Drawing.Point(13, 13);
			this.TxtTrace.Multiline = true;
			this.TxtTrace.Name = "TxtTrace";
			this.TxtTrace.Size = new System.Drawing.Size(779, 284);
			this.TxtTrace.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.BtnGetUTCTime);
			this.groupBox1.Controls.Add(this.BtnExit);
			this.groupBox1.Location = new System.Drawing.Point(13, 303);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(779, 100);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			// 
			// TestMainFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(804, 416);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.TxtTrace);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "TestMainFrm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnGetUTCTime;
		private System.Windows.Forms.Button BtnExit;
		private System.Windows.Forms.TextBox TxtTrace;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}

