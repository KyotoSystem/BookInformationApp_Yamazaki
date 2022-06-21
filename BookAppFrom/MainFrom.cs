using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using BookAppLibrary.Controllers;
using BookAppLibrary.Models;
using BookAppFrom.Edit;
using BookAppFrom.Sales;
using BookAppFrom.MonthTop;

namespace BookAppFrom.Main
{
    public partial class MainFrom : Form
    {
       
        public MainFrom()
        {
            InitializeComponent();
        }

        #region 読み取り専用

        /// <summary>
        /// ファイルパス
        /// </summary>
        public static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data");

        #endregion


        public Sale sale = new Sale();

        private void MainFrom_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (Book book in BookDataController.GetBook())
                {
                    ListViewItem item = new ListViewItem()
                    {
                        Text = book.Code,
                        Tag = book,
                    };

                    item.SubItems.Add(book.GetSpecies());
                    item.SubItems.Add(book.Title);
                    item.SubItems.Add(book.Publisher);
                    item.SubItems.Add(book.ReleaseData.ToShortDateString());
                    item.SubItems.Add(book.SubStance);
                    item.SubItems.Add(book.Stocks.ToString());

                    BooksList.Items.Add(item);

                    BooksList.FullRowSelect = true;

                }



            }
            catch(Exception ex)
            {
                
                throw ex;
            }

        }

        private void BooksList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Book book = GetBookList();

            if (book != null)
            {
                string picturebox = Path.Combine(FilePath, $@"{book.Image}");

                BooksPicture.SizeMode = PictureBoxSizeMode.StretchImage;

                BooksPicture.Image = Image.FromFile(picturebox);
                //小説の場合
                if (book is Novel novel)
                {
                    GetGenre(novel);
                }
                //漫画の場合
                else if (book is Comic comic)
                {
                    GetGenre(comic);
                }
                //絵本の場合
                else if (book is PictureBook pictureBook)
                {
                    GetGenre(pictureBook);
                }
                //辞書の場合
                else if (book is Dictionary dictionary)
                {
                    GetGenre(dictionary);
                }
                //料理の場合
                else if (book is CookingBooks cookingBooks)
                {
                    GetGenre(cookingBooks);
                }
                //旅行の場合
                else if (book is TravelBooks travelBooks)
                {
                    GetGenre(travelBooks);
                }
                //趣味の場合
                else if (book is HobbyBooks hobbyBooks)
                {
                    GetGenre(hobbyBooks);
                }
                //歴史の場合
                else if (book is HistoryBooks historyBooks)
                {
                    GetGenre(historyBooks);
                }
                //専門書の場合
                else if (book is TechnicalBooks technicalBooks)
                {
                    GetGenre(technicalBooks);
                }
                //写真集の場合
                else if (book is PhotoBooks photoBooks)
                {
                    GetGenre(photoBooks);
                }
                //グラビア写真集の場合
                else if (book is GravurePhotoBooks gravure)
                {
                    GetGenre(gravure);
                }
                //図書カードの場合
                else if (book is BookCard bookCard)
                {
                    GetGenre(bookCard);
                }
                //景品の場合
                else if (book is Gift gift)
                {
                    GetGenre(gift);
                }
                //その他の場合
                else if (book is Other other)
                {
                    GetGenre(other);
                }

            }
            
            
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {

            string title = TitleText.Text;
            string specise = TypeText.Text;
            string code = CodeText.Text;
            string publisher = PublisherText.Text;
            string releasedata = ReleaseDataText.Text;
            string text = "";

            try
            {

                foreach (Book book in BookDataController.GetBook())
                {
                    bool flag = false;
                    if (book.Title == title){

                       if(book.GetSpecies() == specise){ flag = true; }
                        else if(book.GetSpecies() != specise) {
                            if (text == specise){ flag = true; }
                            if (text == code) { }
                            else if(book.Code != code) { flag = false; }
                            if(text == publisher) { }
                            else if(book.Publisher != publisher){ flag = false; }
                            if (text == releasedata) { }
                            else if (book.ReleaseData.ToShortDateString() != releasedata) { flag = false; }
                        }
                        if (book.Code == code) { flag = true; }
                        else if (book.Code != code)
                        {
                            if (text == code) { flag = true; }
                            if (text == specise) { }
                            else if(book.GetSpecies() != specise) { flag = false; }
                            if (text == publisher) { }
                            else if (book.Publisher != publisher) { flag = false; }
                            if (text == releasedata) { }
                            else if (book.ReleaseData.ToShortDateString() != releasedata) { flag = false; }
                        }
                        if (book.Publisher == publisher) { flag = true; }
                        else if (book.Publisher != publisher)
                        {
                            if (text == publisher) { flag = true; }
                            if (text == specise) { }
                            else if (book.GetSpecies() != specise) { flag = false; }
                            if (text == code) { }
                            else if (book.Code != code) { flag = false; }
                            if (text == releasedata) { }
                            else if (book.ReleaseData.ToShortDateString() != releasedata) { flag = false; }
                        }
                        if (book.ReleaseData.ToShortDateString() == releasedata) { 
                            flag = true;
                            if (text == specise) { }
                            else if (book.GetSpecies() != specise) { flag = false; }
                            if (text == code) { }
                            else if (book.Code != code) { flag = false; }
                            if (text == publisher) { }
                            else if (book.Publisher != publisher) { flag = false; }
                        }
                        else if (book.ReleaseData.ToShortDateString() != releasedata)
                        {
                            if (text == releasedata) { flag = true; }
                            if (text == specise) { }
                            else if (book.GetSpecies() != specise) { flag = false; }
                            if (text == code) { }
                            else if (book.Code != code) { flag = false; }
                            if (text == publisher) { }
                            else if (book.Publisher != publisher) { flag = false; }
                        }

                        if (flag == true)
                        {
                            BooksList.Items.Clear();
                            ListViewItem item = new ListViewItem()
                            {
                                Text = book.Code,
                                Tag = book,
                            };

                            item.SubItems.Add(book.GetSpecies());
                            item.SubItems.Add(book.Title);
                            item.SubItems.Add(book.Publisher);
                            item.SubItems.Add(book.ReleaseData.ToShortDateString());
                            item.SubItems.Add(book.SubStance);
                            item.SubItems.Add(book.Stocks.ToString());

                            BooksList.Items.Add(item);
                            BooksList.FullRowSelect = true;
                        }
                        

                    }else if (book.GetSpecies() == specise)
                    {

                        if (book.Title == title){flag = true;}
                        else if (book.Title != title)
                        {
                            if (text == title){ flag = true; }
                            if(text == code) {}
                            else if (book.Code != code) { flag = false; }
                            if (text == publisher) { }
                            else if (book.Publisher != publisher) { flag = false; }
                            if (text == releasedata) { }
                            else if (book.ReleaseData.ToShortDateString() != releasedata) { flag = false; }
                        }
                        if (book.Code == code) { flag = true; }
                        else if (book.Code != code)
                        {
                            if (text == code) { flag = true; }
                            if (text == title) {}
                            else if (book.Title != title) { flag = false; }
                            if (text == publisher) {}
                            else if (book.Publisher != publisher) { flag = false; }
                            if (text == releasedata) { }
                            else if (book.ReleaseData.ToShortDateString() != releasedata) { flag = false; }
                        }
                        if (book.Publisher == publisher) { flag = true; }
                        else if (book.Publisher != publisher)
                        {
                            if (text == publisher) { flag = true; }
                            if (text == title) {}
                            else if (book.Title != title) { flag = false; }
                            if (text == code) {}
                            else if (book.Code != code) { flag = false; }
                            if (text == releasedata) { }
                            else if (book.ReleaseData.ToShortDateString() != releasedata) { flag = false; }
                        }
                        if (book.ReleaseData.ToShortDateString() == releasedata) { 
                            flag = true;
                            if (text == title) { }
                            else if (book.Title != title) { flag = false; }
                            if (text == code) { }
                            else if (book.Code != code) { flag = false; }
                            if (text == publisher) { }
                            else if (book.Publisher != publisher) { flag = false; }

                        }
                        else if (book.ReleaseData.ToShortDateString() != releasedata)
                        {
                            if (text == releasedata) { flag = true; }
                            if (text == title) { }
                            else if (book.Title != title) { flag = false; }
                            if (text == code) { }
                            else if (book.Code != code) { flag = false; }
                            if (text == publisher) { }
                            else if (book.Publisher != publisher) { flag = false; }
                        }
                        if (flag == true)
                        {
                            BooksList.Items.Clear();
                            ListViewItem item = new ListViewItem()
                            {
                                Text = book.Code,
                                Tag = book,
                            };

                            item.SubItems.Add(book.GetSpecies());
                            item.SubItems.Add(book.Title);
                            item.SubItems.Add(book.Publisher);
                            item.SubItems.Add(book.ReleaseData.ToShortDateString());
                            item.SubItems.Add(book.SubStance);
                            item.SubItems.Add(book.Stocks.ToString());

                            BooksList.Items.Add(item);
                            BooksList.FullRowSelect = true;
                        }
                    }else if(book.Code == code)
                    {
                       if (book.Title == title) { flag = true; }
                        else if (book.Title != title)
                        {
                            if (text == title) { flag = true; }
                            if (text == specise) { }
                            else if (book.GetSpecies() != specise) { flag = false; }
                            if (text == publisher) {}
                            else if (book.Publisher != publisher) { flag = false; }
                            if (text == releasedata) { }
                            else if (book.ReleaseData.ToShortDateString() != releasedata) { flag = false; }
                        }
                        if (book.GetSpecies() == specise) { flag = true; }
                        else if (book.GetSpecies() != specise)
                        {
                            if (text == specise) { flag = true; }
                            if (text == title) {}
                            else if (book.Title != title) { flag = false; }
                            if (text == publisher) {}
                            if (book.Publisher != publisher) { flag = false; }
                            if (text == releasedata) { }
                            else if (book.ReleaseData.ToShortDateString() != releasedata) { flag = false; }
                        }
                        if (book.Publisher == publisher) { flag = true; }
                        else if (book.Publisher != publisher)
                        {
                            if (text == publisher) { flag = true; }
                            if (text == title) {}
                            else if (book.Title != title) { flag = false; }
                            if (text == specise) {}
                            else if (book.GetSpecies() != specise) { flag = false; }
                            if (text == releasedata) { }
                            else if (book.ReleaseData.ToShortDateString() != releasedata) { flag = false; }

                        }
                        if (book.ReleaseData.ToShortDateString() == releasedata) { 
                            flag = true;
                            if (text == title) { }
                            else if (book.Title != title) { flag = false; }
                            if (text == specise) { }
                            else if (book.GetSpecies() != specise) { flag = false; }
                            if (text == publisher) { }
                            else if (book.Publisher != publisher) { flag = false; }
                        }
                        else if (book.ReleaseData.ToShortDateString() != releasedata)
                        {
                            if (text == releasedata) { flag = true; }
                            if (text == title) { }
                            else if (book.Title != title) { flag = false; }
                            if (text == specise) { }
                            else if (book.GetSpecies() != specise) { flag = false; }
                            if (text == publisher) { }
                            else if (book.Publisher != publisher) { flag = false; }
                        }
                        if (flag == true)
                        {
                            BooksList.Items.Clear();
                            ListViewItem item = new ListViewItem()
                            {
                                Text = book.Code,
                                Tag = book,
                            };

                            item.SubItems.Add(book.GetSpecies());
                            item.SubItems.Add(book.Title);
                            item.SubItems.Add(book.Publisher);
                            item.SubItems.Add(book.ReleaseData.ToShortDateString());
                            item.SubItems.Add(book.SubStance);
                            item.SubItems.Add(book.Stocks.ToString());

                            BooksList.Items.Add(item);
                            BooksList.FullRowSelect = true;
                        }
                    }else if(book.Publisher == publisher)
                    {
                        if (book.Title == title) { flag = true; }
                        else if (book.Title != title)
                        {
                            if (text == title) { flag = true; }
                            if (text == specise) {}
                            else if (book.GetSpecies() != specise) { flag = false; }
                            if (text == code) {}
                            else if (book.Code != code) { flag = false; }
                            if (text == releasedata) { }
                            else if (book.ReleaseData.ToShortDateString() != releasedata) { flag = false; }
                        }
                        if (book.GetSpecies() == specise) { flag = true; }
                        else if (book.GetSpecies() != specise)
                        {
                            if (text == specise) { flag = true; }
                            if (text == title) {}
                            else if (book.Title != title) { flag = false; }
                            if (text == code) {}
                            else if (book.Code != code) { flag = false; }
                            if (text == releasedata) { }
                            else if (book.ReleaseData.ToShortDateString() != releasedata) { flag = false; }
                        }
                        if (book.Code == code) { flag = true; }
                        else if (book.Code != code)
                        {
                            if (text == code) { flag = true;}
                            if (text == title) {}
                            else if (book.Title != title) { flag = false; }
                            if (text == specise) {}
                            else if (book.GetSpecies() != specise) { flag = false; }
                            if (text == releasedata) { }
                            else if (book.ReleaseData.ToShortDateString() != releasedata) { flag = false; }
                        }
                        if (book.ReleaseData.ToShortDateString() == releasedata) { 
                            flag = true;
                            if (text == title) { }
                            else if (book.Title != title) { flag = false; }
                            if (text == specise) { }
                            else if (book.GetSpecies() != specise) { flag = false; }
                            if (text == code) { }
                            else if (book.Code != code) { flag = false; }
                        }
                        else if (book.ReleaseData.ToShortDateString() != releasedata)
                        {
                            if (text == releasedata) { flag = true; }
                            if (text == title) { }
                            else if (book.Title != title) { flag = false; }
                            if (text == specise) { }
                            else if (book.GetSpecies() != specise) { flag = false; }
                            if (text == code) { }
                            else if (book.Code != code) { flag = false; }
                        }
                        if (flag == true)
                        {
                            BooksList.Items.Clear();
                            ListViewItem item = new ListViewItem()
                            {
                                Text = book.Code,
                                Tag = book,
                            };

                            item.SubItems.Add(book.GetSpecies());
                            item.SubItems.Add(book.Title);
                            item.SubItems.Add(book.Publisher);
                            item.SubItems.Add(book.ReleaseData.ToShortDateString());
                            item.SubItems.Add(book.SubStance);
                            item.SubItems.Add(book.Stocks.ToString());

                            BooksList.Items.Add(item);
                            BooksList.FullRowSelect = true;
                        }
                    } else if(book.ReleaseData.ToShortDateString() == releasedata)
                    {
                        if (book.Title == title) { flag = true; }
                        else if (book.Title != title)
                        {
                            if (text == title) { flag = true; }
                            if (text == specise) { }
                            else if (book.GetSpecies() != specise) { flag = false; }
                            if (text == code) { }
                            else if (book.Code != code) { flag = false; }
                            if (text == publisher) { }
                            else if (book.Publisher != publisher) { flag = false; }
                        }
                        if (book.GetSpecies() == specise) { flag = true; }
                        else if (book.GetSpecies() != specise)
                        {
                            if (text == specise) { flag = true; }
                            if (text == title) { }
                            else if (book.Title != title) { flag = false; }
                            if (text == code) { }
                            else if (book.Code != code) { flag = false; }
                            if (text == publisher) { }
                            else if (book.Publisher != publisher) { flag = false; }
                        }
                        if (book.Code == code) { flag = true; }
                        else if (book.Code != code)
                        {
                            if (text == code) { flag = true; }
                            if (text == title) { }
                            else if (book.Title != title) { flag = false; }
                            if (text == specise) { }
                            else if (book.GetSpecies() != specise) { flag = false; }
                            if (text == publisher) { }
                            else if (book.Publisher != publisher) { flag = false; }
                        }
                        if (book.Publisher == publisher) { 
                            flag = true;
                            if (text == title) { }
                            else if (book.Title != title) { flag = false; }
                            if (text == specise) { }
                            else if (book.GetSpecies() != specise) { flag = false; }
                            if (text == code) { }
                            else if (book.Code != code) { flag = false; }
                        }
                        else if (book.Publisher != publisher)
                        {
                            if (text == publisher) { flag = true; }
                            if (text == title) { }
                            else if (book.Title != title) { flag = false; }
                            if (text == specise) { }
                            else if (book.GetSpecies() != specise) { flag = false; }
                            if(text == code) { }
                            else if(book.Code != code) { flag = false; }
                        }
                        if (flag == true)
                        {
                            BooksList.Items.Clear();
                            ListViewItem item = new ListViewItem()
                            {
                                Text = book.Code,
                                Tag = book,
                            };

                            item.SubItems.Add(book.GetSpecies());
                            item.SubItems.Add(book.Title);
                            item.SubItems.Add(book.Publisher);
                            item.SubItems.Add(book.ReleaseData.ToShortDateString());
                            item.SubItems.Add(book.SubStance);
                            item.SubItems.Add(book.Stocks.ToString());

                            BooksList.Items.Add(item);
                            BooksList.FullRowSelect = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RegistrationButton_Click(object sender, EventArgs e)
        {
            EditForm edit = new EditForm();
            edit.Flag = true;
            edit.ShowDialog();
            BooksList.Items.Clear();
            MainFrom_Load(sender, e);


        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            Book book = GetBookList();
            
            if (book != null)
            {
                EditForm edit = new EditForm();
                edit.Rodeng(book);
                edit.Flag = false;
                edit.ShowDialog();
                BooksList.Items.Clear();
                MainFrom_Load(sender, e);
            }

        }

        private void DeletionButton_Click(object sender, EventArgs e)
        {
            BookDataController bookData = new BookDataController();
            for (int i = 0; i < BooksList.SelectedItems.Count; i++)
            {

                ListViewItem item = BooksList.SelectedItems[i];
                if (item != null)
                {
                    if (item.Tag is Book book)
                    {
                        bookData.DelBook(book);

                        if (BookDataController.GetPrice().Where(x => x.Code == book.Code).FirstOrDefault() is Price price)
                        {
                            bookData.DelPrice(price);
                        }
                         //小説の場合
                        if(book is Novel novel)
                        {
                            bookData.DelGenre(novel);
                        }//漫画の場合
                        else if (book is Comic comic)
                        {
                            bookData.DelGenre(comic);
                        }
                        //絵本の場合
                        else if (book is PictureBook pictureBook)
                        {
                            bookData.DelGenre(pictureBook);
                        }
                        //辞書の場合
                        else if (book is Dictionary dictionary)
                        {
                            bookData.DelGenre(dictionary);
                        }
                        //料理の場合
                        else if (book is CookingBooks cookingBooks)
                        {
                            bookData.DelGenre(cookingBooks);
                        }
                        //旅行の場合
                        else if (book is TravelBooks travelBooks)
                        {
                            bookData.DelGenre(travelBooks);
                        }
                        //趣味の場合
                        else if (book is HobbyBooks hobbyBooks)
                        {
                            bookData.DelGenre(hobbyBooks);
                        }
                        //歴史の場合
                        else if (book is HistoryBooks historyBooks)
                        {
                            bookData.DelGenre(historyBooks);
                        }
                        //専門書の場合
                        else if (book is TechnicalBooks technicalBooks)
                        {
                            bookData.DelGenre(technicalBooks);
                        }
                        //写真集の場合
                        else if (book is PhotoBooks photoBooks)
                        {
                            bookData.DelGenre(photoBooks);
                        }
                        //グラビア写真集の場合
                        else if (book is GravurePhotoBooks gravure)
                        {
                            bookData.DelGenre(gravure);
                        }
                        //図書カードの場合
                        else if (book is BookCard bookCard)
                        {
                            bookData.DelGenre(bookCard);
                        }
                        //景品の場合
                        else if (book is Gift gift)
                        {
                            bookData.DelGenre(gift);
                        }
                        //その他の場合
                        else if (book is Other other)
                        {
                            bookData.DelGenre(other);
                        }
                    }
                }
            }
            BooksList.Items.Clear();
            MainFrom_Load(sender, e);
        }

        private void SalesButton_Click(object sender, EventArgs e)
        {

            if (sale.Visible == false)
            {
                if (sale == null || sale.IsDisposed)
                {
                    sale = new Sale();
                }
                sale.Show();

            }
            sale.Count = 1;
                for (int i = 0; i < BooksList.SelectedItems.Count; i++)
                {
                    ListViewItem item = BooksList.SelectedItems[i];
                    if (item != null)
                    {
                        if (item.Tag is Book book)
                        {
                       
                            sale.Selected(book);
                        }
                    }
                }
            
            
        }

        private void Top10Item_Click(object sender, EventArgs e)
        {
            Month month = new Month();

            month.ShowDialog();
            
        }

        /// <summary>
        /// 選択中の書籍を取得する
        /// </summary>
        /// <returns></returns>
        public Book GetBookList()
        {
            if(0 < BooksList.SelectedItems.Count)
            {
                ListViewItem item = BooksList.SelectedItems[0];
                if(item != null)
                {
                    if(item.Tag is Book book)
                    {
                        return book;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 小説追加表示
        /// </summary>
        /// <param name="novel">小説</param>
        public void GetGenre(Novel novel)
        {
            NovelsBox.Visible = true;
            ComicBox.Visible = false;
            PictureBook.Visible = false;
            DictionaryBox.Visible = false;
            CookingBooksBox.Visible = false;
            TravelBooksBox.Visible = false;
            HobbyBooksBox.Visible = false;
            HistoryBooksBox.Visible = false;
            TechnicalBooksBox.Visible = false;
            PhotoBooksBox.Visible = false;
            GravurePhotoBooksBox.Visible = false;
            BookCardBox.Visible = false;
            GiftBox.Visible = false;
            OtherBox.Visible = false;

            NovelsAuthorText.Text = novel.Author;
        }
        /// <summary>
        /// 漫画追加表示
        /// </summary>
        /// <param name="comic">漫画</param>
        public void GetGenre(Comic comic)
        {
            NovelsBox.Visible = false;
            ComicBox.Visible = true;
            PictureBook.Visible = false;
            DictionaryBox.Visible = false;
            CookingBooksBox.Visible = false;
            TravelBooksBox.Visible = false;
            HobbyBooksBox.Visible = false;
            HistoryBooksBox.Visible = false;
            TechnicalBooksBox.Visible = false;
            PhotoBooksBox.Visible = false;
            GravurePhotoBooksBox.Visible = false;
            BookCardBox.Visible = false;
            GiftBox.Visible = false;
            OtherBox.Visible = false;

            ComicAuthorText.Text = comic.Author;
            ComicIllustratorText.Text = comic.Illustrator;
        }
        /// <summary>
        /// 絵本追加表示
        /// </summary>
        /// <param name="pictureBook">絵本</param>
        public void GetGenre(PictureBook pictureBook)
        {
            NovelsBox.Visible = false;
            ComicBox.Visible = false;
            PictureBook.Visible = true;
            DictionaryBox.Visible = false;
            CookingBooksBox.Visible = false;
            TravelBooksBox.Visible = false;
            HobbyBooksBox.Visible = false;
            HistoryBooksBox.Visible = false;
            TechnicalBooksBox.Visible = false;
            PhotoBooksBox.Visible = false;
            GravurePhotoBooksBox.Visible = false;
            BookCardBox.Visible = false;
            GiftBox.Visible = false;
            OtherBox.Visible = false;

            PictureBookAuthorText.Text = pictureBook.Author;
            PictureBookIllustratorText.Text = pictureBook.Illustrator;
        }
        /// <summary>
        /// 辞書追加表示
        /// </summary>
        /// <param name="dictionary">辞書</param>
        public void GetGenre(Dictionary dictionary)
        {
            NovelsBox.Visible = false;
            ComicBox.Visible = false;
            PictureBook.Visible = false;
            DictionaryBox.Visible = true;
            CookingBooksBox.Visible = false;
            TravelBooksBox.Visible = false;
            HobbyBooksBox.Visible = false;
            HistoryBooksBox.Visible = false;
            TechnicalBooksBox.Visible = false;
            PhotoBooksBox.Visible = false;
            GravurePhotoBooksBox.Visible = false;
            BookCardBox.Visible = false;
            GiftBox.Visible = false;
            OtherBox.Visible = false;

            DictionarySupervisionText.Text = dictionary.Supervision;

        }
        /// <summary>
        /// 旅行追加表示
        /// </summary>
        /// <param name="cookingBooks">料理</param>
        public void GetGenre(CookingBooks cookingBooks)
        {
            NovelsBox.Visible = false;
            ComicBox.Visible = false;
            PictureBook.Visible = false;
            DictionaryBox.Visible = false;
            CookingBooksBox.Visible = true;
            TravelBooksBox.Visible = false;
            HobbyBooksBox.Visible = false;
            HistoryBooksBox.Visible = false;
            TechnicalBooksBox.Visible = false;
            PhotoBooksBox.Visible = false;
            GravurePhotoBooksBox.Visible = false;
            BookCardBox.Visible = false;
            GiftBox.Visible = false;
            OtherBox.Visible = false;

            CookingBooksGenreText.Text = cookingBooks.Genre;
            CookingBooksSupervisionText.Text = cookingBooks.Supervision;
        }
        /// <summary>
        /// 旅行追加表示
        /// </summary>
        /// <param name="travelBooks">旅行</param>
        public void GetGenre(TravelBooks travelBooks)
        {
            NovelsBox.Visible = false;
            ComicBox.Visible = false;
            PictureBook.Visible = false;
            DictionaryBox.Visible = false;
            CookingBooksBox.Visible = false;
            TravelBooksBox.Visible = true;
            HobbyBooksBox.Visible = false;
            HistoryBooksBox.Visible = false;
            TechnicalBooksBox.Visible = false;
            PhotoBooksBox.Visible = false;
            GravurePhotoBooksBox.Visible = false;
            BookCardBox.Visible = false;
            GiftBox.Visible = false;
            OtherBox.Visible = false;

            TravelBooksTtravelNotationText.Text = travelBooks.TravelNotation;
            TravelBooksSupervisionText.Text = travelBooks.Supervision;
        }
        /// <summary>
        /// 趣味追加表示
        /// </summary>
        /// <param name="hobbyBooks">趣味</param>
        public void GetGenre(HobbyBooks hobbyBooks)
        {
            NovelsBox.Visible = false;
            ComicBox.Visible = false;
            PictureBook.Visible = false;
            DictionaryBox.Visible = false;
            CookingBooksBox.Visible = false;
            TravelBooksBox.Visible = false;
            HobbyBooksBox.Visible = true;
            HistoryBooksBox.Visible = false;
            TechnicalBooksBox.Visible = false;
            PhotoBooksBox.Visible = false;
            GravurePhotoBooksBox.Visible = false;
            BookCardBox.Visible = false;
            GiftBox.Visible = false;
            OtherBox.Visible = false;

            HobbyBooksGenreText.Text = hobbyBooks.Genre;
            HobbyBooksSupervisionText.Text = hobbyBooks.Supervision;
        }
        /// <summary>
        /// 歴史追加表示
        /// </summary>
        /// <param name="historyBooks">歴史</param>
        public void GetGenre(HistoryBooks historyBooks)
        {
            NovelsBox.Visible = false;
            ComicBox.Visible = false;
            PictureBook.Visible = false;
            DictionaryBox.Visible = false;
            CookingBooksBox.Visible = false;
            TravelBooksBox.Visible = false;
            HobbyBooksBox.Visible = false;
            HistoryBooksBox.Visible = true;
            TechnicalBooksBox.Visible = false;
            PhotoBooksBox.Visible = false;
            GravurePhotoBooksBox.Visible = false;
            BookCardBox.Visible = false;
            GiftBox.Visible = false;
            OtherBox.Visible = false;

            HistoryBooksHistoricalNotationText.Text = historyBooks.HistoricalNotation;
            string text = String.Join("、", historyBooks.Figure);
            HistoryBooksFigureText.Text = text;
            HistoryBooksSupervisionText.Text = historyBooks.Supervision;
        }
        /// <summary>
        /// 専門書追加表示
        /// </summary>
        /// <param name="technicalBooks">専門書</param>
        public void GetGenre(TechnicalBooks technicalBooks)
        {
            NovelsBox.Visible = false;
            ComicBox.Visible = false;
            PictureBook.Visible = false;
            DictionaryBox.Visible = false;
            CookingBooksBox.Visible = false;
            TravelBooksBox.Visible = false;
            HobbyBooksBox.Visible = false;
            HistoryBooksBox.Visible = false;
            TechnicalBooksBox.Visible = true;
            PhotoBooksBox.Visible = false;
            GravurePhotoBooksBox.Visible = false;
            BookCardBox.Visible = false;
            GiftBox.Visible = false;
            OtherBox.Visible = false;

            TechnicalBooksGenreText.Text = technicalBooks.Genre;
            TechnicalBooksSupervisionText.Text = technicalBooks.Supervision;
        }
        /// <summary>
        /// 写真集追加表示
        /// </summary>
        /// <param name="photoBooks">写真集</param>
        public void GetGenre(PhotoBooks photoBooks)
        {
            NovelsBox.Visible = false;
            ComicBox.Visible = false;
            PictureBook.Visible = false;
            DictionaryBox.Visible = false;
            CookingBooksBox.Visible = false;
            TravelBooksBox.Visible = false;
            HobbyBooksBox.Visible = false;
            HistoryBooksBox.Visible = false;
            TechnicalBooksBox.Visible = false;
            PhotoBooksBox.Visible = true;
            GravurePhotoBooksBox.Visible = false;
            BookCardBox.Visible = false;
            GiftBox.Visible = false;
            OtherBox.Visible = false;

            PhotoBooksGenreText.Text = photoBooks.Genre;
            PhotoBooksPhotoGrapherText.Text = photoBooks.PhotoGrapher;
            PhotoBooksSupervisionText.Text = photoBooks.Supervision;
        }
        /// <summary>
        /// グラビア写真集追加表示
        /// </summary>
        /// <param name="gravure">グラビア写真集</param>
        public void GetGenre(GravurePhotoBooks gravure)
        {
            NovelsBox.Visible = false;
            ComicBox.Visible = false;
            PictureBook.Visible = false;
            DictionaryBox.Visible = false;
            CookingBooksBox.Visible = false;
            TravelBooksBox.Visible = false;
            HobbyBooksBox.Visible = false;
            HistoryBooksBox.Visible = false;
            TechnicalBooksBox.Visible = false;
            PhotoBooksBox.Visible = false;
            GravurePhotoBooksBox.Visible = true;
            BookCardBox.Visible = false;
            GiftBox.Visible = false;
            OtherBox.Visible = false;

            GravurePhotoBooksPhotoGrapherText.Text = gravure.PhotoGrapher;
            string text = String.Join("、", gravure.Performance);
            GravurePhotoBooksPerformanceText.Text += text;
            GravurePhotoBooksSupervisionText.Text = gravure.Supervision;
        }
        /// <summary>
        ///　1000円図書カード追加表示
        /// </summary>
        /// <param name="bookCard">1000円図書カード</param>
        public void GetGenre(BookCard bookCard)
        {
            NovelsBox.Visible = false;
            ComicBox.Visible = false;
            PictureBook.Visible = false;
            DictionaryBox.Visible = false;
            CookingBooksBox.Visible = false;
            TravelBooksBox.Visible = false;
            HobbyBooksBox.Visible = false;
            HistoryBooksBox.Visible = false;
            TechnicalBooksBox.Visible = false;
            PhotoBooksBox.Visible = false;
            GravurePhotoBooksBox.Visible = false;
            BookCardBox.Visible = true;
            GiftBox.Visible = false;
            OtherBox.Visible = false;

            BookCardImageCharacterText.Text = bookCard.ImageCharacter;
        }
        /// <summary>
        /// 景品追加表示
        /// </summary>
        /// <param name="gift">景品</param>
        public void GetGenre(Gift gift)
        {
            NovelsBox.Visible = false;
            ComicBox.Visible = false;
            PictureBook.Visible = false;
            DictionaryBox.Visible = false;
            CookingBooksBox.Visible = false;
            TravelBooksBox.Visible = false;
            HobbyBooksBox.Visible = false;
            HistoryBooksBox.Visible = false;
            TechnicalBooksBox.Visible = false;
            PhotoBooksBox.Visible = false;
            GravurePhotoBooksBox.Visible = false;
            BookCardBox.Visible = false;
            GiftBox.Visible = true;
            OtherBox.Visible = false;

            GiftGenreText.Text = gift.Genre;
        }
        /// <summary>
        /// その他追加表示
        /// </summary>
        /// <param name="other">その他</param>
        public void GetGenre(Other other)
        {
            NovelsBox.Visible = false;
            ComicBox.Visible = false;
            PictureBook.Visible = false;
            DictionaryBox.Visible = false;
            CookingBooksBox.Visible = false;
            TravelBooksBox.Visible = false;
            HobbyBooksBox.Visible = false;
            HistoryBooksBox.Visible = false;
            TechnicalBooksBox.Visible = false;
            PhotoBooksBox.Visible = false;
            GravurePhotoBooksBox.Visible = false;
            BookCardBox.Visible = false;
            GiftBox.Visible = false;
            OtherBox.Visible = true;

            OtherGenreText.Text = other.Genre;
        }

        
    }
}
