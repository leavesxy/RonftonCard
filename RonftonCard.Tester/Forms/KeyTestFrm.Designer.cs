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
			this.BtnGetInfo = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.TxtUserPwd = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.TxtAdminPwd = new System.Windows.Forms.TextBox();
			this.BtnUniqueKey = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TxtDbg
			// 
			this.TxtDbg.Location = new System.Drawing.Point(12, 44);
			this.TxtDbg.Multiline = true;
			this.TxtDbg.Name = "TxtDbg";
			this.TxtDbg.Size = new System.Drawing.Size(736, 330);
			this.TxtDbg.TabIndex = 0;
			// 
			// BtnGetInfo
			// 
			this.BtnGetInfo.Location = new System.Drawing.Point(12, 380);
			this.BtnGetInfo.Name = "BtnGetInfo";
			this.BtnGetInfo.Size = new System.Drawing.Size(75, 29);
			this.BtnGetInfo.TabIndex = 1;
			this.BtnGetInfo.Text = "锁信息";
			this.BtnGetInfo.UseVisualStyleBackColor = true;
			this.BtnGetInfo.Click += new System.EventHandler(this.BtnGetInfo_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "用户密码(BCD)：";
			// 
			// TxtUserPwd
			// 
			this.TxtUserPwd.Location = new System.Drawing.Point(126, 13);
			this.TxtUserPwd.Name = "TxtUserPwd";
			this.TxtUserPwd.Size = new System.Drawing.Size(191, 25);
			this.TxtUserPwd.TabIndex = 3;
			this.TxtUserPwd.Text = "3132333435363738";
			this.TxtUserPwd.TextChanged += new System.EventHandler(this.TxtUserPwd_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(349, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(132, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "开发商密码(BCD)：";
			// 
			// TxtAdminPwd
			// 
			this.TxtAdminPwd.Location = new System.Drawing.Point(487, 13);
			this.TxtAdminPwd.Name = "TxtAdminPwd";
			this.TxtAdminPwd.Size = new System.Drawing.Size(177, 25);
			this.TxtAdminPwd.TabIndex = 3;
			this.TxtAdminPwd.Text = "FFFFFFFFFFFFFFFF";
			this.TxtAdminPwd.TextChanged += new System.EventHandler(this.TxtDevPwd_TextChanged);
			// 
			// BtnUniqueKey
			// 
			this.BtnUniqueKey.Location = new System.Drawing.Point(93, 380);
			this.BtnUniqueKey.Name = "BtnUniqueKey";
			this.BtnUniqueKey.Size = new System.Drawing.Size(75, 29);
			this.BtnUniqueKey.TabIndex = 1;
			this.BtnUniqueKey.Text = "唯一化";
			this.BtnUniqueKey.UseVisualStyleBackColor = true;
			this.BtnUniqueKey.Click += new System.EventHandler(this.BtnUniqueKey_Click);
			// 
			// KeyTestFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(760, 421);
			this.Controls.Add(this.TxtAdminPwd);
			this.Controls.Add(this.TxtUserPwd);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.BtnUniqueKey);
			this.Controls.Add(this.BtnGetInfo);
			this.Controls.Add(this.TxtDbg);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "KeyTestFrm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "KeyTestFrm";
			this.Load += new System.EventHandler(this.KeyTestFrm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TxtDbg;
		private System.Windows.Forms.Button BtnGetInfo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TxtUserPwd;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox TxtAdminPwd;
		private System.Windows.Forms.Button BtnUniqueKey;
	}
}