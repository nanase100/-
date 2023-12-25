namespace 台本ツール
{
	partial class Form2
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
			button1 = new Button();
			button2 = new Button();
			checkBox1 = new CheckBox();
			label1 = new Label();
			textBox1 = new TextBox();
			groupBox1 = new GroupBox();
			checkBox2 = new CheckBox();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// button1
			// 
			button1.DialogResult = DialogResult.Yes;
			button1.Location = new Point(12, 52);
			button1.Name = "button1";
			button1.Size = new Size(151, 34);
			button1.TabIndex = 0;
			button1.Text = "はい";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.DialogResult = DialogResult.No;
			button2.Location = new Point(169, 52);
			button2.Name = "button2";
			button2.Size = new Size(151, 34);
			button2.TabIndex = 1;
			button2.Text = "いいえ";
			button2.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			checkBox1.AutoSize = true;
			checkBox1.Location = new Point(8, 25);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new Size(301, 19);
			checkBox1.TabIndex = 2;
			checkBox1.Text = "ボイス数が　　　　個以下のキャラはモブとして登録しない";
			checkBox1.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 11);
			label1.Name = "label1";
			label1.Size = new Size(196, 30);
			label1.TabIndex = 4;
			label1.Text = "登録したテキストから自動で\r\nボイス有りのキャラ一覧を作成しますか？";
			// 
			// textBox1
			// 
			textBox1.Location = new Point(86, 22);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(34, 23);
			textBox1.TabIndex = 3;
			textBox1.Text = "5";
			textBox1.TextChanged += textBox1_TextChanged;
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(checkBox2);
			groupBox1.Controls.Add(textBox1);
			groupBox1.Controls.Add(checkBox1);
			groupBox1.Location = new Point(9, 92);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(311, 76);
			groupBox1.TabIndex = 6;
			groupBox1.TabStop = false;
			groupBox1.Text = "オプション";
			// 
			// checkBox2
			// 
			checkBox2.AutoSize = true;
			checkBox2.Location = new Point(8, 50);
			checkBox2.Name = "checkBox2";
			checkBox2.Size = new Size(220, 19);
			checkBox2.TabIndex = 6;
			checkBox2.Text = "キャラIDを連続アルファベットで仮設定する";
			checkBox2.UseVisualStyleBackColor = true;
			// 
			// Form2
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(332, 180);
			Controls.Add(groupBox1);
			Controls.Add(label1);
			Controls.Add(button2);
			Controls.Add(button1);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "Form2";
			SizeGripStyle = SizeGripStyle.Hide;
			Text = "キャラ一覧更新確認";
			Load += Form2_Load;
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button button1;
		private Button button2;
		private CheckBox checkBox1;
		private Label label1;
		private TextBox textBox1;
		private GroupBox groupBox1;
		private CheckBox checkBox2;
	}
}