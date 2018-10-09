namespace RonftonCard.Tester.Forms
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.CbAddrTemplete = new System.Windows.Forms.ComboBox();
			this.CbReaderType = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.CbStruTemplete = new System.Windows.Forms.ComboBox();
			this.CbCardType = new System.Windows.Forms.ComboBox();
			this.BtnExit = new System.Windows.Forms.Button();
			this.TxtDbg = new System.Windows.Forms.TextBox();
			this.BtnClear = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.CbAddrTemplete);
			this.groupBox1.Controls.Add(this.CbReaderType);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.CbStruTemplete);
			this.groupBox1.Controls.Add(this.CbCardType);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(12, 13);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox1.Size = new System.Drawing.Size(764, 115);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "参数设置";
			// 
			// CbAddrTemplete
			// 
			this.CbAddrTemplete.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CbAddrTemplete.FormattingEnabled = true;
			this.CbAddrTemplete.Location = new System.Drawing.Point(95, 68);
			this.CbAddrTemplete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.CbAddrTemplete.Name = "CbAddrTemplete";
			this.CbAddrTemplete.Size = new System.Drawing.Size(282, 27);
			this.CbAddrTemplete.TabIndex = 1;
			this.CbAddrTemplete.SelectedIndexChanged += new System.EventHandler(this.CbAddrTemplete_SelectedIndexChanged);
			// 
			// CbReaderType
			// 
			this.CbReaderType.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CbReaderType.FormattingEnabled = true;
			this.CbReaderType.Location = new System.Drawing.Point(97, 25);
			this.CbReaderType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.CbReaderType.Name = "CbReaderType";
			this.CbReaderType.Size = new System.Drawing.Size(282, 27);
			this.CbReaderType.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(390, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "卡片类型：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(390, 70);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(79, 20);
			this.label4.TabIndex = 0;
			this.label4.Text = "数据模板：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(10, 70);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 20);
			this.label3.TabIndex = 0;
			this.label3.Text = "卡片模板：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(10, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "读卡器类型：";
			// 
			// CbStruTemplete
			// 
			this.CbStruTemplete.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CbStruTemplete.FormattingEnabled = true;
			this.CbStruTemplete.Location = new System.Drawing.Point(480, 68);
			this.CbStruTemplete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.CbStruTemplete.Name = "CbStruTemplete";
			this.CbStruTemplete.Size = new System.Drawing.Size(260, 27);
			this.CbStruTemplete.TabIndex = 1;
			this.CbStruTemplete.SelectedIndexChanged += new System.EventHandler(this.CbStruTemplete_SelectedIndexChanged);
			// 
			// CbCardType
			// 
			this.CbCardType.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CbCardType.FormattingEnabled = true;
			this.CbCardType.Location = new System.Drawing.Point(480, 25);
			this.CbCardType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.CbCardType.Name = "CbCardType";
			this.CbCardType.Size = new System.Drawing.Size(260, 27);
			this.CbCardType.TabIndex = 1;
			// 
			// BtnExit
			// 
			this.BtnExit.Location = new System.Drawing.Point(690, 488);
			this.BtnExit.Name = "BtnExit";
			this.BtnExit.Size = new System.Drawing.Size(86, 32);
			this.BtnExit.TabIndex = 5;
			this.BtnExit.Text = "Exit";
			this.BtnExit.UseVisualStyleBackColor = true;
			this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
			// 
			// TxtDbg
			// 
			this.TxtDbg.Location = new System.Drawing.Point(12, 150);
			this.TxtDbg.Multiline = true;
			this.TxtDbg.Name = "TxtDbg";
			this.TxtDbg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TxtDbg.Size = new System.Drawing.Size(764, 332);
			this.TxtDbg.TabIndex = 6;
			// 
			// BtnClear
			// 
			this.BtnClear.Location = new System.Drawing.Point(598, 488);
			this.BtnClear.Name = "BtnClear";
			this.BtnClear.Size = new System.Drawing.Size(86, 32);
			this.BtnClear.TabIndex = 5;
			this.BtnClear.Text = "Clear";
			this.BtnClear.UseVisualStyleBackColor = true;
			this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
			// 
			// TestMainFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(795, 532);
			this.Controls.Add(this.TxtDbg);
			this.Controls.Add(this.BtnClear);
			this.Controls.Add(this.BtnExit);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "TestMainFrm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "TestMainFrm";
			this.Load += new System.EventHandler(this.TestMainFrm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox CbAddrTemplete;
		private System.Windows.Forms.ComboBox CbReaderType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox CbStruTemplete;
		private System.Windows.Forms.ComboBox CbCardType;
		private System.Windows.Forms.Button BtnExit;
		private System.Windows.Forms.TextBox TxtDbg;
		private System.Windows.Forms.Button BtnClear;
	}
}