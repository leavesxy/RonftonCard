namespace RonftonCard.Main.Forms
{
	partial class AuthenKeyForm
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
			this.CbKeySelected = new System.Windows.Forms.ComboBox();
			this.TxtTdesKey = new System.Windows.Forms.TextBox();
			this.TxtAdminPin = new System.Windows.Forms.TextBox();
			this.TxtUserID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.BtnCreateAuthenKey = new System.Windows.Forms.Button();
			this.BtnRestore = new System.Windows.Forms.Button();
			this.BtnCreateUserRootKey = new System.Windows.Forms.Button();
			this.BtnEnumKey = new System.Windows.Forms.Button();
			this.TxtTrace = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.CbKeyModel = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// CbKeySelected
			// 
			this.CbKeySelected.FormattingEnabled = true;
			this.CbKeySelected.Location = new System.Drawing.Point(113, 76);
			this.CbKeySelected.Name = "CbKeySelected";
			this.CbKeySelected.Size = new System.Drawing.Size(670, 27);
			this.CbKeySelected.TabIndex = 33;
			this.CbKeySelected.SelectedIndexChanged += new System.EventHandler(this.CbKeySelected_SelectedIndexChanged);
			// 
			// TxtTdesKey
			// 
			this.TxtTdesKey.Location = new System.Drawing.Point(336, 44);
			this.TxtTdesKey.Name = "TxtTdesKey";
			this.TxtTdesKey.Size = new System.Drawing.Size(241, 25);
			this.TxtTdesKey.TabIndex = 32;
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
			this.label5.Location = new System.Drawing.Point(258, 47);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(86, 20);
			this.label5.TabIndex = 20;
			this.label5.Text = "3DES密钥：";
			// 
			// BtnCreateAuthenKey
			// 
			this.BtnCreateAuthenKey.Location = new System.Drawing.Point(163, 428);
			this.BtnCreateAuthenKey.Name = "BtnCreateAuthenKey";
			this.BtnCreateAuthenKey.Size = new System.Drawing.Size(148, 29);
			this.BtnCreateAuthenKey.TabIndex = 17;
			this.BtnCreateAuthenKey.Text = "创建用户授权KEY";
			this.BtnCreateAuthenKey.UseVisualStyleBackColor = true;
			this.BtnCreateAuthenKey.Click += new System.EventHandler(this.BtnCreateAuthenKey_Click);
			// 
			// BtnRestore
			// 
			this.BtnRestore.Location = new System.Drawing.Point(12, 428);
			this.BtnRestore.Name = "BtnRestore";
			this.BtnRestore.Size = new System.Drawing.Size(148, 29);
			this.BtnRestore.TabIndex = 16;
			this.BtnRestore.Text = "一键还原";
			this.BtnRestore.UseVisualStyleBackColor = true;
			this.BtnRestore.Click += new System.EventHandler(this.BtnRestore_Click);
			// 
			// BtnCreateUserRootKey
			// 
			this.BtnCreateUserRootKey.Location = new System.Drawing.Point(163, 398);
			this.BtnCreateUserRootKey.Name = "BtnCreateUserRootKey";
			this.BtnCreateUserRootKey.Size = new System.Drawing.Size(148, 29);
			this.BtnCreateUserRootKey.TabIndex = 10;
			this.BtnCreateUserRootKey.Text = "创建用户根密钥KEY";
			this.BtnCreateUserRootKey.UseVisualStyleBackColor = true;
			this.BtnCreateUserRootKey.Click += new System.EventHandler(this.BtnCreateUserRootKey_Click);
			// 
			// BtnEnumKey
			// 
			this.BtnEnumKey.Location = new System.Drawing.Point(12, 398);
			this.BtnEnumKey.Name = "BtnEnumKey";
			this.BtnEnumKey.Size = new System.Drawing.Size(148, 29);
			this.BtnEnumKey.TabIndex = 18;
			this.BtnEnumKey.Text = "枚举KEY";
			this.BtnEnumKey.UseVisualStyleBackColor = true;
			this.BtnEnumKey.Click += new System.EventHandler(this.BtnEnumKey_Click);
			// 
			// TxtTrace
			// 
			this.TxtTrace.Location = new System.Drawing.Point(12, 109);
			this.TxtTrace.Multiline = true;
			this.TxtTrace.Name = "TxtTrace";
			this.TxtTrace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TxtTrace.Size = new System.Drawing.Size(771, 283);
			this.TxtTrace.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(302, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 20);
			this.label1.TabIndex = 24;
			this.label1.Text = "KEY类型：";
			// 
			// CbKeyModel
			// 
			this.CbKeyModel.FormattingEnabled = true;
			this.CbKeyModel.Location = new System.Drawing.Point(369, 11);
			this.CbKeyModel.Name = "CbKeyModel";
			this.CbKeyModel.Size = new System.Drawing.Size(414, 27);
			this.CbKeyModel.TabIndex = 33;
			this.CbKeyModel.SelectedIndexChanged += new System.EventHandler(this.CbKeyModel_SelectedIndexChanged);
			// 
			// AuthenKeyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(795, 466);
			this.Controls.Add(this.CbKeyModel);
			this.Controls.Add(this.CbKeySelected);
			this.Controls.Add(this.TxtTdesKey);
			this.Controls.Add(this.TxtAdminPin);
			this.Controls.Add(this.TxtUserID);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.BtnCreateAuthenKey);
			this.Controls.Add(this.BtnRestore);
			this.Controls.Add(this.BtnCreateUserRootKey);
			this.Controls.Add(this.BtnEnumKey);
			this.Controls.Add(this.TxtTrace);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "AuthenKeyForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AuthenKeyForm";
			this.Load += new System.EventHandler(this.AuthenKeyForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox CbKeySelected;
		private System.Windows.Forms.TextBox TxtTdesKey;
		private System.Windows.Forms.TextBox TxtAdminPin;
		private System.Windows.Forms.TextBox TxtUserID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button BtnCreateAuthenKey;
		private System.Windows.Forms.Button BtnRestore;
		private System.Windows.Forms.Button BtnCreateUserRootKey;
		private System.Windows.Forms.Button BtnEnumKey;
		private System.Windows.Forms.TextBox TxtTrace;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox CbKeyModel;
	}
}