namespace RonftonCard.Main.Forms
{
	partial class DongleForm
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
			this.CbDongle = new System.Windows.Forms.ComboBox();
			this.TxtUserRootKey = new System.Windows.Forms.TextBox();
			this.TxtAdminPin = new System.Windows.Forms.TextBox();
			this.TxtUserID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.BtnCreateAuthenKey = new System.Windows.Forms.Button();
			this.BtnRestore = new System.Windows.Forms.Button();
			this.BtnCreateUserRootKey = new System.Windows.Forms.Button();
			this.BtnEnumerate = new System.Windows.Forms.Button();
			this.Trace = new System.Windows.Forms.TextBox();
			this.BntEncryptByUserRoot = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.TxtPlain = new System.Windows.Forms.TextBox();
			this.BtnEncryptByPri = new System.Windows.Forms.Button();
			this.BtnUtcTime = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// CbDongle
			// 
			this.CbDongle.FormattingEnabled = true;
			this.CbDongle.Location = new System.Drawing.Point(113, 76);
			this.CbDongle.Name = "CbDongle";
			this.CbDongle.Size = new System.Drawing.Size(670, 27);
			this.CbDongle.TabIndex = 33;
			this.CbDongle.SelectedIndexChanged += new System.EventHandler(this.CbDongleSelected_SelectedIndexChanged);
			// 
			// TxtUserRootKey
			// 
			this.TxtUserRootKey.Location = new System.Drawing.Point(429, 11);
			this.TxtUserRootKey.Name = "TxtUserRootKey";
			this.TxtUserRootKey.Size = new System.Drawing.Size(354, 25);
			this.TxtUserRootKey.TabIndex = 32;
			// 
			// TxtAdminPin
			// 
			this.TxtAdminPin.Location = new System.Drawing.Point(113, 11);
			this.TxtAdminPin.Name = "TxtAdminPin";
			this.TxtAdminPin.Size = new System.Drawing.Size(164, 25);
			this.TxtAdminPin.TabIndex = 30;
			// 
			// TxtUserID
			// 
			this.TxtUserID.Location = new System.Drawing.Point(113, 44);
			this.TxtUserID.Name = "TxtUserID";
			this.TxtUserID.Size = new System.Drawing.Size(125, 25);
			this.TxtUserID.TabIndex = 29;
			this.TxtUserID.Text = "01234567";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 20);
			this.label2.TabIndex = 24;
			this.label2.Text = "KEY保护密钥：";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(13, 47);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(66, 20);
			this.label6.TabIndex = 23;
			this.label6.Text = "用户ID：";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(13, 76);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(76, 20);
			this.label7.TabIndex = 21;
			this.label7.Text = "当前KEY：";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(302, 14);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(121, 20);
			this.label5.TabIndex = 20;
			this.label5.Text = "用户测试根密钥：";
			// 
			// BtnCreateAuthenKey
			// 
			this.BtnCreateAuthenKey.Location = new System.Drawing.Point(367, 398);
			this.BtnCreateAuthenKey.Name = "BtnCreateAuthenKey";
			this.BtnCreateAuthenKey.Size = new System.Drawing.Size(117, 29);
			this.BtnCreateAuthenKey.TabIndex = 17;
			this.BtnCreateAuthenKey.Text = "创建应用授权";
			this.BtnCreateAuthenKey.UseVisualStyleBackColor = true;
			this.BtnCreateAuthenKey.Click += new System.EventHandler(this.BtnCreateAuthenKey_Click);
			// 
			// BtnRestore
			// 
			this.BtnRestore.Location = new System.Drawing.Point(12, 428);
			this.BtnRestore.Name = "BtnRestore";
			this.BtnRestore.Size = new System.Drawing.Size(117, 29);
			this.BtnRestore.TabIndex = 16;
			this.BtnRestore.Text = "一键还原";
			this.BtnRestore.UseVisualStyleBackColor = true;
			this.BtnRestore.Click += new System.EventHandler(this.BtnRestore_Click);
			// 
			// BtnCreateUserRootKey
			// 
			this.BtnCreateUserRootKey.Location = new System.Drawing.Point(248, 398);
			this.BtnCreateUserRootKey.Name = "BtnCreateUserRootKey";
			this.BtnCreateUserRootKey.Size = new System.Drawing.Size(117, 29);
			this.BtnCreateUserRootKey.TabIndex = 10;
			this.BtnCreateUserRootKey.Text = "创建用户根密钥";
			this.BtnCreateUserRootKey.UseVisualStyleBackColor = true;
			this.BtnCreateUserRootKey.Click += new System.EventHandler(this.BtnCreateUserRootKey_Click);
			// 
			// BtnEnumerate
			// 
			this.BtnEnumerate.Location = new System.Drawing.Point(12, 398);
			this.BtnEnumerate.Name = "BtnEnumerate";
			this.BtnEnumerate.Size = new System.Drawing.Size(117, 29);
			this.BtnEnumerate.TabIndex = 18;
			this.BtnEnumerate.Text = "枚举设备";
			this.BtnEnumerate.UseVisualStyleBackColor = true;
			this.BtnEnumerate.Click += new System.EventHandler(this.BtnEnumerate_Click);
			// 
			// Trace
			// 
			this.Trace.Location = new System.Drawing.Point(12, 109);
			this.Trace.Multiline = true;
			this.Trace.Name = "Trace";
			this.Trace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.Trace.Size = new System.Drawing.Size(771, 283);
			this.Trace.TabIndex = 5;
			// 
			// BntEncryptByUserRoot
			// 
			this.BntEncryptByUserRoot.Location = new System.Drawing.Point(249, 428);
			this.BntEncryptByUserRoot.Name = "BntEncryptByUserRoot";
			this.BntEncryptByUserRoot.Size = new System.Drawing.Size(117, 29);
			this.BntEncryptByUserRoot.TabIndex = 10;
			this.BntEncryptByUserRoot.Text = "根密钥加密";
			this.BntEncryptByUserRoot.UseVisualStyleBackColor = true;
			this.BntEncryptByUserRoot.Click += new System.EventHandler(this.BntEncryptByUserRoot_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(302, 47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 20);
			this.label1.TabIndex = 20;
			this.label1.Text = "测试明文：";
			// 
			// TxtPlain
			// 
			this.TxtPlain.Location = new System.Drawing.Point(429, 47);
			this.TxtPlain.Name = "TxtPlain";
			this.TxtPlain.Size = new System.Drawing.Size(354, 25);
			this.TxtPlain.TabIndex = 32;
			// 
			// BtnEncryptByPri
			// 
			this.BtnEncryptByPri.Location = new System.Drawing.Point(367, 428);
			this.BtnEncryptByPri.Name = "BtnEncryptByPri";
			this.BtnEncryptByPri.Size = new System.Drawing.Size(117, 29);
			this.BtnEncryptByPri.TabIndex = 17;
			this.BtnEncryptByPri.Text = "私钥加密";
			this.BtnEncryptByPri.UseVisualStyleBackColor = true;
			this.BtnEncryptByPri.Click += new System.EventHandler(this.BtnEncryptByPri_Click);
			// 
			// BtnUtcTime
			// 
			this.BtnUtcTime.Location = new System.Drawing.Point(130, 398);
			this.BtnUtcTime.Name = "BtnUtcTime";
			this.BtnUtcTime.Size = new System.Drawing.Size(117, 29);
			this.BtnUtcTime.TabIndex = 17;
			this.BtnUtcTime.Text = "显示时钟";
			this.BtnUtcTime.UseVisualStyleBackColor = true;
			this.BtnUtcTime.Click += new System.EventHandler(this.BtnUtcTime_Click);
			// 
			// DongleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(795, 466);
			this.Controls.Add(this.CbDongle);
			this.Controls.Add(this.TxtPlain);
			this.Controls.Add(this.TxtUserRootKey);
			this.Controls.Add(this.TxtAdminPin);
			this.Controls.Add(this.TxtUserID);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.BtnEncryptByPri);
			this.Controls.Add(this.BtnUtcTime);
			this.Controls.Add(this.BtnCreateAuthenKey);
			this.Controls.Add(this.BtnRestore);
			this.Controls.Add(this.BntEncryptByUserRoot);
			this.Controls.Add(this.BtnCreateUserRootKey);
			this.Controls.Add(this.BtnEnumerate);
			this.Controls.Add(this.Trace);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "DongleForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AuthenKeyForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DongleForm_FormClosing);
			this.Load += new System.EventHandler(this.DongleForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox CbDongle;
		private System.Windows.Forms.TextBox TxtUserRootKey;
		private System.Windows.Forms.TextBox TxtAdminPin;
		private System.Windows.Forms.TextBox TxtUserID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button BtnCreateAuthenKey;
		private System.Windows.Forms.Button BtnRestore;
		private System.Windows.Forms.Button BtnCreateUserRootKey;
		private System.Windows.Forms.Button BtnEnumerate;
		private System.Windows.Forms.TextBox Trace;
		private System.Windows.Forms.Button BntEncryptByUserRoot;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TxtPlain;
		private System.Windows.Forms.Button BtnEncryptByPri;
		private System.Windows.Forms.Button BtnUtcTime;
	}
}