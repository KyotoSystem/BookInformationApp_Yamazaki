using System;
using BookAppLibrary.Enums;

namespace BookAppLibrary.Models
{
    public abstract class Book
    {

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="code">コード</param>
        /// <param name="type">分類</param>
        public Book(string code , SpeciesType type)
        {
            Code = code;
            Type = type;
        }
        #endregion

        #region プロパティ

        /// <summary>
        /// 書籍コード
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 種別
        /// </summary>
        public SpeciesType Type { get; set; }
         /// <summary>
        /// 種別書き込み
        /// </summary>
        public string Species { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 出版社
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// 発売日
        /// </summary>
        public DateTime ReleaseData { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string SubStance { get; set; }

        /// <summary>
        /// 画像
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 在庫数
        /// </summary>
        public int Stocks { get; set; }

        #endregion

        #region メソッド
        /// <summary>
        /// 種別名を取得
        /// </summary>
        public abstract string GetSpecies();
        #endregion
    }
}
