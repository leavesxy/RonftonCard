namespace RonftonCard.Main.Forms
{
	partial class ConfigForm
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
			this.TxtTrace = new System.Windows.Forms.TextBox();
			this.BtnReaderInit = new System.Windows.Forms.Button();
			this.BtnCardTemplete = new System.Windows.Forms.Button();
			this.BtnCardDataTest = new System.Windows.Forms.Button();
			this.BtnVirtualCardTest = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TxtTrace
			// 
			this.TxtTrace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtTrace.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtTrace.Location = new System.Drawing.Point(13, 12);
			this.TxtTrace.Multiline = true;
			this.TxtTrace.Name = "TxtTrace";
			this.TxtTrace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TxtTrace.Size = new System.Drawing.Size(770, 411);
			this.TxtTrace.TabIndex = 15;
			this.TxtTrace.WordWrap = false;
			// 
			// BtnReaderInit
			// 
			this.BtnReaderInit.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnReaderInit.Location = new System.Drawing.Point(359, 429);
			this.BtnReaderInit.Name = "BtnReaderInit";
			this.BtnReaderInit.Size = new System.Drawing.Size(110, 28);
			this.BtnReaderInit.TabIndex = 11;
			this.BtnReaderInit.Text = "ReaderInit";
			this.BtnReaderInit.UseVisualStyleBackColor = true;
			// 
			// BtnCardTemplete
			// 
			this.BtnCardTemplete.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnCardTemplete.Location = new System.Drawing.Point(11, 429);
			this.BtnCardTemplete.Name = "BtnCardTemplete";
			this.BtnCardTemplete.Size = new System.Drawing.Size(110, 28);
			this.BtnCardTemplete.TabIndex = 12;
			this.BtnCardTemplete.Text = "显示卡片模板";
			this.BtnCardTemplete.UseVisualStyleBackColor = true;
			this.BtnCardTemplete.Click += new System.EventHandler(this.BtnCardTemplete_Click);
			// 
			// BtnCardDataTest
			// 
			this.BtnCardDataTest.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnCardDataTest.Location = new System.Drawing.Point(127, 429);
			this.BtnCardDataTest.Name = "BtnCardDataTest";
			this.BtnCardDataTest.Size = new System.Drawing.Size(110, 28);
			this.BtnCardDataTest.TabIndex = 13;
			this.BtnCardDataTest.Text = "显示测试数据";
			this.BtnCardDataTest.UseVisualStyleBackColor = true;
			this.BtnCardDataTest.Click += new System.EventHandler(this.BtnCardDataTest_Click);
			// 
			// BtnVirtualCardTest
			// 
			this.BtnVirtualCardTest.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnVirtualCardTest.Location = new System.Drawing.Point(243, 429);
			this.BtnVirtualCardTest.Name = "BtnVirtualCardTest";
			this.BtnVirtualCardTest.Size = new System.Drawing.Size(110, 28);
			this.BtnVirtualCardTest.TabIndex = 14;
			this.BtnVirtualCardTest.Text = "虚拟卡测试";
			this.BtnVirtualCardTest.UseVisualStyleBackColor = true;
			this.BtnVirtualCardTest.Click += new System.EventHandler(this.BtnVirtualCardTest_Click);
			// 
			// ConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(795, 466);
			this.Controls.Add(this.TxtTrace);
			this.Controls.Add(this.BtnReaderInit);
			this.Controls.Add(this.BtnCardTemplete);
			this.Controls.Add(this.BtnCardDataTest);
			this.Controls.Add(this.BtnVirtualCardTest);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ConfigForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ConfigForm";
			this.Load += new System.EventHandler(this.ConfigForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TxtTrace;
		private System.Windows.Forms.Button BtnReaderInit;
		private System.Windows.Forms.Button BtnCardTemplete;
		private System.Windows.Forms.Button BtnCardDataTest;
		private System.Windows.Forms.Button BtnVirtualCardTest;
	}
}