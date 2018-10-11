﻿namespace RonftonCard.Tester.Forms
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
			this.CbReaderType = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.CbCardTemplete = new System.Windows.Forms.ComboBox();
			this.CbCardType = new System.Windows.Forms.ComboBox();
			this.BtnExit = new System.Windows.Forms.Button();
			this.TxtDbg = new System.Windows.Forms.TextBox();
			this.BtnDbgCardTemplete = new System.Windows.Forms.Button();
			this.BtnDbgCardEntity = new System.Windows.Forms.Button();
			this.BtnWriteVirtualCard = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.CbReaderType);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.CbCardTemplete);
			this.groupBox1.Controls.Add(this.CbCardType);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(12, 13);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox1.Size = new System.Drawing.Size(764, 115);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "参数设置";
			// 
			// CbReaderType
			// 
			this.CbReaderType.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CbReaderType.FormattingEnabled = true;
			this.CbReaderType.Location = new System.Drawing.Point(97, 25);
			this.CbReaderType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.CbReaderType.Name = "CbReaderType";
			this.CbReaderType.Size = new System.Drawing.Size(282, 27);
			this.CbReaderType.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(391, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "卡片类型：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(7, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(79, 20);
			this.label4.TabIndex = 0;
			this.label4.Text = "卡片模板：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(7, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "读卡器类型：";
			// 
			// CbCardTemplete
			// 
			this.CbCardTemplete.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CbCardTemplete.FormattingEnabled = true;
			this.CbCardTemplete.Location = new System.Drawing.Point(97, 70);
			this.CbCardTemplete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.CbCardTemplete.Name = "CbCardTemplete";
			this.CbCardTemplete.Size = new System.Drawing.Size(282, 27);
			this.CbCardTemplete.TabIndex = 1;
			this.CbCardTemplete.SelectedIndexChanged += new System.EventHandler(this.CbCardTemplete_SelectedIndexChanged);
			// 
			// CbCardType
			// 
			this.CbCardType.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CbCardType.FormattingEnabled = true;
			this.CbCardType.Location = new System.Drawing.Point(476, 25);
			this.CbCardType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.CbCardType.Name = "CbCardType";
			this.CbCardType.Size = new System.Drawing.Size(260, 27);
			this.CbCardType.TabIndex = 1;
			// 
			// BtnExit
			// 
			this.BtnExit.Location = new System.Drawing.Point(690, 539);
			this.BtnExit.Name = "BtnExit";
			this.BtnExit.Size = new System.Drawing.Size(86, 32);
			this.BtnExit.TabIndex = 5;
			this.BtnExit.Text = "退出";
			this.BtnExit.UseVisualStyleBackColor = true;
			this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
			// 
			// TxtDbg
			// 
			this.TxtDbg.Font = new System.Drawing.Font("Microsoft YaHei Light", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.TxtDbg.Location = new System.Drawing.Point(12, 136);
			this.TxtDbg.Multiline = true;
			this.TxtDbg.Name = "TxtDbg";
			this.TxtDbg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TxtDbg.Size = new System.Drawing.Size(764, 397);
			this.TxtDbg.TabIndex = 6;
			// 
			// BtnDbgCardTemplete
			// 
			this.BtnDbgCardTemplete.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnDbgCardTemplete.Location = new System.Drawing.Point(12, 539);
			this.BtnDbgCardTemplete.Name = "BtnDbgCardTemplete";
			this.BtnDbgCardTemplete.Size = new System.Drawing.Size(110, 32);
			this.BtnDbgCardTemplete.TabIndex = 5;
			this.BtnDbgCardTemplete.Text = "显示卡片模板";
			this.BtnDbgCardTemplete.UseVisualStyleBackColor = true;
			this.BtnDbgCardTemplete.Click += new System.EventHandler(this.BtnDbgCardTemplete_Click);
			// 
			// BtnDbgCardEntity
			// 
			this.BtnDbgCardEntity.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnDbgCardEntity.Location = new System.Drawing.Point(128, 539);
			this.BtnDbgCardEntity.Name = "BtnDbgCardEntity";
			this.BtnDbgCardEntity.Size = new System.Drawing.Size(110, 32);
			this.BtnDbgCardEntity.TabIndex = 5;
			this.BtnDbgCardEntity.Text = "显示测试数据";
			this.BtnDbgCardEntity.UseVisualStyleBackColor = true;
			this.BtnDbgCardEntity.Click += new System.EventHandler(this.BtnDbgCardEntity_Click);
			// 
			// BtnWriteVirtualCard
			// 
			this.BtnWriteVirtualCard.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnWriteVirtualCard.Location = new System.Drawing.Point(244, 539);
			this.BtnWriteVirtualCard.Name = "BtnWriteVirtualCard";
			this.BtnWriteVirtualCard.Size = new System.Drawing.Size(110, 32);
			this.BtnWriteVirtualCard.TabIndex = 5;
			this.BtnWriteVirtualCard.Text = "虚拟卡测试";
			this.BtnWriteVirtualCard.UseVisualStyleBackColor = true;
			this.BtnWriteVirtualCard.Click += new System.EventHandler(this.BtnWriteVirtualCard_Click);
			// 
			// TestMainFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(795, 579);
			this.Controls.Add(this.TxtDbg);
			this.Controls.Add(this.BtnWriteVirtualCard);
			this.Controls.Add(this.BtnDbgCardEntity);
			this.Controls.Add(this.BtnDbgCardTemplete);
			this.Controls.Add(this.BtnExit);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "TestMainFrm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "TestMainFrm";
			this.Load += new System.EventHandler(this.TestMainFrm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox CbReaderType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox CbCardTemplete;
		private System.Windows.Forms.ComboBox CbCardType;
		private System.Windows.Forms.Button BtnExit;
		private System.Windows.Forms.TextBox TxtDbg;
		private System.Windows.Forms.Button BtnDbgCardTemplete;
		private System.Windows.Forms.Button BtnDbgCardEntity;
		private System.Windows.Forms.Button BtnWriteVirtualCard;
	}
}