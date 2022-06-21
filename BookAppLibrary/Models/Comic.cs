using BookAppLibrary.Enums;

namespace BookAppLibrary.Models
{
    public class Comic : Book
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="code">コード</param>
        public Comic(string code) : base (code, SpeciesType.Comic) { }
        #endregion

        #region プロパティ
        ///<summary>
        ///作者
        /// </summary>
        public string Author { get; set; }

        ///<summary>
        ///作画
        /// </summary>
        public string Illustrator { get; set; }

        #endregion

        #region メソッド
        /// <summary>
        /// 種別名を取得する。
        /// </summary>
        /// <returns>種別名</returns>
        public override string GetSpecies()
        {
            return "漫画";
        }
        #endregion
    }
}
