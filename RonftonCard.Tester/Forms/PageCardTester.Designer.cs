namespace RonftonCard.Tester.Forms
{
	partial class PageCardTester
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
			this.BtnInitCard = new System.Windows.Forms.Button();
			this.BtnDbgSector = new System.Windows.Forms.Button();
			this.BtnCardReader = new System.Windows.Forms.Button();
			this.BtnBeep = new System.Windows.Forms.Button();
			this.TxtDbg = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// BtnInitCard
			// 
			this.BtnInitCard.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnInitCard.Location = new System.Drawing.Point(134, 280);
			this.BtnInitCard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnInitCard.Name = "BtnInitCard";
			this.BtnInitCard.Size = new System.Drawing.Size(133, 39);
			this.BtnInitCard.TabIndex = 3;
			this.BtnInitCard.Text = "初始化卡片";
			this.BtnInitCard.UseVisualStyleBackColor = true;
			// 
			// BtnDbgSector
			// 
			this.BtnDbgSector.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnDbgSector.Location = new System.Drawing.Point(134, 320);
			this.BtnDbgSector.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnDbgSector.Name = "BtnDbgSector";
			this.BtnDbgSector.Size = new System.Drawing.Size(133, 39);
			this.BtnDbgSector.TabIndex = 4;
			this.BtnDbgSector.Text = "读卡片扇区";
			this.BtnDbgSector.UseVisualStyleBackColor = true;
			// 
			// BtnCardReader
			// 
			this.BtnCardReader.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnCardReader.Location = new System.Drawing.Point(9, 280);
			this.BtnCardReader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnCardReader.Name = "BtnCardReader";
			this.BtnCardReader.Size = new System.Drawing.Size(124, 39);
			this.BtnCardReader.TabIndex = 5;
			this.BtnCardReader.Text = " 测试读卡器";
			this.BtnCardReader.UseVisualStyleBackColor = true;
			// 
			// BtnBeep
			// 
			this.BtnBeep.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnBeep.Location = new System.Drawing.Point(9, 320);
			this.BtnBeep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnBeep.Name = "BtnBeep";
			this.BtnBeep.Size = new System.Drawing.Size(124, 39);
			this.BtnBeep.TabIndex = 6;
			this.BtnBeep.Text = "蜂鸣测试";
			this.BtnBeep.UseVisualStyleBackColor = true;
			// 
			// TxtDbg
			// 
			this.TxtDbg.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtDbg.Location = new System.Drawing.Point(9, 12);
			this.TxtDbg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.TxtDbg.Multiline = true;
			this.TxtDbg.Name = "TxtDbg";
			this.TxtDbg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TxtDbg.Size = new System.Drawing.Size(723, 257);
			this.TxtDbg.TabIndex = 2;
			// 
			// PageCardTester
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(752, 371);
			this.Controls.Add(this.BtnInitCard);
			this.Controls.Add(this.BtnDbgSector);
			this.Controls.Add(this.BtnCardReader);
			this.Controls.Add(this.BtnBeep);
			this.Controls.Add(this.TxtDbg);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "PageCardTester";
			this.Text = "TabCardTester";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnInitCard;
		private System.Windows.Forms.Button BtnDbgSector;
		private System.Windows.Forms.Button BtnCardReader;
		private System.Windows.Forms.Button BtnBeep;
		private System.Windows.Forms.TextBox TxtDbg;
	}
}