namespace 台本ツール
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			button1 = new Button();
			splitContainer1 = new SplitContainer();
			groupBox2 = new GroupBox();
			dataGridFileList = new DataGridView();
			dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			filePath = new DataGridViewTextBoxColumn();
			groupBox1 = new GroupBox();
			button3 = new Button();
			dataGridCharList = new DataGridView();
			CHAR_NAME = new DataGridViewTextBoxColumn();
			CHAR_ID = new DataGridViewTextBoxColumn();
			voiceCount = new DataGridViewTextBoxColumn();
			AnotherName1 = new DataGridViewTextBoxColumn();
			AnotherName2 = new DataGridViewTextBoxColumn();
			AnotherName3 = new DataGridViewTextBoxColumn();
			groupBox3 = new GroupBox();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridFileList).BeginInit();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridCharList).BeginInit();
			SuspendLayout();
			// 
			// button1
			// 
			button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			button1.Location = new Point(912, 471);
			button1.Name = "button1";
			button1.Size = new Size(129, 55);
			button1.TabIndex = 0;
			button1.Text = "出力";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// splitContainer1
			// 
			splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			splitContainer1.Location = new Point(12, 12);
			splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			splitContainer1.Panel1.Controls.Add(groupBox2);
			// 
			// splitContainer1.Panel2
			// 
			splitContainer1.Panel2.Controls.Add(groupBox1);
			splitContainer1.Size = new Size(1029, 385);
			splitContainer1.SplitterDistance = 450;
			splitContainer1.TabIndex = 3;
			// 
			// groupBox2
			// 
			groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			groupBox2.Controls.Add(dataGridFileList);
			groupBox2.Location = new Point(17, 13);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new Size(430, 363);
			groupBox2.TabIndex = 2;
			groupBox2.TabStop = false;
			groupBox2.Text = "シナリオ一覧";
			// 
			// dataGridFileList
			// 
			dataGridFileList.AllowDrop = true;
			dataGridFileList.AllowUserToAddRows = false;
			dataGridFileList.AllowUserToDeleteRows = false;
			dataGridFileList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dataGridFileList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridFileList.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, filePath });
			dataGridFileList.Location = new Point(6, 22);
			dataGridFileList.Name = "dataGridFileList";
			dataGridFileList.RowHeadersVisible = false;
			dataGridFileList.Size = new Size(418, 281);
			dataGridFileList.TabIndex = 0;
			dataGridFileList.DragDrop += dataGridViewFile_DragDrop;
			dataGridFileList.DragEnter += dataGridViewFile_DragEnter;
			dataGridFileList.DragOver += dataGridFileList_DragOver;
			dataGridFileList.KeyDown += dataGridFileList_KeyDown;
			dataGridFileList.MouseDown += dataGridFileList_MouseDown;
			dataGridFileList.MouseMove += dataGridFileList_MouseMove;
			// 
			// dataGridViewTextBoxColumn1
			// 
			dataGridViewTextBoxColumn1.HeaderText = "シナリオファイル名";
			dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			dataGridViewTextBoxColumn1.Width = 120;
			// 
			// dataGridViewTextBoxColumn2
			// 
			dataGridViewTextBoxColumn2.HeaderText = "台本に表示する名前";
			dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			dataGridViewTextBoxColumn2.Width = 130;
			// 
			// filePath
			// 
			filePath.HeaderText = "ファイルパス";
			filePath.Name = "filePath";
			filePath.Width = 250;
			// 
			// groupBox1
			// 
			groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			groupBox1.Controls.Add(button3);
			groupBox1.Controls.Add(dataGridCharList);
			groupBox1.Location = new Point(3, 13);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(560, 363);
			groupBox1.TabIndex = 1;
			groupBox1.TabStop = false;
			groupBox1.Text = "キャラ一覧";
			// 
			// button3
			// 
			button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			button3.Location = new Point(457, 309);
			button3.Name = "button3";
			button3.Size = new Size(97, 45);
			button3.TabIndex = 1;
			button3.Text = "ボイス数 更新";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// dataGridCharList
			// 
			dataGridCharList.AllowUserToAddRows = false;
			dataGridCharList.AllowUserToDeleteRows = false;
			dataGridCharList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dataGridCharList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridCharList.Columns.AddRange(new DataGridViewColumn[] { CHAR_NAME, CHAR_ID, voiceCount, AnotherName1, AnotherName2, AnotherName3 });
			dataGridCharList.Location = new Point(6, 21);
			dataGridCharList.Name = "dataGridCharList";
			dataGridCharList.RowHeadersVisible = false;
			dataGridCharList.Size = new Size(548, 282);
			dataGridCharList.TabIndex = 0;
			dataGridCharList.CellValueChanged += dataGridCharList_CellValueChanged;
			dataGridCharList.KeyDown += dataGridCharList_KeyDown;
			// 
			// CHAR_NAME
			// 
			CHAR_NAME.HeaderText = "キャラ名";
			CHAR_NAME.Name = "CHAR_NAME";
			// 
			// CHAR_ID
			// 
			CHAR_ID.HeaderText = "キャラID";
			CHAR_ID.Name = "CHAR_ID";
			CHAR_ID.Width = 70;
			// 
			// voiceCount
			// 
			voiceCount.HeaderText = "ボイス数";
			voiceCount.Name = "voiceCount";
			voiceCount.Width = 70;
			// 
			// AnotherName1
			// 
			AnotherName1.HeaderText = "別名1";
			AnotherName1.Name = "AnotherName1";
			// 
			// AnotherName2
			// 
			AnotherName2.HeaderText = "別名2";
			AnotherName2.Name = "AnotherName2";
			// 
			// AnotherName3
			// 
			AnotherName3.HeaderText = "別名3";
			AnotherName3.Name = "AnotherName3";
			// 
			// groupBox3
			// 
			groupBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			groupBox3.Location = new Point(29, 403);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new Size(480, 127);
			groupBox3.TabIndex = 4;
			groupBox3.TabStop = false;
			groupBox3.Text = "オプション設定";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1053, 538);
			Controls.Add(groupBox3);
			Controls.Add(splitContainer1);
			Controls.Add(button1);
			Name = "Form1";
			Text = "台本作ーる";
			FormClosing += Form1_FormClosing;
			Load += Form1_Load;
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridFileList).EndInit();
			groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridCharList).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Button button1;
		private SplitContainer splitContainer1;
		private GroupBox groupBox2;
		private DataGridView dataGridFileList;
		private GroupBox groupBox1;
		private DataGridView dataGridCharList;
		private GroupBox groupBox3;
		private DataGridViewTextBoxColumn CHAR_NAME;
		private DataGridViewTextBoxColumn CHAR_ID;
		private DataGridViewTextBoxColumn voiceCount;
		private DataGridViewTextBoxColumn AnotherName1;
		private DataGridViewTextBoxColumn AnotherName2;
		private DataGridViewTextBoxColumn AnotherName3;
		private Button button3;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private DataGridViewTextBoxColumn filePath;
	}
}
