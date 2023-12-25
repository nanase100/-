using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 台本ツール
{
	public class pdfManager
	{
		private Form1 m_parent;

		public pdfManager(Form1 parent)
		{
			//questpdfを使うときのライセンスレベルの指定。
			QuestPDF.Settings.License = LicenseType.Community;
			m_parent = parent;
		}


		/// <summary>
		/// 
		/// </summary>
		public void CreateSample()
		{
			// code in your main method
			Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.A4);
					page.Margin(2, Unit.Centimetre);
					page.PageColor(Colors.White);
					page.DefaultTextStyle(x => x.FontSize(60));

					page.DefaultTextStyle(x =>
					{
						return x
						.FontFamily("Source Han Sans VF")
						.FontSize(20);
					});

					page.Header()
						.Text("Hello PDF!")
						.SemiBold().FontSize(12).FontColor(Colors.Blue.Medium);

					page.Content()
						.PaddingVertical(1, Unit.Centimetre)
						.Column(x =>
						{
							x.Spacing(20);

							x.Item().Text(Placeholders.LoremIpsum());

							//x.Item().Image(Placeholders.Image(200, 100));
							x.Item().Text("ングの対策は細かい技で堅実に立ち回るのが一番いいかと思う。後は、中距離では早い置き技を置いておくこと。");

						});

					page.Footer()
						.AlignCenter()
						.Text(x =>
						{
							x.Span("Page ");
							x.CurrentPageNumber();
						});
				});
			})
			.GeneratePdf("hello.pdf");
		}






		public void CreateInvoice()
		{
			// 請求書データ生成
			var billingData = DateTime.Now.AddDays(-DateTime.Today.Day);
			var payDate = DateTime.Now.AddMonths(1);
			payDate = payDate.AddDays(-payDate.Day);

			var docNumber = "220934";

			var postCode = "540-0002";
			var address = "大阪府大阪市中央区大阪城 88-88 大阪テキトウビル 12F";
			var company = "株式会社ああいいううええおお";

			var sPostCode = "540-0002";
			var sAddress1 = "大阪府大阪市中央区大阪城 99-99";
			var sAddress2 = "大阪城ビル 2F";
			var sCompany = "システムクラフト";
			var sPayee = "しすくら銀行(000x) 大阪支店(123) 普通　1234789\r\n口座名義 システム タロウ";

			var data = new List<(string name, int count, string unit, int unitAmount, int tax, bool expenses)> {
			("システム開発支援作業", 1, "式", 240000, 10, false),
			("システム開発支援作業", 1, "式", 100000, 8, false),
			("出張旅費", 1, "日", 52000, 10, true)
		};
			// 金額を計算しておく
			var totalTax10 = data.Where(d => d.tax == 10).Sum(d => d.unitAmount * d.count);
			var totalTax8 = data.Where(d => d.tax == 8).Sum(d => d.unitAmount * d.count);
			var totalExp = data.Where(d => d.expenses).Sum(d => d.unitAmount * d.count);
			var totalExpTax = data.Where(d => d.expenses).Sum(d => d.unitAmount * d.count * (d.tax * 0.01 + 1));
			var toalAll = totalTax10 * 1.1 + totalTax8 * 1.08;

			// ドキュメント生成
			Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.A4);
					page.MarginHorizontal(2f, Unit.Centimetre);
					page.MarginVertical(1f, Unit.Centimetre);
					page.DefaultTextStyle(x =>
					{
						x = x.FontFamily("Source Han Sans VF");
						x = x.FontSize(10);
						return x;
					});

					page.Header()
						.Column(column1 =>
						{
							column1.Item().Row(row =>
							{
								row.RelativeItem().Column(column =>
								{
									column.Item().Text($"〒{postCode}");
									column.Item().Text(address).Bold();
								});
								row.ConstantItem(100).Column(column =>
								{
									column.Item().AlignCenter().Text($"No.{docNumber}");
									column.Item().LineHorizontal(0.5f);
									column.Item().AlignCenter().Text(billingData.ToString("yyyy年MM月dd日"));

								});
							});
							column1.Item().Row(row =>
							{
								row.RelativeItem().PaddingTop(0.3f, Unit.Centimetre).Column(column =>
								{
									column.Item().Text(company).FontFamily("Source Han Serif VF").FontSize(15f);
									column.Item().LineHorizontal(1f);
								});
								row.ConstantItem(220).PaddingTop(0.5f, Unit.Centimetre).Column(column =>
								{
									column.Item().PaddingLeft(0.2f, Unit.Centimetre).Text("御中");
								});
							});
							column1.Item().Row(row =>
							{
								row.RelativeItem();
								row.ConstantItem(180).Column(column =>
								{
									column.Item().Text($"〒{sPostCode}").FontSize(10);
									column.Item().Text(sAddress1).FontSize(10);
									column.Item().Text(sAddress2).FontSize(10);
									column.Item().Text(sCompany).FontSize(10);
								});
							});
							column1.Item().PaddingTop(0.5f, Unit.Centimetre).Row(row =>
							{
								row.RelativeItem();
								row.ConstantItem(250).Background(Colors.Grey.Medium).Column(column =>
								{
									column.Item().AlignCenter().Text("御　請　求　書").Bold().FontSize(18).FontColor(Colors.White);
								});
								row.RelativeItem();
							});
							column1.Item().PaddingTop(2).Row(row =>
							{
								row.RelativeItem().Text("下記の通りご請求申し上げます。");
							});
							column1.Item().PaddingTop(-3).Row(row =>
							{
								row.RelativeItem().AlignBottom().Column(column =>
								{
									column.Item().Text("合計金額");
								});
								row.RelativeItem().AlignRight().AlignBottom().Text($"￥{toalAll.ToString("n0")}-").FontSize(18).FontFamily("Source Han Serif VF");
								row.ConstantItem(50);
								row.ConstantItem(150).PaddingLeft(1, Unit.Centimetre).Column(column =>
								{
									column.Item().Row(row1 =>
									{
										row1.ConstantItem(40).Border(1).AlignCenter().PaddingVertical(2).Text("締日").FontSize(10);
										row1.RelativeItem().Border(1).AlignCenter().Text("お支払期限").FontSize(10);
									});
									column.Item().Row(row1 =>
									{
										row1.ConstantItem(40).Border(1).AlignCenter().PaddingVertical(2).Text("末日").FontSize(10);
										row1.RelativeItem().Border(1).AlignCenter().Text(payDate.ToString("yyyy/MM/dd")).FontSize(10);
									});
								});
							});
							column1.Item().PaddingTop(1).Row(row => { row.ConstantItem(280).LineHorizontal(2); });
							column1.Item().PaddingTop(2).Row(row =>
							{
								row.ConstantItem(30).PaddingTop(10).Text("振込先").FontSize(9);
								row.RelativeItem().PaddingTop(10).Border(1).Column(column =>
								{
									column.Item().Row(r => r.RelativeItem().Height(28).AlignMiddle().PaddingLeft(2).Text(sPayee).FontSize(9));
								});
								row.ConstantItem(30);
								row.ConstantItem(45).Height(48).Border(1);
								row.ConstantItem(45).Height(48).Border(1);
								row.ConstantItem(45).Height(48).Border(1);
							});
							// 明細部
							column1.Item().PaddingTop(2).Row(row =>
							{
								row.RelativeItem().Border(1).Height(20).AlignMiddle().AlignCenter().Text("件名").FontSize(10);
								row.ConstantItem(35).Border(1).AlignMiddle().AlignCenter().Text("数量").FontSize(10);
								row.ConstantItem(30).Border(1).AlignMiddle().AlignCenter().Text("単位").FontSize(10);
								row.ConstantItem(60).Border(1).AlignMiddle().AlignCenter().Text("単価").FontSize(10);
								row.ConstantItem(60).Border(1).AlignMiddle().AlignCenter().Text("金額").FontSize(10);
								row.ConstantItem(60).Border(1).AlignMiddle().AlignCenter().Text("消費税").FontSize(10);
							});
							column1.Item().PaddingTop(2).Row(row => { row.RelativeItem().LineHorizontal(1); });
							for (var i = 0; i < 13; i++)
							{
								(string name, int count, string unit, int unitAmount, int tax, bool expenses)? record = data.Count > i ? data[i] : null;
								column1.Item().Row(row =>
								{
									row.RelativeItem().Border(1).BorderHorizontal(0.5f).Column(column =>
									{
										column.Item().Row(r => r.RelativeItem().Height(28).AlignMiddle().PaddingLeft(2).Text($"{record?.name}").FontSize(10));
									});
									row.ConstantItem(35).Border(1).BorderHorizontal(0.5f).AlignMiddle().AlignCenter().Text($"{record?.count}").FontSize(10);
									row.ConstantItem(30).Border(1).BorderHorizontal(0.5f).AlignMiddle().AlignCenter().Text($"{record?.unit}").FontSize(10);
									row.ConstantItem(60).Border(1).BorderHorizontal(0.5f).Column(column =>
									{
										column.Item().Row(r => r.RelativeItem().Height(28).AlignMiddle().AlignRight().PaddingRight(2).Text(record == null ? "" : $"￥{record?.unitAmount.ToString("n0")}-").FontSize(10));
									});
									row.ConstantItem(60).Border(1).BorderHorizontal(0.5f).Column(column =>
									{
										column.Item().Row(r => r.RelativeItem().Height(28).AlignMiddle().AlignRight().PaddingRight(2).Text(record == null ? "" : $"￥{(record?.unitAmount * record?.count)?.ToString("n0")}-").FontSize(10));
									});
									row.ConstantItem(60).Border(1).BorderHorizontal(0.5f).AlignMiddle().AlignCenter().Text(record == null ? "" : $"対象({record?.tax}%)").FontSize(10);
								});
							}
							column1.Item().Row(row =>
							{
								row.RelativeItem().BorderTop(1).Height(28);
								row.ConstantItem(125).Border(1).AlignMiddle().AlignCenter().Text("(10%対象)").FontSize(10);
								row.ConstantItem(60).Border(1).Column(column =>
								{
									column.Item().Row(r => r.RelativeItem().Height(28).AlignMiddle().AlignRight().PaddingRight(2).Text($"￥{totalTax10.ToString("n0")}-").FontSize(10));
								});
								row.ConstantItem(60).Border(1).Column(column =>
								{
									column.Item().Row(r => r.RelativeItem().Height(28).AlignMiddle().AlignRight().PaddingRight(2).Text($"￥{(totalTax10 * 1.1).ToString("n0")}-").FontSize(10));
								});
							});
							column1.Item().Row(row =>
							{
								row.RelativeItem().Height(28);
								row.ConstantItem(125).Border(1).AlignMiddle().AlignCenter().Text("(8%対象)").FontSize(10);
								row.ConstantItem(60).Border(1).Column(column =>
								{
									column.Item().Row(r => r.RelativeItem().Height(28).AlignMiddle().AlignRight().PaddingRight(2).Text($"￥{totalTax8.ToString("n0")}-").FontSize(10));
								});
								row.ConstantItem(60).Border(1).Column(column =>
								{
									column.Item().Row(r => r.RelativeItem().Height(28).AlignMiddle().AlignRight().PaddingRight(2).Text($"￥{(totalTax8 * 1.08).ToString("n0")}-").FontSize(10));
								});
							});
							column1.Item().Row(row =>
							{
								row.RelativeItem().Height(28);
								row.ConstantItem(125).Border(1).AlignMiddle().AlignCenter().Text("経費等計").FontSize(10);
								row.ConstantItem(60).Border(1).Column(column =>
								{
									column.Item().Row(r => r.RelativeItem().Height(28).AlignMiddle().AlignRight().PaddingRight(2).Text($"￥{totalExp.ToString("n0")}-").FontSize(10));
								});
								row.ConstantItem(60).Border(1).Column(column =>
								{
									column.Item().Row(r => r.RelativeItem().Height(28).AlignMiddle().AlignRight().PaddingRight(2).Text($"￥{totalExpTax.ToString("n0")}-").FontSize(10));
								});
							});
							column1.Item().PaddingTop(2).Row(row =>
							{
								row.RelativeItem();
								row.ConstantItem(245).LineHorizontal(1);
							});
							column1.Item().Row(row =>
							{
								row.RelativeItem().Height(28);
								row.ConstantItem(125).Border(1).Background(Colors.Grey.Lighten3).AlignMiddle().AlignCenter().Text("合計").FontSize(10);
								row.ConstantItem(120).Border(1).Background(Colors.Grey.Lighten3).AlignMiddle().AlignCenter().Text($"￥{toalAll.ToString("n0")}-").FontSize(10);
							});
						});
				});
			}).GeneratePdf("invoice.pdf");
		}

	}
}
