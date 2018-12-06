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
			this.BtnLedControl = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.RbLedBlink = new System.Windows.Forms.RadioButton();
			this.RbLedOn = new System.Windows.Forms.RadioButton();
			this.RbLedOff = new System.Windows.Forms.RadioButton();
			this.BtnDongleInfo2Json = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtnGetUTCTime
			// 
			this.BtnGetUTCTime.Location = new System.Drawing.Point(9, 38);
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
			this.BtnExit.Location = new System.Drawing.Point(654, 38);
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
			this.TxtTrace.Size = new System.Drawing.Size(779, 260);
			this.TxtTrace.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.BtnDongleInfo2Json);
			this.groupBox1.Controls.Add(this.BtnLedControl);
			this.groupBox1.Controls.Add(this.BtnGetUTCTime);
			this.groupBox1.Controls.Add(this.BtnExit);
			this.groupBox1.Location = new System.Drawing.Point(13, 341);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(779, 74);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			// 
			// BtnLedControl
			// 
			this.BtnLedControl.Location = new System.Drawing.Point(134, 38);
			this.BtnLedControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnLedControl.Name = "BtnLedControl";
			this.BtnLedControl.Size = new System.Drawing.Size(119, 27);
			this.BtnLedControl.TabIndex = 0;
			this.BtnLedControl.Text = "LED";
			this.BtnLedControl.UseVisualStyleBackColor = true;
			this.BtnLedControl.Click += new System.EventHandler(this.BtnLedControl_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.RbLedBlink);
			this.groupBox2.Controls.Add(this.RbLedOn);
			this.groupBox2.Controls.Add(this.RbLedOff);
			this.groupBox2.Location = new System.Drawing.Point(14, 279);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(196, 50);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			// 
			// RbLedBlink
			// 
			this.RbLedBlink.AutoSize = true;
			this.RbLedBlink.Location = new System.Drawing.Point(123, 17);
			this.RbLedBlink.Name = "RbLedBlink";
			this.RbLedBlink.Size = new System.Drawing.Size(64, 24);
			this.RbLedBlink.TabIndex = 0;
			this.RbLedBlink.TabStop = true;
			this.RbLedBlink.Text = "Blink";
			this.RbLedBlink.UseVisualStyleBackColor = true;
			this.RbLedBlink.CheckedChanged += new System.EventHandler(this.RbLedBlink_CheckedChanged);
			// 
			// RbLedOn
			// 
			this.RbLedOn.AutoSize = true;
			this.RbLedOn.Location = new System.Drawing.Point(67, 18);
			this.RbLedOn.Name = "RbLedOn";
			this.RbLedOn.Size = new System.Drawing.Size(50, 24);
			this.RbLedOn.TabIndex = 0;
			this.RbLedOn.TabStop = true;
			this.RbLedOn.Text = "On";
			this.RbLedOn.UseVisualStyleBackColor = true;
			this.RbLedOn.CheckedChanged += new System.EventHandler(this.RbLedOn_CheckedChanged);
			// 
			// RbLedOff
			// 
			this.RbLedOff.AutoSize = true;
			this.RbLedOff.Location = new System.Drawing.Point(10, 18);
			this.RbLedOff.Name = "RbLedOff";
			this.RbLedOff.Size = new System.Drawing.Size(51, 24);
			this.RbLedOff.TabIndex = 0;
			this.RbLedOff.TabStop = true;
			this.RbLedOff.Text = "Off";
			this.RbLedOff.UseVisualStyleBackColor = true;
			this.RbLedOff.CheckedChanged += new System.EventHandler(this.RbLedOff_CheckedChanged);
			// 
			// BtnDongleInfo2Json
			// 
			this.BtnDongleInfo2Json.Location = new System.Drawing.Point(259, 38);
			this.BtnDongleInfo2Json.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnDongleInfo2Json.Name = "BtnDongleInfo2Json";
			this.BtnDongleInfo2Json.Size = new System.Drawing.Size(145, 27);
			this.BtnDongleInfo2Json.TabIndex = 0;
			this.BtnDongleInfo2Json.Text = "DongleInfo2Json";
			this.BtnDongleInfo2Json.UseVisualStyleBackColor = true;
			this.BtnDongleInfo2Json.Click += new System.EventHandler(this.BtnDongleInfo2Json_Click);
			// 
			// TestMainFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(804, 416);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.TxtTrace);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "TestMainFrm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnGetUTCTime;
		private System.Windows.Forms.Button BtnExit;
		private System.Windows.Forms.TextBox TxtTrace;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button BtnLedControl;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton RbLedBlink;
		private System.Windows.Forms.RadioButton RbLedOn;
		private System.Windows.Forms.RadioButton RbLedOff;
		private System.Windows.Forms.Button BtnDongleInfo2Json;
	}
}

