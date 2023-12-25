using Hnx8.ReadJEnc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using static SkiaSharp.HarfBuzz.SKShaper;

public class ScriptManager
{
	//ファイル名、ファイルパス、テキスト全文
	private List<string>					filePathList		= new List<string>();
	private List<string>					allTextList			= new List<string>();
	
	//文単位で分割したメインデータ。ファイル数>文数>文
	private List<ScenarioFIle>				scenarioList		= new List<ScenarioFIle>();

	Dictionary<string,int>					dupNameTable		= new Dictionary<string, int>();



	private	string							namingEval			= "{FILE}_{CHAR}_{C_NO}";

	public Dictionary<string,CharVoiceSet>	charVoiceSetDic		= new Dictionary<string,CharVoiceSet>();




	public ScriptManager()
	{

	}

	public void ClearAll() 
	{
		filePathList.Clear();
		allTextList.Clear();
		scenarioList.Clear();
		
		charVoiceSetDic.Clear();
	}

	public void AddFilePath(string filePaht)
	{
		filePathList.Add(filePaht);
	}



	/// <summary>
	/// 
	/// </summary>
	public void LoadAllText()
	{
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

		int nowID = 0;

		ScenarioFIle scenarioFile;

		foreach( var filePath in  filePathList )
		{
			System.IO.FileInfo file = new System.IO.FileInfo(filePath);

			// ファイル自動判別読み出しクラスを生成
			using (Hnx8.ReadJEnc.FileReader reader = new FileReader(file))
			{
				Hnx8.ReadJEnc.CharCode c = reader.Read(file);			// 判別読み出し実行。判別結果はReadメソッドの戻り値で把握できます
			
				string name = c.Name;									// 戻り値のNameプロパティから文字コード名を取得できます
			
				System.Text.Encoding enc = c.GetEncoding(); 

				allTextList.Add( reader.Text );
			}

			//------------------------------------

			scenarioFile = new ScenarioFIle();

			string[] allTextSplitCRLF	= allTextList[nowID].Split( Environment.NewLine );
			ScenarioBlock newBlock		= new ScenarioBlock();

			Regex regexNull				= new Regex( @"^[ \t]*$", RegexOptions.Singleline);
			Regex regexComment			= new Regex( @"^[ \t]*//");

			scenarioFile.fileNmae		= System.IO.Path.GetFileNameWithoutExtension(filePath);

			for( int j = 0;j < allTextSplitCRLF.Count(); j++ )
			{
				newBlock.text.Add(allTextSplitCRLF[j]);
				
				if ( j+1 < allTextSplitCRLF.Count() && (regexNull.IsMatch(allTextSplitCRLF[j+1]) || regexComment.IsMatch(allTextSplitCRLF[j+1])) ||  (regexNull.IsMatch(allTextSplitCRLF[j]) || regexComment.IsMatch(allTextSplitCRLF[j])) )
				{
					scenarioFile.scenarioBlocks.Add( newBlock );
					newBlock = new ScenarioBlock();
				}
			}

			nowID++;

			scenarioList.Add(scenarioFile);
		
		}
	}

	public void CrearVoiceTableFromText()
	{
		charVoiceSetDic.Clear();
	}

	public void AddVoiceTableCharName( string name, string voiceID, params string[] nameList )
	{
		if( charVoiceSetDic.ContainsKey(name) == false ) charVoiceSetDic[name] = new CharVoiceSet();

		charVoiceSetDic[name].name.Add(name);
		charVoiceSetDic[name].name.AddRange( nameList.ToList());
		charVoiceSetDic[name].voiceID		= voiceID;
		charVoiceSetDic[name].voiceCount	= 0;
		charVoiceSetDic[name].voiceCountByChapter.Clear();
	}


	public void SetVoiceName()
	{
		PreCountVoice();

		bool doubleBreak = false;

		Dictionary<string,int>	voiceCountChapter	= new Dictionary<string,int>();
		Dictionary<string,int>	voiceCountTortal	= new Dictionary<string,int>();

		string totalName = "";

		foreach( var file in scenarioList )
		{
			foreach ( var line in file.scenarioBlocks )
			{
				foreach( var charList in charVoiceSetDic )
				{
					foreach( var name in charList.Value.name )
					{
						if( line.name == name )
						{
							totalName = namingEval.Replace( "{FILE}",file.fileNmae);
							totalName = totalName.Replace("{CHAR}",charList.Value.voiceID);
							if( voiceCountChapter.ContainsKey(name)) totalName = totalName.Replace("{C_NO}", voiceCountChapter[name].ToString());
							if( voiceCountTortal.ContainsKey(name))totalName = totalName.Replace("{T_NO}",voiceCountTortal[name].ToString());

							line.voiceName = totalName;

							if(voiceCountChapter.ContainsKey(charList.Value.name[0]))
							{
								voiceCountChapter[charList.Value.name[0]]+=1;
							}else{
								voiceCountChapter[charList.Value.name[0]]=1;
							}
							if(voiceCountTortal.ContainsKey(charList.Value.name[0]))
							{
								voiceCountTortal[charList.Value.name[0]]+=1;
							}else{
								voiceCountTortal[charList.Value.name[0]]=1;
							}

							doubleBreak = true;
							break;
						}
					}
					if( doubleBreak ) 
					{
						doubleBreak = false;
						break;
					}
				}
			}

			List<string> keyList = new List<string>(voiceCountChapter.Keys);
			foreach(string key in keyList)	  voiceCountChapter[key] = 0;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public void CreateVoiceTableFromText( bool isAuotIDSet = false )
	{
		charVoiceSetDic.Clear();

		int l = 0;

		try {
		foreach( var txtChapter in  scenarioList )
		{
			foreach( var block in txtChapter.scenarioBlocks )
			{
				if( Regex.Match(block.text[0],@"^[^ /\t].+").Success)
				{
					if( charVoiceSetDic.ContainsKey(block.text[0]) == false )
					{
						charVoiceSetDic[block.text[0]] = new  CharVoiceSet();
						charVoiceSetDic[block.text[0]].name.Add(block.text[0]);
						charVoiceSetDic[block.text[0]].voiceCount = 1;
						
					}else{
						charVoiceSetDic[block.text[0]].voiceCount++;
					}

					if(charVoiceSetDic[block.text[0]].voiceCountByChapter.ContainsKey(txtChapter.fileNmae) )
					{ 
						charVoiceSetDic[block.text[0]].voiceCountByChapter[txtChapter.fileNmae]++;
					}
					else
					{
						charVoiceSetDic[block.text[0]].voiceCountByChapter[txtChapter.fileNmae] = 1;
					}
					
					

					block.name = block.text[0];

				}
			}
			l++;
		}
		}
		catch(Exception ex){

		}
		if( isAuotIDSet == false ) return;
		
		//キャラIDを自動で割り振る
		char topAlpha = (char)65;
		char botAlpha = (char)65;
		int i = 0;
		foreach( var tmp in charVoiceSetDic )
		{
			topAlpha = (char)(65+(i/26));
			botAlpha = (char)(65+(i%26));

			charVoiceSetDic[tmp.Key].voiceID = (topAlpha).ToString()+(botAlpha).ToString();

			i++;
		}
	}

	/// <summary>
	/// キャラテーブルの名義被り数を計算する
	/// </summary>
	public void CheckNameDup()
	{
		dupNameTable.Clear();
		
		foreach( var tmp in charVoiceSetDic )
		{
			foreach( string item in tmp.Value.name )
			{
				if( dupNameTable.ContainsKey(item) == false ) dupNameTable[item] = 1;
				else dupNameTable[item]++;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public void PreCountVoice()
	{

		List<ScenarioBlock> blocks;

		foreach( var tmp in charVoiceSetDic) { charVoiceSetDic[tmp.Key].voiceCount = 0; }


		foreach( var txtChapter in  scenarioList )
		{
			foreach( var block in txtChapter.scenarioBlocks )
			{
			
				foreach( var nameList in charVoiceSetDic )
				{
					foreach( var name in nameList.Value.name )
					{
						if( name != "" && block.text[0] == name )
						{
							string baseName = nameList.Value.name[0];
							block.name = name;
							charVoiceSetDic[baseName].voiceCount++;
						}
					}
				}
			}
		}
	}


	/// <summary>
	/// キャラテーブルからボイス数が一定以下のキャラを除外
	/// </summary>
	/// <param name="cutLimit"></param>
	public void CutOutVoiceTable( int cutLimit )
	{
		var removeList = charVoiceSetDic.Where(kvp=>kvp.Value.voiceCount <= cutLimit).ToList();

		foreach (var item in removeList) {
			charVoiceSetDic.Remove(item.Key);
		}
	}

	
	public class CharVoiceSet
	{
		public string					voiceID				{set;get;}
		public int						voiceCount			{set;get;}
		public Dictionary<string,int>	voiceCountByChapter	{set;get;} = new Dictionary<string,int>();
		public List<string>				name				{set;get;} = new List<string>();

		public CharVoiceSet()
		{
			voiceID			= "";
			voiceCount		= 0;
		}
	}

	public class ScenarioBlock
	{
		public string		voiceName	{ set;get;} = "";
		public string		name		{ set;get;} = "";
		public List<string> text		{get;set; } = new List<string>();
	}

	public class ScenarioFIle
	{
		public string fileNmae { set;get;}	= "";

		public List<ScenarioBlock> scenarioBlocks { set;get;} = new List<ScenarioBlock>();

	}
	
}
