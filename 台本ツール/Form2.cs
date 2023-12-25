using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 台本ツール
{
	public partial class Form2 : Form
	{

		private Form1 m_parent;

		public bool isAutoCut = false;
		public bool isIDSet = false;
		public int cutLimit = 5;

		public Form2(Form1 parent)
		{
			InitializeComponent();

			m_parent = parent;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			isAutoCut = checkBox1.Checked;
			isIDSet = checkBox2.Checked;

			if (int.TryParse(textBox1.Text, out cutLimit) == false) cutLimit = 5;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void Form2_Load(object sender, EventArgs e)
		{
			int x = System.Windows.Forms.Cursor.Position.X;
			int y = System.Windows.Forms.Cursor.Position.Y;

			this.Left = x;
			this.Top = y;
		}
	}
}
