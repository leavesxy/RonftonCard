namespace RonftonCard.Tester.Forms
{
	partial class KeyRsaPubDecryptFrm
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
			this.label1 = new System.Windows.Forms.Label();
			this.TxtPlain = new System.Windows.Forms.TextBox();
			this.BtnReturn = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.TxtRsaPubKey = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.TxtDbg = new System.Windows.Forms.TextBox();
			this.BtnRsaPubDecrypt = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Plain Text :";
			// 
			// TxtPlain
			// 
			this.TxtPlain.AllowDrop = true;
			this.TxtPlain.Location = new System.Drawing.Point(96, 12);
			this.TxtPlain.Multiline = true;
			this.TxtPlain.Name = "TxtPlain";
			this.TxtPlain.Size = new System.Drawing.Size(737, 57);
			this.TxtPlain.TabIndex = 1;
			// 
			// BtnReturn
			// 
			this.BtnReturn.Location = new System.Drawing.Point(758, 435);
			this.BtnReturn.Name = "BtnReturn";
			this.BtnReturn.Size = new System.Drawing.Size(75, 28);
			this.BtnReturn.TabIndex = 2;
			this.BtnReturn.Text = "返回";
			this.BtnReturn.UseVisualStyleBackColor = true;
			this.BtnReturn.Click += new System.EventHandler(this.BtnReturn_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 78);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "RSA-PUB :";
			// 
			// TxtRsaPubKey
			// 
			this.TxtRsaPubKey.Location = new System.Drawing.Point(95, 75);
			this.TxtRsaPubKey.Multiline = true;
			this.TxtRsaPubKey.Name = "TxtRsaPubKey";
			this.TxtRsaPubKey.Size = new System.Drawing.Size(737, 136);
			this.TxtRsaPubKey.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 220);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 17);
			this.label3.TabIndex = 0;
			this.label3.Text = "Result : ";
			// 
			// TxtDbg
			// 
			this.TxtDbg.Location = new System.Drawing.Point(95, 217);
			this.TxtDbg.Multiline = true;
			this.TxtDbg.Name = "TxtDbg";
			this.TxtDbg.Size = new System.Drawing.Size(737, 136);
			this.TxtDbg.TabIndex = 1;
			// 
			// BtnRsaPubDecrypt
			// 
			this.BtnRsaPubDecrypt.Location = new System.Drawing.Point(677, 435);
			this.BtnRsaPubDecrypt.Name = "BtnRsaPubDecrypt";
			this.BtnRsaPubDecrypt.Size = new System.Drawing.Size(75, 28);
			this.BtnRsaPubDecrypt.TabIndex = 2;
			this.BtnRsaPubDecrypt.Text = "解密";
			this.BtnRsaPubDecrypt.UseVisualStyleBackColor = true;
			this.BtnRsaPubDecrypt.Click += new System.EventHandler(this.BtnRsaPubDecrypt_Click);
			// 
			// KeyRsaPubDecryptFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(846, 470);
			this.Controls.Add(this.BtnRsaPubDecrypt);
			this.Controls.Add(this.BtnReturn);
			this.Controls.Add(this.TxtDbg);
			this.Controls.Add(this.TxtRsaPubKey);
			this.Controls.Add(this.TxtPlain);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "KeyRsaPubDecryptFrm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "KeyRsaPubDecrypt";
			this.Load += new System.EventHandler(this.KeyRsaPubDecryptFrm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TxtPlain;
		private System.Windows.Forms.Button BtnReturn;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox TxtRsaPubKey;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox TxtDbg;
		private System.Windows.Forms.Button BtnRsaPubDecrypt;
	}
}