namespace RonftonCard.Tester.Forms
{
	partial class PageDbg
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
			this.BtnDbgStruTemplete = new System.Windows.Forms.Button();
			this.BtnDbgAddrTemplete = new System.Windows.Forms.Button();
			this.BtnChkAddrTemplete = new System.Windows.Forms.Button();
			this.BtnChkStruTemplete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TxtDbg
			// 
			this.TxtDbg.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtDbg.Location = new System.Drawing.Point(12, 14);
			this.TxtDbg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.TxtDbg.Multiline = true;
			this.TxtDbg.Name = "TxtDbg";
			this.TxtDbg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TxtDbg.Size = new System.Drawing.Size(723, 257);
			this.TxtDbg.TabIndex = 0;
			// 
			// BtnDbgStruTemplete
			// 
			this.BtnDbgStruTemplete.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnDbgStruTemplete.Location = new System.Drawing.Point(8, 282);
			this.BtnDbgStruTemplete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnDbgStruTemplete.Name = "BtnDbgStruTemplete";
			this.BtnDbgStruTemplete.Size = new System.Drawing.Size(124, 39);
			this.BtnDbgStruTemplete.TabIndex = 1;
			this.BtnDbgStruTemplete.Text = "显示卡结构";
			this.BtnDbgStruTemplete.UseVisualStyleBackColor = true;
			this.BtnDbgStruTemplete.Click += new System.EventHandler(this.BtnDbgStruTemplete_Click);
			// 
			// BtnDbgAddrTemplete
			// 
			this.BtnDbgAddrTemplete.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnDbgAddrTemplete.Location = new System.Drawing.Point(137, 282);
			this.BtnDbgAddrTemplete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnDbgAddrTemplete.Name = "BtnDbgAddrTemplete";
			this.BtnDbgAddrTemplete.Size = new System.Drawing.Size(133, 39);
			this.BtnDbgAddrTemplete.TabIndex = 1;
			this.BtnDbgAddrTemplete.Text = "显示扇区地址";
			this.BtnDbgAddrTemplete.UseVisualStyleBackColor = true;
			this.BtnDbgAddrTemplete.Click += new System.EventHandler(this.BtnDbgAddrTemplete_Click);
			// 
			// BtnChkAddrTemplete
			// 
			this.BtnChkAddrTemplete.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnChkAddrTemplete.Location = new System.Drawing.Point(137, 322);
			this.BtnChkAddrTemplete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnChkAddrTemplete.Name = "BtnChkAddrTemplete";
			this.BtnChkAddrTemplete.Size = new System.Drawing.Size(133, 39);
			this.BtnChkAddrTemplete.TabIndex = 1;
			this.BtnChkAddrTemplete.Text = "检查扇区地址";
			this.BtnChkAddrTemplete.UseVisualStyleBackColor = true;
			this.BtnChkAddrTemplete.Click += new System.EventHandler(this.BtnChkAddrTemplete_Click);
			// 
			// BtnChkStruTemplete
			// 
			this.BtnChkStruTemplete.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnChkStruTemplete.Location = new System.Drawing.Point(8, 322);
			this.BtnChkStruTemplete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BtnChkStruTemplete.Name = "BtnChkStruTemplete";
			this.BtnChkStruTemplete.Size = new System.Drawing.Size(124, 39);
			this.BtnChkStruTemplete.TabIndex = 1;
			this.BtnChkStruTemplete.Text = "检查卡结构";
			this.BtnChkStruTemplete.UseVisualStyleBackColor = true;
			this.BtnChkStruTemplete.Click += new System.EventHandler(this.BtnChkStruTemplete_Click);
			// 
			// PageDbg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(750, 374);
			this.Controls.Add(this.BtnChkAddrTemplete);
			this.Controls.Add(this.BtnDbgAddrTemplete);
			this.Controls.Add(this.BtnChkStruTemplete);
			this.Controls.Add(this.BtnDbgStruTemplete);
			this.Controls.Add(this.TxtDbg);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "PageDbg";
			this.Text = "TabDbgTemplete";
			this.Load += new System.EventHandler(this.PageDbg_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TxtDbg;
		private System.Windows.Forms.Button BtnDbgStruTemplete;
		private System.Windows.Forms.Button BtnDbgAddrTemplete;
		private System.Windows.Forms.Button BtnChkAddrTemplete;
		private System.Windows.Forms.Button BtnChkStruTemplete;
	}
}