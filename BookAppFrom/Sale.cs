using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using BookAppLibrary.Controllers;
using BookAppLibrary.Models;
using BookAppLibrary.Enums;
using BookAppFrom.Main;

namespace BookAppFrom.Sales
{
    public partial class Sale : Form
    {

        
        public Sale()
        {
            InitializeComponent();
        }

        #region 読み取り専用

        /// <summary>
        /// ファイルパス
        /// </summary>
        public static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data");

        #endregion

        public int Count { get; set; }
        private void Sale_Load(object sender, EventArgs e)
        {
            int total = 0;
            for (int i = 0; i < BooksList.Items.Count; i++)
            {
                total += int.Parse(BooksList.Items[i].SubItems[1].Text);
            }
            TotalFeeText.Text = total.ToString();
        }
        private void BooksList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Book book = GetBookList();
            if (book != null)
            {
                
                CodeText.Text = book.Code;
                QuantityText.Text = BooksList.SelectedItems[0].SubItems[2].Text;
                int total = 0;
                for (int i = 0; i < BooksList.Items.Count; i++)
                {
                    total += int.Parse(BooksList.Items[i].SubItems[1].Text);
                }
                TotalFeeText.Text = total.ToString();
            }


        }
        public Book Selected(Book book)
        {
            
            ListViewItem item = new ListViewItem()
            {
                Text = book.Title,
                Tag = book

            };

            if (BookDataController.GetPrice().Where(x => x.Code == book.Code).FirstOrDefault() is Price price)
            {
                item.SubItems.Add(price.GetPrice().ToString());
            }
            
            item.SubItems.Add(Count.ToString());
            
            BooksList.Items.Add(item);
            BooksList.FullRowSelect = true;

            return book;
        }

        private void CodeButton_Click(object sender, EventArgs e)
        {

            string code = CodeText.Text;
            foreach (Book book in BookDataController.GetBook())
            {
                if (book.Code == code)
                {
                   
                    Selected(book);

                }
            }

        }

        private void QuantityButton_Click(object sender, EventArgs e)
        {
            try
            {
                Book book = GetBookList();
                int Totalbook = 0;
                int total = 0;
                if (book == null)
                {
                    throw new Exception("書籍コードが入力されていません");
                }
                if (BookDataController.GetPrice().Where(x => x.Code == book.Code).FirstOrDefault() is Price price)
                {
                    Totalbook = price.GetPrice();
                }
                if (QuantityText.Text == "")
                {
                    throw new Exception("数量が入力されていません");
                }
                Count = int.Parse(QuantityText.Text);
                int Quantity = Count;
                BooksList.SelectedItems[0].SubItems[2].Text = Count.ToString();
                int sum = Totalbook * Quantity;
                BooksList.SelectedItems[0].SubItems[1].Text = sum.ToString();
                for (int i = 0; i < BooksList.Items.Count; i++)
                {
                    total += int.Parse(BooksList.Items[i].SubItems[1].Text);
                }
                TotalFeeText.Text = total.ToString();
            }
            catch (Exception　ex)
            {
                MessageBox.Show(this, "もう一度入力してください。", ex.Message, MessageBoxButtons.OK);
            }
        }

        private void PaymentButton_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (TotalFeeText.Text != "" && PaymentText.Text != "")
                {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
                    int Total = int.Parse(TotalFeeText.Text);

                    int Payment = int.Parse(PaymentText.Text);

                    int sum = Payment - Total;

                    ChangeText.Text = sum.ToString();

                    RegisterSalse();
                    BookDataController bookData = new BookDataController();
                    List<string> code = new List<string>() { };
                    List<string> num = new List<string>() { };
                    for (int i = 0; i < BooksList.Items.Count; i++)
                    {
                        ListViewItem item = BooksList.Items[i];
                        if (item.Tag is Book book)
                        {
                            code.Add(book.Code);
                            bookData.codeList = code;
                            num.Add(BooksList.Items[i].SubItems[1].Text);
                            bookData.numList = num;
                            
                        }
                    }

                    bookData.SetSalse();

                } else
                {
                    throw new Exception("金額が入力されていません");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "もう一度入力してください。", ex.Message, MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// 選択中の書籍を取得する
        /// </summary>
        /// <returns></returns>
        public Book GetBookList()
        {
            if (0 < BooksList.SelectedItems.Count)
            {
                ListViewItem item = BooksList.SelectedItems[0];
                if (item != null)
                {
                    if (item.Tag is Book book)
                    {
                        return book;
                    }
                }
            }
            return null;
        }
        
        public void RegisterSalse()
        {

            try
            {
                //現在の日時を取得
                DateTime dtNow = DateTime.Now;
                int Year = dtNow.Year;
                int Month = dtNow.Month;
                List<int> total = new List<int>() { };
                int count = 0;
                List<string> list = new List<string>() { };
                List<string> listnum = new List<string>() { };
                string[] codenum = new string[] { };
                string numtext = String.Empty;
                string[] sal = new string[] { };

                //ファイルがない状態で　会計をした場合
                if (!File.Exists(Path.Combine(FilePath, $@"MonthData\{Year}\{Year}_{Month}\Salse{Year}_{Month}.txt")))
                {
                    string folder = $@"{FilePath}\MonthData\{Year}\{Year}_{Month}";
                    Directory.CreateDirectory(folder);

                    FileStream path = File.Create(Path.Combine(FilePath, $@"MonthData\{Year}\{Year}_{Month}\Salse{Year}_{Month}.txt"));
                    path.Close();

                    string FileSalse = Path.Combine(FilePath, $@"MonthData\{Year}\{Year}_{Month}\Salse{Year}_{Month}.txt");

                    foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
                    {
                        codenum = data.Split(',');
                        sal = new string[]
                        {
                        codenum[0],
                        count.ToString()
                        };
                        numtext = String.Join(",", sal);
                        listnum.Add(numtext);
                    }

                    for (int i = 0; i < listnum.Count; i++)
                    {
                        File.AppendAllText(FileSalse, $"{listnum[i]}\n");
                    }

                }
            }
            catch (Exception)
            {

            }
            
        }

    }
}
