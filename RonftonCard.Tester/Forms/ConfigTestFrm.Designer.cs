namespace RonftonCard.Tester.Forms
{
	partial class ConfigTestFrm
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
			this.BtnReaderInit = new System.Windows.Forms.Button();
			this.BtnDbgCardTemplete = new System.Windows.Forms.Button();
			this.BtnDbgCardEntity = new System.Windows.Forms.Button();
			this.BtnWriteVirtualCard = new System.Windows.Forms.Button();
			this.TxtDbg = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// BtnReaderInit
			// 
			this.BtnReaderInit.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnReaderInit.Location = new System.Drawing.Point(359, 424);
			this.BtnReaderInit.Name = "BtnReaderInit";
			this.BtnReaderInit.Size = new System.Drawing.Size(110, 32);
			this.BtnReaderInit.TabIndex = 6;
			this.BtnReaderInit.Text = "ReaderInit";
			this.BtnReaderInit.UseVisualStyleBackColor = true;
			this.BtnReaderInit.Click += new System.EventHandler(this.BtnReaderInit_Click);
			// 
			// BtnDbgCardTemplete
			// 
			this.BtnDbgCardTemplete.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnDbgCardTemplete.Location = new System.Drawing.Point(11, 424);
			this.BtnDbgCardTemplete.Name = "BtnDbgCardTemplete";
			this.BtnDbgCardTemplete.Size = new System.Drawing.Size(110, 32);
			this.BtnDbgCardTemplete.TabIndex = 7;
			this.BtnDbgCardTemplete.Text = "显示卡片模板";
			this.BtnDbgCardTemplete.UseVisualStyleBackColor = true;
			this.BtnDbgCardTemplete.Click += new System.EventHandler(this.BtnDbgCardTemplete_Click);
			// 
			// BtnDbgCardEntity
			// 
			this.BtnDbgCardEntity.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnDbgCardEntity.Location = new System.Drawing.Point(127, 424);
			this.BtnDbgCardEntity.Name = "BtnDbgCardEntity";
			this.BtnDbgCardEntity.Size = new System.Drawing.Size(110, 32);
			this.BtnDbgCardEntity.TabIndex = 8;
			this.BtnDbgCardEntity.Text = "显示测试数据";
			this.BtnDbgCardEntity.UseVisualStyleBackColor = true;
			this.BtnDbgCardEntity.Click += new System.EventHandler(this.BtnDbgCardEntity_Click);
			// 
			// BtnWriteVirtualCard
			// 
			this.BtnWriteVirtualCard.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnWriteVirtualCard.Location = new System.Drawing.Point(243, 424);
			this.BtnWriteVirtualCard.Name = "BtnWriteVirtualCard";
			this.BtnWriteVirtualCard.Size = new System.Drawing.Size(110, 32);
			this.BtnWriteVirtualCard.TabIndex = 9;
			this.BtnWriteVirtualCard.Text = "虚拟卡测试";
			this.BtnWriteVirtualCard.UseVisualStyleBackColor = true;
			this.BtnWriteVirtualCard.Click += new System.EventHandler(this.BtnWriteVirtualCard_Click);
			// 
			// TxtDbg
			// 
			this.TxtDbg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtDbg.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtDbg.Location = new System.Drawing.Point(13, 13);
			this.TxtDbg.Multiline = true;
			this.TxtDbg.Name = "TxtDbg";
			this.TxtDbg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TxtDbg.Size = new System.Drawing.Size(753, 405);
			this.TxtDbg.TabIndex = 10;
			this.TxtDbg.WordWrap = false;
			// 
			// ConfigTestFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(778, 468);
			this.Controls.Add(this.TxtDbg);
			this.Controls.Add(this.BtnReaderInit);
			this.Controls.Add(this.BtnDbgCardTemplete);
			this.Controls.Add(this.BtnDbgCardEntity);
			this.Controls.Add(this.BtnWriteVirtualCard);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConfigTestFrm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ConfigTestFrm";
			this.Load += new System.EventHandler(this.ConfigTestFrm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnReaderInit;
		private System.Windows.Forms.Button BtnDbgCardTemplete;
		private System.Windows.Forms.Button BtnDbgCardEntity;
		private System.Windows.Forms.Button BtnWriteVirtualCard;
		private System.Windows.Forms.TextBox TxtDbg;
	}
}