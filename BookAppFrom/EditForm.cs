using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using BookAppLibrary.Controllers;
using BookAppLibrary.Models;
using BookAppLibrary.Enums;
using BookAppFrom.Main;

namespace BookAppFrom.Edit
{
    public partial class EditForm : Form
    {
        public EditForm()
        {
            InitializeComponent();
        }

        #region 読み取り専用

        /// <summary>
        /// ファイルパス
        /// </summary>
        public static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data");

        #endregion

        public string fileBook { get; set; }

        public string PictureBooks { get; set; }

        public bool Flag { get; set; }
        private void EditForm_Load(object sender, EventArgs e)
        {
            HistoryBooksFigureText.Multiline = true;
        }

        private void TypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        { 

            int index = TypeCombo.SelectedIndex;

            if(index == 0){ GetSelcetNovel();}
            else if (index == 1){ GetSelcetComic(); }
            else if (index == 2){ GetSelcetPctureBook(); }
            else if (index == 3){ GetSelcetDictionary(); }
            else if (index == 4){ GetSelcetCookingBooks(); }
            else if (index == 5){ GetSelcetTravelBooks(); }
            else if (index == 6){ GetSelcetHobbyBooks(); }
            else if (index == 7){ GetSelcetHistoryBooks(); }
            else if (index == 8){ GetSelcetTechnicalBooks(); }
            else if (index == 9){ GetSelcetPhotoBooks(); }
            else if (index == 10){ GetSelcetGravurePhotoBook(); }
            else if (index == 11){ GetSelcetBookCard(); }
            else if (index == 12){ GetSelcetGift(); }
            else if (index == 13){ GetSelcetOther(); }

        }

        public void OpenFile_Click(object sender, EventArgs e)
        {
            string file = String.Empty;
            string savefile = String.Empty;
            OpenFileDialog openFile = new OpenFileDialog();
            SaveFileDialog saveFile = new SaveFileDialog();
            try
            {
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    file = $@"PictureData\{openFile.SafeFileName}";

                    string picturebox = openFile.FileName;
                    saveFile.FileName = openFile.SafeFileName;
                    savefile = saveFile.FileName;


                    ImagePicture.SizeMode = PictureBoxSizeMode.StretchImage;

                    ImagePicture.Image = Image.FromFile(picturebox);
                    Bitmap bitmap = new Bitmap(picturebox);

                    if (picturebox != savefile)
                    {
                        bitmap.Save($@"{Path.Combine(FilePath, file)}");
                    }

                }
                fileBook = file;
            }
            catch (Exception)
            {

            }

        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                Book book = RegisterBook();
                Price price = RegisterPrice();
                
                BookDataController bookData = new BookDataController();
                bookData.Flag = Flag;
                if (book != null && price != null && CodeText.MaxLength == 8)
                {
                    
                    bookData.SetBook(book);
                    bookData.SetPrice(price);

                    this.Close();
                }
                else
                {
                    throw new Exception("情報が入力されていません");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(this, "もう一度入力してください。", ex.Message, MessageBoxButtons.OK);
            }
            
           
        }

        public Book RegisterBook()
        {
                Book book = null;
                
            try
            {
                for (BookType type = BookType.Code; (int)type < Enum.GetValues(typeof(BookType)).Length; type++)
                {

                    string code = String.Empty;
                    int index = TypeCombo.SelectedIndex;
                    int num = index + 1;
                    switch (type)
                    {
                        case BookType.Code:
                            code = CodeText.Text;
                            book = SetData(code, (SpeciesType)num);
                            if(book == null)
                            {
                                throw new Exception();
                            }
                            break;
                        case BookType.Type:
                            if (code == null)
                            {
                                throw new Exception();
                            }
                            else { book.Species = num.ToString(); }
                            
                            break;
                        case BookType.Title:
                            book.Title = TitleText.Text;
                            break;
                        case BookType.Publisher:
                            book.Publisher = PublisherText.Text;
                            break;
                        case BookType.ReleaseDate:
                            book.ReleaseData = ReleaseData.Value;
                            break;
                        case BookType.SubStance:
                            book.SubStance = SubStanceText.Text;
                            break;
                        case BookType.Image:
                            if (fileBook != null)
                            {
                                book.Image = fileBook;
                            }
                            else
                            {
                                
                                book.Image = PictureBooks;
                            }
                            break;
                        case BookType.Stocks:
                            book.Stocks = (int)InventoryUpDown.Value;
                            break;
                    }
                }
                
            }catch(Exception)
            {
                
            }
                return book;
            }

        public Price RegisterPrice()
        {
            Price price = null;
            try
            {
                for (PriceType type = PriceType.Code; (int)type < Enum.GetValues(typeof(PriceType)).Length; type++)
                {
                    string code = String.Empty;
                    switch (type)
                    {
                        case PriceType.Code:
                            code = CodeText.Text;
                            price = new Price(code);
                            break;
                        case PriceType.Price:
                            price.Prices = int.Parse(PriceText.Text);
                            break;
                    }
                }
                
            }
            catch(Exception)
            {
                
            }

            return price;
        }

        /// <summary>
        /// 書籍インスタンスを作成
        /// </summary>
        /// <param name="code">コード</param>
        /// <param name="species">種別</param>
        /// <returns></returns>
        public Book SetData(string code ,SpeciesType species)
        {
          
                Book book = null;
            try
            {
                switch (species)
                {
                    case SpeciesType.Novel:
                        book = new Novel(code);
                        break;
                    case SpeciesType.Comic:
                        book = new Comic(code);
                        break;
                    case SpeciesType.PictureBook:
                        book = new PictureBook(code);
                        break;
                    case SpeciesType.Dictionary:
                        book = new Dictionary(code);
                        break;
                    case SpeciesType.CookingBooks:
                        book = new CookingBooks(code);
                        break;
                    case SpeciesType.TravelBooks:
                        book = new TravelBooks(code);
                        break;
                    case SpeciesType.HobbyBooks:
                        book = new HobbyBooks(code);
                        break;
                    case SpeciesType.HistoryBooks:
                        book = new HistoryBooks(code);
                        break;
                    case SpeciesType.TechnicalBooks:
                        book = new TechnicalBooks(code);
                        break;
                    case SpeciesType.PhotoBooks:
                        book = new PhotoBooks(code);
                        break;
                    case SpeciesType.GravurePhotoBooks:
                        book = new GravurePhotoBooks(code);
                        break;
                    case SpeciesType.BookCard:
                        book = new BookCard(code);
                        break;
                    case SpeciesType.Gift:
                        book = new Gift(code);
                        break;
                    case SpeciesType.Other:
                        book = new Other(code);
                        break;
                }
                Updata(book);

        }
            catch(Exception) {

            }
            
             return book;
        }

        /// <summary>
        ///　データを更新
        /// </summary>
        /// <param name="book">書籍</param>
        public void Updata(Book book)
        {
            //小説の場合
            if (book is Novel novel)
            {
                novel.Author = NovelAuthorText.Text;
                BookDataController bookData = new BookDataController();
                bookData.Flag = Flag;
                bookData.SetGenre(novel);
            }
            //漫画の場合
            else if (book is Comic comic)
            {
                comic.Author = ComicAuthorText.Text;
                comic.Illustrator = ComicIllustratorText.Text;
                BookDataController bookData = new BookDataController();
                bookData.Flag = Flag;
                bookData.SetGenre(comic);

            }
            //絵本の場合
            else if (book is PictureBook pictureBook)
            {
                pictureBook.Author = PctureBookAuthorText.Text;
                pictureBook.Illustrator = PctureBookIllustratorText.Text;
                BookDataController bookData = new BookDataController();
                bookData.Flag = Flag;
                bookData.SetGenre(pictureBook);
            }
            //辞書の場合
            else if (book is Dictionary dictionary)
            {
                dictionary.Supervision = DictionarySupervisionText.Text;
                BookDataController bookData = new BookDataController();
                bookData.Flag = Flag;
                bookData.SetGenre(dictionary);
            }
            //料理の場合
            else if (book is CookingBooks cookingBooks)
            {
                cookingBooks.Genre = CookingBooksGenreText.Text;
                cookingBooks.Supervision = CookingBooksSupervisionText.Text;
                BookDataController bookData = new BookDataController();
                bookData.Flag = Flag;
                bookData.SetGenre(cookingBooks);
            }
            //旅行の場合
            else if (book is TravelBooks travelBooks)
            {
                travelBooks.TravelNotation = TravelBooksTtravelNotationText.Text;
                travelBooks.Supervision = TravelBooksSupervisionText.Text;
                BookDataController bookData = new BookDataController();
                bookData.Flag = Flag;
                bookData.SetGenre(travelBooks);
            }
            //趣味の場合
            else if (book is HobbyBooks hobbyBooks)
            {
                hobbyBooks.Genre = HobbyBooksGenreText.Text;
                hobbyBooks.Supervision = HobbyBooksSupervisionText.Text;
                BookDataController bookData = new BookDataController();
                bookData.Flag = Flag;
                bookData.SetGenre(hobbyBooks);
            }
            //歴史の場合
            else if (book is HistoryBooks historyBooks)
            {
                historyBooks.HistoricalNotation = HistoryBooksHistoricalNotationText.Text;

                string[] figure = HistoryBooksFigureText.Lines;
                string text = String.Join(",", figure);
                historyBooks.Figure = new List<string>() {text};
                historyBooks.Supervision = HistoryBooksSupervisionText.Text;
                BookDataController bookData = new BookDataController();
                bookData.Flag = Flag;
                bookData.SetGenre(historyBooks);
            }
            //専門書の場合
            else if (book is TechnicalBooks technicalBooks)
            {
                technicalBooks.Genre = TechnicalBooksGenreText.Text;
                technicalBooks.Supervision = TechnicalBooksSupervisionText.Text;
                BookDataController bookData = new BookDataController();
                bookData.Flag = Flag;
                bookData.SetGenre(technicalBooks);
            }
            //写真集の場合
            else if (book is PhotoBooks photoBooks)
            {
                photoBooks.Genre = PhotoBooksGenreText.Text;
                photoBooks.PhotoGrapher = PhotoBooksPhotoGrapherText.Text;
                photoBooks.Supervision = PhotoBooksSupervisionText.Text;
                BookDataController bookData = new BookDataController();
                bookData.Flag = Flag;
                bookData.SetGenre(photoBooks);
            }
            //グラビア写真集の場合
            else if (book is GravurePhotoBooks gravurePhotoBooks)
            {
                gravurePhotoBooks.PhotoGrapher = GravurePhotoBooksPerformanceText.Text;
                string[] performance = GravurePhotoBooksPerformanceText.Lines;
                string text = String.Join(",", performance);
                gravurePhotoBooks.Performance = new List<string>() {text};
                gravurePhotoBooks.Supervision = GravurePhotoBooksSupervisionText.Text;
                BookDataController bookData = new BookDataController();
                bookData.Flag = Flag;
                bookData.SetGenre(gravurePhotoBooks);
            }
            //図書カードの場合
            else if (book is BookCard bookCard)
            {
                bookCard.ImageCharacter = BookCardImageCharacterText.Text;
                BookDataController bookData = new BookDataController();
                bookData.Flag = Flag;
                bookData.SetGenre(bookCard);
            }
            //景品の場合
            else if (book is Gift gift)
            {
                gift.Genre = GiftGenreText.Text;
                BookDataController bookData = new BookDataController();
                bookData.Flag = Flag;
                bookData.SetGenre(gift);
            }
            //その他の場合
            else if (book is Other other)
            {
                other.Genre = OtherGenreText.Text;
                BookDataController bookData = new BookDataController();
                bookData.Flag = Flag;
                bookData.SetGenre(other);
            }
        }



        /// <summary>
        /// 編集画面の表示
        /// </summary>
        /// <param name="book">本</param>
        /// <returns></returns>
        public Book Rodeng(Book book)
        {
            CodeText.Text =book.Code;
            TypeCombo.Text = book.GetSpecies();
            TitleText.Text = book.Title;
            PublisherText.Text = book.Publisher;
            ReleaseData.Value = book.ReleaseData;
            SubStanceText.Text = book.SubStance;
            string picturebox = Path.Combine(FilePath, $@"{book.Image}");
            PictureBooks = picturebox;
            ImagePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            ImagePicture.Image = Image.FromFile(picturebox);
            InventoryUpDown.Value = book.Stocks;
            if (BookDataController.GetPrice().Where(x => x.Code == book.Code).FirstOrDefault() is Price price)
            {
                PriceText.Text = price.Prices.ToString();
            }

            if (book is Novel novel)
            {
                NovelAuthorText.Text = novel.Author;

            }else if(book is Comic comic)
            {
                ComicAuthorText.Text = comic.Author;
                ComicIllustratorText.Text = comic.Illustrator;

            }else if(book is PictureBook pictureBook)
            {
                PctureBookAuthorText.Text = pictureBook.Author;
                PctureBookIllustratorText.Text = pictureBook.Illustrator;

            }else if(book is Dictionary dictionary)
            {
                DictionarySupervisionText.Text = dictionary.Supervision;

            }else if(book is CookingBooks cookingBooks)
            {
                CookingBooksGenreText.Text = cookingBooks.Genre;
                CookingBooksSupervisionText.Text = cookingBooks.Supervision;

            }else if(book is TravelBooks travelBooks)
            {
                TravelBooksTtravelNotationText.Text = travelBooks.TravelNotation;
                TravelBooksSupervisionText.Text = travelBooks.Supervision;

            }else if(book is HobbyBooks hobbyBooks)
            {
                HobbyBooksGenreText.Text = hobbyBooks.Genre;
                HobbyBooksSupervisionText.Text = hobbyBooks.Supervision;

            }else if(book is HistoryBooks historyBooks)
            {
                HistoryBooksHistoricalNotationText.Text = historyBooks.HistoricalNotation;
                string text = String.Join("、", historyBooks.Figure);
                HistoryBooksFigureText.Text= text;
                HistoryBooksSupervisionText.Text = historyBooks.Supervision;

            }else if (book is TechnicalBooks technicalBooks)
            {
                TechnicalBooksGenreText.Text = technicalBooks.Genre;
                TechnicalBooksSupervisionText.Text = technicalBooks.Supervision;

            }else if(book is PhotoBooks photoBooks)
            {
                PhotoBooksGenreText.Text = photoBooks.Genre;
                PhotoBooksPhotoGrapherText.Text = photoBooks.PhotoGrapher;
                PhotoBooksSupervisionText.Text = photoBooks.Supervision;

            }else if(book is GravurePhotoBooks gravure)
            {
                GravurePhotoBooksPhotoGrapherText.Text = gravure.PhotoGrapher;
                string text = String.Join("、", gravure.Performance);
                GravurePhotoBooksPerformanceText.Text = text;
                GravurePhotoBooksSupervisionText.Text = gravure.Supervision;

            }else if(book is BookCard bookCard)
            {
                BookCardImageCharacterText.Text = bookCard.ImageCharacter;

            }else if (book is Gift gift)
            {
                GiftGenreText.Text = gift.Genre;

            }else if (book is Other other)
            {
                OtherGenreText.Text = other.Genre;
            }

            return book;
        }

        /// <summary>
        /// 小説情報表示
        /// </summary>
        public void GetSelcetNovel() {

            NovelBox.Visible = true;
            ComicBox.Visible = false;
            PctureBookBox.Visible = false;
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
        }

        /// <summary>
        /// 漫画情報表示
        /// </summary>
        public void GetSelcetComic()
        {
            NovelBox.Visible = false;
            ComicBox.Visible = true;
            PctureBookBox.Visible = false;
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
        }

        /// <summary>
        /// 絵本情報表示
        /// </summary>
        public void GetSelcetPctureBook()
        {
            NovelBox.Visible = false;
            ComicBox.Visible = false;
            PctureBookBox.Visible = true;
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

        }

        /// <summary>
        /// 辞書情報表示
        /// </summary>
        public void GetSelcetDictionary()
        {
            NovelBox.Visible = false;
            ComicBox.Visible = false;
            PctureBookBox.Visible = false;
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

        }

        /// <summary>
        /// 料理情報表示
        /// </summary>
        public void GetSelcetCookingBooks()
        {
            NovelBox.Visible = false;
            ComicBox.Visible = false;
            PctureBookBox.Visible = false;
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

        }

        /// <summary>
        /// 旅行情報表示
        /// </summary>
        public void GetSelcetTravelBooks()
        {
            NovelBox.Visible = false;
            ComicBox.Visible = false;
            PctureBookBox.Visible = false;
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

        }

        /// <summary>
        /// 趣味情報表示
        /// </summary>
        public void GetSelcetHobbyBooks()
        {
            NovelBox.Visible = false;
            ComicBox.Visible = false;
            PctureBookBox.Visible = false;
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

        }

        /// <summary>
        /// 歴史情報表示
        /// </summary>
        public void GetSelcetHistoryBooks()
        {
            NovelBox.Visible = false;
            ComicBox.Visible = false;
            PctureBookBox.Visible = false;
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

        }

        /// <summary>
        /// 専門書情報表示
        /// </summary>
        public void GetSelcetTechnicalBooks()
        {
            NovelBox.Visible = false;
            ComicBox.Visible = false;
            PctureBookBox.Visible = false;
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

        }

        /// <summary>
        /// 写真集情報表示
        /// </summary>
        public void GetSelcetPhotoBooks()
        {
            NovelBox.Visible = false;
            ComicBox.Visible = false;
            PctureBookBox.Visible = false;
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

        }

        /// <summary>
        /// グラビア写真集情報表示
        /// </summary>
        public void GetSelcetGravurePhotoBook()
        {
            NovelBox.Visible = false;
            ComicBox.Visible = false;
            PctureBookBox.Visible = false;
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

        }

        /// <summary>
        /// 図書カード情報表示
        /// </summary>
        public void GetSelcetBookCard()
        {
            NovelBox.Visible = false;
            ComicBox.Visible = false;
            PctureBookBox.Visible = false;
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

        }

        /// <summary>
        /// 景品情報表示
        /// </summary>
        public void GetSelcetGift()
        {
            NovelBox.Visible = false;
            ComicBox.Visible = false;
            PctureBookBox.Visible = false;
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

        }

        /// <summary>
        /// その他情報表示
        /// </summary>
        public void GetSelcetOther()
        {
            NovelBox.Visible = false;
            ComicBox.Visible = false;
            PctureBookBox.Visible = false;
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

        }

     
    }
}
