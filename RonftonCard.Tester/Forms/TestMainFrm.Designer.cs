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
			this.CbCardReader = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.CbCardTemplete = new System.Windows.Forms.ComboBox();
			this.BtnExit = new System.Windows.Forms.Button();
			this.TabMainControl = new System.Windows.Forms.TabControl();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.SeedKeyMgrMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.CompanySeedKeyMgr = new System.Windows.Forms.ToolStripMenuItem();
			this.UserSeedKeyMgr = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.CbCardReader);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.CbCardTemplete);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(12, 38);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox1.Size = new System.Drawing.Size(802, 65);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "参数设置";
			// 
			// CbCardReader
			// 
			this.CbCardReader.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CbCardReader.FormattingEnabled = true;
			this.CbCardReader.Location = new System.Drawing.Point(100, 23);
			this.CbCardReader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.CbCardReader.Name = "CbCardReader";
			this.CbCardReader.Size = new System.Drawing.Size(361, 27);
			this.CbCardReader.TabIndex = 1;
			this.CbCardReader.SelectedIndexChanged += new System.EventHandler(this.CbCardReader_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(467, 26);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(79, 20);
			this.label4.TabIndex = 0;
			this.label4.Text = "卡片模板：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(10, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "读卡器：";
			// 
			// CbCardTemplete
			// 
			this.CbCardTemplete.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CbCardTemplete.FormattingEnabled = true;
			this.CbCardTemplete.Location = new System.Drawing.Point(552, 23);
			this.CbCardTemplete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.CbCardTemplete.Name = "CbCardTemplete";
			this.CbCardTemplete.Size = new System.Drawing.Size(230, 27);
			this.CbCardTemplete.TabIndex = 1;
			this.CbCardTemplete.SelectedIndexChanged += new System.EventHandler(this.CbCardTemplete_SelectedIndexChanged);
			// 
			// BtnExit
			// 
			this.BtnExit.Location = new System.Drawing.Point(728, 615);
			this.BtnExit.Name = "BtnExit";
			this.BtnExit.Size = new System.Drawing.Size(86, 32);
			this.BtnExit.TabIndex = 5;
			this.BtnExit.Text = "退出";
			this.BtnExit.UseVisualStyleBackColor = true;
			this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
			// 
			// TabMainControl
			// 
			this.TabMainControl.Location = new System.Drawing.Point(12, 110);
			this.TabMainControl.Name = "TabMainControl";
			this.TabMainControl.SelectedIndex = 0;
			this.TabMainControl.Size = new System.Drawing.Size(802, 499);
			this.TabMainControl.TabIndex = 8;
			this.TabMainControl.SelectedIndexChanged += new System.EventHandler(this.TabMainControl_SelectedIndexChanged);
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SeedKeyMgrMenu});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(826, 28);
			this.menuStrip1.TabIndex = 9;
			this.menuStrip1.Text = "MainMenu";
			// 
			// SeedKeyMgrMenu
			// 
			this.SeedKeyMgrMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CompanySeedKeyMgr,
            this.UserSeedKeyMgr});
			this.SeedKeyMgrMenu.Name = "SeedKeyMgrMenu";
			this.SeedKeyMgrMenu.Size = new System.Drawing.Size(101, 24);
			this.SeedKeyMgrMenu.Text = "种子卡管理";
			// 
			// CompanySeedKeyMgr
			// 
			this.CompanySeedKeyMgr.Name = "CompanySeedKeyMgr";
			this.CompanySeedKeyMgr.Size = new System.Drawing.Size(212, 26);
			this.CompanySeedKeyMgr.Text = "公司种子卡制做";
			this.CompanySeedKeyMgr.Click += new System.EventHandler(this.CompanySeedKeyMgr_Click);
			// 
			// UserSeedKeyMgr
			// 
			this.UserSeedKeyMgr.Name = "UserSeedKeyMgr";
			this.UserSeedKeyMgr.Size = new System.Drawing.Size(212, 26);
			this.UserSeedKeyMgr.Text = "学校根密钥卡制做";
			// 
			// TestMainFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(826, 653);
			this.Controls.Add(this.TabMainControl);
			this.Controls.Add(this.BtnExit);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "TestMainFrm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "TestMainFrm";
			this.Load += new System.EventHandler(this.TestMainFrm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox CbCardReader;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox CbCardTemplete;
		private System.Windows.Forms.Button BtnExit;
		private System.Windows.Forms.TabControl TabMainControl;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem SeedKeyMgrMenu;
		private System.Windows.Forms.ToolStripMenuItem CompanySeedKeyMgr;
		private System.Windows.Forms.ToolStripMenuItem UserSeedKeyMgr;
	}
}