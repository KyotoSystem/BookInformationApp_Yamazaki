using BookAppLibrary.Enums;

namespace BookAppLibrary.Models
{
    public class BookCard : Book
    {
        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="code">コード</param>
        public BookCard(string code) : base(code , SpeciesType.BookCard) { }

        #endregion

        #region　プロパティ

        /// <summary>
        /// イメージキャラクター
        /// </summary>
        public string ImageCharacter { get; set; }

        #endregion

        #region　メソッド

        /// <summary>
        /// 種別名を取得
        /// </summary>
        /// <returns>種別名</returns>
        public override string GetSpecies()
        {
            return "図書カード";
        }

        #endregion
    }
}
