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
			this.TxtKeyB = new System.Windows.Forms.TextBox();
			this.TxtKeyA = new System.Windows.Forms.TextBox();
			this.TxtBlockNum = new System.Windows.Forms.TextBox();
			this.TxtControlBlock = new System.Windows.Forms.TextBox();
			this.CbCardReader = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.BtnUpdateKeyA = new System.Windows.Forms.Button();
			this.BtnWriteBlock = new System.Windows.Forms.Button();
			this.BtnReadBlockA = new System.Windows.Forms.Button();
			this.BtnSelectCard = new System.Windows.Forms.Button();
			this.BtnReaderInit = new System.Windows.Forms.Button();
			this.BtnReadBlockB = new System.Windows.Forms.Button();
			this.BtnUpdateKeyB = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.TxtKeyB);
			this.groupBox1.Controls.Add(this.TxtKeyA);
			this.groupBox1.Controls.Add(this.TxtBlockNum);
			this.groupBox1.Controls.Add(this.TxtControlBlock);
			this.groupBox1.Controls.Add(this.CbCardReader);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label3);
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
			this.groupBox1.Size = new System.Drawing.Size(764, 147);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "参数设置";
			// 
			// TxtKeyB
			// 
			this.TxtKeyB.Location = new System.Drawing.Point(383, 109);
			this.TxtKeyB.Name = "TxtKeyB";
			this.TxtKeyB.Size = new System.Drawing.Size(236, 25);
			this.TxtKeyB.TabIndex = 3;
			this.TxtKeyB.Text = "0f0f0f0f0f0f";
			// 
			// TxtKeyA
			// 
			this.TxtKeyA.Location = new System.Drawing.Point(59, 109);
			this.TxtKeyA.Name = "TxtKeyA";
			this.TxtKeyA.Size = new System.Drawing.Size(251, 25);
			this.TxtKeyA.TabIndex = 3;
			this.TxtKeyA.Text = "010101010101";
			// 
			// TxtBlockNum
			// 
			this.TxtBlockNum.Location = new System.Drawing.Point(693, 109);
			this.TxtBlockNum.Name = "TxtBlockNum";
			this.TxtBlockNum.Size = new System.Drawing.Size(43, 25);
			this.TxtBlockNum.TabIndex = 3;
			this.TxtBlockNum.Text = "0";
			// 
			// TxtControlBlock
			// 
			this.TxtControlBlock.Location = new System.Drawing.Point(395, 73);
			this.TxtControlBlock.Name = "TxtControlBlock";
			this.TxtControlBlock.Size = new System.Drawing.Size(341, 25);
			this.TxtControlBlock.TabIndex = 3;
			// 
			// CbCardReader
			// 
			this.CbCardReader.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CbCardReader.FormattingEnabled = true;
			this.CbCardReader.Location = new System.Drawing.Point(97, 25);
			this.CbCardReader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.CbCardReader.Name = "CbCardReader";
			this.CbCardReader.Size = new System.Drawing.Size(282, 27);
			this.CbCardReader.TabIndex = 1;
			this.CbCardReader.SelectedIndexChanged += new System.EventHandler(this.CbCardReader_SelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label7.Location = new System.Drawing.Point(332, 114);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(45, 20);
			this.label7.TabIndex = 2;
			this.label7.Text = "KeyB:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label6.Location = new System.Drawing.Point(7, 112);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(46, 20);
			this.label6.TabIndex = 2;
			this.label6.Text = "KeyA:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label5.Location = new System.Drawing.Point(625, 112);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(51, 20);
			this.label5.TabIndex = 2;
			this.label5.Text = "块号：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(300, 73);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(87, 20);
			this.label3.TabIndex = 2;
			this.label3.Text = "M1控制块：";
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
			this.label1.Size = new System.Drawing.Size(65, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "读卡器：";
			// 
			// CbCardTemplete
			// 
			this.CbCardTemplete.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CbCardTemplete.FormattingEnabled = true;
			this.CbCardTemplete.Location = new System.Drawing.Point(97, 70);
			this.CbCardTemplete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.CbCardTemplete.Name = "CbCardTemplete";
			this.CbCardTemplete.Size = new System.Drawing.Size(178, 27);
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
			this.BtnExit.Location = new System.Drawing.Point(690, 590);
			this.BtnExit.Name = "BtnExit";
			this.BtnExit.Size = new System.Drawing.Size(86, 32);
			this.BtnExit.TabIndex = 5;
			this.BtnExit.Text = "退出";
			this.BtnExit.UseVisualStyleBackColor = true;
			this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
			// 
			// TxtDbg
			// 
			this.TxtDbg.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.TxtDbg.Location = new System.Drawing.Point(12, 184);
			this.TxtDbg.Multiline = true;
			this.TxtDbg.Name = "TxtDbg";
			this.TxtDbg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TxtDbg.Size = new System.Drawing.Size(764, 299);
			this.TxtDbg.TabIndex = 6;
			// 
			// BtnDbgCardTemplete
			// 
			this.BtnDbgCardTemplete.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnDbgCardTemplete.Location = new System.Drawing.Point(6, 24);
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
			this.BtnDbgCardEntity.Location = new System.Drawing.Point(6, 57);
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
			this.BtnWriteVirtualCard.Location = new System.Drawing.Point(6, 90);
			this.BtnWriteVirtualCard.Name = "BtnWriteVirtualCard";
			this.BtnWriteVirtualCard.Size = new System.Drawing.Size(110, 32);
			this.BtnWriteVirtualCard.TabIndex = 5;
			this.BtnWriteVirtualCard.Text = "虚拟卡测试";
			this.BtnWriteVirtualCard.UseVisualStyleBackColor = true;
			this.BtnWriteVirtualCard.Click += new System.EventHandler(this.BtnWriteVirtualCard_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.BtnDbgCardTemplete);
			this.groupBox2.Controls.Add(this.BtnDbgCardEntity);
			this.groupBox2.Controls.Add(this.BtnWriteVirtualCard);
			this.groupBox2.Location = new System.Drawing.Point(12, 489);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(130, 133);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "配置测试";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.BtnUpdateKeyB);
			this.groupBox3.Controls.Add(this.BtnUpdateKeyA);
			this.groupBox3.Controls.Add(this.BtnWriteBlock);
			this.groupBox3.Controls.Add(this.BtnReadBlockB);
			this.groupBox3.Controls.Add(this.BtnReadBlockA);
			this.groupBox3.Controls.Add(this.BtnSelectCard);
			this.groupBox3.Controls.Add(this.BtnReaderInit);
			this.groupBox3.Location = new System.Drawing.Point(157, 489);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(403, 133);
			this.groupBox3.TabIndex = 7;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "读卡器测试";
			// 
			// BtnUpdateKeyA
			// 
			this.BtnUpdateKeyA.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnUpdateKeyA.Location = new System.Drawing.Point(6, 90);
			this.BtnUpdateKeyA.Name = "BtnUpdateKeyA";
			this.BtnUpdateKeyA.Size = new System.Drawing.Size(110, 32);
			this.BtnUpdateKeyA.TabIndex = 5;
			this.BtnUpdateKeyA.Text = "UpdateKey_A";
			this.BtnUpdateKeyA.UseVisualStyleBackColor = true;
			this.BtnUpdateKeyA.Click += new System.EventHandler(this.BtnUpdateKeyA_Click);
			// 
			// BtnWriteBlock
			// 
			this.BtnWriteBlock.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnWriteBlock.Location = new System.Drawing.Point(238, 24);
			this.BtnWriteBlock.Name = "BtnWriteBlock";
			this.BtnWriteBlock.Size = new System.Drawing.Size(110, 32);
			this.BtnWriteBlock.TabIndex = 5;
			this.BtnWriteBlock.Text = "WriteBlock";
			this.BtnWriteBlock.UseVisualStyleBackColor = true;
			this.BtnWriteBlock.Click += new System.EventHandler(this.BtnWriteBlock_Click);
			// 
			// BtnReadBlockA
			// 
			this.BtnReadBlockA.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnReadBlockA.Location = new System.Drawing.Point(6, 57);
			this.BtnReadBlockA.Name = "BtnReadBlockA";
			this.BtnReadBlockA.Size = new System.Drawing.Size(110, 32);
			this.BtnReadBlockA.TabIndex = 5;
			this.BtnReadBlockA.Text = "ReadBlock_A";
			this.BtnReadBlockA.UseVisualStyleBackColor = true;
			this.BtnReadBlockA.Click += new System.EventHandler(this.BtnReadBlock_A_Click);
			// 
			// BtnSelectCard
			// 
			this.BtnSelectCard.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnSelectCard.Location = new System.Drawing.Point(122, 24);
			this.BtnSelectCard.Name = "BtnSelectCard";
			this.BtnSelectCard.Size = new System.Drawing.Size(110, 32);
			this.BtnSelectCard.TabIndex = 5;
			this.BtnSelectCard.Text = "SelectCard";
			this.BtnSelectCard.UseVisualStyleBackColor = true;
			this.BtnSelectCard.Click += new System.EventHandler(this.BtnSelectCard_Click);
			// 
			// BtnReaderInit
			// 
			this.BtnReaderInit.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnReaderInit.Location = new System.Drawing.Point(6, 24);
			this.BtnReaderInit.Name = "BtnReaderInit";
			this.BtnReaderInit.Size = new System.Drawing.Size(110, 32);
			this.BtnReaderInit.TabIndex = 5;
			this.BtnReaderInit.Text = "ReaderInit";
			this.BtnReaderInit.UseVisualStyleBackColor = true;
			this.BtnReaderInit.Click += new System.EventHandler(this.BtnReaderInit_Click);
			// 
			// BtnReadBlockB
			// 
			this.BtnReadBlockB.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnReadBlockB.Location = new System.Drawing.Point(122, 57);
			this.BtnReadBlockB.Name = "BtnReadBlockB";
			this.BtnReadBlockB.Size = new System.Drawing.Size(110, 32);
			this.BtnReadBlockB.TabIndex = 5;
			this.BtnReadBlockB.Text = "ReadBlock_B";
			this.BtnReadBlockB.UseVisualStyleBackColor = true;
			this.BtnReadBlockB.Click += new System.EventHandler(this.BtnReadBlock_B_Click);
			// 
			// BtnUpdateKeyB
			// 
			this.BtnUpdateKeyB.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnUpdateKeyB.Location = new System.Drawing.Point(122, 90);
			this.BtnUpdateKeyB.Name = "BtnUpdateKeyB";
			this.BtnUpdateKeyB.Size = new System.Drawing.Size(110, 32);
			this.BtnUpdateKeyB.TabIndex = 5;
			this.BtnUpdateKeyB.Text = "UpdateKey_B";
			this.BtnUpdateKeyB.UseVisualStyleBackColor = true;
			this.BtnUpdateKeyB.Click += new System.EventHandler(this.BtnUpdateKeyB_Click);
			// 
			// TestMainFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(795, 630);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.TxtDbg);
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
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox CbCardReader;
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
		private System.Windows.Forms.TextBox TxtControlBlock;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button BtnReaderInit;
		private System.Windows.Forms.Button BtnSelectCard;
		private System.Windows.Forms.Button BtnReadBlockA;
		private System.Windows.Forms.TextBox TxtBlockNum;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button BtnWriteBlock;
		private System.Windows.Forms.Button BtnUpdateKeyA;
		private System.Windows.Forms.TextBox TxtKeyB;
		private System.Windows.Forms.TextBox TxtKeyA;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button BtnReadBlockB;
		private System.Windows.Forms.Button BtnUpdateKeyB;
	}
}