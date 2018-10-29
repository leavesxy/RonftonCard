namespace RonftonCard.Tester.Forms
{
	partial class KeyTestFrm
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
			this.TxtDbg = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.TxtUserPwd = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.TxtAdminPwd = new System.Windows.Forms.TextBox();
			this.BtnUniqueKey = new System.Windows.Forms.Button();
			this.BtnAdminAuthen = new System.Windows.Forms.Button();
			this.BtnUserAuthen = new System.Windows.Forms.Button();
			this.BtnCreateCompanySeed = new System.Windows.Forms.Button();
			this.BtnCreateRsaKeyFile = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.TxtPlain = new System.Windows.Forms.TextBox();
			this.BtnEncryptByCompanySeed = new System.Windows.Forms.Button();
			this.BtnRestore = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.TxtSeed = new System.Windows.Forms.TextBox();
			this.BtnRsaPriEncrypt = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.TxtTdesKey = new System.Windows.Forms.TextBox();
			this.BtnSetUserID = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.TxtUserID = new System.Windows.Forms.TextBox();
			this.BtnDbgErrorMsg = new System.Windows.Forms.Button();
			this.BtnEnumKey = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.CbCurrentKey = new System.Windows.Forms.ComboBox();
			this.BtnCreateUserRootKey = new System.Windows.Forms.Button();
			this.BtnEncryptByUserRoot = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TxtDbg
			// 
			this.TxtDbg.Location = new System.Drawing.Point(12, 117);
			this.TxtDbg.Multiline = true;
			this.TxtDbg.Name = "TxtDbg";
			this.TxtDbg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TxtDbg.Size = new System.Drawing.Size(771, 274);
			this.TxtDbg.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "用户密码：";
			// 
			// TxtUserPwd
			// 
			this.TxtUserPwd.Location = new System.Drawing.Point(93, 10);
			this.TxtUserPwd.Name = "TxtUserPwd";
			this.TxtUserPwd.Size = new System.Drawing.Size(148, 25);
			this.TxtUserPwd.TabIndex = 3;
			this.TxtUserPwd.TextChanged += new System.EventHandler(this.TxtUserPwd_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(254, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "开发商密码：";
			// 
			// TxtAdminPwd
			// 
			this.TxtAdminPwd.Location = new System.Drawing.Point(341, 10);
			this.TxtAdminPwd.Name = "TxtAdminPwd";
			this.TxtAdminPwd.Size = new System.Drawing.Size(164, 25);
			this.TxtAdminPwd.TabIndex = 3;
			this.TxtAdminPwd.TextChanged += new System.EventHandler(this.TxtAdminPwd_TextChanged);
			// 
			// BtnUniqueKey
			// 
			this.BtnUniqueKey.Location = new System.Drawing.Point(12, 428);
			this.BtnUniqueKey.Name = "BtnUniqueKey";
			this.BtnUniqueKey.Size = new System.Drawing.Size(90, 29);
			this.BtnUniqueKey.TabIndex = 1;
			this.BtnUniqueKey.Text = "唯一化";
			this.BtnUniqueKey.UseVisualStyleBackColor = true;
			this.BtnUniqueKey.Click += new System.EventHandler(this.BtnUniqueKey_Click);
			// 
			// BtnAdminAuthen
			// 
			this.BtnAdminAuthen.Location = new System.Drawing.Point(194, 397);
			this.BtnAdminAuthen.Name = "BtnAdminAuthen";
			this.BtnAdminAuthen.Size = new System.Drawing.Size(90, 29);
			this.BtnAdminAuthen.TabIndex = 1;
			this.BtnAdminAuthen.Text = "Admin认证";
			this.BtnAdminAuthen.UseVisualStyleBackColor = true;
			this.BtnAdminAuthen.Click += new System.EventHandler(this.BtnAdminAuthen_Click);
			// 
			// BtnUserAuthen
			// 
			this.BtnUserAuthen.Location = new System.Drawing.Point(194, 428);
			this.BtnUserAuthen.Name = "BtnUserAuthen";
			this.BtnUserAuthen.Size = new System.Drawing.Size(90, 29);
			this.BtnUserAuthen.TabIndex = 1;
			this.BtnUserAuthen.Text = "User认证";
			this.BtnUserAuthen.UseVisualStyleBackColor = true;
			this.BtnUserAuthen.Click += new System.EventHandler(this.BtnUserAuthen_Click);
			// 
			// BtnCreateCompanySeed
			// 
			this.BtnCreateCompanySeed.Location = new System.Drawing.Point(286, 397);
			this.BtnCreateCompanySeed.Name = "BtnCreateCompanySeed";
			this.BtnCreateCompanySeed.Size = new System.Drawing.Size(104, 29);
			this.BtnCreateCompanySeed.TabIndex = 1;
			this.BtnCreateCompanySeed.Text = "创建公司种子";
			this.BtnCreateCompanySeed.UseVisualStyleBackColor = true;
			this.BtnCreateCompanySeed.Click += new System.EventHandler(this.BtnCreateCompanySeed_Click);
			// 
			// BtnCreateRsaKeyFile
			// 
			this.BtnCreateRsaKeyFile.Location = new System.Drawing.Point(520, 397);
			this.BtnCreateRsaKeyFile.Name = "BtnCreateRsaKeyFile";
			this.BtnCreateRsaKeyFile.Size = new System.Drawing.Size(143, 29);
			this.BtnCreateRsaKeyFile.TabIndex = 1;
			this.BtnCreateRsaKeyFile.Text = "创建RSA私钥文件";
			this.BtnCreateRsaKeyFile.UseVisualStyleBackColor = true;
			this.BtnCreateRsaKeyFile.Click += new System.EventHandler(this.BtnCreateRsaKeyFile_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(340, 51);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(107, 20);
			this.label3.TabIndex = 2;
			this.label3.Text = "测试加密明文：";
			// 
			// TxtPlain
			// 
			this.TxtPlain.Location = new System.Drawing.Point(453, 46);
			this.TxtPlain.Name = "TxtPlain";
			this.TxtPlain.Size = new System.Drawing.Size(161, 25);
			this.TxtPlain.TabIndex = 3;
			this.TxtPlain.Text = "01234567";
			this.TxtPlain.TextChanged += new System.EventHandler(this.TxtUserPwd_TextChanged);
			// 
			// BtnEncryptByCompanySeed
			// 
			this.BtnEncryptByCompanySeed.Location = new System.Drawing.Point(286, 428);
			this.BtnEncryptByCompanySeed.Name = "BtnEncryptByCompanySeed";
			this.BtnEncryptByCompanySeed.Size = new System.Drawing.Size(104, 29);
			this.BtnEncryptByCompanySeed.TabIndex = 1;
			this.BtnEncryptByCompanySeed.Text = "公司种子加密";
			this.BtnEncryptByCompanySeed.UseVisualStyleBackColor = true;
			this.BtnEncryptByCompanySeed.Click += new System.EventHandler(this.BtnEncryptByCompanySeed_Click);
			// 
			// BtnRestore
			// 
			this.BtnRestore.Location = new System.Drawing.Point(691, 397);
			this.BtnRestore.Name = "BtnRestore";
			this.BtnRestore.Size = new System.Drawing.Size(92, 29);
			this.BtnRestore.TabIndex = 1;
			this.BtnRestore.Text = "一键还原";
			this.BtnRestore.UseVisualStyleBackColor = true;
			this.BtnRestore.Click += new System.EventHandler(this.BtnRestore_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(520, 13);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65, 20);
			this.label4.TabIndex = 2;
			this.label4.Text = "种子码：";
			// 
			// TxtSeed
			// 
			this.TxtSeed.Location = new System.Drawing.Point(577, 10);
			this.TxtSeed.Name = "TxtSeed";
			this.TxtSeed.Size = new System.Drawing.Size(206, 25);
			this.TxtSeed.TabIndex = 3;
			this.TxtSeed.TextChanged += new System.EventHandler(this.TxtAdminPwd_TextChanged);
			// 
			// BtnRsaPriEncrypt
			// 
			this.BtnRsaPriEncrypt.Location = new System.Drawing.Point(520, 428);
			this.BtnRsaPriEncrypt.Name = "BtnRsaPriEncrypt";
			this.BtnRsaPriEncrypt.Size = new System.Drawing.Size(90, 29);
			this.BtnRsaPriEncrypt.TabIndex = 1;
			this.BtnRsaPriEncrypt.Text = "私钥加密";
			this.BtnRsaPriEncrypt.UseVisualStyleBackColor = true;
			this.BtnRsaPriEncrypt.Click += new System.EventHandler(this.BtnRsaPriEncrypt_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(13, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(86, 20);
			this.label5.TabIndex = 2;
			this.label5.Text = "3DES密钥：";
			// 
			// TxtTdesKey
			// 
			this.TxtTdesKey.Location = new System.Drawing.Point(93, 46);
			this.TxtTdesKey.Name = "TxtTdesKey";
			this.TxtTdesKey.Size = new System.Drawing.Size(241, 25);
			this.TxtTdesKey.TabIndex = 3;
			this.TxtTdesKey.TextChanged += new System.EventHandler(this.TxtAdminPwd_TextChanged);
			// 
			// BtnSetUserID
			// 
			this.BtnSetUserID.Location = new System.Drawing.Point(103, 397);
			this.BtnSetUserID.Name = "BtnSetUserID";
			this.BtnSetUserID.Size = new System.Drawing.Size(90, 29);
			this.BtnSetUserID.TabIndex = 1;
			this.BtnSetUserID.Text = "设置用户ID";
			this.BtnSetUserID.UseVisualStyleBackColor = true;
			this.BtnSetUserID.Click += new System.EventHandler(this.BtnSetUserID_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(620, 49);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(66, 20);
			this.label6.TabIndex = 2;
			this.label6.Text = "用户ID：";
			// 
			// TxtUserID
			// 
			this.TxtUserID.Location = new System.Drawing.Point(679, 46);
			this.TxtUserID.Name = "TxtUserID";
			this.TxtUserID.Size = new System.Drawing.Size(104, 25);
			this.TxtUserID.TabIndex = 3;
			this.TxtUserID.Text = "01234567";
			this.TxtUserID.TextChanged += new System.EventHandler(this.TxtUserPwd_TextChanged);
			// 
			// BtnDbgErrorMsg
			// 
			this.BtnDbgErrorMsg.Location = new System.Drawing.Point(693, 428);
			this.BtnDbgErrorMsg.Name = "BtnDbgErrorMsg";
			this.BtnDbgErrorMsg.Size = new System.Drawing.Size(90, 29);
			this.BtnDbgErrorMsg.TabIndex = 1;
			this.BtnDbgErrorMsg.Text = "错误信息";
			this.BtnDbgErrorMsg.UseVisualStyleBackColor = true;
			this.BtnDbgErrorMsg.Click += new System.EventHandler(this.BtnDbgErrorMsg_Click);
			// 
			// BtnEnumKey
			// 
			this.BtnEnumKey.Location = new System.Drawing.Point(12, 397);
			this.BtnEnumKey.Name = "BtnEnumKey";
			this.BtnEnumKey.Size = new System.Drawing.Size(90, 29);
			this.BtnEnumKey.TabIndex = 1;
			this.BtnEnumKey.Text = "枚举锁信息";
			this.BtnEnumKey.UseVisualStyleBackColor = true;
			this.BtnEnumKey.Click += new System.EventHandler(this.BtnEnumKey_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(13, 85);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(93, 20);
			this.label7.TabIndex = 2;
			this.label7.Text = "当前加密狗：";
			// 
			// CbCurrentKey
			// 
			this.CbCurrentKey.FormattingEnabled = true;
			this.CbCurrentKey.Location = new System.Drawing.Point(113, 85);
			this.CbCurrentKey.Name = "CbCurrentKey";
			this.CbCurrentKey.Size = new System.Drawing.Size(670, 27);
			this.CbCurrentKey.TabIndex = 4;
			this.CbCurrentKey.SelectedIndexChanged += new System.EventHandler(this.CbCurrentKey_SelectedIndexChanged);
			// 
			// BtnCreateUserRootKey
			// 
			this.BtnCreateUserRootKey.Location = new System.Drawing.Point(389, 397);
			this.BtnCreateUserRootKey.Name = "BtnCreateUserRootKey";
			this.BtnCreateUserRootKey.Size = new System.Drawing.Size(104, 29);
			this.BtnCreateUserRootKey.TabIndex = 1;
			this.BtnCreateUserRootKey.Text = "创建用户根密钥";
			this.BtnCreateUserRootKey.UseVisualStyleBackColor = true;
			this.BtnCreateUserRootKey.Click += new System.EventHandler(this.BtnCreateUserRootKey_Click);
			// 
			// BtnEncryptByUserRoot
			// 
			this.BtnEncryptByUserRoot.Location = new System.Drawing.Point(389, 428);
			this.BtnEncryptByUserRoot.Name = "BtnEncryptByUserRoot";
			this.BtnEncryptByUserRoot.Size = new System.Drawing.Size(104, 29);
			this.BtnEncryptByUserRoot.TabIndex = 1;
			this.BtnEncryptByUserRoot.Text = "用户根加密";
			this.BtnEncryptByUserRoot.UseVisualStyleBackColor = true;
			this.BtnEncryptByUserRoot.Click += new System.EventHandler(this.BtnEncryptByUserRoot_Click);
			// 
			// KeyTestFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(795, 466);
			this.Controls.Add(this.CbCurrentKey);
			this.Controls.Add(this.TxtTdesKey);
			this.Controls.Add(this.TxtSeed);
			this.Controls.Add(this.TxtAdminPwd);
			this.Controls.Add(this.TxtUserID);
			this.Controls.Add(this.TxtPlain);
			this.Controls.Add(this.TxtUserPwd);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.BtnUserAuthen);
			this.Controls.Add(this.BtnCreateRsaKeyFile);
			this.Controls.Add(this.BtnRestore);
			this.Controls.Add(this.BtnDbgErrorMsg);
			this.Controls.Add(this.BtnRsaPriEncrypt);
			this.Controls.Add(this.BtnEncryptByUserRoot);
			this.Controls.Add(this.BtnEncryptByCompanySeed);
			this.Controls.Add(this.BtnCreateUserRootKey);
			this.Controls.Add(this.BtnCreateCompanySeed);
			this.Controls.Add(this.BtnAdminAuthen);
			this.Controls.Add(this.BtnUniqueKey);
			this.Controls.Add(this.BtnSetUserID);
			this.Controls.Add(this.BtnEnumKey);
			this.Controls.Add(this.TxtDbg);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "KeyTestFrm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "KeyTestFrm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeyTestFrm_FormClosing);
			this.Load += new System.EventHandler(this.KeyTestFrm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TxtDbg;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TxtUserPwd;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox TxtAdminPwd;
		private System.Windows.Forms.Button BtnUniqueKey;
		private System.Windows.Forms.Button BtnAdminAuthen;
		private System.Windows.Forms.Button BtnUserAuthen;
		private System.Windows.Forms.Button BtnCreateCompanySeed;
		private System.Windows.Forms.Button BtnCreateRsaKeyFile;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox TxtPlain;
		private System.Windows.Forms.Button BtnEncryptByCompanySeed;
		private System.Windows.Forms.Button BtnRestore;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox TxtSeed;
		private System.Windows.Forms.Button BtnRsaPriEncrypt;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox TxtTdesKey;
		private System.Windows.Forms.Button BtnSetUserID;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox TxtUserID;
		private System.Windows.Forms.Button BtnDbgErrorMsg;
		private System.Windows.Forms.Button BtnEnumKey;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox CbCurrentKey;
		private System.Windows.Forms.Button BtnCreateUserRootKey;
		private System.Windows.Forms.Button BtnEncryptByUserRoot;
	}
}