namespace RonftonCard.Main.Forms
{
	partial class CardForm
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
			this.BntReset = new System.Windows.Forms.Button();
			this.BtnUpdateKeyB = new System.Windows.Forms.Button();
			this.BtnUpdateKeyA = new System.Windows.Forms.Button();
			this.BtnWriteBlock = new System.Windows.Forms.Button();
			this.BtnReadBlockB = new System.Windows.Forms.Button();
			this.BtnReadBlockA = new System.Windows.Forms.Button();
			this.BtnSelectCard = new System.Windows.Forms.Button();
			this.Cb15 = new System.Windows.Forms.CheckBox();
			this.Cb14 = new System.Windows.Forms.CheckBox();
			this.Cb13 = new System.Windows.Forms.CheckBox();
			this.Cb12 = new System.Windows.Forms.CheckBox();
			this.Cb11 = new System.Windows.Forms.CheckBox();
			this.Cb10 = new System.Windows.Forms.CheckBox();
			this.Cb9 = new System.Windows.Forms.CheckBox();
			this.Cb8 = new System.Windows.Forms.CheckBox();
			this.TxtDbg = new System.Windows.Forms.TextBox();
			this.Cb7 = new System.Windows.Forms.CheckBox();
			this.Cb5 = new System.Windows.Forms.CheckBox();
			this.Cb4 = new System.Windows.Forms.CheckBox();
			this.Cb3 = new System.Windows.Forms.CheckBox();
			this.Cb2 = new System.Windows.Forms.CheckBox();
			this.Cb1 = new System.Windows.Forms.CheckBox();
			this.Cb0 = new System.Windows.Forms.CheckBox();
			this.CbAll = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.Cb6 = new System.Windows.Forms.CheckBox();
			this.TxtControlBlock = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.TxtKeyA = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.TxtKeyB = new System.Windows.Forms.TextBox();
			this.BtnInitialize = new System.Windows.Forms.Button();
			this.BtnTestKeyA = new System.Windows.Forms.Button();
			this.BtnTestKeyB = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// BntReset
			// 
			this.BntReset.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BntReset.Location = new System.Drawing.Point(702, 39);
			this.BntReset.Name = "BntReset";
			this.BntReset.Size = new System.Drawing.Size(72, 27);
			this.BntReset.TabIndex = 37;
			this.BntReset.Text = "Reset";
			this.BntReset.UseVisualStyleBackColor = true;
			this.BntReset.Click += new System.EventHandler(this.BntReset_Click);
			// 
			// BtnUpdateKeyB
			// 
			this.BtnUpdateKeyB.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnUpdateKeyB.Location = new System.Drawing.Point(354, 427);
			this.BtnUpdateKeyB.Name = "BtnUpdateKeyB";
			this.BtnUpdateKeyB.Size = new System.Drawing.Size(110, 32);
			this.BtnUpdateKeyB.TabIndex = 30;
			this.BtnUpdateKeyB.Text = "写扇区(B)";
			this.BtnUpdateKeyB.UseVisualStyleBackColor = true;
			this.BtnUpdateKeyB.Click += new System.EventHandler(this.BtnUpdateKeyB_Click);
			// 
			// BtnUpdateKeyA
			// 
			this.BtnUpdateKeyA.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnUpdateKeyA.Location = new System.Drawing.Point(354, 395);
			this.BtnUpdateKeyA.Name = "BtnUpdateKeyA";
			this.BtnUpdateKeyA.Size = new System.Drawing.Size(110, 32);
			this.BtnUpdateKeyA.TabIndex = 31;
			this.BtnUpdateKeyA.Text = "写扇区(A)";
			this.BtnUpdateKeyA.UseVisualStyleBackColor = true;
			this.BtnUpdateKeyA.Click += new System.EventHandler(this.BtnUpdateKeyA_Click);
			// 
			// BtnWriteBlock
			// 
			this.BtnWriteBlock.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnWriteBlock.Location = new System.Drawing.Point(464, 395);
			this.BtnWriteBlock.Name = "BtnWriteBlock";
			this.BtnWriteBlock.Size = new System.Drawing.Size(110, 32);
			this.BtnWriteBlock.TabIndex = 32;
			this.BtnWriteBlock.Text = "写控制块";
			this.BtnWriteBlock.UseVisualStyleBackColor = true;
			this.BtnWriteBlock.Click += new System.EventHandler(this.BtnWriteBlock_Click);
			// 
			// BtnReadBlockB
			// 
			this.BtnReadBlockB.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnReadBlockB.Location = new System.Drawing.Point(244, 427);
			this.BtnReadBlockB.Name = "BtnReadBlockB";
			this.BtnReadBlockB.Size = new System.Drawing.Size(110, 32);
			this.BtnReadBlockB.TabIndex = 33;
			this.BtnReadBlockB.Text = "读取扇区(B)";
			this.BtnReadBlockB.UseVisualStyleBackColor = true;
			this.BtnReadBlockB.Click += new System.EventHandler(this.BtnReadBlockB_Click);
			// 
			// BtnReadBlockA
			// 
			this.BtnReadBlockA.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnReadBlockA.Location = new System.Drawing.Point(244, 395);
			this.BtnReadBlockA.Name = "BtnReadBlockA";
			this.BtnReadBlockA.Size = new System.Drawing.Size(110, 32);
			this.BtnReadBlockA.TabIndex = 34;
			this.BtnReadBlockA.Text = "读取扇区(A)";
			this.BtnReadBlockA.UseVisualStyleBackColor = true;
			this.BtnReadBlockA.Click += new System.EventHandler(this.BtnReadBlockA_Click);
			// 
			// BtnSelectCard
			// 
			this.BtnSelectCard.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnSelectCard.Location = new System.Drawing.Point(24, 395);
			this.BtnSelectCard.Name = "BtnSelectCard";
			this.BtnSelectCard.Size = new System.Drawing.Size(110, 32);
			this.BtnSelectCard.TabIndex = 35;
			this.BtnSelectCard.Text = "寻卡";
			this.BtnSelectCard.UseVisualStyleBackColor = true;
			this.BtnSelectCard.Click += new System.EventHandler(this.BtnSelectCard_Click);
			// 
			// Cb15
			// 
			this.Cb15.AutoSize = true;
			this.Cb15.Location = new System.Drawing.Point(618, 17);
			this.Cb15.Name = "Cb15";
			this.Cb15.Size = new System.Drawing.Size(47, 24);
			this.Cb15.TabIndex = 0;
			this.Cb15.Text = "15";
			this.Cb15.UseVisualStyleBackColor = true;
			// 
			// Cb14
			// 
			this.Cb14.AutoSize = true;
			this.Cb14.Location = new System.Drawing.Point(572, 17);
			this.Cb14.Name = "Cb14";
			this.Cb14.Size = new System.Drawing.Size(47, 24);
			this.Cb14.TabIndex = 0;
			this.Cb14.Text = "14";
			this.Cb14.UseVisualStyleBackColor = true;
			// 
			// Cb13
			// 
			this.Cb13.AutoSize = true;
			this.Cb13.Location = new System.Drawing.Point(527, 17);
			this.Cb13.Name = "Cb13";
			this.Cb13.Size = new System.Drawing.Size(47, 24);
			this.Cb13.TabIndex = 0;
			this.Cb13.Text = "13";
			this.Cb13.UseVisualStyleBackColor = true;
			// 
			// Cb12
			// 
			this.Cb12.AutoSize = true;
			this.Cb12.Location = new System.Drawing.Point(481, 17);
			this.Cb12.Name = "Cb12";
			this.Cb12.Size = new System.Drawing.Size(47, 24);
			this.Cb12.TabIndex = 0;
			this.Cb12.Text = "12";
			this.Cb12.UseVisualStyleBackColor = true;
			// 
			// Cb11
			// 
			this.Cb11.AutoSize = true;
			this.Cb11.Location = new System.Drawing.Point(438, 17);
			this.Cb11.Name = "Cb11";
			this.Cb11.Size = new System.Drawing.Size(47, 24);
			this.Cb11.TabIndex = 0;
			this.Cb11.Text = "11";
			this.Cb11.UseVisualStyleBackColor = true;
			// 
			// Cb10
			// 
			this.Cb10.AutoSize = true;
			this.Cb10.Location = new System.Drawing.Point(392, 17);
			this.Cb10.Name = "Cb10";
			this.Cb10.Size = new System.Drawing.Size(47, 24);
			this.Cb10.TabIndex = 0;
			this.Cb10.Text = "10";
			this.Cb10.UseVisualStyleBackColor = true;
			// 
			// Cb9
			// 
			this.Cb9.AutoSize = true;
			this.Cb9.Location = new System.Drawing.Point(353, 17);
			this.Cb9.Name = "Cb9";
			this.Cb9.Size = new System.Drawing.Size(39, 24);
			this.Cb9.TabIndex = 0;
			this.Cb9.Text = "9";
			this.Cb9.UseVisualStyleBackColor = true;
			// 
			// Cb8
			// 
			this.Cb8.AutoSize = true;
			this.Cb8.Location = new System.Drawing.Point(314, 17);
			this.Cb8.Name = "Cb8";
			this.Cb8.Size = new System.Drawing.Size(39, 24);
			this.Cb8.TabIndex = 0;
			this.Cb8.Text = "8";
			this.Cb8.UseVisualStyleBackColor = true;
			// 
			// TxtDbg
			// 
			this.TxtDbg.Location = new System.Drawing.Point(24, 97);
			this.TxtDbg.Multiline = true;
			this.TxtDbg.Name = "TxtDbg";
			this.TxtDbg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TxtDbg.Size = new System.Drawing.Size(753, 297);
			this.TxtDbg.TabIndex = 29;
			// 
			// Cb7
			// 
			this.Cb7.AutoSize = true;
			this.Cb7.Location = new System.Drawing.Point(275, 17);
			this.Cb7.Name = "Cb7";
			this.Cb7.Size = new System.Drawing.Size(39, 24);
			this.Cb7.TabIndex = 0;
			this.Cb7.Text = "7";
			this.Cb7.UseVisualStyleBackColor = true;
			// 
			// Cb5
			// 
			this.Cb5.AutoSize = true;
			this.Cb5.Location = new System.Drawing.Point(198, 17);
			this.Cb5.Name = "Cb5";
			this.Cb5.Size = new System.Drawing.Size(39, 24);
			this.Cb5.TabIndex = 0;
			this.Cb5.Text = "5";
			this.Cb5.UseVisualStyleBackColor = true;
			// 
			// Cb4
			// 
			this.Cb4.AutoSize = true;
			this.Cb4.Location = new System.Drawing.Point(157, 17);
			this.Cb4.Name = "Cb4";
			this.Cb4.Size = new System.Drawing.Size(39, 24);
			this.Cb4.TabIndex = 0;
			this.Cb4.Text = "4";
			this.Cb4.UseVisualStyleBackColor = true;
			// 
			// Cb3
			// 
			this.Cb3.AutoSize = true;
			this.Cb3.Location = new System.Drawing.Point(118, 17);
			this.Cb3.Name = "Cb3";
			this.Cb3.Size = new System.Drawing.Size(39, 24);
			this.Cb3.TabIndex = 0;
			this.Cb3.Text = "3";
			this.Cb3.UseVisualStyleBackColor = true;
			// 
			// Cb2
			// 
			this.Cb2.AutoSize = true;
			this.Cb2.Location = new System.Drawing.Point(84, 17);
			this.Cb2.Name = "Cb2";
			this.Cb2.Size = new System.Drawing.Size(39, 24);
			this.Cb2.TabIndex = 0;
			this.Cb2.Text = "2";
			this.Cb2.UseVisualStyleBackColor = true;
			// 
			// Cb1
			// 
			this.Cb1.AutoSize = true;
			this.Cb1.Location = new System.Drawing.Point(45, 17);
			this.Cb1.Name = "Cb1";
			this.Cb1.Size = new System.Drawing.Size(39, 24);
			this.Cb1.TabIndex = 0;
			this.Cb1.Text = "1";
			this.Cb1.UseVisualStyleBackColor = true;
			// 
			// Cb0
			// 
			this.Cb0.AutoSize = true;
			this.Cb0.Location = new System.Drawing.Point(7, 17);
			this.Cb0.Name = "Cb0";
			this.Cb0.Size = new System.Drawing.Size(39, 24);
			this.Cb0.TabIndex = 0;
			this.Cb0.Text = "0";
			this.Cb0.UseVisualStyleBackColor = true;
			// 
			// CbAll
			// 
			this.CbAll.AutoSize = true;
			this.CbAll.Location = new System.Drawing.Point(702, 67);
			this.CbAll.Name = "CbAll";
			this.CbAll.Size = new System.Drawing.Size(49, 24);
			this.CbAll.TabIndex = 36;
			this.CbAll.Text = "All";
			this.CbAll.UseVisualStyleBackColor = true;
			this.CbAll.CheckedChanged += new System.EventHandler(this.CbAll_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.Cb15);
			this.groupBox1.Controls.Add(this.Cb14);
			this.groupBox1.Controls.Add(this.Cb13);
			this.groupBox1.Controls.Add(this.Cb12);
			this.groupBox1.Controls.Add(this.Cb11);
			this.groupBox1.Controls.Add(this.Cb10);
			this.groupBox1.Controls.Add(this.Cb9);
			this.groupBox1.Controls.Add(this.Cb8);
			this.groupBox1.Controls.Add(this.Cb7);
			this.groupBox1.Controls.Add(this.Cb6);
			this.groupBox1.Controls.Add(this.Cb5);
			this.groupBox1.Controls.Add(this.Cb4);
			this.groupBox1.Controls.Add(this.Cb3);
			this.groupBox1.Controls.Add(this.Cb2);
			this.groupBox1.Controls.Add(this.Cb1);
			this.groupBox1.Controls.Add(this.Cb0);
			this.groupBox1.Location = new System.Drawing.Point(24, 41);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(672, 49);
			this.groupBox1.TabIndex = 28;
			this.groupBox1.TabStop = false;
			// 
			// Cb6
			// 
			this.Cb6.AutoSize = true;
			this.Cb6.Location = new System.Drawing.Point(237, 17);
			this.Cb6.Name = "Cb6";
			this.Cb6.Size = new System.Drawing.Size(39, 24);
			this.Cb6.TabIndex = 0;
			this.Cb6.Text = "6";
			this.Cb6.UseVisualStyleBackColor = true;
			// 
			// TxtControlBlock
			// 
			this.TxtControlBlock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtControlBlock.Location = new System.Drawing.Point(496, 10);
			this.TxtControlBlock.Name = "TxtControlBlock";
			this.TxtControlBlock.Size = new System.Drawing.Size(233, 25);
			this.TxtControlBlock.TabIndex = 27;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(419, 13);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(87, 20);
			this.label3.TabIndex = 26;
			this.label3.Text = "M1控制块：";
			// 
			// TxtKeyA
			// 
			this.TxtKeyA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtKeyA.Location = new System.Drawing.Point(69, 10);
			this.TxtKeyA.Name = "TxtKeyA";
			this.TxtKeyA.Size = new System.Drawing.Size(140, 25);
			this.TxtKeyA.TabIndex = 25;
			this.TxtKeyA.Text = "010101010101";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label7.Location = new System.Drawing.Point(225, 13);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(45, 20);
			this.label7.TabIndex = 22;
			this.label7.Text = "KeyB:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label6.Location = new System.Drawing.Point(17, 13);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(46, 20);
			this.label6.TabIndex = 23;
			this.label6.Text = "KeyA:";
			// 
			// TxtKeyB
			// 
			this.TxtKeyB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtKeyB.Location = new System.Drawing.Point(273, 10);
			this.TxtKeyB.Name = "TxtKeyB";
			this.TxtKeyB.Size = new System.Drawing.Size(140, 25);
			this.TxtKeyB.TabIndex = 24;
			this.TxtKeyB.Text = "0f0f0f0f0f0f";
			// 
			// BtnInitialize
			// 
			this.BtnInitialize.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnInitialize.Location = new System.Drawing.Point(573, 395);
			this.BtnInitialize.Name = "BtnInitialize";
			this.BtnInitialize.Size = new System.Drawing.Size(110, 32);
			this.BtnInitialize.TabIndex = 32;
			this.BtnInitialize.Text = "初始化";
			this.BtnInitialize.UseVisualStyleBackColor = true;
			this.BtnInitialize.Click += new System.EventHandler(this.BtnInitialize_Click);
			// 
			// BtnTestKeyA
			// 
			this.BtnTestKeyA.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnTestKeyA.Location = new System.Drawing.Point(133, 395);
			this.BtnTestKeyA.Name = "BtnTestKeyA";
			this.BtnTestKeyA.Size = new System.Drawing.Size(110, 32);
			this.BtnTestKeyA.TabIndex = 35;
			this.BtnTestKeyA.Text = "测试A密钥";
			this.BtnTestKeyA.UseVisualStyleBackColor = true;
			this.BtnTestKeyA.Click += new System.EventHandler(this.BtnTestKeyA_Click);
			// 
			// BtnTestKeyB
			// 
			this.BtnTestKeyB.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnTestKeyB.Location = new System.Drawing.Point(133, 427);
			this.BtnTestKeyB.Name = "BtnTestKeyB";
			this.BtnTestKeyB.Size = new System.Drawing.Size(110, 32);
			this.BtnTestKeyB.TabIndex = 35;
			this.BtnTestKeyB.Text = "测试B密钥";
			this.BtnTestKeyB.UseVisualStyleBackColor = true;
			this.BtnTestKeyB.Click += new System.EventHandler(this.BtnTestKeyB_Click);
			// 
			// CardForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(795, 466);
			this.Controls.Add(this.BntReset);
			this.Controls.Add(this.BtnUpdateKeyB);
			this.Controls.Add(this.BtnUpdateKeyA);
			this.Controls.Add(this.BtnInitialize);
			this.Controls.Add(this.BtnWriteBlock);
			this.Controls.Add(this.BtnReadBlockB);
			this.Controls.Add(this.BtnReadBlockA);
			this.Controls.Add(this.BtnTestKeyB);
			this.Controls.Add(this.BtnTestKeyA);
			this.Controls.Add(this.BtnSelectCard);
			this.Controls.Add(this.TxtDbg);
			this.Controls.Add(this.CbAll);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.TxtControlBlock);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.TxtKeyA);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.TxtKeyB);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "CardForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "CardForm";
			this.Activated += new System.EventHandler(this.CardForm_Activated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CardForm_FormClosing);
			this.Load += new System.EventHandler(this.CardForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BntReset;
		private System.Windows.Forms.Button BtnUpdateKeyB;
		private System.Windows.Forms.Button BtnUpdateKeyA;
		private System.Windows.Forms.Button BtnWriteBlock;
		private System.Windows.Forms.Button BtnReadBlockB;
		private System.Windows.Forms.Button BtnReadBlockA;
		private System.Windows.Forms.Button BtnSelectCard;
		private System.Windows.Forms.CheckBox Cb15;
		private System.Windows.Forms.CheckBox Cb14;
		private System.Windows.Forms.CheckBox Cb13;
		private System.Windows.Forms.CheckBox Cb12;
		private System.Windows.Forms.CheckBox Cb11;
		private System.Windows.Forms.CheckBox Cb10;
		private System.Windows.Forms.CheckBox Cb9;
		private System.Windows.Forms.CheckBox Cb8;
		private System.Windows.Forms.TextBox TxtDbg;
		private System.Windows.Forms.CheckBox Cb7;
		private System.Windows.Forms.CheckBox Cb5;
		private System.Windows.Forms.CheckBox Cb4;
		private System.Windows.Forms.CheckBox Cb3;
		private System.Windows.Forms.CheckBox Cb2;
		private System.Windows.Forms.CheckBox Cb1;
		private System.Windows.Forms.CheckBox Cb0;
		private System.Windows.Forms.CheckBox CbAll;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox Cb6;
		private System.Windows.Forms.TextBox TxtControlBlock;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox TxtKeyA;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox TxtKeyB;
		private System.Windows.Forms.Button BtnInitialize;
		private System.Windows.Forms.Button BtnTestKeyA;
		private System.Windows.Forms.Button BtnTestKeyB;
	}
}