namespace RonftonCard.Main.Forms
{
	partial class TestForm
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
			this.Trace = new System.Windows.Forms.TextBox();
			this.RbLedBlink = new System.Windows.Forms.RadioButton();
			this.RbLedOn = new System.Windows.Forms.RadioButton();
			this.RbLedOff = new System.Windows.Forms.RadioButton();
			this.BtnConvert2Json = new System.Windows.Forms.Button();
			this.BtnLedControl = new System.Windows.Forms.Button();
			this.BtnGetUTCTime = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Trace
			// 
			this.Trace.Location = new System.Drawing.Point(12, 12);
			this.Trace.Multiline = true;
			this.Trace.Name = "Trace";
			this.Trace.Size = new System.Drawing.Size(771, 291);
			this.Trace.TabIndex = 8;
			// 
			// RbLedBlink
			// 
			this.RbLedBlink.AutoSize = true;
			this.RbLedBlink.Location = new System.Drawing.Point(123, 19);
			this.RbLedBlink.Name = "RbLedBlink";
			this.RbLedBlink.Size = new System.Drawing.Size(64, 24);
			this.RbLedBlink.TabIndex = 2;
			this.RbLedBlink.TabStop = true;
			this.RbLedBlink.Text = "Blink";
			this.RbLedBlink.UseVisualStyleBackColor = true;
			this.RbLedBlink.CheckedChanged += new System.EventHandler(this.RbLedBlink_CheckedChanged);
			// 
			// RbLedOn
			// 
			this.RbLedOn.AutoSize = true;
			this.RbLedOn.Location = new System.Drawing.Point(67, 19);
			this.RbLedOn.Name = "RbLedOn";
			this.RbLedOn.Size = new System.Drawing.Size(50, 24);
			this.RbLedOn.TabIndex = 3;
			this.RbLedOn.TabStop = true;
			this.RbLedOn.Text = "On";
			this.RbLedOn.UseVisualStyleBackColor = true;
			this.RbLedOn.CheckedChanged += new System.EventHandler(this.RbLedOn_CheckedChanged);
			// 
			// RbLedOff
			// 
			this.RbLedOff.AutoSize = true;
			this.RbLedOff.Location = new System.Drawing.Point(10, 19);
			this.RbLedOff.Name = "RbLedOff";
			this.RbLedOff.Size = new System.Drawing.Size(51, 24);
			this.RbLedOff.TabIndex = 4;
			this.RbLedOff.TabStop = true;
			this.RbLedOff.Text = "Off";
			this.RbLedOff.UseVisualStyleBackColor = true;
			this.RbLedOff.CheckedChanged += new System.EventHandler(this.RbLedOff_CheckedChanged);
			// 
			// BtnConvert2Json
			// 
			this.BtnConvert2Json.Location = new System.Drawing.Point(260, 405);
			this.BtnConvert2Json.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnConvert2Json.Name = "BtnConvert2Json";
			this.BtnConvert2Json.Size = new System.Drawing.Size(145, 27);
			this.BtnConvert2Json.TabIndex = 5;
			this.BtnConvert2Json.Text = "DongleInfo2Json";
			this.BtnConvert2Json.UseVisualStyleBackColor = true;
			this.BtnConvert2Json.Click += new System.EventHandler(this.BtnConvert2Json_Click);
			// 
			// BtnLedControl
			// 
			this.BtnLedControl.Location = new System.Drawing.Point(135, 405);
			this.BtnLedControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnLedControl.Name = "BtnLedControl";
			this.BtnLedControl.Size = new System.Drawing.Size(119, 27);
			this.BtnLedControl.TabIndex = 6;
			this.BtnLedControl.Text = "LED";
			this.BtnLedControl.UseVisualStyleBackColor = true;
			// 
			// BtnGetUTCTime
			// 
			this.BtnGetUTCTime.Location = new System.Drawing.Point(12, 405);
			this.BtnGetUTCTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnGetUTCTime.Name = "BtnGetUTCTime";
			this.BtnGetUTCTime.Size = new System.Drawing.Size(119, 27);
			this.BtnGetUTCTime.TabIndex = 7;
			this.BtnGetUTCTime.Text = "GetUtcTime";
			this.BtnGetUTCTime.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.RbLedOn);
			this.groupBox1.Controls.Add(this.RbLedOff);
			this.groupBox1.Controls.Add(this.RbLedBlink);
			this.groupBox1.Location = new System.Drawing.Point(12, 303);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 54);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			// 
			// TestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(795, 475);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.Trace);
			this.Controls.Add(this.BtnConvert2Json);
			this.Controls.Add(this.BtnLedControl);
			this.Controls.Add(this.BtnGetUTCTime);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TestForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "TestForm";
			this.Load += new System.EventHandler(this.TestForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox Trace;
		private System.Windows.Forms.RadioButton RbLedBlink;
		private System.Windows.Forms.RadioButton RbLedOn;
		private System.Windows.Forms.RadioButton RbLedOff;
		private System.Windows.Forms.Button BtnConvert2Json;
		private System.Windows.Forms.Button BtnLedControl;
		private System.Windows.Forms.Button BtnGetUTCTime;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}