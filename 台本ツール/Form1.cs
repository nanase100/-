using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;
using System.Collections;
using System.Security.Cryptography;
using System.Windows.Forms;
namespace 台本ツール
{
	public partial class Form1 : Form
	{
		public ScriptManager	m_scriptManager = new ScriptManager();

		bool					isReceive = false;

		private Rectangle		dragBoxFromMouseDown;
		private int				rowID_SRC = -1;

		private bool			IsInit = false;

		private pdfManager		m_pdfManager;

		public Form2			m_voiceConfForm;

		public Form1()
		{
			InitializeComponent();

			FontManager.RegisterFont(File.OpenRead("font/SourceHanSans-VF.ttf"));
			FontManager.RegisterFont(File.OpenRead("font/SourceHanSerif-VF.ttf"));

			
			m_pdfManager = new pdfManager(this);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, EventArgs e)
		{
			m_pdfManager.CreateSample();
			m_pdfManager.CreateInvoice();
		}



		
		private void Form1_Load(object sender, EventArgs e)
		{
			IsInit = true;
		}

		private void dataGridViewFile_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.All;
			else
				e.Effect = DragDropEffects.None;
		}


		private void dataGridViewFile_DragDrop(object sender, DragEventArgs e)
		{
			//コントロール内にドロップされたとき実行される
			//ドロップされたすべてのファイル名を取得する
			string[] fileNameArray = (string[])e.Data.GetData(DataFormats.FileDrop, false);

			if (fileNameArray != null)
			{
				dataGridFileList.Rows.Clear();

				m_scriptManager.ClearAll();

				foreach (var name in fileNameArray)
				{
					if (System.IO.Directory.Exists(name) == true)
					{
						string[] subFileArray = System.IO.Directory.GetFiles(name, "*.txt", System.IO.SearchOption.AllDirectories);

						foreach (var subName in subFileArray)
						{
							var newRowID = dataGridFileList.Rows.Add(System.IO.Path.GetFileNameWithoutExtension(subName));
							dataGridFileList.Rows[newRowID].Cells[2].Value = subName;
						}
					}
					else
					{
						var newRowID = dataGridFileList.Rows.Add(System.IO.Path.GetFileNameWithoutExtension(name));
						dataGridFileList.Rows[newRowID].Cells[2].Value = name;
					}
				}

				ScriptLoad();

				m_scriptManager.SetVoiceName();

				
			}
			else
			{
				Point clientPoint = dataGridFileList.PointToClient(new Point(e.X, e.Y));

				// Get the row index of the item the mouse is below. 
				int rowIndexOfItemUnderMouseToDrop = dataGridFileList.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

				// If the drag operation was a move then remove and insert the row.
				if (e.Effect == DragDropEffects.Move)
				{
					DataGridViewRow rowToMove = dataGridFileList.Rows[rowID_SRC];

					if ((rowIndexOfItemUnderMouseToDrop >= 0) && (rowIndexOfItemUnderMouseToDrop < dataGridFileList.Rows.Count - 1))
					{
						DataGridViewRow newItem = new DataGridViewRow();
						newItem.Cells.Add(new DataGridViewTextBoxCell());
						newItem.Cells.Add(new DataGridViewTextBoxCell());
						newItem.Cells[0].Value = rowToMove.Cells[0].Value;
						newItem.Cells[1].Value = rowToMove.Cells[1].Value;
						dataGridFileList.Rows.RemoveAt(rowID_SRC);
						dataGridFileList.Rows.Insert(rowIndexOfItemUnderMouseToDrop, newItem);//, rowToMove);

						dataGridFileList.ClearSelection();
					}
				}
			}
		}

		private void ScriptLoad()
		{

			foreach (DataGridViewRow row in dataGridFileList.Rows)
			{
				m_scriptManager.AddFilePath(row.Cells[2].Value.ToString());
			}

			m_scriptManager.LoadAllText();
			
			UpdateVoiceList();
		}



		private void UpdateVoiceList()
		{
			isReceive = true;

			//if( System.Windows.Forms.MessageBox.Show("ボイスのあるキャラ一覧を自動で取得・更新しますか？","確認",MessageBoxButtons.YesNo) == DialogResult.No) return;
			
			m_voiceConfForm = new Form2(this);
			var result = m_voiceConfForm.ShowDialog();

			if (result != DialogResult.Yes) return;

			bool isCut = m_voiceConfForm.isAutoCut;
			int cutLimit = m_voiceConfForm.cutLimit;
			bool isIDSet = m_voiceConfForm.isIDSet;

			m_voiceConfForm.Dispose();

			//------------------------------

			m_scriptManager.CreateVoiceTableFromText(isIDSet);
			//m_scriptManager.CountVoice();

			if (isCut) m_scriptManager.CutOutVoiceTable(cutLimit);

			dataGridCharList.Rows.Clear();

			foreach (var tmp in m_scriptManager.charVoiceSetDic)
			{
				dataGridCharList.Rows.Add(tmp.Key);
				dataGridCharList.Rows[dataGridCharList.Rows.Count - 1].Cells[2].Value = tmp.Value.voiceCount;
				dataGridCharList.Rows[dataGridCharList.Rows.Count - 1].Cells[1].Value = tmp.Value.voiceID;
			}

			isReceive = false;
		}




		private void dataGridFileList_MouseDown(object sender, MouseEventArgs e)
		{
			rowID_SRC = dataGridFileList.HitTest(e.X, e.Y).RowIndex;

			if (rowID_SRC != -1)
			{
				System.Drawing.Size dragSize = SystemInformation.DragSize;
				dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
			}
			else
				dragBoxFromMouseDown = Rectangle.Empty;
		}

		private void dataGridFileList_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}

		private void dataGridFileList_MouseMove(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
				{
					DragDropEffects dropEffect = dataGridCharList.DoDragDrop(dataGridCharList.Rows[rowID_SRC], DragDropEffects.Move);
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridCharList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (isReceive || IsInit != true) return;

			m_scriptManager.CrearVoiceTableFromText();

			for (int i = 0; i < dataGridCharList.RowCount; i++)
			{
				string name			= (dataGridCharList.Rows[i].Cells[0].Value != null ? dataGridCharList.Rows[i].Cells[0].Value.ToString() : "");
				string voiceID		= (dataGridCharList.Rows[i].Cells[1].Value != null ? dataGridCharList.Rows[i].Cells[1].Value.ToString() : "");
				string anotherName1	= (dataGridCharList.Rows[i].Cells[3].Value != null ? dataGridCharList.Rows[i].Cells[3].Value.ToString() : "");
				string anotherName2	= (dataGridCharList.Rows[i].Cells[4].Value != null ? dataGridCharList.Rows[i].Cells[4].Value.ToString() : "");
				string anotherName3	= (dataGridCharList.Rows[i].Cells[5].Value != null ? dataGridCharList.Rows[i].Cells[5].Value.ToString() : "");
				m_scriptManager.AddVoiceTableCharName(name, voiceID, anotherName1, anotherName2, anotherName3);
			}

			//m_scriptManager.CountVoice();

			//isReceive = true;

			//for (int i = 0; i < dataGridCharList.RowCount; i++)
			//{
			//	string name = dataGridCharList.Rows[i].Cells[0].Value.ToString();
			//	dataGridCharList.Rows[i].Cells[2].Value = m_scriptManager.charVoiceSetDic[name].voiceCount;
			//}

			//isReceive = false;

			m_scriptManager.CheckNameDup();

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridCharList_KeyDown(object sender, KeyEventArgs e)
		{
			//削除
			if (e.KeyCode == Keys.Delete)
			{
				if (System.Windows.Forms.MessageBox.Show("選択しているキャラのボイス登録を削除してよろしいですか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					HashSet<int> rowList = new HashSet<int>();

					foreach (DataGridViewCell r in dataGridCharList.SelectedCells)
					{
						rowList.Add(r.RowIndex);
					}

					List<int> rowListProc = rowList.OrderByDescending(x => x).ToList();

					for (int i = 0; i < rowListProc.Count; i++)
					{
						dataGridCharList.Rows.RemoveAt(rowListProc[i]);
					}
				}
			}

			//一括貼付け
			if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
			{
				IDataObject data = Clipboard.GetDataObject();
				string str;
				string[] paster;
				if (data.GetDataPresent(DataFormats.Text))
				{
					str = (string)data.GetData(DataFormats.Text);
					paster = str.Split(Environment.NewLine);
					var nowcell	= dataGridCharList.SelectedCells[0];
					int nowRow	= dataGridCharList.SelectedCells[0].RowIndex;
					int actCol	= dataGridCharList.SelectedCells[0].ColumnIndex;
					for (int i = 0; i < paster.Length; i++)
					{
						nowcell.Value = paster[i];

						nowRow++;

						if (dataGridCharList.Rows.Count <= nowRow || dataGridCharList.Columns.Count <= actCol) break;

						nowcell = dataGridCharList.Rows[nowRow].Cells[actCol];
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (System.Windows.Forms.MessageBox.Show("終了してもよろしいですか？", "確認", MessageBoxButtons.YesNo) != DialogResult.Yes)
				e.Cancel = true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridFileList_KeyDown(object sender, KeyEventArgs e)
		{
			//削除
			if (e.KeyCode == Keys.Delete)
			{
				if (System.Windows.Forms.MessageBox.Show("選択しているシナリオ登録を削除してよろしいですか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					HashSet<int> rowList = new HashSet<int>();

					foreach (DataGridViewCell r in dataGridFileList.SelectedCells)
					{
						rowList.Add(r.RowIndex);
					}

					List<int> rowListProc = rowList.OrderByDescending(x => x).ToList();

					for (int i = 0; i < rowListProc.Count; i++)
					{
						dataGridFileList.Rows.RemoveAt(rowListProc[i]);
					}
				}
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			m_scriptManager.PreCountVoice();

			if( m_scriptManager.charVoiceSetDic.Count == 0 ) return;

			isReceive = true;

			int i = 0;
			foreach (var tmp in m_scriptManager.charVoiceSetDic)
			{
				dataGridCharList.Rows[i].Cells[2].Value = tmp.Value.voiceCount;
				i++;
			}

			isReceive = false;
		}
	}



}


