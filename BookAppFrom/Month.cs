using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookAppLibrary.Models;
using BookAppLibrary.Enums;
using BookAppLibrary.Controllers;
using BookAppFrom.Sales;

namespace BookAppFrom.MonthTop
{
    public partial class Month : Form
    {
        public Month()
        {
            InitializeComponent();
        }

        #region 読み取り専用

        /// <summary>
        /// ファイルパス
        /// </summary>
        public static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data");

        #endregion

        public FileInfo file { get; set; }

        private void Month_Load(object sender, EventArgs e)
        {
            //現在の日時を取得
            DateTime dtNow = DateTime.Now;
             int Year = dtNow.Year;
             int Month = dtNow.Month;
            //フォルダー作成
            string folder = $@"{FilePath}\MonthData\{Year}\{Year}_{Month}";
            Directory.CreateDirectory(folder);
            if (!File.Exists($@"{folder}\Salse{Year}_{Month}.txt"))
            {
                FileStream path = File.Create(Path.Combine(folder, $@"Salse{Year}_{Month}.txt"));
                path.Close();
            }

            file = new FileInfo($@"{folder}\Salse{Year}_{Month}.txt");
            string[] code = new string[] { };
            int count = 0;
            //初期状態
            if (file.Length == 0)
            {
                //データファイル作成
               
                string FileSalse = Path.Combine(folder, $@"Salse{Year}_{Month}.txt");
                List<string> list = new List<string>() { };
                foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
                {
                    code = data.Split(',');
                    string[] salse = new string[]
                    {
                        code[0],
                        count.ToString()
                    };
                    string text = String.Join(",", salse);
                    list.Add(text);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    File.AppendAllText(FileSalse, $"{list[i]}\n");
                }
                AllSaleList();
            }
            else
            {
                

                AllSaleList();

            }


        }

        public void AllSaleList()
        {
            int count = 1;
            foreach (Book book in BookDataController.GetBook())
            {
                ListViewItem item = new ListViewItem()
                {
                    Text = count.ToString(),
                    Tag = book,
                };
                item.SubItems.Add(book.Code);
                item.SubItems.Add(book.GetSpecies());
                item.SubItems.Add(book.Title);
                item.SubItems.Add(book.Publisher);
                item.SubItems.Add(book.ReleaseData.ToShortDateString());
                item.SubItems.Add(book.SubStance);
                item.SubItems.Add(book.Stocks.ToString());
                if (BookDataController.GetSalses().Where(x => x.Code == book.Code).FirstOrDefault() is Salse salse)
                {
                    item.SubItems.Add(salse.SalesAmount.ToString());
                }
                count++;
                JanOverallSalseList.Items.Add(item);
                JanOverallSalseList.View = View.Details;
                JanOverallSalseList.FullRowSelect = true;

            }
        }
    }
}
