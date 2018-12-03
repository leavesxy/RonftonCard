namespace RonftonCard.Main.Forms
{
	partial class MainFrm
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
			this.CbDongle = new System.Windows.Forms.ComboBox();
			this.CbCardReader = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.CbCardTemplete = new System.Windows.Forms.ComboBox();
			this.TabMainControl = new System.Windows.Forms.TabControl();
			this.BtnExit = new System.Windows.Forms.Button();
			this.BtnRefresh = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.CbCardType = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.CbDongle);
			this.groupBox1.Controls.Add(this.CbCardReader);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.CbCardType);
			this.groupBox1.Controls.Add(this.CbCardTemplete);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(12, 6);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.groupBox1.Size = new System.Drawing.Size(802, 98);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "参数设置";
			// 
			// CbDongle
			// 
			this.CbDongle.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CbDongle.FormattingEnabled = true;
			this.CbDongle.Location = new System.Drawing.Point(96, 55);
			this.CbDongle.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.CbDongle.Name = "CbDongle";
			this.CbDongle.Size = new System.Drawing.Size(365, 27);
			this.CbDongle.TabIndex = 1;
			this.CbDongle.SelectedIndexChanged += new System.EventHandler(this.CbDongle_SelectedIndexChanged);
			// 
			// CbCardReader
			// 
			this.CbCardReader.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CbCardReader.FormattingEnabled = true;
			this.CbCardReader.Location = new System.Drawing.Point(96, 21);
			this.CbCardReader.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.CbCardReader.Name = "CbCardReader";
			this.CbCardReader.Size = new System.Drawing.Size(365, 27);
			this.CbCardReader.TabIndex = 1;
			this.CbCardReader.SelectedIndexChanged += new System.EventHandler(this.CbCardReader_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(467, 58);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(79, 20);
			this.label4.TabIndex = 0;
			this.label4.Text = "卡片模板：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(10, 58);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 20);
			this.label2.TabIndex = 0;
			this.label2.Text = "加密狗类型：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(10, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "读卡器类型：";
			// 
			// CbCardTemplete
			// 
			this.CbCardTemplete.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CbCardTemplete.FormattingEnabled = true;
			this.CbCardTemplete.Location = new System.Drawing.Point(552, 55);
			this.CbCardTemplete.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.CbCardTemplete.Name = "CbCardTemplete";
			this.CbCardTemplete.Size = new System.Drawing.Size(230, 27);
			this.CbCardTemplete.TabIndex = 1;
			this.CbCardTemplete.SelectedIndexChanged += new System.EventHandler(this.CbCardTemplete_SelectedIndexChanged);
			// 
			// TabMainControl
			// 
			this.TabMainControl.Location = new System.Drawing.Point(12, 113);
			this.TabMainControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.TabMainControl.Name = "TabMainControl";
			this.TabMainControl.SelectedIndex = 0;
			this.TabMainControl.Size = new System.Drawing.Size(802, 503);
			this.TabMainControl.TabIndex = 12;
			this.TabMainControl.SelectedIndexChanged += new System.EventHandler(this.TabMainControl_SelectedIndexChanged);
			// 
			// BtnExit
			// 
			this.BtnExit.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnExit.Location = new System.Drawing.Point(728, 617);
			this.BtnExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnExit.Name = "BtnExit";
			this.BtnExit.Size = new System.Drawing.Size(86, 33);
			this.BtnExit.TabIndex = 11;
			this.BtnExit.Text = "退出";
			this.BtnExit.UseVisualStyleBackColor = true;
			this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
			// 
			// BtnRefresh
			// 
			this.BtnRefresh.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnRefresh.Location = new System.Drawing.Point(636, 617);
			this.BtnRefresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnRefresh.Name = "BtnRefresh";
			this.BtnRefresh.Size = new System.Drawing.Size(86, 33);
			this.BtnRefresh.TabIndex = 11;
			this.BtnRefresh.Text = "刷新";
			this.BtnRefresh.UseVisualStyleBackColor = true;
			this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(467, 25);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 20);
			this.label3.TabIndex = 0;
			this.label3.Text = "卡片类型：";
			// 
			// CbCardType
			// 
			this.CbCardType.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CbCardType.FormattingEnabled = true;
			this.CbCardType.Location = new System.Drawing.Point(552, 21);
			this.CbCardType.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.CbCardType.Name = "CbCardType";
			this.CbCardType.Size = new System.Drawing.Size(230, 27);
			this.CbCardType.TabIndex = 1;
			this.CbCardType.SelectedIndexChanged += new System.EventHandler(this.CbCardType_SelectedIndexChanged);
			// 
			// MainFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(826, 653);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.TabMainControl);
			this.Controls.Add(this.BtnRefresh);
			this.Controls.Add(this.BtnExit);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "MainFrm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.MainFrm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox CbCardReader;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox CbCardTemplete;
		private System.Windows.Forms.TabControl TabMainControl;
		private System.Windows.Forms.Button BtnExit;
		private System.Windows.Forms.ComboBox CbDongle;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button BtnRefresh;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox CbCardType;
	}
}

