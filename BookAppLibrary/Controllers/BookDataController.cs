using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using BookAppLibrary.Enums;
using BookAppLibrary.Models;

namespace BookAppLibrary.Controllers
{
    public class BookDataController
    {
        #region 読み取り専用

        /// <summary>
        /// ファイルパス
        /// </summary>
        public static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data");

        #endregion

        public bool Flag { get; set; }

        public List<string> codeList { get; set; }

        public List<string> numList { get; set; }

        #region メソッド

        public static IEnumerable<Book> GetBook()
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] data = text.Split(',');

                if (data.Any())
                {
                    Book book = null;

                    //項目数が一致した時
                    if (Enum.GetValues(typeof(BookType)).Length == data.Length)
                    {
                        string code = string.Empty;

                        for (BookType type = BookType.Code; (int)type < data.Length; type++)
                        {
                            switch (type)
                            {
                                case BookType.Code:

                                    code = data[(int)type];
                                    break;
                                case BookType.Type:
                                    book = CreateType(code, (SpeciesType)int.Parse(data[(int)type]));
                                    break;
                                case BookType.Title:
                                    book.Title = data[(int)type];
                                    break;
                                case BookType.Publisher:
                                    book.Publisher = data[(int)type];
                                    break;
                                case BookType.ReleaseDate:
                                    book.ReleaseData = DateTime.ParseExact(data[(int)type], @"yyyy/mm/dd", null);
                                    break;
                                case BookType.SubStance:
                                    book.SubStance = data[(int)type];
                                    break;
                                case BookType.Image:
                                    book.Image = data[(int)type];
                                    break;
                                case BookType.Stocks:
                                    book.Stocks = int.Parse(data[(int)type]);
                                    break;
                            }
                        }
                        if (book != null)
                        {
                            yield return book;
                        }
                    }
                }

            }
        }

        public Book SetBook(Book book)
        {

            string FileBook = Path.Combine(FilePath, @"BooksData\Book.txt");
            List<string> list = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] ctext = data.Split(',');
                list.Add(ctext[0]);
                Cpyelist.Add(data);
            }

            string[] Books = new string[] {
                book.Code,
                book.Species,
                book.Title,
                book.Publisher,
                book.ReleaseData.ToShortDateString(),
                book.SubStance,
                book.Image,
                book.Stocks.ToString()
            };

            string text = String.Join(",", Books);
            if (Flag == true)
            {
                if (!list.Contains(Books[0]) && Books[0] != "")
                {

                    File.AppendAllText(FileBook, $"\n{text}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FileBook, $"");
                for (int i = 0; i < list.Count; i++)
                {
                    if (book.Code == list[i])
                    {
                        File.AppendAllText(FileBook, $"\n{text}");
                    }
                    else if (book.Code != list[i])
                    {

                        File.AppendAllText(FileBook, $"\n{Cpyelist[i]}");
                    }
                }
            }
            return book;


        }

        public Book DelBook(Book book)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\Book.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == book.Code)
                {
                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");

                }
            }
            return book;
        }
        public static IEnumerable<Price> GetPrice()
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\Price.txt")))
            {
                string[] data = text.Split(',');

                if (data.Any())
                {
                    Price price = null;

                    //項目数が一致した時
                    if (Enum.GetValues(typeof(PriceType)).Length == data.Length)
                    {
                        string code = string.Empty;

                        for (PriceType type = PriceType.Code; (int)type < data.Length; type++)
                        {
                            switch (type)
                            {
                                case PriceType.Code:
                                    code = data[(int)type];
                                    price = new Price(code);
                                    break;
                                case PriceType.Price:
                                    price.Prices = int.Parse(data[(int)type]);
                                    break;

                            }
                        }
                        if (price != null)
                        {
                            yield return price;
                        }
                    }
                }

            }
        }

        public Price SetPrice(Price price)
        {


            string FilePrice = Path.Combine(FilePath, @"BooksData\Price.txt");
            List<string> list = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Price.txt")))
            {
                string[] ctext = data.Split(',');
                list.Add(ctext[0]);
                Cpyelist.Add(data);

            }

            string[] Prices = new string[] {
                price.Code,
                price.Prices.ToString()
            };

            string text = String.Join(",", Prices);
            if (Flag == true)
            {
                if (!list.Contains(Prices[0]) && Prices[0] != "")
                {

                    File.AppendAllText(FilePrice, $"\n{text}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FilePrice, $"");
                for (int i = 0; i < list.Count; i++)
                {
                    if (price.Code == list[i])
                    {
                        File.AppendAllText(FilePrice, $"\n{text}");

                    }
                    else if (price.Code != list[i])
                    {
                        File.AppendAllText(FilePrice, $"\n{Cpyelist[i]}");
                    }
                }
            }
            return price;


        }

        public Price DelPrice(Price price)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\Price.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\Price.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == price.Code)
                {

                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");

                }
            }
            return price;
        }


        /// <summary>
        /// 書籍インスタンスを作成
        /// </summary>
        /// <param name="code">書籍コード</param>
        /// <param name="species">種別</param>
        /// <returns></returns>
        private static Book CreateType(string code, SpeciesType species)
        {
            Book book = null;

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
            return book;
        }


        /// <summary>
        /// データを更新する
        /// </summary>
        /// <param name="book">書籍</param>
        private static void Updata(Book book)
        {

            //小説の場合
            if (book is Novel novel)
            {
                Updata(novel);

            }
            //漫画の場合
            else if (book is Comic comic)
            {
                Updata(comic);
            }
            //絵本の場合
            else if (book is PictureBook pictureBook)
            {
                Updata(pictureBook);
            }
            //辞書の場合
            else if (book is Dictionary dictionary)
            {
                Updata(dictionary);
            }
            //料理の場合
            else if (book is CookingBooks cookingBooks)
            {
                Updata(cookingBooks);
            }
            //旅行の場合
            else if (book is TravelBooks travelBooks)
            {
                Updata(travelBooks);
            }
            //趣味の場合
            else if (book is HobbyBooks hobbyBooks)
            {
                Updata(hobbyBooks);
            }
            //歴史の場合
            else if (book is HistoryBooks historyBooks)
            {
                Updata(historyBooks);
            }
            //専門書の場合
            else if (book is TechnicalBooks technicalBooks)
            {
                Updata(technicalBooks);
            }
            //写真集の場合
            else if (book is PhotoBooks photoBooks)
            {
                Updata(photoBooks);
            }
            //グラビア写真集の場合
            else if (book is GravurePhotoBooks gravurePhotoBooks)
            {
                Updata(gravurePhotoBooks);
            }
            //図書カードの場合
            else if (book is BookCard bookCard)
            {
                Updata(bookCard);
            }
            //景品の場合
            else if (book is Gift gift)
            {
                Updata(gift);
            }
            //その他の場合
            else if (book is Other other)
            {
                Updata(other);
            }

        }

        /// <summary>
        /// 小説を更新
        /// </summary>
        /// <param name="novel">小説</param>
        private static void Updata(Novel novel)
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\Novel.txt")))
            {
                string[] data = text.Split(',');
                if (data.Any())
                {
                    //書籍コードが異なるとき
                    if (data[(int)NovelType.Code] != novel.Code)
                    {
                        continue;
                    }

                    for (NovelType type = NovelType.Code; (int)type < data.Length; type++)
                    {
                        switch (type)
                        {
                            case NovelType.Author:
                                novel.Author = data[(int)type];
                                break;
                        }
                    }
                }
            }

        }
        /// <summary>
        /// 小説を追加
        /// </summary>
        /// <param name="novel">小説</param>
        /// <returns></returns>
        public Novel SetGenre(Novel novel)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\Novel.txt");
            List<string> listBook = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] ctext = data.Split(',');
                listBook.Add(ctext[0]);

            }

            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Novel.txt")))
            {
                Cpyelist.Add(data);
            }

            string[] Novels = new string[] {
               novel.Code,
               novel.Author
            };

            string text = String.Join(",", Novels);
            if (Flag == true)
            {
                if (!listBook.Contains(Novels[0]))
                {

                    File.AppendAllText(FileBook, $"\n{text}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FileBook, $"");
                for (int i = 0; i < listBook.Count; i++)
                {
                    if (novel.Code == listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{text}");
                    }
                    else if (novel.Code != listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{Cpyelist[i]}");
                    }
                }
            }

            return novel;
        }

        public Novel DelGenre(Novel novel)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\Novel.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\Novel.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == novel.Code)
                {

                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");

                }
            }
            return novel;
        }

        /// <summary>
        /// 漫画を更新
        /// </summary>
        /// <param name="comic">漫画</param>
        private static void Updata(Comic comic)
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\Coimc.txt")))
            {
                string[] data = text.Split(',');
                if (data.Any())
                {
                    //書籍コードが異なるとき
                    if (data[(int)ComicType.Code] != comic.Code)
                    {
                        continue;
                    }

                    for (ComicType type = ComicType.Code; (int)type < data.Length; type++)
                    {
                        switch (type)
                        {
                            case ComicType.Author:
                                comic.Author = data[(int)type];
                                break;
                            case ComicType.Illustrator:
                                comic.Illustrator = data[(int)type];
                                break;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 漫画を追加
        /// </summary>
        /// <param name="comic">漫画</param>
        /// <returns></returns>
        public Comic SetGenre(Comic comic)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\Coimc.txt");
            List<string> listBook = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] ctext = data.Split(',');
                listBook.Add(ctext[0]);

            }

            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Coimc.txt")))
            {
                Cpyelist.Add(data);
            }

            string[] Comics = new string[] {
               comic.Code,
               comic.Author,
               comic.Illustrator
            };

            string text = String.Join(",", Comics);

            if (Flag == true)
            {
                if (!listBook.Contains(Comics[0]))
                {

                    File.AppendAllText(FileBook, $"\n{text}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FileBook, $"");
                for (int i = 0; i < listBook.Count; i++)
                {
                    if (comic.Code == listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{text}");
                    }
                    else if (comic.Code != listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{Cpyelist[i]}");
                    }
                }
            }

            return comic;
        }

        public Comic DelGenre(Comic comic)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\Coimc.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\Coimc.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == comic.Code)
                {
                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");

                }
            }
            return comic;
        }

        /// <summary>
        /// 絵本を更新
        /// </summary>
        /// <param name="pictureBook">絵本</param>
        private static void Updata(PictureBook pictureBook)
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\PictureBook.txt")))
            {
                string[] data = text.Split(',');
                if (data.Any())
                {
                    //書籍コードが異なるとき
                    if (data[(int)PictureBookType.Code] != pictureBook.Code)
                    {
                        continue;
                    }
                    for (PictureBookType type = PictureBookType.Code; (int)type < data.Length; type++)
                    {
                        switch (type)
                        {
                            case PictureBookType.Author:
                                pictureBook.Author = data[(int)type];
                                break;
                            case PictureBookType.Illustrator:
                                pictureBook.Illustrator = data[(int)type];
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 絵本を追加
        /// </summary>
        /// <param name="pictureBook">絵本</param>
        /// <returns></returns>
        public PictureBook SetGenre(PictureBook pictureBook)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\PictureBook.txt");

            List<string> listBook = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] ctext = data.Split(',');
                listBook.Add(ctext[0]);

            }

            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\PictureBook.txt")))
            {
                Cpyelist.Add(data);
            }

            string[] picture = new string[] {
               pictureBook.Code,
               pictureBook.Author,
               pictureBook.Illustrator
            };

            string text = String.Join(",", picture);

            if (Flag == true)
            {
                if (!listBook.Contains(picture[0]))
                {

                    File.AppendAllText(FileBook, $"\n{text}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FileBook, $"");
                for (int i = 0; i < listBook.Count; i++)
                {
                    if (pictureBook.Code == listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{text}");
                    }
                    else if (pictureBook.Code != listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{Cpyelist[i]}");
                    }
                }
            }


            return pictureBook;
        }

        public PictureBook DelGenre(PictureBook pictureBook)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\PictureBook.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\PictureBook.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == pictureBook.Code)
                {
                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");

                }
            }
            return pictureBook;
        }

        /// <summary>
        /// 辞書を更新
        /// </summary>
        /// <param name="dictionary">辞書</param>
        private static void Updata(Dictionary dictionary)
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\Dictionary.txt")))
            {
                string[] data = text.Split(',');
                if (data.Any())
                {
                    //書籍コードが異なるとき
                    if (data[(int)DictionaryType.Code] != dictionary.Code)
                    {
                        continue;
                    }
                    for (DictionaryType type = DictionaryType.Code; (int)type < data.Length; type++)
                    {
                        switch (type)
                        {
                            case DictionaryType.Supervision:
                                dictionary.Supervision = data[(int)type];
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 辞書の追加
        /// </summary>
        /// <param name="dictionary">辞書</param>
        /// <returns></returns>
        public Dictionary SetGenre(Dictionary dictionary)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\Dictionary.txt");
            List<string> listBook = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] ctext = data.Split(',');
                listBook.Add(ctext[0]);

            }

            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Dictionary.txt")))
            {
                Cpyelist.Add(data);
            }

            string[] dic = new string[] {
               dictionary.Code,
               dictionary.Supervision
            };

            string text = String.Join(",", dic);
            if (Flag == true)
            {
                if (!listBook.Contains(dic[0]))
                {

                    File.AppendAllText(FileBook, $"\n{text}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FileBook, $"");
                for (int i = 0; i < listBook.Count; i++)
                {
                    if (dictionary.Code == listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{text}");
                    }
                    else if (dictionary.Code != listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{Cpyelist[i]}");
                    }
                }
            }
            return dictionary;
        }

        public Dictionary DelGenre(Dictionary dictionary)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\Dictionary.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\Dictionary.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == dictionary.Code)
                {
                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");

                }
            }
            return dictionary;
        }

        /// <summary>
        /// 料理を更新
        /// </summary>
        /// <param name="cookingBooks">料理</param>
        private static void Updata(CookingBooks cookingBooks)
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\CookingBooks.txt")))
            {
                string[] data = text.Split(',');

                if (data.Any())
                {
                    //書籍コードが異なるとき
                    if (data[(int)CookingBooksType.Code] != cookingBooks.Code)
                    {
                        continue;
                    }
                    for (CookingBooksType type = CookingBooksType.Code; (int)type < data.Length; type++)
                    {
                        switch (type)
                        {
                            case CookingBooksType.Genre:
                                cookingBooks.Genre = data[(int)type];
                                break;
                            case CookingBooksType.Supervision:
                                cookingBooks.Supervision = data[(int)type];
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 料理の追加
        /// </summary>
        /// <param name="cookingBooks">料理</param>
        /// <returns></returns>
        public CookingBooks SetGenre(CookingBooks cookingBooks)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\CookingBooks.txt");
            List<string> listBook = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] ctext = data.Split(',');
                listBook.Add(ctext[0]);

            }

            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\CookingBooks.txt")))
            {
                Cpyelist.Add(data);
            }

            string[] cooking = new string[] {
               cookingBooks.Code,
               cookingBooks.Genre,
               cookingBooks.Supervision
            };

            string text = String.Join(",", cooking);

            if (Flag == true)
            {
                if (!listBook.Contains(cooking[0]))
                {

                    File.AppendAllText(FileBook, $"\n{text}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FileBook, $"");
                for (int i = 0; i < listBook.Count; i++)
                {
                    if (cookingBooks.Code == listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{text}");
                    }
                    else if (cookingBooks.Code != listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{Cpyelist[i]}");
                    }
                }
            }
            return cookingBooks;
        }

        public CookingBooks DelGenre(CookingBooks cookingBooks)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\CookingBooks.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\CookingBooks.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == cookingBooks.Code)
                {
                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");

                }
            }
            return cookingBooks;
        }

        /// <summary>
        /// 旅行を更新
        /// </summary>
        /// <param name="travelBooks">旅行</param>
        private static void Updata(TravelBooks travelBooks)
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\TravelBooks.txt")))
            {
                string[] data = text.Split(',');
                if (data.Any())
                {
                    //書籍コードが異なるとき
                    if (data[(int)TravelBooksType.Code] != travelBooks.Code)
                    {
                        continue;
                    }
                    for (TravelBooksType type = TravelBooksType.Code; (int)type < data.Length; type++)
                    {
                        switch (type)
                        {
                            case TravelBooksType.TravelNotation:
                                travelBooks.TravelNotation = data[(int)type];
                                break;
                            case TravelBooksType.Supervision:
                                travelBooks.Supervision = data[(int)type];
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 旅行の追加
        /// </summary>
        /// <param name="travelBooks">旅行</param>
        /// <returns></returns>
        public TravelBooks SetGenre(TravelBooks travelBooks)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\TravelBooks.txt");
            List<string> listBook = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] ctext = data.Split(',');
                listBook.Add(ctext[0]);

            }

            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\TravelBooks.txt")))
            {
                Cpyelist.Add(data);
            }

            string[] travel = new string[] {
               travelBooks.Code,
               travelBooks.TravelNotation,
               travelBooks.Supervision
            };

            string text = String.Join(",", travel);
            if (Flag == true)
            {
                if (!listBook.Contains(travel[0]))
                {

                    File.AppendAllText(FileBook, $"\n{text}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FileBook, $"");
                for (int i = 0; i < listBook.Count; i++)
                {
                    if (travelBooks.Code == listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{text}");
                    }
                    else if (travelBooks.Code != listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{Cpyelist[i]}");
                    }
                }
            }
            return travelBooks;
        }


        public TravelBooks DelGenre(TravelBooks travelBooks)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\TravelBooks.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\TravelBooks.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == travelBooks.Code)
                {
                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");

                }
            }
            return travelBooks;
        }
        /// <summary>
        /// 趣味を更新
        /// </summary>
        /// <param name="hobbyBooks">趣味</param>
        private static void Updata(HobbyBooks hobbyBooks)
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\HobbyBooks.txt")))
            {
                string[] data = text.Split(',');
                if (data.Any())
                {
                    //書籍コードが異なるとき
                    if (data[(int)HobbyBooksType.Code] != hobbyBooks.Code)
                    {
                        continue;
                    }
                    for (HobbyBooksType type = HobbyBooksType.Code; (int)type < data.Length; type++)
                    {
                        switch (type)
                        {
                            case HobbyBooksType.Genre:
                                hobbyBooks.Genre = data[(int)type];
                                break;
                            case HobbyBooksType.Supervision:
                                hobbyBooks.Supervision = data[(int)type];
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 趣味の追加
        /// </summary>
        /// <param name="hobbyBooks">趣味</param>
        /// <returns></returns>
        public HobbyBooks SetGenre(HobbyBooks hobbyBooks)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\HobbyBooks.txt");
            List<string> listBook = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] ctext = data.Split(',');
                listBook.Add(ctext[0]);

            }

            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\HobbyBooks.txt")))
            {
                Cpyelist.Add(data);
            }

            string[] hobby = new string[] {
               hobbyBooks.Code,
               hobbyBooks.Genre,
               hobbyBooks.Supervision
            };

            string text = String.Join(",", hobby);
            if (Flag == true)
            {
                if (!listBook.Contains(hobby[0]))
                {

                    File.AppendAllText(FileBook, $"\n{text}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FileBook, $"");
                for (int i = 0; i < listBook.Count; i++)
                {
                    if (hobbyBooks.Code == listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{text}");
                    }
                    else if (hobbyBooks.Code != listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{Cpyelist[i]}");
                    }
                }
            }
            return hobbyBooks;
        }

        public HobbyBooks DelGenre(HobbyBooks hobbyBooks)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\HobbyBooks.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\HobbyBooks.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == hobbyBooks.Code)
                {
                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");

                }
            }
            return hobbyBooks;
        }
        /// <summary>
        /// 歴史を更新する
        /// </summary>
        /// <param name="historyBooks">歴史</param>
        private static void Updata(HistoryBooks historyBooks)
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\HistoryBooks.txt")))
            {
                string[] data = text.Split(',');
                if (data.Any())
                {
                    //書籍コードが異なるとき
                    if (data[(int)HistoryBooksType.Code] != historyBooks.Code)
                    {
                        continue;
                    }
                    for (HistoryBooksType type = HistoryBooksType.Code; (int)type < data.Length; type++)
                    {
                        switch (type)
                        {
                            case HistoryBooksType.HistoryNotation:
                                historyBooks.HistoricalNotation = data[(int)type];
                                break;
                            case HistoryBooksType.Supervision:
                                historyBooks.Supervision = data[(int)type];
                                break;
                        }
                    }
                }
            }
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\HistoryFigure.txt")))
            {
                string[] datafigure = text.Split(',');
                if (datafigure.Any())
                {
                    //書籍コードが異なるとき
                    if (datafigure[(int)HistoryBooksType.Code] != historyBooks.Code)
                    {
                        continue;
                    }
                    for (HistoryFigureType type = HistoryFigureType.Code; (int)type < datafigure.Length; type++)
                    {
                        switch (type)
                        {
                            case HistoryFigureType.Figure:
                                historyBooks.Figure = new List<string>(datafigure.Skip(1));
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 歴史の追加
        /// </summary>
        /// <param name="historyBooks">歴史</param>
        /// <returns></returns>
        public HistoryBooks SetGenre(HistoryBooks historyBooks)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\HistoryBooks.txt");
            string FigureBook = Path.Combine(FilePath, @"BooksData\HistoryFigure.txt");
            List<string> listBook = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            List<string> fCpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] ctext = data.Split(',');
                listBook.Add(ctext[0]);

            }
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\HistoryBooks.txt")))
            {
                Cpyelist.Add(data);
            }
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\HistoryFigure.txt")))
            {
                fCpyelist.Add(data);
            }
            string[] history = new string[] {
               historyBooks.Code,
               historyBooks.HistoricalNotation,
               historyBooks.Supervision
            };

            string[] fig = historyBooks.Figure.ToArray();
            string[] fdata = new string[] { };

            foreach (string data in fig)
            {
                string[] figure = new string[] {
                    historyBooks.Code,
                    data
                 };
                fdata = figure;
            }
            string text = String.Join(",", history);
            string ftext = String.Join(",", fdata);
            if (Flag == true)
            {
                if (!listBook.Contains(history[0]))
                {

                    File.AppendAllText(FileBook, $"\n{text}");
                    File.AppendAllText(FigureBook, $"\n{ftext}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FileBook, $"");
                File.WriteAllText(FigureBook, $"");
                for (int i = 0; i < listBook.Count; i++)
                {
                    if (historyBooks.Code == listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{text}");
                        File.AppendAllText(FigureBook, $"\n{ftext}");
                    }
                    else if (historyBooks.Code != listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{Cpyelist}");
                        File.AppendAllText(FigureBook, $"\n{fCpyelist}");
                    }
                }
            }


            return historyBooks;
        }

        public HistoryBooks DelGenre(HistoryBooks historyBooks)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\HistoryBooks.txt");
            string FigureBook = Path.Combine(FilePath, @"BooksData\HistoryFigure.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            List<string> FAppendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\HistoryBooks.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\HistoryBooks.txt")))
            {
                FAppendlist.Add(text);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == historyBooks.Code)
                {
                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                    FAppendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");
                    File.AppendAllText(FileBook, $"\n{FAppendlist[i]}");

                }
            }
            return historyBooks;
        }

        /// <summary>
        /// 専門書を更新する
        /// </summary>
        /// <param name="technicalBooks">専門書</param>
        private static void Updata(TechnicalBooks technicalBooks)
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\TechnicalBooks.txt")))
            {
                string[] data = text.Split(',');
                if (data.Any())
                {
                    //書籍コードが異なるとき
                    if (data[(int)TechnicalBooksType.Code] != technicalBooks.Code)
                    {
                        continue;
                    }
                    for (TechnicalBooksType type = TechnicalBooksType.Code; (int)type < data.Length; type++)
                    {
                        switch (type)
                        {
                            case TechnicalBooksType.Genre:
                                technicalBooks.Genre = data[(int)type];
                                break;
                            case TechnicalBooksType.Supervision:
                                technicalBooks.Supervision = data[(int)type];
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 専門書の追加
        /// </summary>
        /// <param name="technicalBooks">専門書</param>
        /// <returns></returns>
        public TechnicalBooks SetGenre(TechnicalBooks technicalBooks)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\TechnicalBooks.txt");
            List<string> listBook = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] ctext = data.Split(',');
                listBook.Add(ctext[0]);

            }

            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\TechnicalBooks.txt")))
            {
                Cpyelist.Add(data);
            }

            string[] technical = new string[] {
               technicalBooks.Code,
               technicalBooks.Genre,
               technicalBooks.Supervision
            };

            string text = String.Join(",", technical);
            if (Flag == true)
            {
                if (!listBook.Contains(technical[0]))
                {

                    File.AppendAllText(FileBook, $"\n{text}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FileBook, $"");
                for (int i = 0; i < listBook.Count; i++)
                {
                    if (technicalBooks.Code == listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{text}");
                    }
                    else if (technicalBooks.Code != listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{Cpyelist[i]}");
                    }
                }
            }
            return technicalBooks;
        }

        public TechnicalBooks DelGenre(TechnicalBooks technicalBooks)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\TechnicalBooks.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\TechnicalBooks.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == technicalBooks.Code)
                {
                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");

                }
            }
            return technicalBooks;
        }
        /// <summary>
        /// 写真集の更新
        /// </summary>
        /// <param name="photoBooks">写真集</param>
        private static void Updata(PhotoBooks photoBooks)
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\PhotoBooks.txt")))
            {
                string[] data = text.Split(',');
                if (data.Any())
                {
                    //書籍コードが異なるとき
                    if (data[(int)PhotoBooksType.Code] != photoBooks.Code)
                    {
                        continue;
                    }
                    for (PhotoBooksType type = PhotoBooksType.Code; (int)type < data.Length; type++)
                    {
                        switch (type)
                        {
                            case PhotoBooksType.Genre:
                                photoBooks.Genre = data[(int)type];
                                break;
                            case PhotoBooksType.PhotoGrapher:
                                photoBooks.PhotoGrapher = data[(int)type];
                                break;
                            case PhotoBooksType.Supervision:
                                photoBooks.Supervision = data[(int)type];
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 写真集の追加
        /// </summary>
        /// <param name="photoBooks">写真集</param>
        /// <returns></returns>
        public PhotoBooks SetGenre(PhotoBooks photoBooks)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\PhotoBooks.txt");
            List<string> listBook = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] ctext = data.Split(',');
                listBook.Add(ctext[0]);

            }

            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\PhotoBooks.txt")))
            {
                Cpyelist.Add(data);
            }

            string[] photo = new string[] {
               photoBooks.Code,
               photoBooks.Genre,
               photoBooks.PhotoGrapher,
               photoBooks.Supervision
            };

            string text = String.Join(",", photo);
            if (Flag == true)
            {
                if (!listBook.Contains(photo[0]))
                {

                    File.AppendAllText(FileBook, $"\n{text}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FileBook, $"");
                for (int i = 0; i < listBook.Count; i++)
                {
                    if (photoBooks.Code == listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{text}");
                    }
                    else if (photoBooks.Code != listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{Cpyelist[i]}");
                    }
                }
            }
            return photoBooks;
        }

        public PhotoBooks DelGenre(PhotoBooks photoBooks)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\PhotoBooks.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\PhotoBooks.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == photoBooks.Code)
                {
                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");

                }
            }
            return photoBooks;
        }

        /// <summary>
        /// グラビア写真集の更新
        /// </summary>
        /// <param name="gravurePhotoBooks">更新</param>
        private static void Updata(GravurePhotoBooks gravurePhotoBooks)
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\GravurePhotoBooks.txt")))
            {
                string[] data = text.Split(',');
                if (data.Any())
                {
                    //書籍コードが異なるとき
                    if (data[(int)GravurePhotoBooksType.Code] != gravurePhotoBooks.Code)
                    {
                        continue;
                    }
                    for (GravurePhotoBooksType type = GravurePhotoBooksType.Code; (int)type < data.Length; type++)
                    {
                        switch (type)
                        {
                            case GravurePhotoBooksType.PhotoGrapher:
                                gravurePhotoBooks.PhotoGrapher = data[(int)type];
                                break;
                            case GravurePhotoBooksType.Supervision:
                                gravurePhotoBooks.Supervision = data[(int)type];
                                break;
                        }
                    }
                }
            }
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\GravurePergformance.txt")))
            {
                string[] datapergformance = text.Split(',');
                if (datapergformance.Any())
                {
                    //書籍コードが異なるとき
                    if (datapergformance[(int)GravurePergformanceType.Code] != gravurePhotoBooks.Code)
                    {
                        continue;
                    }
                    for (GravurePergformanceType type = GravurePergformanceType.Code; (int)type < datapergformance.Length; type++)
                    {
                        switch (type)
                        {
                            case GravurePergformanceType.Pergformance:
                                gravurePhotoBooks.Performance = new List<string>(datapergformance.Skip(1));
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// グラビア写真集の追加
        /// </summary>
        /// <param name="gravurePhotoBooks">グラビア写真集</param>
        /// <returns></returns>
        public GravurePhotoBooks SetGenre(GravurePhotoBooks gravurePhotoBooks)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\GravurePhotoBooks.txt");
            string FilePerformance = Path.Combine(FilePath, @"BooksData\GravurePergformance.txt");
            List<string> listBook = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] ctext = data.Split(',');
                listBook.Add(ctext[0]);

            }

            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\GravurePhotoBooks.txt")))
            {
                Cpyelist.Add(data);
            }

            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\GravurePergformance.txt")))
            {
                Cpyelist.Add(data);
            }
            string[] gravure = new string[] {
               gravurePhotoBooks.Code,

               gravurePhotoBooks.PhotoGrapher,
               gravurePhotoBooks.Supervision
            };
            string[] per = gravurePhotoBooks.Performance.ToArray();
            string[] pdata = new string[] { };
            foreach (string data in per)
            {
                string[] performance = new string[] {
                    gravurePhotoBooks.Code,
                    data
                 };
                pdata = performance;
            }

            string text = String.Join(",", gravure);
            string ptext = String.Join(",", pdata);
            if (Flag == true)
            {
                if (!listBook.Contains(gravure[0]))
                {

                    File.AppendAllText(FileBook, $"\n{text}");
                    File.AppendAllText(FilePerformance, $"\n{ptext}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FileBook, $"");
                File.WriteAllText(FilePerformance, $"");
                for (int i = 0; i < listBook.Count; i++)
                {
                    if (gravurePhotoBooks.Code == listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{text}");
                    }
                    else if (gravurePhotoBooks.Code != listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{Cpyelist[i]}");
                    }
                }
            }


            return gravurePhotoBooks;
        }

        public GravurePhotoBooks DelGenre(GravurePhotoBooks gravurePhotoBooks)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\GravurePhotoBooks.txt");
            string FigureBook = Path.Combine(FilePath, @"BooksData\GravurePergformance.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            List<string> GAppendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\GravurePhotoBooks.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\GravurePergformance.txt")))
            {
                GAppendlist.Add(text);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == gravurePhotoBooks.Code)
                {
                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                    GAppendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");
                    File.AppendAllText(FileBook, $"\n{GAppendlist[i]}");

                }
            }
            return gravurePhotoBooks;
        }

        /// <summary>
        /// 図書カードの更新
        /// </summary>
        /// <param name="bookCard">図書カード</param>
        private static void Updata(BookCard bookCard)
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\BookCard.txt")))
            {
                string[] data = text.Split(',');
                if (data.Any())
                {
                    //書籍コードが異なるとき
                    if (data[(int)BookCardType.Code] != bookCard.Code)
                    {
                        continue;
                    }
                    for (BookCardType type = BookCardType.Code; (int)type < data.Length; type++)
                    {
                        switch (type)
                        {
                            case BookCardType.ImageCharacter:
                                bookCard.ImageCharacter = data[(int)type];
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 図書カードの追加
        /// </summary>
        /// <param name="bookCard">図書カード</param>
        /// <returns></returns>
        public BookCard SetGenre(BookCard bookCard)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\BookCard.txt");
            List<string> listBook = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] ctext = data.Split(',');
                listBook.Add(ctext[0]);

            }

            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\BookCard.txt")))
            {
                Cpyelist.Add(data);
            }

            string[] Card = new string[] {
               bookCard.Code,
               bookCard.ImageCharacter
            };

            string text = String.Join(",", Card);
            if (Flag == true)
            {
                if (!listBook.Contains(Card[0]))
                {

                    File.AppendAllText(FileBook, $"\n{text}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FileBook, $"");
                for (int i = 0; i < listBook.Count; i++)
                {
                    if (bookCard.Code == listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{text}");
                    }
                    else if (bookCard.Code != listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{Cpyelist[i]}");
                    }
                }
            }

            return bookCard;
        }

        public BookCard DelGenre(BookCard bookCard)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\BookCard.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\BookCard.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == bookCard.Code)
                {
                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");

                }
            }
            return bookCard;
        }
        /// <summary>
        /// 景品を更新
        /// </summary>
        /// <param name="gift">景品</param>
        private static void Updata(Gift gift)
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\Gift.txt")))
            {
                string[] data = text.Split(',');
                if (data.Any())
                {
                    //書籍コードが異なるとき
                    if (data[(int)GiftType.Code] != gift.Code)
                    {
                        continue;
                    }
                    for (GiftType type = GiftType.Code; (int)type < data.Length; type++)
                    {
                        switch (type)
                        {
                            case GiftType.Genre:
                                gift.Genre = data[(int)type];
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 景品を追加
        /// </summary>
        /// <param name="gift">景品</param>
        /// <returns></returns>
        public Gift SetGenre(Gift gift)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\Gift.txt");
            List<string> listBook = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] ctext = data.Split(',');
                listBook.Add(ctext[0]);

            }

            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Gift.txt")))
            {
                Cpyelist.Add(data);
            }

            string[] gifts = new string[] {
               gift.Code,
               gift.Genre
            };

            string text = String.Join(",", gifts);
            bool flag = Flag;
            if (Flag == true)
            {
                if (!listBook.Contains(gifts[0]))
                {

                    File.AppendAllText(FileBook, $"\n{text}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FileBook, $"");
                for (int i = 0; i < listBook.Count; i++)
                {
                    if (gift.Code == listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{text}");
                    }
                    else if (gift.Code != listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{Cpyelist[i]}");
                    }
                }
            }
            return gift;
        }

        public Gift DelGenre(Gift gift)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\Gift.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\Gift.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == gift.Code)
                {
                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");

                }
            }
            return gift;
        }

        /// <summary>
        /// その他の更新
        /// </summary>
        /// <param name="other">その他</param>
        private static void Updata(Other other)
        {
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\Other.txt")))
            {
                string[] data = text.Split(',');
                if (data.Any())
                {
                    //書籍コードが異なるとき
                    if (data[(int)OtherType.Code] != other.Code)
                    {
                        continue;
                    }
                    for (OtherType type = OtherType.Code; (int)type < data.Length; type++)
                    {
                        switch (type)
                        {
                            case OtherType.Genre:
                                other.Genre = data[(int)type];
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// その他の追加
        /// </summary>
        /// <param name="other">その他</param>
        /// <returns></returns>
        public Other SetGenre(Other other)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\Other.txt");
            List<string> listBook = new List<string>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Book.txt")))
            {
                string[] ctext = data.Split(',');
                listBook.Add(ctext[0]);

            }

            foreach (string data in File.ReadLines(Path.Combine(FilePath, @"BooksData\Other.txt")))
            {
                Cpyelist.Add(data);
            }

            string[] others = new string[] {
               other.Code,
               other.Genre
            };

            string text = String.Join(",", others);
            if (Flag == true)
            {
                if (!listBook.Contains(others[0]))
                {

                    File.AppendAllText(FileBook, $"\n{text}");
                }
            }
            else if (Flag == false)
            {
                File.WriteAllText(FileBook, $"");
                for (int i = 0; i < listBook.Count; i++)
                {
                    if (other.Code == listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{text}");
                    }
                    else if (other.Code != listBook[i])
                    {
                        File.AppendAllText(FileBook, $"\n{Cpyelist[i]}");
                    }
                }
            }

            return other;
        }
        public Other DelGenre(Other other)
        {
            string FileBook = Path.Combine(FilePath, @"BooksData\Other.txt");
            List<string> Dellist = new List<string>() { };
            List<string> Appendlist = new List<string>() { };
            foreach (string text in File.ReadLines(Path.Combine(FilePath, @"BooksData\Other.txt")))
            {
                string[] data = text.Split(',');
                Appendlist.Add(text);
                Dellist.Add(data[0]);
            }
            File.WriteAllText(FileBook, $"");
            for (int i = 0; i < Appendlist.Count; i++)
            {
                if (Dellist[i] == other.Code)
                {
                    Dellist.Skip(i);
                    Appendlist.Skip(i);
                }
                else
                {

                    File.AppendAllText(FileBook, $"\n{Appendlist[i]}");

                }
            }
            return other;
        }

        public static IEnumerable<Salse> GetSalses()
        {
            //現在の日時を取得
            DateTime dtNow = DateTime.Now;
            int Year = dtNow.Year;
            int Month = dtNow.Month;

            foreach (string text in File.ReadLines(Path.Combine(FilePath, $@"MonthData\{Year}\{Year}_{Month}\Salse{Year}_{Month}.txt")))
            {
                string[] data = text.Split(',');

                if (data.Any())
                {
                    Salse salse = null;

                    //項目数が一致した時
                    if (Enum.GetValues(typeof(SalseType)).Length == data.Length)
                    {
                        string code = string.Empty;

                        for (SalseType type = SalseType.Code; (int)type < data.Length; type++)
                        {
                            switch (type)
                            {
                                case SalseType.Code:
                                    code = data[(int)type];
                                    salse = new Salse(code);
                                    break;
                                case SalseType.SalesAmount:
                                    salse.SalesAmount = int.Parse(data[(int)type]);
                                    break;

                            }
                        }
                        if (salse != null)
                        {
                            yield return salse;
                        }
                    }
                }
            }


        }

        public void SetSalse()
        {
            //現在の日時を取得
            DateTime dtNow = DateTime.Now;
            int Year = dtNow.Year;
            int Month = dtNow.Month;


            string FilePrice = Path.Combine(FilePath, $@"MonthData\{Year}\{Year}_{Month}\Salse{Year}_{Month}.txt");
            List<string> list = new List<string>() { };
            List<int> listsalse = new List<int>() { };
            List<string> Cpyelist = new List<string>() { };
            foreach (string data in File.ReadLines(Path.Combine(FilePath, $@"MonthData\{Year}\{Year}_{Month}\Salse{Year}_{Month}.txt")))
            {
                string[] ctext = data.Split(',');
                list.Add(ctext[0]);
                listsalse.Add(int.Parse(ctext[1]));
                Cpyelist.Add(data);

            }
            
            List<string> salsecode = new List<string> { };
            File.WriteAllText(FilePrice, $"");
            int j = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (codeList.Contains(list[i]))
                {
                    int total = int.Parse(numList[j]) + listsalse[i]; 
                    File.AppendAllText(FilePrice, $"{codeList[j]},{total.ToString()}\n");
                    j++;
                }
                else if(!codeList.Contains(list[i]))
                {
                    File.AppendAllText(FilePrice, $"{Cpyelist[i]}\n");
                }
            }
        }
        #endregion
    }
}
